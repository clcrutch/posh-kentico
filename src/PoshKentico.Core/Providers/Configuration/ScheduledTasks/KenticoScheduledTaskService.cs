// <copyright file="KenticoScheduledTaskService.cs" company="Chris Crutchfield">
// Copyright (C) 2017  Chris Crutchfield
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see &lt;http://www.gnu.org/licenses/&gt;.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using Castle.DynamicProxy;
using CMS.Scheduler;
using ImpromptuInterface;
using PoshKentico.Core.AppDomainProxies;
using PoshKentico.Core.Services.Configuration.ScheduledTasks;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Core.Providers.Configuration.ScheduledTasks
{
    /// <summary>
    /// Implementation of <see cref="IScheduledTaskService"/> for Kentico.
    /// </summary>
    [Export(typeof(IScheduledTaskService))]
    public class KenticoScheduledTaskService : IScheduledTaskService
    {
        #region Fields

        private readonly ProxyGenerator proxyGenerator = new ProxyGenerator();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the CMS Application services.  Populated by MEF.
        /// </summary>
        [Import]
        public ICmsApplicationService CmsApplicationService { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        public IEnumerable<IScheduledTask> ScheduledTasks =>
            (from t in TaskInfoProvider.GetTasks().Cast<TaskInfo>()
             select t.ActLike<IScheduledTask>()).ToArray();

        /// <inheritdoc />
        public void ExecuteScheduledTask(IScheduledTask scheduledTask)
        {
            var originalDirectory = Environment.CurrentDirectory;
            Environment.CurrentDirectory = this.CmsApplicationService.SiteLocation;

            SchedulingExecutor.ExecuteTask(scheduledTask.UndoActLike());

            Environment.CurrentDirectory = originalDirectory;
        }

        /// <inheritdoc />
        public void ExecuteScheduledTaskInNewAppDomain(IScheduledTask scheduledTask)
        {
            var domainInfo = new AppDomainSetup
            {
                ApplicationBase = Path.Combine(this.CmsApplicationService.SiteLocation, "bin"),
            };
            var appDomain = AppDomain.CreateDomain(Guid.NewGuid().ToString(), AppDomain.CurrentDomain.Evidence, domainInfo);

            Type proxyType = typeof(ScheduledTaskProxy);
            var proxy = (ScheduledTaskProxy)appDomain.CreateInstanceFrom(typeof(ProxyBase).Assembly.Location, proxyType.FullName).Unwrap();

            proxy.ExecuteScheduledTask(scheduledTask.TaskID);

            AppDomain.Unload(appDomain);
        }

        /// <inheritdoc />
        public IScheduledTask GetScheduledTask(int id) =>
            TaskInfoProvider.GetTaskInfo(id).ActLike<IScheduledTask>();

        /// <inheritdoc />
        public IScheduledTaskInterval GetScheduledTaskInterval(IScheduledTask scheduledTask) =>
            this.AppendScheduledTask(SchedulingHelper.DecodeInterval(scheduledTask.TaskInterval), scheduledTask).ActLike<IScheduledTaskInterval>();

        /// <inheritdoc />
        public IScheduledTask NewScheduledTask(IScheduledTask scheduledTask, IScheduledTaskInterval scheduledTaskInterval)
        {
            var task = this.CreateScheduledTask(scheduledTask, scheduledTaskInterval);

            TaskInfoProvider.SetTaskInfo(task);

            return task.ActLike<IScheduledTask>();
        }

        /// <inheritdoc />
        public void Remove(IScheduledTask scheduledTask) =>
            TaskInfoProvider.DeleteTaskInfo(TaskInfoProvider.GetTaskInfo(scheduledTask.TaskID));

        /// <inheritdoc />
        public void Set(IScheduledTask scheduledTask, IScheduledTaskInterval scheduledTaskInterval)
        {
            var task = this.CreateScheduledTask(scheduledTask, scheduledTaskInterval);
            task.TaskID = scheduledTask.TaskID;

            if (scheduledTaskInterval != null)
            {
                task.TaskInterval = scheduledTaskInterval.Encode();
            }

            TaskInfoProvider.SetTaskInfo(task);
        }

        private TaskInterval AppendScheduledTask(TaskInterval taskInterval, IScheduledTask scheduledTask)
        {
            var options = new ProxyGenerationOptions();
            options.AddMixinInstance(new ScheduledTaskHelper
            {
                ScheduledTask = scheduledTask,
            });

            var result = this.proxyGenerator.CreateClassProxyWithTarget(taskInterval, options);

            return result as TaskInterval;
        }

        private TaskInfo CreateScheduledTask(IScheduledTask scheduledTask, IScheduledTaskInterval scheduledTaskInterval) =>
            new TaskInfo
            {
                TaskAssemblyName = scheduledTask.TaskAssemblyName,
                TaskClass = scheduledTask.TaskClass,
                TaskData = scheduledTask.TaskData,
                TaskDisplayName = scheduledTask.TaskDisplayName,
                TaskEnabled = true,
                TaskInterval = scheduledTask.TaskInterval ?? scheduledTaskInterval.Encode(),
                TaskName = scheduledTask.TaskName,
                TaskNextRunTime = scheduledTaskInterval.GetFirstRun(),
                TaskSiteID = scheduledTask.TaskSiteID,
            };

        #endregion

    }
}

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
    [Export(typeof(IScheduledTaskService))]
    public class KenticoScheduledTaskService : IScheduledTaskService
    {
        #region Fields

        private readonly ProxyGenerator proxyGenerator = new ProxyGenerator();

        #endregion

        #region Properties

        [Import]
        public ICmsApplicationService CmsApplicationService { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        public IEnumerable<IScheduledTask> ScheduledTasks =>
            (from t in TaskInfoProvider.GetTasks().Cast<TaskInfo>()
             select t.ActLike<IScheduledTask>()).ToArray();

        public void ExecuteScheduledTask(IScheduledTask scheduledTask)
        {
            var originalDirectory = Environment.CurrentDirectory;
            Environment.CurrentDirectory = this.CmsApplicationService.SiteLocation;

            SchedulingExecutor.ExecuteTask(scheduledTask.UndoActLike());

            Environment.CurrentDirectory = originalDirectory;
        }

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

        public IScheduledTask GetScheduledTask(int id) =>
            TaskInfoProvider.GetTaskInfo(id).ActLike<IScheduledTask>();

        public IScheduledTaskInterval GetScheduledTaskInterval(IScheduledTask scheduledTask) =>
            this.AppendScheduledTask(SchedulingHelper.DecodeInterval(scheduledTask.TaskInterval), TaskInfoProvider.GetTaskInfo(scheduledTask.TaskID)).ActLike<IScheduledTaskInterval>();

        public IScheduledTask NewScheduledTask(IScheduledTask scheduledTask, IScheduledTaskInterval scheduledTaskInterval)
        {
            var task = new TaskInfo
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

            TaskInfoProvider.SetTaskInfo(task);

            return task.ActLike<IScheduledTask>();
        }

        public void Remove(IScheduledTask scheduledTask) =>
            TaskInfoProvider.DeleteTaskInfo(TaskInfoProvider.GetTaskInfo(scheduledTask.TaskID));

        private TaskInterval AppendScheduledTask(TaskInterval taskInterval, TaskInfo taskInfo)
        {
            var options = new ProxyGenerationOptions();
            options.AddMixinInstance(new TaskInfoHolder
            {
                TaskInfo = taskInfo,
            });

            var result = this.proxyGenerator.CreateClassProxyWithTarget(taskInterval, options);

            return result as TaskInterval;
        }

        #endregion

    }
}

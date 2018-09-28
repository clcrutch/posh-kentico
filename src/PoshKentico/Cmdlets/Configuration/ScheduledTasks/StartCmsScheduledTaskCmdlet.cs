// <copyright file="StartCmsScheduledTaskCmdlet.cs" company="Chris Crutchfield">
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

using System.ComponentModel.Composition;
using System.Management.Automation;
using CMS.Scheduler;
using ImpromptuInterface;
using PoshKentico.Business.Configuration.ScheduledTasks;
using PoshKentico.Core.Services.Configuration.ScheduledTasks;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.ScheduledTasks
{
    /// <summary>
    /// <para type="synopsis">Starts the scheduled task for the provided input.</para>
    /// <para type="description">Starts the scheduled tasks selected by the provided input.  This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">Without parameters, this command starts all scheduled tasks.</para>
    /// <para type="description">With parameters, this command starts the scheduled tasks that match the criteria.</para>
    /// <example>
    ///     <para>Starts all of the scheduled tasks.</para>
    ///     <code>Start-CMSScheduledTask</code>
    /// </example>
    /// <example>
    ///     <para>Starts all scheduled tasks with an assembly name that matches "test".</para>
    ///     <code>Start-CMSScheduledTask -AssemblyName *test*</code>
    /// </example>
    /// <example>
    ///     <para>Starts all the scheduled tasks with a display name or name that matches "test".</para>
    ///     <code>Start-CMSScheduledTask -Name *test*</code>
    /// </example>
    /// <example>
    ///     <para>Starts all the scheduled tasks that are associated with a site.</para>
    ///     <code>$site | Start-CMSScheduledTask</code>
    /// </example>
    /// <example>
    ///     <para>Starts the specified scheduled task.</para>
    ///     <code>$scheduledTask | Start-CMSScheduleTask</code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsLifecycle.Start, "CMSScheduledTask", DefaultParameterSetName = NONE)]
    [OutputType(typeof(TaskInfo[]))]
    [Alias("sast")]
    public class StartCmsScheduledTaskCmdlet : GetCmsScheduledTaskCmdlet
    {
        #region Constants

        private const string SCHEDULEDTASK = "Scheduled Task";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the web part category.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        /// <para type="description">The scheduled task to start.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = SCHEDULEDTASK)]
        [Alias("Task", "TaskInfo")]
        public TaskInfo ScheduledTask { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this web part. Populated by MEF.
        /// </summary>
        [Import]
        public StartCmsScheduledTaskBusiness StartBusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case SCHEDULEDTASK:
                    this.ActOnObject(this.ScheduledTask.ActLike<IScheduledTask>());
                    break;
                default:
                    base.ProcessRecord();
                    break;
            }
        }

        /// <inheritdoc />
        protected override void ActOnObject(IScheduledTask scheduledTask)
        {
            this.StartBusinessLayer.ExecuteTask(scheduledTask);

            if (this.PassThru.ToBool())
            {
                this.WriteObject(scheduledTask.UndoActLike());
            }
        }

        #endregion

    }
}

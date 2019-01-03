// <copyright file="RemoveCmsScheduledTaskCmdlet.cs" company="Chris Crutchfield">
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
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using CMS.Scheduler;
using ImpromptuInterface;
using PoshKentico.Business.Configuration.ScheduledTasks;
using PoshKentico.Core.Services.Configuration.ScheduledTasks;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.ScheduledTasks
{
    /// <summary>
    /// <para type="synopsis">Removes the scheduled task for the provided input.</para>
    /// <para type="description">Removes the scheduled tasks selected by the provided input.  This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">Without parameters, this command removes all scheduled tasks.</para>
    /// <para type="description">With parameters, this command removes the scheduled tasks that match the criteria.</para>
    /// <example>
    ///     <para>Removes all of the scheduled tasks.</para>
    ///     <code>Remove-CMSScheduledTask</code>
    /// </example>
    /// <example>
    ///     <para>Removes all scheduled tasks with an assembly name that matches "test".</para>
    ///     <code>Remove-CMSScheduledTask -AssemblyName *test*</code>
    /// </example>
    /// <example>
    ///     <para>Removes all the scheduled tasks with a display name or name that matches "test".</para>
    ///     <code>Remove-CMSScheduledTask -Name *test*</code>
    /// </example>
    /// <example>
    ///     <para>Removes all the scheduled tasks that are associated with a site.</para>
    ///     <code>$site | Remove-CMSScheduledTask</code>
    /// </example>
    /// <example>
    ///     <para>Removes the specified scheduled task.</para>
    ///     <code>$scheduledTask | Remove-CMSScheduleTask</code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Remove, "CMSScheduledTask", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = NONE)]
    [ExcludeFromCodeCoverage]
    [Alias("rmst")]
    public class RemoveCmsScheduledTaskCmdlet : GetCmsScheduledTaskCmdlet
    {
        #region Constants

        private const string SCHEDULEDTASK = "Scheduled Task";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The scheduled task to remove.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = SCHEDULEDTASK)]
        [Alias("Task", "TaskInfo")]
        public TaskInfo ScheduledTask { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this web part. Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCmsScheduledTaskBusiness RemoveBusinessLayer { get; set; }

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
            this.RemoveBusinessLayer.Remove(scheduledTask);
        }

        #endregion

    }
}

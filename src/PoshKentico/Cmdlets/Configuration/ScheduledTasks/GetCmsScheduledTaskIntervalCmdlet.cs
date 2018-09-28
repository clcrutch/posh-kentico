// <copyright file="GetCmsScheduledTaskIntervalCmdlet.cs" company="Chris Crutchfield">
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
    /// <para type="synopsis">Gets the interval for the supplied scheduled task.</para>
    /// <para type="description">Gets the interval for the supplied scheduled task.  This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <example>
    ///     <para>Get the interval for a scheduled task.</para>
    ///     <code>$scheduledTask | Get-CMSScheduledTaskInterval</code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "CMSScheduledTaskInterval")]
    [OutputType(typeof(TaskInterval[]))]
    [Alias("gsti")]
    public class GetCmsScheduledTaskIntervalCmdlet : MefCmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">The scheduled task to get the interval for.</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, Mandatory = true)]
        [Alias("Task", "TaskInfo")]
        public TaskInfo ScheduledTask { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for adding culture to this site.  Populated by MEF.
        /// </summary>
        [Import]
        public GetCmsScheduledTaskIntervalBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            this.WriteObject(this.BusinessLayer.GetScheduledTaskInterval(this.ScheduledTask.ActLike<IScheduledTask>()).UndoActLike());
        }

        #endregion

    }
}

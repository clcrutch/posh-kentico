// <copyright file="SetCmsScheduledTaskCmdlet.cs" company="Chris Crutchfield">
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
    /// <para type="synopsis">Sets a scheduled task in Kentico.</para>
    /// <para type="description">Sets a scheduled task in Kentico.  This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <example>
    ///     <para>Sets the scheduled task in Kentico without updating the interval.</para>
    ///     <code>$scheduledTask | Set-CMSScheduledTask</code>
    /// </example>
    /// <example>
    ///     <para>Sets the scheduled task in Kentico with the interval.</para>
    ///     <code>$scheduledTask | Set-CMSScheduledTask -Interval $interval</code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "CMSScheduledTask")]
    [OutputType(typeof(TaskInfo[]), ParameterSetName = new string[] { PASSTHRU })]
    [ExcludeFromCodeCoverage]
    [Alias("sst")]
    public class SetCmsScheduledTaskCmdlet : MefCmdlet
    {
        #region Constants

        private const string PASSTHRU = "PassThru";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The interval for the scheduled task.</para>
        /// </summary>
        [Parameter(Position = 1)]
        public TaskInterval Interval { get; set; }

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the scheduled task.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = PASSTHRU)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        /// <para type="description">The scheduled task to set.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [Alias("Task", "TaskInfo")]
        public TaskInfo ScheduledTask { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this cmdlet. Populated by MEF.
        /// </summary>
        [Import]
        public SetCmsScheduledTaskBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            this.BusinessLayer.Set(this.ScheduledTask?.ActLike<IScheduledTask>(), this.Interval?.ActLike<IScheduledTaskInterval>());

            if (this.PassThru.ToBool())
            {
                this.WriteObject(this.ScheduledTask);
            }
        }

        #endregion

    }
}

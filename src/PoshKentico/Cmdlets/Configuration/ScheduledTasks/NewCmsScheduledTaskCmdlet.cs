// <copyright file="NewCmsScheduledTaskCmdlet.cs" company="Chris Crutchfield">
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
using CMS.SiteProvider;
using ImpromptuInterface;
using PoshKentico.Business.Configuration.ScheduledTasks;
using PoshKentico.Core.Services.Configuration.ScheduledTasks;
using PoshKentico.Core.Services.Configuration.Sites;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.ScheduledTasks
{
    /// <summary>
    /// <para type="synopsis">Creates a new scheduled task with the provided input.</para>
    /// <para type="description">Creates a new scheduled task with the provided input.  This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <example>
    ///     <para>Create a new scheduled task that runs on Mondays</para>
    ///     <code>
    ///         # Creates the scheduling interval for the task
    ///         $interval = New-Object -TypeName CMS.Scheduler.TaskInterval
    ///
    ///         # Sets the interval properties
    ///         $interval.Period = [CMS.Scheduler.SchedulingHelper]::PERIOD_DAY
    ///         $interval.StartTime = Get-Date
    ///         $interval.Every = 2
    ///         $interval.Days = @([DayOfWeek]::Monday)
    ///
    ///         New-CMSScheduledTask -AssemblyName Assembly.Name -Class Class.Name -Data TaskData -DisplayName "Display Name" -Interval $interval -Name Task.Name -Site $site
    ///     </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "CMSScheduledTask")]
    [OutputType(typeof(TaskInfo[]))]
    [ExcludeFromCodeCoverage]
    [Alias("nst")]
    public class NewCmsScheduledTaskCmdlet : MefCmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">The assembly name for the scheduled task.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 2)]
        public string AssemblyName { get; set; }

        /// <summary>
        /// <para type="description">The class name for the scheduled task.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 3)]
        public string Class { get; set; }

        /// <summary>
        /// <para type="description">The data for the scheduled task.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 4)]
        public string Data { get; set; }

        /// <summary>
        /// <para type="description">The display name for the scheduled task.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        public string DisplayName { get; set; }

        /// <summary>
        /// <para type="description">The interval for the scheduled task.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 5)]
        public TaskInterval Interval { get; set; }

        /// <summary>
        /// <para type="description">The name for the scheduled task.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">The site for the scheduled task.</para>
        /// </summary>
        [Parameter]
        public SiteInfo Site { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for adding culture to this site.  Populated by MEF.
        /// </summary>
        [Import]
        public NewCmsScheduledTaskBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            var scheduledTask = this.BusinessLayer.New(
                this.AssemblyName,
                this.Class,
                this.Data,
                this.DisplayName,
                this.Interval?.ActLike<IScheduledTaskInterval>(),
                this.Name,
                this.Site?.ActLike<ISite>());

            this.WriteObject(scheduledTask.UndoActLike());
        }

        #endregion

    }
}

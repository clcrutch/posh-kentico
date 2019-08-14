// <copyright file="ConvertFromCmsScheduledTaskIntervalCmdlet.cs" company="Chris Crutchfield">
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
    /// <para type="synopsis">Converts from a Task Interval to a string.</para>
    /// <para type="description">Converts from a Task Interval to a string using Kentico's SchedulingHelper.</para>
    /// <example>
    ///     <para>Convert from interval to string.</para>
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
    ///         $interval | ConvertFrom-CMSScheduledTaskInterval
    ///     </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsData.ConvertFrom, "CMSScheduledTaskInterval")]
    [OutputType(typeof(string[]))]
    [ExcludeFromCodeCoverage]
    public class ConvertFromCmsScheduledTaskIntervalCmdlet : MefCmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">The scheduled task interval to convert from.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [Alias("Interval")]
        public TaskInterval TaskInterval { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this web part. Populated by MEF.
        /// </summary>
        [Import]
        public ConvertFromCmsScheduledTaskIntervalBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord() =>
            this.WriteObject(this.BusinessLayer.EncodeScheduledTaskInterval(this.TaskInterval.ActLike<IScheduledTaskInterval>()));

        #endregion
    }
}

// <copyright file="ConvertToCmsScheduledTaskIntervalCmdlet.cs" company="Chris Crutchfield">
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

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.ScheduledTasks
{
    /// <summary>
    /// <para type="synopsis">Converts to a Task Interval from a string.</para>
    /// <para type="description">Converts to a Task Interval from a string using Kentico's SchedulingHelper.</para>
    /// <example>
    ///     <para>Convert to interval from string.</para>
    ///     <code>
    ///         'day;9/6/2018 11:00:00 PM;1;Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday' | ConvertFrom-CMSScheduledTaskInterval
    ///     </code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsData.ConvertTo, "CMSScheduledTaskInterval")]
    [OutputType(typeof(TaskInterval[]))]
    [ExcludeFromCodeCoverage]
    public class ConvertToCmsScheduledTaskIntervalCmdlet : MefCmdlet<ConvertToCmsScheduledTaskIntervalBusiness>
    {
        #region Properties

        /// <summary>
        /// <para type="description">The encoded scheduled task interval to convert.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [Alias("Interval")]
        public string EncodedInterval { get; set; }

        #endregion

        #region Methdos

        /// <inheritdoc />
        protected override void ProcessRecord() =>
            this.WriteObject(this.BusinessLayer.DecodeScheduledTaskInterval(this.EncodedInterval).UndoActLike() as TaskInterval);

        #endregion

    }
}

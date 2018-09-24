// <copyright file="GetCmsScheduledTaskCmdlet.cs" company="Chris Crutchfield">
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

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Management.Automation;
using CMS.Scheduler;
using ImpromptuInterface;
using PoshKentico.Business.Configuration.ScheduledTasks;
using PoshKentico.Core.Services.Configuration.ScheduledTasks;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.ScheduledTasks
{
    [Cmdlet(VerbsCommon.Get, "CMSScheduledTask", DefaultParameterSetName = NONE)]
    [OutputType(typeof(TaskInfo[]))]
    [Alias("gst")]
    public class GetCmsScheduledTaskCmdlet : MefCmdlet
    {
        #region Constants

        /// <summary>
        /// Represents no parameters.
        /// </summary>
        protected const string NONE = "None";

        private const string ASSEMBLYNAME = "Assembly Name";
        private const string NAME = "Name";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The assembly name for the scheduled task.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = ASSEMBLYNAME)]
        public string AssemblyName { get; set; }

        /// <summary>
        /// <para type="description">The name or display name for scheduled task.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = NAME)]
        [Alias("DisplayName")]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Indicates if the CategoryName supplied is a regular expression.</para>
        /// </summary>
        [Parameter(ParameterSetName = NAME)]
        [Alias("Regex")]
        public SwitchParameter RegularExpression { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this web part. Populated by MEF.
        /// </summary>
        [Import]
        public GetCmsScheduledTaskBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<IScheduledTask> scheduledTasks = null;

            switch (this.ParameterSetName)
            {
                case ASSEMBLYNAME:
                    scheduledTasks = this.BusinessLayer.GetScheduledTasksByAssemblyName(this.AssemblyName, this.RegularExpression.ToBool());
                    break;
                case NAME:
                    scheduledTasks = this.BusinessLayer.GetScheduledTasksByNameOrDisplayName(this.Name, this.RegularExpression.ToBool());
                    break;

                case NONE:
                    scheduledTasks = this.BusinessLayer.GetScheduledTasks();
                    break;
            }

            foreach (var scheduledTask in scheduledTasks)
            {
                this.ActOnObject(scheduledTask);
            }
        }

        /// <summary>
        /// When overridden in a child class, operates on the specified <see cref="IScheduledTask"/>.
        /// </summary>
        /// <param name="scheduledTask">The <see cref="IScheduledTask"/> to operate on.</param>
        protected virtual void ActOnObject(IScheduledTask scheduledTask) =>
            this.WriteObject(scheduledTask.UndoActLike());

        #endregion

    }
}

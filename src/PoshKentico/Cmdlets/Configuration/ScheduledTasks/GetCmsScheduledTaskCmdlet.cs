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
    /// <para type="synopsis">Gets the scheduled tasks for the provided input.</para>
    /// <para type="description">Gets the scheduled task selected by the provided input.  This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">Without parameters, this command returns all scheduled tasks.</para>
    /// <para type="description">With parameters, this command returns the scheduled tasks that match the criteria.</para>
    /// <example>
    ///     <para>Get all of the scheduled tasks.</para>
    ///     <code>Get-CMSScheduledTask</code>
    /// </example>
    /// <example>
    ///     <para>Get all scheduled tasks with an assembly name that matches "test".</para>
    ///     <code>Get-CMSScheduledTask -AssemblyName *test*</code>
    /// </example>
    /// <example>
    ///     <para>Get all the scheduled tasks with a display name or name that matches "test".</para>
    ///     <code>Get-CMSScheduledTask -Name *test*</code>
    /// </example>
    /// <example>
    ///     <para>Get all the scheduled tasks that are associated with a site.</para>
    ///     <code>$site | Get-CMSScheduledTask</code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "CMSScheduledTask", DefaultParameterSetName = NONE)]
    [OutputType(typeof(TaskInfo[]))]
    [ExcludeFromCodeCoverage]
    [Alias("gst")]
    public class GetCmsScheduledTaskCmdlet : MefCmdlet<GetCmsScheduledTaskBusiness>
    {
        #region Constants

        /// <summary>
        /// Represents no parameters.
        /// </summary>
        protected const string NONE = "None";

        private const string ASSEMBLYNAME = "Assembly Name";
        private const string NAME = "Name";
        private const string SITE = "Site";
        private const string SITEANDASSEMBLYNAME = "Site and Assembly Name";
        private const string SITEANDNAME = "Site and Name";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The assembly name for the scheduled task.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = ASSEMBLYNAME)]
        [Parameter(Mandatory = true, ParameterSetName = SITEANDASSEMBLYNAME)]
        public string AssemblyName { get; set; }

        /// <summary>
        /// <para type="description">The name or display name for scheduled task.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = NAME)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = SITEANDNAME)]
        [Alias("DisplayName")]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Indicates if the CategoryName supplied is a regular expression.</para>
        /// </summary>
        [Parameter(ParameterSetName = ASSEMBLYNAME)]
        [Parameter(ParameterSetName = NAME)]
        [Parameter(ParameterSetName = SITEANDASSEMBLYNAME)]
        [Parameter(ParameterSetName = SITEANDNAME)]
        [Alias("Regex")]
        public SwitchParameter RegularExpression { get; set; }

        /// <summary>
        /// <para type="description">The site to get the scheduled tasks for.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = SITE)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = SITEANDASSEMBLYNAME)]
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = SITEANDNAME)]
        public SiteInfo Site { get; set; }

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
                case SITE:
                    scheduledTasks = this.BusinessLayer.GetScheduledTasks(this.Site.ActLike<ISite>());
                    break;
                case SITEANDASSEMBLYNAME:
                    scheduledTasks = this.BusinessLayer.GetScheduledTasksByAssemblyName(this.Name, this.RegularExpression.ToBool(), this.Site.ActLike<ISite>());
                    break;
                case SITEANDNAME:
                    scheduledTasks = this.BusinessLayer.GetScheduledTasksByNameOrDisplayName(this.Name, this.RegularExpression.ToBool(), this.Site.ActLike<ISite>());
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

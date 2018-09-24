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
using System.Management.Automation;
using CMS.Scheduler;
using CMS.SiteProvider;
using ImpromptuInterface;
using PoshKentico.Business.Configuration.ScheduledTasks;
using PoshKentico.Core.Services.Configuration.ScheduledTasks;
using PoshKentico.Core.Services.Configuration.Sites;

namespace PoshKentico.Cmdlets.Configuration.ScheduledTasks
{
    [Cmdlet(VerbsCommon.New, "CMSScheduleTask")]
    public class NewCmsScheduledTaskCmdlet : MefCmdlet
    {
        #region Properties

        [Parameter(Mandatory = true, Position = 2)]
        public string AssemblyName { get; set; }

        [Parameter(Mandatory = true, Position = 3)]
        public string Class { get; set; }

        [Parameter(Mandatory = true, Position = 4)]
        public string Data { get; set; }

        [Parameter(Mandatory = true, Position = 1)]
        public string DisplayName { get; set; }

        [Parameter(Mandatory = true, Position = 5)]
        public TaskInterval Interval { get; set; }

        [Parameter(Mandatory = true, Position = 0)]
        public string Name { get; set; }

        [Parameter]
        public SiteInfo Site { get; set; }

        [Import]
        public NewCmsScheduledTaskBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

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

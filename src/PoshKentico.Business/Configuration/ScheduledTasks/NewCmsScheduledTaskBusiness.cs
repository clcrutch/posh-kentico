// <copyright file="NewCmsScheduledTaskBusiness.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Configuration.ScheduledTasks;

using ISite = PoshKentico.Core.Services.Configuration.Sites.ISite;

namespace PoshKentico.Business.Configuration.ScheduledTasks
{
    /// <summary>
    /// Business layer of the New-CMSScheduledTask cmdlet.
    /// </summary>
    [Export(typeof(NewCmsScheduledTaskBusiness))]
    public class NewCmsScheduledTaskBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the scheduled task service.  Populated by MEF.
        /// </summary>
        [Import]
        public IScheduledTaskService ScheduledTaskService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new <see cref="IScheduledTask"/>.
        /// </summary>
        /// <param name="assemblyName">The assembly name for the new <see cref="IScheduledTask"/>.</param>
        /// <param name="class">The class name for the new <see cref="IScheduledTask"/>.</param>
        /// <param name="data">The data for the new <see cref="IScheduledTask"/>.</param>
        /// <param name="displayName">The display name for the new <see cref="IScheduledTask"/>.</param>
        /// <param name="interval">The <see cref="IScheduledTaskInterval"/> for the new <see cref="IScheduledTask"/>.</param>
        /// <param name="name">The name for the new <see cref="IScheduledTask"/>.</param>
        /// <param name="site">The <see cref="ISite"/> to associate the <see cref="IScheduledTask"/> with.</param>
        /// <returns>The newly created <see cref="IScheduledTask"/>.</returns>
        public IScheduledTask New(string assemblyName, string @class, string data, string displayName, IScheduledTaskInterval interval, string name, ISite site)
        {
            var scheduledTask = new ScheduledTask
            {
                TaskAssemblyName = assemblyName,
                TaskClass = @class,
                TaskData = data,
                TaskDisplayName = displayName,
                TaskName = name,
            };

            return this.ScheduledTaskService.NewScheduledTask(scheduledTask, interval);
        }

        #endregion

        #region Classes

        private class ScheduledTask : IScheduledTask
        {
            public string TaskAssemblyName { get; set; }

            public string TaskClass { get; set; }

            public string TaskData { get; set; }

            public string TaskDisplayName { get; set; }

            public int TaskID { get; set; }

            public string TaskInterval { get; set; }

            public string TaskName { get; set; }

            public int TaskSiteID { get; set; }
        }

        #endregion

    }
}

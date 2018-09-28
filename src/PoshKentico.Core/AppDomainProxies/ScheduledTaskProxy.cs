// <copyright file="ScheduledTaskProxy.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Core.AppDomainProxies
{
    /// <summary>
    /// The proxy for scheduled tasks.
    /// </summary>
    internal class ScheduledTaskProxy : ProxyBase
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
        /// Executes the scheduled task within the current app domain.
        /// </summary>
        /// <param name="id">The ID to execute.</param>
        public void ExecuteScheduledTask(int id)
        {
            this.Initialize();

            this.ScheduledTaskService.ExecuteScheduledTask(this.ScheduledTaskService.GetScheduledTask(id));
        }

        #endregion

    }
}

﻿// <copyright file="SetCmsScheduledTaskBusiness.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Business.Configuration.ScheduledTasks
{
    /// <summary>
    /// Business layer of the Set-CMSScheduledTask.
    /// </summary>
    [Export(typeof(SetCmsScheduledTaskBusiness))]
    public class SetCmsScheduledTaskBusiness : CmdletBusinessBase
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
        /// Sets the <see cref="IScheduledTask"/> in Kentico.
        /// </summary>
        /// <param name="scheduledTask">The <see cref="IScheduledTask"/> to set in Kentico.</param>
        /// <param name="scheduledTaskInterval">The <see cref="IScheduledTaskInterval"/> to set on the scheduled task.</param>
        public void Set(IScheduledTask scheduledTask, IScheduledTaskInterval scheduledTaskInterval) =>
            this.ScheduledTaskService.Set(scheduledTask, scheduledTaskInterval);

        #endregion

    }
}

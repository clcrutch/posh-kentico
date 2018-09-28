// <copyright file="IScheduledTaskService.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Core.Services.Configuration.ScheduledTasks
{
    /// <summary>
    /// Service for interacting with scheduled tasks.
    /// </summary>
    public interface IScheduledTaskService
    {
        #region Properties

        /// <summary>
        /// Gets a list of <see cref="IScheduledTask"/> setup in the CMS system.
        /// </summary>
        IEnumerable<IScheduledTask> ScheduledTasks { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the specified <see cref="IScheduledTask"/> within the current App Domain.
        /// </summary>
        /// <param name="scheduledTask">The <see cref="IScheduledTask"/> to execute.</param>
        void ExecuteScheduledTask(IScheduledTask scheduledTask);

        /// <summary>
        /// Executes the specified <see cref="IScheduledTask"/> within a new App Domain.
        /// </summary>
        /// <param name="scheduledTask">The <see cref="IScheduledTask"/> to execute.</param>
        void ExecuteScheduledTaskInNewAppDomain(IScheduledTask scheduledTask);

        /// <summary>
        /// Gets the <see cref="IScheduledTask"/> that matches the current ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="IScheduledTask"/> to return.</param>
        /// <returns>The <see cref="IScheduledTask"/> which matches the ID.</returns>
        IScheduledTask GetScheduledTask(int id);

        /// <summary>
        /// Gets the <see cref="IScheduledTaskInterval"/> from the <see cref="IScheduledTask"/>.
        /// </summary>
        /// <param name="scheduledTask">The <see cref="IScheduledTask"/> to get the <see cref="IScheduledTaskInterval"/> for.</param>
        /// <returns>The <see cref="IScheduledTaskInterval"/> for the specified <see cref="IScheduledTask"/>.</returns>
        IScheduledTaskInterval GetScheduledTaskInterval(IScheduledTask scheduledTask);

        /// <summary>
        /// Creates new <see cref="IScheduledTask"/> given the specified <see cref="IScheduledTaskInterval"/>.
        /// </summary>
        /// <param name="scheduledTask">The <see cref="IScheduledTask"/> to create in the system.</param>
        /// <param name="scheduledTaskInterval">The <see cref="IScheduledTaskInterval"/> for the new <see cref="IScheduledTask"/>.</param>
        /// <returns>The newly created <see cref="IScheduledTask"/>.</returns>
        IScheduledTask NewScheduledTask(IScheduledTask scheduledTask, IScheduledTaskInterval scheduledTaskInterval);

        /// <summary>
        /// Removes a <see cref="IScheduledTask"/> from the CMS system.
        /// </summary>
        /// <param name="scheduledTask">The <see cref="IScheduledTask"/> to remove from the system.</param>
        void Remove(IScheduledTask scheduledTask);

        /// <summary>
        /// Sets the <see cref="IScheduledTask"/> in the CMS system.
        /// </summary>
        /// <param name="scheduledTask">The <see cref="IScheduledTask"/> to set in the system.</param>
        /// <param name="scheduledTaskInterval">The <see cref="IScheduledTaskInterval"/> for the scheduled task.</param>
        void Set(IScheduledTask scheduledTask, IScheduledTaskInterval scheduledTaskInterval);

        #endregion

    }
}

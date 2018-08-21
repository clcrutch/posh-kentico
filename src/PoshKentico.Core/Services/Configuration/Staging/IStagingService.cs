// <copyright file="IStagingService.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Configuration.Roles;

namespace PoshKentico.Core.Services.Configuration.Staging
{
    /// <summary>
    /// Service for providing access to a CMS staging servers.
    /// </summary>
    public interface IStagingService
    {
        #region Properties

        /// <summary>
        /// Gets a list of all of the <see cref="IServer"/> provided by the CMS System.
        /// </summary>
        IEnumerable<IServer> Servers { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the <see cref="IServer"/>.
        /// </summary>
        /// <param name="server">The <see cref="IServer"/> to create.</param>
        /// <returns>The newly created <see cref="IServer"/>.</returns>
        IServer Create(IServer server);

        /// <summary>
        /// Gets the <see cref="IServer"/> which matches the supplied ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="IServer"/> to return.</param>
        /// <returns>The <see cref="IServer"/> which matches the ID, else null.</returns>
        IServer GetServer(int id);

        /// <summary>
        /// Gets the <see cref="IServer"/> which matches the supplied server name.
        /// </summary>
        /// <param name="serverName">The Server Name of the <see cref="IServer"/> to return.</param>
        /// <param name="serverSiteId">The Server site id of the <see cref="IServer"/>to return.</param>
        /// <returns>The <see cref="IServer"/> which matches the server name, else null.</returns>
        IServer GetServer(string serverName, int serverSiteId);

        /// <summary>
        /// Updates the <see cref="IServer"/>.
        /// </summary>
        /// <param name="server">The <see cref="IServer"/> to update.</param>
        void Update(IServer server);

        /// <summary>
        /// Deletes the specified <see cref="IServer"/>.
        /// </summary>
        /// <param name="server">The <see cref="IServer"/> to delete.</param>
        void Delete(IServer server);

        /// <summary>
        /// Synchronize staging tasks at specified <see cref="IServer"/>
        /// </summary>
        /// <param name="server">The <see cref="IServer"/> to synchronize the tasks.</param>
        /// <returns>the error message</returns>
        string SynchronizeStagingTask(IServer server);

        /// <summary>
        /// Delete staging tasks at specified <see cref="IServer"/>
        /// </summary>
        /// <param name="server">The <see cref="IServer"/> to delete the tasks.</param>
        void DeleteStagingTask(IServer server);

        /// <summary>
        /// Set a new role for logging staging tasks under specific task groups
        /// </summary>
        /// <param name="role">The <see cref="IRole"/>.</param>
        /// <param name="taskGroupName">the task group code name</param>
        void SetLoggingRole(IRole role, string taskGroupName);

        /// <summary>
        /// Set a new role for running code without logging of staging tasks
        /// </summary>
        /// <param name="role">The <see cref="IRole"/>.</param>
        void SetNoLoggingRole(IRole role);

        #endregion

    }
}

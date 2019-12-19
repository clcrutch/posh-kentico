// <copyright file="KenticoStagingService.cs" company="Chris Crutchfield">
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
using System.Linq;
using CMS.Base;
using CMS.Membership;
using CMS.Synchronization;
using ImpromptuInterface;
using PoshKentico.Core.Services.Configuration.Roles;
using PoshKentico.Core.Services.Configuration.Staging;

using IServer = PoshKentico.Core.Services.Configuration.Staging.IServer;

namespace PoshKentico.Core.Providers.Configuration.Staging
{
    /// <summary>
    /// Implementation of <see cref="IStagingService"/> that uses Kentico.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Export(typeof(IStagingService))]
    public class KenticoStagingService : IStagingService
    {
        #region Properties

        /// <inheritdoc/>
        public IEnumerable<IServer> Servers => (from c in ServerInfoProvider.GetServers()
                                                select Impromptu.ActLike<IServer>(c as ServerInfo)).ToArray();

        #endregion

        #region Methods

        /// <inheritdoc/>
        public IServer Create(IServer server)
        {
            var newServer = new ServerInfo
            {
                ServerDisplayName = server.ServerDisplayName,
                ServerName = server.ServerName,
                ServerEnabled = (bool)server.ServerEnabled,
                ServerURL = server.ServerURL,
                ServerAuthentication = server.ServerAuthentication,
                ServerUsername = server.ServerUsername,
                ServerPassword = server.ServerPassword,
                ServerSiteID = server.ServerSiteID,
            };

            // Saves the staging server to the database
            ServerInfoProvider.SetServerInfo(newServer);

            return newServer.ActLike<IServer>();
        }

        /// <inheritdoc/>
        public void Delete(IServer server)
        {
            // Gets the staging server
            ServerInfo deleteServer = ServerInfoProvider.GetServerInfo(server.ServerName, server.ServerSiteID);

            if (deleteServer != null)
            {
                // Deletes the staging server
                ServerInfoProvider.DeleteServerInfo(deleteServer);
            }
        }

        /// <inheritdoc/>
        public IServer GetServer(int id)
        {
            return (ServerInfoProvider.GetServerInfo(id) as ServerInfo)?.ActLike<IServer>();
        }

        /// <inheritdoc/>
        public IServer GetServer(string serverName, int serverSiteId)
        {
            return (ServerInfoProvider.GetServerInfo(serverName, serverSiteId) as ServerInfo)?.ActLike<IServer>();
        }

        /// <inheritdoc/>
        public IServer Update(IServer server, bool isReplace = true)
        {
            // Gets the staging server
            ServerInfo updateServer = ServerInfoProvider.GetServerInfo(server.ServerName, server.ServerSiteID);
            if (updateServer != null)
            {
                if (isReplace)
                {
                    updateServer = server.UndoActLike();
                }
                else
                {
                    // Updates the server properties
                    updateServer.ServerDisplayName = server.ServerDisplayName ?? updateServer.ServerDisplayName;
                    updateServer.ServerURL = server.ServerURL ?? updateServer.ServerURL;
                    updateServer.ServerEnabled = server.ServerEnabled == null ? updateServer.ServerEnabled : (bool)server.ServerEnabled;
                    updateServer.ServerAuthentication = server.ServerAuthentication;
                    updateServer.ServerUsername = server.ServerUsername ?? updateServer.ServerUsername;
                    updateServer.ServerPassword = server.ServerPassword ?? updateServer.ServerPassword;
                }

                // Saves the updated server to the database
                ServerInfoProvider.SetServerInfo(updateServer);
            }

            return updateServer.ActLike<IServer>();
        }

        /// <inheritdoc/>
        public string SynchronizeStagingTask(IServer server)
        {
            string res = string.Empty;

            // Gets a staging server
            ServerInfo stagingServer = this.GetServer(server.ServerName, server.ServerSiteID).UndoActLike();

            if (stagingServer != null)
            {
                // Gets all staging tasks that target the given server
                var tasks = StagingTaskInfoProvider.SelectTaskList(stagingServer.ServerSiteID, stagingServer.ServerID, null, null);

                if (tasks.Count == 0)
                {
                    return "There is no task that target the given server.";
                }

                // Loops through individual staging tasks
                foreach (StagingTaskInfo task in tasks)
                {
                    // Synchronizes the staging task
                    string result = new StagingTaskRunner(stagingServer.ServerID).RunSynchronization(task.TaskID);

                    // The task synchronization failed
                    // The 'result' string returned by the RunSynchronization method contains the error message for the given task
                    if (!string.IsNullOrEmpty(result))
                    {
                        res += result + "\n";
                    }
                }
            }

            return res;
        }

        /// <inheritdoc/>
        public void DeleteStagingTask(IServer server)
        {
            // Gets the staging server
            ServerInfo stagingServer = this.GetServer(server.ServerName, server.ServerSiteID).UndoActLike();

            if (stagingServer != null)
            {
                // Gets all staging tasks that target the given server
                var tasks = StagingTaskInfoProvider.SelectTaskList(stagingServer.ServerSiteID, stagingServer.ServerID, null, null);

                // Loops through individual staging tasks
                foreach (StagingTaskInfo task in tasks)
                {
                    // Deletes the staging task
                    StagingTaskInfoProvider.DeleteTaskInfo(task);
                }
            }
        }

        /// <inheritdoc/>
        public void SetNoLoggingRole(IRole role)
        {
            // Prepares an action context for running code without logging of staging tasks
            using (new CMSActionContext() { LogSynchronization = false })
            {
                // Creates a new role without logging any staging tasks
                RoleInfo newRole = new RoleInfo
                {
                    RoleDisplayName = role.RoleDisplayName,
                    RoleName = role.RoleName,
                    SiteID = role.SiteID,
                };

                RoleInfoProvider.SetRoleInfo(newRole);
            }
        }

        /// <inheritdoc/>
        public void SetLoggingRole(IRole role, string taskGroupName)
        {
            // Gets a "collection" of task groups (in this case one group whose code name is equal to "Group_Name")
            // TODO: get task groups from a task group service as well.
            var taskGroups = TaskGroupInfoProvider.GetTaskGroups().WhereEquals("TaskGroupCodeName", taskGroupName);

            // Prepares a synchronization action context
            // The context ensures that any staging tasks logged by the wrapped code are included in the specified task groups
            using (new SynchronizationActionContext() { TaskGroups = taskGroups })
            {
                // Creates a new role object
                RoleInfo newRole = new RoleInfo
                {
                    // Sets the role properties
                    RoleDisplayName = role.RoleDisplayName,
                    RoleName = role.RoleName,
                    SiteID = role.SiteID,
                };

                // Saves the role to the database
                RoleInfoProvider.SetRoleInfo(newRole);
            }
        }

        #endregion
    }
}

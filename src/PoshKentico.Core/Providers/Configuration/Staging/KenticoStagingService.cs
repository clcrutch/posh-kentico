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
using System.Linq;
using CMS.SiteProvider;
using CMS.Synchronization;
using ImpromptuInterface;
using PoshKentico.Core.Services.Configuration.Staging;

namespace PoshKentico.Core.Providers.Configuration
{
    /// <summary>
    /// Implementation of <see cref="IStagingService"/> that uses Kentico.
    /// </summary>
    [Export(typeof(IStagingService))]
    public class KenticoStagingService : IStagingService
    {
        /// <inheritdoc/>
        public IEnumerable<IServer> Servers => (from c in ServerInfoProvider.GetServers()
                                                select Impromptu.ActLike<IServer>(c as ServerInfo)).ToArray();

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
        public void Update(IServer server)
        {
            // Gets the staging server
            ServerInfo updateServer = ServerInfoProvider.GetServerInfo(server.ServerName, server.ServerSiteID);
            if (updateServer != null)
            {
                // Updates the server properties
                updateServer.ServerDisplayName = server.ServerDisplayName == null ? updateServer.ServerDisplayName : server.ServerDisplayName;
                updateServer.ServerURL = server.ServerURL == null ? updateServer.ServerURL : server.ServerURL;
                updateServer.ServerEnabled = server.ServerEnabled == null ? updateServer.ServerEnabled : (bool)server.ServerEnabled;
                updateServer.ServerAuthentication = server.ServerAuthentication;
                updateServer.ServerUsername = server.ServerUsername == null ? updateServer.ServerUsername : server.ServerUsername;
                updateServer.ServerPassword = server.ServerPassword == null ? updateServer.ServerPassword : server.ServerPassword;

                // Saves the updated server to the database
                ServerInfoProvider.SetServerInfo(updateServer);
            }
        }
    }
}

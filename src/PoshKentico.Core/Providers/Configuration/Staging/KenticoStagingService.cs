﻿// <copyright file="KenticoStagingService.cs" company="Chris Crutchfield">
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
                ServerDisplayName = server.DisplayName,
                ServerName = server.ServerName,
                ServerEnabled = server.ServerEnabled,
                ServerURL = server.ServerURL,
                ServerAuthentication = server.ServerAuthentication,
                ServerUsername = server.UserName,
                ServerPassword = server.Password,
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
            ServerInfo deleteServer = ServerInfoProvider.GetServerInfo(server.ServerName, SiteContext.CurrentSiteID);

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
        public IServer GetServer(string serverName)
        {
            return (ServerInfoProvider.GetServerInfo(serverName, SiteContext.CurrentSiteID) as ServerInfo)?.ActLike<IServer>();
        }

        /// <inheritdoc/>
        public void Update(IServer server)
        {
            // Gets the staging server
            ServerInfo updateServer = ServerInfoProvider.GetServerInfo(server.ServerName, SiteContext.CurrentSiteID);
            if (updateServer != null)
            {
                // Updates the server properties
                updateServer.ServerDisplayName = server.DisplayName;
                updateServer.ServerName = server.ServerName;
                updateServer.ServerURL = server.ServerURL;
                updateServer.ServerEnabled = server.ServerEnabled;
                updateServer.ServerAuthentication = server.ServerAuthentication;
                updateServer.ServerUsername = server.UserName;
                updateServer.ServerPassword = server.Password;

                // Saves the updated server to the database
                ServerInfoProvider.SetServerInfo(updateServer);
            }
        }
    }
}

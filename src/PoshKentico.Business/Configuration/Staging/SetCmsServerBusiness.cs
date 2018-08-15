// <copyright file="SetCmsServerBusiness.cs" company="Chris Crutchfield">
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
using CMS.Synchronization;
using ImpromptuInterface;
using PoshKentico.Core.Services.Configuration.Staging;

namespace PoshKentico.Business.Configuration.Staging
{
    /// <summary>
    /// Business Layer for the Set-CMSServer cmdlet.
    /// </summary>
    [Export(typeof(SetCmsServerBusiness))]
    public class SetCmsServerBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the Site Service.  Populated by MEF.
        /// </summary>
        [Import]
        public IStagingService StagingService { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Sets the <see cref="IServer"/> in the CMS System.
        /// </summary>
        /// <param name="server">The <see cref="IServer"/> to set.</param>
        public void Set(IServer server)
        {
            this.StagingService.Update(server);
        }

        /// <summary>
        /// Sets the <see cref="IServer"/> in the CMS System.
        /// </summary>
        /// <param name="serverName">The server name to look for the server</param>
        /// <param name="serverSiteId">The server site id to look for the server</param>
        /// <param name="displayName">The Display Name for server to update</param>
        /// <param name="serverUrl">The Server Url for server to update</param>
        /// <param name="authentication">The authentication for server to update</param>
        /// <param name="enabled">The enabled status for server to update</param>
        /// <param name="userName">The user name for server to update</param>
        /// <param name="password">The password for server to update</param>
        public void Set(
                        string serverName,
                        int serverSiteId,
                        string displayName,
                        string serverUrl,
                        ServerAuthenticationEnum authentication,
                        bool? enabled,
                        string userName,
                        string password)
        {
            var data = new
            {
                ServerName = serverName,
                ServerSiteID = serverSiteId,
                ServerDisplayName = displayName,
                ServerURL = serverUrl,
                ServerAuthentication = authentication,
                ServerEnabled = enabled,
                ServerUsername = userName,
                ServerPassword = password,
            };

            this.StagingService.Update(data.ActLike<IServer>());
        }

        #endregion

    }
}

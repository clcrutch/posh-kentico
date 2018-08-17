// <copyright file="NewCmsServerBusiness.cs" company="Chris Crutchfield">
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
using System.Globalization;
using CMS.Synchronization;
using ImpromptuInterface;
using PoshKentico.Core.Services.Configuration.Sites;
using PoshKentico.Core.Services.Configuration.Staging;

namespace PoshKentico.Business.Configuration.Staging
{
    /// <summary>
    /// Business layer for the New-CMSServer cmdlet.
    /// </summary>
    [Export(typeof(NewCmsServerBusiness))]
    public class NewCmsServerBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the Staging Service.  Populated by MEF.
        /// </summary>
        [Import]
        public IStagingService StagingService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new <see cref="IServer"/> in the CMS System.
        /// </summary>
        /// <param name="displayName">The Display Name for the new Server</param>
        /// <param name="serverName">The Server Name for the new Server</param>
        /// <param name="serverUrl">The Url for the new Server</param>
        /// <param name="authentication">The authentication for the new Server</param>
        /// <param name="enabled">The enabled status for the new Server</param>
        /// <param name="userName">The user name for the new Server</param>
        /// <param name="password">The password for the new Server</param>
        /// <param name="siteID">The site id associated with the new Server</param>
        /// <returns>The newly created <see cref="IServer"/>.</returns>
        public IServer CreateServer(
                                    string displayName,
                                    string serverName,
                                    string serverUrl,
                                    ServerAuthenticationEnum authentication,
                                    bool? enabled,
                                    string userName,
                                    string password,
                                    int siteID)
        {
            // default authentication is UserName
            authentication = authentication == ServerAuthenticationEnum.X509 ? authentication : ServerAuthenticationEnum.UserName;

            // default is Enabled
            enabled = enabled == false ? false : true;

            // default user name and password value cannot be null
            userName = string.IsNullOrEmpty(userName) ? string.Empty : userName;
            password = string.IsNullOrEmpty(password) ? string.Empty : password;

            // default server name
            TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;
            var newServerName = string.IsNullOrEmpty(serverName) ? txtInfo.ToTitleCase(displayName).Replace(" ", string.Empty) : serverName;

            var data = new
            {
                ServerDisplayName = displayName,
                ServerName = newServerName,
                ServerURL = serverUrl,
                ServerAuthentication = authentication,
                ServerEnabled = enabled,
                ServerUsername = userName,
                ServerPassword = password,
                ServerSiteID = siteID,
            };

            return this.StagingService.Create(data.ActLike<IServer>());
        }

        #endregion
    }
}

// <copyright file="IServer.cs" company="Chris Crutchfield">
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

using CMS.Synchronization;

namespace PoshKentico.Core.Services.Configuration.Staging
{
    /// <summary>
    /// Represents a Server Object.
    /// </summary>
    public interface IServer
    {
        #region Properties

        /// <summary>
        /// Gets the display name for the server.
        /// </summary>
        string ServerDisplayName { get; }

        /// <summary>
        /// Gets the server name.
        /// </summary>
        string ServerName { get; }

        /// <summary>
        /// Gets the server Url.
        /// </summary>
        string ServerURL { get; }

        /// <summary>
        /// Gets a value indicating whether the server is in enabled status.
        /// </summary>
        bool? ServerEnabled { get; }

        /// <summary>
        /// Gets server authentication Enum.
        /// </summary>
        ServerAuthenticationEnum ServerAuthentication { get; }

        /// <summary>
        /// Gets the server ID for the server.
        /// </summary>
        int ServerID { get; }

        /// <summary>
        /// Gets the server site ID for the server.
        /// </summary>
        int ServerSiteID { get; }

        /// <summary>
        /// Gets the Server user name.
        /// </summary>
        string ServerUsername { get; }

        /// <summary>
        /// Gets the Server password.
        /// </summary>
        string ServerPassword { get; }

            #endregion

        }
}

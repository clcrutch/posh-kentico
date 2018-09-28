﻿// <copyright file="IRoleService.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Configuration.Users;

namespace PoshKentico.Core.Services.Configuration.Roles
{
    /// <summary>
    /// Service for providing access to a CMS Roles.
    /// </summary>
    public interface IRoleService
    {
        #region Properties

        /// <summary>
        /// Gets a list of all of the <see cref="IRole"/> provided by the CMS System.
        /// </summary>
        IEnumerable<IRole> Roles { get; }

        /// <summary>
        /// Gets a list of all of the <see cref="IUserRole"/> provided by the CMS System.
        /// </summary>
        IEnumerable<IUserRole> UserRoles { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the role.
        /// </summary>
        /// <param name="role">The <see cref="IRole"/> to create.</param>
        /// <returns>The newly created role.</returns>
        IRole CreateRole(IRole role);

        /// <summary>
        /// Sets the <see cref="IRole"/>.
        /// </summary>
        /// <param name="role">The <see cref="IRole"/> to update to.</param>
        /// <param name="isReplace">To indicate if replace the complete object or update only the properties.</param>
        /// <returns>The updated <see cref="IRole"/>.</returns>
        IRole SetRole(IRole role, bool isReplace = true);

        /// <summary>
        /// Gets the <see cref="IRole"/> which matches the supplied role name.
        /// </summary>
        /// <param name="roleName">The RoleName of the role <see cref="IRole"/> to return. </param>
        /// <param name="siteName">The siteName of the role <see cref="IRole"/> to return.</param>
        /// <returns>The <see cref="IRole"/> which matches the RoleName, else null.</returns>
        IRole GetRole(string roleName, string siteName);

        /// <summary>
        /// Gets the <see cref="IRole"/> which matches the supplied role id.
        /// </summary>
        /// <param name="id">The ID of the role <see cref="IRole"/> to return.</param>
        /// <returns>The <see cref="IRole"/> which matches the ID, else null.</returns>
        IRole GetRole(int id);

        /// <summary>
        /// Deletes the <see cref="IRole"/> which matches the supplied role name.
        /// </summary>
        /// <param name="roleName">The RoleName of the role <see cref="IRole"/> to delete. </param>
        /// <param name="siteID">The SiteID of the role <see cref="IRole"/> to delete.</param>
        void DeleteRole(string roleName, int siteID);

        /// <summary>
        /// Adds the User <see cref="IUser"/> to a Role <see cref="IRole"/>.
        /// </summary>
        /// <param name="userName">The user name of the user <see cref="IUser"/> to assign to a role.</param>
        /// <param name="roleName">The role name of the role to add a user to.</param>
        /// <param name="siteID">The SiteID of the role <see cref="IRole"/>.</param>
        void AddUserToRole(string userName, string roleName, int siteID);

        /// <summary>
        /// Removes the User <see cref="IUser"/> from a Role <see cref="IRole"/>.
        /// </summary>
        /// <param name="userName">The user name of the user <see cref="IUser"/> to remove from a role.</param>
        /// <param name="roleName">The role name of the role to remove a user from.</param>
        /// <param name="siteID">The SiteID of the role <see cref="IRole"/>.</param>
        void RemoveUserFromRole(string userName, string roleName, int siteID);

        /// <summary>
        /// Gets all roles <see cref="IRole"/> of a user <see cref="IUser"/>.
        /// </summary>
        /// <param name="userName">The user name of the user.</param>
        /// <returns>A list of roles that belong to the specified user.</returns>
        IEnumerable<IRole> GetRolesFromUser(string userName);
        #endregion
    }
}

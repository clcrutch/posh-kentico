// <copyright file="IUserService.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Configuration.Sites;

namespace PoshKentico.Core.Services.Configuration.Users
{
    /// <summary>
    /// Service for providing access to a CMS Users.
    /// </summary>
    public interface IUserService
    {
        #region Properties

        /// <summary>
        /// Gets a list of all of the <see cref="IUser"/> provided by the CMS System.
        /// </summary>
        IEnumerable<IUser> Users { get; }

        /// <summary>
        /// Gets a list of all of the <see cref="IUserRole"/> provided by the CMS System.
        /// </summary>
        IEnumerable<IUserRole> UserRoles { get; }
        #endregion

        #region Methods

        /// <summary>
        /// Creates the <see cref="IUser"/>.
        /// </summary>
        /// <param name="user">The <see cref="IUser"/> to create.</param>
        /// <returns>The newly created <see cref="IUser"/>.</returns>
        IUser CreateUser(IUser user);

        /// <summary>
        /// Sets the <see cref="IUser"/>.
        /// </summary>
        /// <param name="user">The <see cref="IUser"/> to update to.</param>
        /// <param name="isReplace">To indicate if replace the complete object or update only the properties.</param>
        /// <returns>The updated <see cref="IUser"/>.</returns>
        IUser SetUser(IUser user, bool isReplace = true);

        /// <summary>
        /// Gets the <see cref="IUser"/> which matches the supplied ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="IUser"/> to return.</param>
        /// <returns>The <see cref="IUser"/> which matches the ID, else null.</returns>
        IUser GetUser(int id);

        /// <summary>
        /// Gets the <see cref="IUser"/> which matches the supplied user name.
        /// </summary>
        /// <param name="userName">The UserName of the user <see cref="IUser"/> to return. </param>
        /// <returns>The <see cref="IUser"/> which matches the UserName, else null.</returns>
        IUser GetUser(string userName);

        /// <summary>
        /// Gets all users <see cref="IUser"/> from a role <see cref="IRole"/>.
        /// </summary>
        /// <param name="roleName">The role name of the role.</param>
        /// <param name="siteID">The SiteID of the role <see cref="IRole"/>.</param>
        /// <returns>A list of users that belong to the specified role.</returns>
        IEnumerable<IUser> GetUsersFromRole(string roleName, int siteID);

        /// <summary>
        /// Deletes the <see cref="IUser"/> which matches the supplied user name.
        /// </summary>
        /// <param name="user">The user <see cref="IUser"/> to delete.</param>
        void DeleteUser(IUser user);

        /// <summary>
        /// Assigns a user to a site.
        /// </summary>
        /// <param name="user">The user <see cref="IUser"/> to assign to a site. </param>
        /// <param name="siteName">The site name of the <see cref="ISite"/> to assign the user to. </param>
        void AddUserToSite(IUser user, string siteName);

        /// <summary>
        /// Removes a user from a site.
        /// </summary>
        /// <param name="user">The user <see cref="IUser"/> to remove from a site. </param>
        /// <param name="siteName">The site name of the <see cref="ISite"/> to remove the user from. </param>
        void RemoveUserFromSite(IUser user, string siteName);
        #endregion

    }
}

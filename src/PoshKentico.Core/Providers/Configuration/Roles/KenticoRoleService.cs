// <copyright file="KenticoRoleService.cs" company="Chris Crutchfield">
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

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using CMS.Membership;
using CMS.SiteProvider;
using ImpromptuInterface;
using PoshKentico.Core.Services.Configuration.Roles;
using PoshKentico.Core.Services.Configuration.Users;

namespace PoshKentico.Core.Providers.Configuration.Roles
{
    /// <summary>
    /// Implementation of <see cref="IRoleService"/> that uses Kentico.
    /// </summary>
    [Export(typeof(IRoleService))]
    public class KenticoRoleService : IRoleService
    {
        /// <inheritdoc/>
        public IEnumerable<IRole> Roles => (from c in RoleInfoProvider.GetRoles()
                                            select Impromptu.ActLike<IRole>(c as RoleInfo)).ToArray();

        /// <inheritdoc/>
        public IRole CreateRole(IRole role)
        {
            // Creates a new role object
            RoleInfo newRole = new RoleInfo
            {
                // Sets the role properties
                RoleDisplayName = role.RoleDisplayName,
                RoleName = role.RoleName,
                SiteID = role.SiteID,
            };

            // Verifies that the role is unique for the current site
            if (RoleInfoProvider.GetRoleInfo(role.RoleName, role.SiteID) == null)
            {
                // Saves the role to the database
                RoleInfoProvider.SetRoleInfo(newRole);
            }
            else
            {
                // A role with the same name already exists on the site
                throw new Exception("A role with the same name already exists on the site");
            }

            return newRole.ActLike<IRole>();
        }

        /// <inheritdoc/>
        public IRole SetRole(IRole role, bool isReplace = true)
        {
            RoleInfo existingRole = RoleInfoProvider.GetRoleInfo(role.RoleName, role.SiteID);

            if (existingRole != null)
            {
                // Updates the role's properties
                if (isReplace)
                {
                    existingRole = role.UndoActLike();
                }
                else
                {
                    existingRole.RoleDisplayName = role.RoleDisplayName ?? existingRole.RoleDisplayName;
                }

                // Saves the changes to the database
                RoleInfoProvider.SetRoleInfo(existingRole);
            }
            else
            {
                // A role with the specified name not exists on the site
                throw new Exception(string.Format("A role with the role name {0} does not exist on the specified site with site ID {1}", role.RoleName, role.SiteID));
            }

            return existingRole.ActLike<IRole>();
        }

        /// <inheritdoc/>
        public IRole GetRole(string roleName, string siteID)
        {
            RoleInfo existingRole = RoleInfoProvider.GetRoleInfo(roleName, siteID);

            return existingRole.ActLike<IRole>();
        }

        /// <inheritdoc/>
        public IRole GetRole(int id)
        {
            return (RoleInfoProvider.GetRoleInfo(id) as RoleInfo)?.ActLike<IRole>();
        }

        /// <inheritdoc/>
        public void DeleteRole(string roleName, int siteID)
        {
            // Gets the role
            RoleInfo deleteRole = RoleInfoProvider.GetRoleInfo(roleName, siteID);

            if (deleteRole != null)
            {
                // Deletes the role
                RoleInfoProvider.DeleteRoleInfo(deleteRole);
            }
        }

        /// <inheritdoc/>
        public void AddUserToRole(string userName, string roleName, int siteID)
        {
            // Gets the user
            UserInfo user = UserInfoProvider.GetUserInfo(userName);

            // Gets the role
            RoleInfo role = RoleInfoProvider.GetRoleInfo(roleName, siteID);
            string siteName = SiteInfoProvider.GetSiteName(siteID);

            if ((user != null) && (role != null))
            {
                // Adds the user to the role
                UserInfoProvider.AddUserToRole(user.UserName, role.RoleName, siteName);
            }
        }

        /// <inheritdoc/>
        public void RemoveUserFromRole(string userName, string roleName, int siteID)
        {
            // Gets the user
            UserInfo user = UserInfoProvider.GetUserInfo(userName);

            // Gets the role
            RoleInfo role = RoleInfoProvider.GetRoleInfo(roleName, siteID);
            string siteName = SiteInfoProvider.GetSiteName(siteID);

            if ((user != null) && (role != null))
            {
                // Adds the user to the role
                UserInfoProvider.RemoveUserFromRole(user.UserName, role.RoleName, siteName);
            }
        }
    }
}

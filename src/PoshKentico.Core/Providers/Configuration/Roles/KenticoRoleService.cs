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
using CMS.Modules;
using CMS.SiteProvider;
using ImpromptuInterface;
using PoshKentico.Core.Services.Configuration.Roles;
using PoshKentico.Core.Services.Configuration.Users;
using PoshKentico.Core.Services.Development.Modules;

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
        public IEnumerable<IUserRole> UserRoles => (from c in UserRoleInfoProvider.GetUserRoles()
                                                    select Impromptu.ActLike<IUserRole>(c as UserRoleInfo)).ToArray();

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
            RoleInfo existingRole = null;

            if (isReplace)
            {
                existingRole = role.UndoActLike();
            }
            else
            {
                existingRole = RoleInfoProvider.GetRoleInfo(role.RoleName, role.SiteID);
                if (existingRole != null)
                {
                    // Updates the role's properties
                    existingRole.RoleDisplayName = role.RoleDisplayName ?? existingRole.RoleDisplayName;
                }
                else
                {
                    // A role with the specified name not exist on the site
                    throw new Exception(string.Format("A role with the role name {0} does not exist on the specified site with site ID {1}", role.RoleName, role.SiteID));
                }
            }

            // Saves the changes to the database
            RoleInfoProvider.SetRoleInfo(existingRole);

            return existingRole.ActLike<IRole>();
        }

        /// <inheritdoc/>
        public IRole GetRole(string roleName, string siteName)
        {
            RoleInfo existingRole = RoleInfoProvider.GetRoleInfo(roleName, siteName);

            return existingRole.ActLike<IRole>();
        }

        /// <inheritdoc/>
        public IRole GetRole(int id)
        {
            return (RoleInfoProvider.GetRoleInfo(id) as RoleInfo)?.ActLike<IRole>();
        }

        /// <inheritdoc/>
        public void DeleteRole(IRole role)
        {
            // Gets the role
            RoleInfo deleteRole = RoleInfoProvider.GetRoleInfo(role.RoleName, role.SiteID);

            if (deleteRole != null)
            {
                // Deletes the role
                RoleInfoProvider.DeleteRoleInfo(deleteRole);
            }
            else
            {
                // A role with the specified name not exists on the site
                throw new Exception(string.Format("A role with the role name {0} does not exist on the specified site with site ID {1}", role.RoleName, role.SiteID));
            }
        }

        /// <inheritdoc/>
        public void AddUserToRole(IUser user, IRole role)
        {
            // Gets the user
            UserInfo userInfo = UserInfoProvider.GetUserInfo(user.UserName);

            // Gets the role
            RoleInfo roleInfo = RoleInfoProvider.GetRoleInfo(role.RoleName, role.SiteID);

            if ((userInfo != null) && (roleInfo != null))
            {
                string siteName = SiteInfoProvider.GetSiteName(role.SiteID);

                // Adds the user to the role
                UserInfoProvider.AddUserToRole(user.UserName, role.RoleName, siteName);
            }
            else
            {
                // A user with the specified user name not exist or role does not exist.
                throw new Exception(string.Format("A user with the user name {0} does not exist", user.UserName));
            }
        }

        /// <inheritdoc/>
        public void RemoveUserFromRole(IUser user, IRole role)
        {
            // Gets the user
            UserInfo userInfo = UserInfoProvider.GetUserInfo(user.UserName);

            // Gets the role
            RoleInfo roleInfo = RoleInfoProvider.GetRoleInfo(role.RoleName, role.SiteID);

            if ((userInfo != null) && (roleInfo != null))
            {
                string siteName = SiteInfoProvider.GetSiteName(role.SiteID);

                // Adds the user to the role
                UserInfoProvider.RemoveUserFromRole(user.UserName, role.RoleName, siteName);
            }
            else
            {
                // A user with the specified user name not exist or role does not exist.
                throw new Exception(string.Format("A user with the user name {0} does not exist", user.UserName));
            }
        }

        /// <inheritdoc/>
        public IEnumerable<IRole> GetRolesFromUser(IUser user)
        {
            // Gets the user
            UserInfo userInfo = UserInfoProvider.GetUserInfo(user.UserName);

            if (userInfo != null)
            {
                // Gets the user's roles
                var userRoleIDs = this.UserRoles.Where(x => x.UserID == user.UserID).Select(x => x.RoleID);
                var roles = RoleInfoProvider.GetRoles().Where(x => userRoleIDs.Contains(x.RoleID));

                return (from c in roles select Impromptu.ActLike<IRole>(c as RoleInfo)).ToArray();
            }
            else
            {
                // A user with the specified user name not exist or role does not exist.
                throw new Exception(string.Format("A user with the user name {0} does not exist", user.UserName));
            }
        }

        /// <inheritdoc/>
        public void AddModulePermissionToRole(IResource module, IRole role)
        {
            foreach (var permission in module.PermissionNames)
            {
                // Gets the module permission
                PermissionNameInfo permissionInfo = PermissionNameInfoProvider.GetPermissionNameInfo(permission, module.ResourceName, module.ClassName);

                // Gets the role
                RoleInfo roleInfo = RoleInfoProvider.GetRoleInfo(role.RoleName, role.SiteID);

                if ((permissionInfo != null) && (roleInfo != null))
                {
                    // Creates an object representing the role-permission relationship
                    RolePermissionInfo newRolePermission = new RolePermissionInfo
                    {
                        // Assigns the permission to the role
                        PermissionID = permissionInfo.PermissionId,
                        RoleID = roleInfo.RoleID,
                    };

                    // Saves the role-permission relationship into the database
                    RolePermissionInfoProvider.SetRolePermissionInfo(newRolePermission);
                }
                else
                {
                    // not exist
                    throw new Exception(string.Format("The role or permission does not exist"));
                }
            }
        }

        /// <inheritdoc/>
        public void RemoveModulePermissionFromRole(IResource module, IRole role)
        {
            foreach (var permission in module.PermissionNames)
            {
                // Gets the module permission
                PermissionNameInfo permissionInfo = PermissionNameInfoProvider.GetPermissionNameInfo(permission, module.ResourceName, module.ClassName);

                // Gets the role
                RoleInfo roleInfo = RoleInfoProvider.GetRoleInfo(role.RoleName, role.SiteID);

                if ((permissionInfo != null) && (roleInfo != null))
                {
                    // Gets the object representing the role-permission relationship
                    RolePermissionInfo deleteRolePermission = RolePermissionInfoProvider.GetRolePermissionInfo(roleInfo.RoleID, permissionInfo.PermissionId);

                    if (deleteRolePermission != null)
                    {
                        // Removes the permission from the role
                        RolePermissionInfoProvider.DeleteRolePermissionInfo(deleteRolePermission);
                    }
                }
                else
                {
                    // not exist
                    throw new Exception(string.Format("The role or permission does not exist"));
                }
            }
        }

        /// <inheritdoc/>
        public void AddUiElementToRole(IUIElement element, IRole role)
        {
            // Gets the role
            RoleInfo roleInfo = RoleInfoProvider.GetRoleInfo(role.RoleName, role.SiteID);

            // Gets the UI element (the element representing the Design tab in the Pages application in this case)
            UIElementInfo elementInfo = UIElementInfoProvider.GetUIElementInfo(element.ElementResourceID, element.ElementName);

            if ((roleInfo != null) && (elementInfo != null))
            {
                // Creates an object representing the role-UI element relationship
                RoleUIElementInfo newRoleElement = new RoleUIElementInfo
                {
                    // Assigns the UI element to the role
                    RoleID = roleInfo.RoleID,
                    ElementID = elementInfo.ElementID,
                };

                // Saves the new relationship to the database
                RoleUIElementInfoProvider.SetRoleUIElementInfo(newRoleElement);
            }
            else
            {
                // not exist
                throw new Exception(string.Format("The role or UI element does not exist"));
            }
        }

        /// <inheritdoc/>
        public void RemoveUiElementFromRole(IUIElement element, IRole role)
        {
            // Gets the role
            RoleInfo roleInfo = RoleInfoProvider.GetRoleInfo(role.RoleName, role.SiteID);

            // Gets the UI element (the element representing the Design tab in the Pages application in this case)
            UIElementInfo elementInfo = UIElementInfoProvider.GetUIElementInfo(element.ElementResourceID, element.ElementName);

            if ((roleInfo != null) && (elementInfo != null))
            {
                // Gets the object representing the relationship between the role and the UI element
                RoleUIElementInfo deleteRoleElement = RoleUIElementInfoProvider.GetRoleUIElementInfo(roleInfo.RoleID, elementInfo.ElementID);

                if (deleteRoleElement != null)
                {
                    // Removes the UI element from the role
                    RoleUIElementInfoProvider.DeleteRoleUIElementInfo(deleteRoleElement);
                }
            }
            else
            {
                // not exist
                throw new Exception(string.Format("The role or UI element does not exist"));
            }
        }

        /// <inheritdoc/>
        public IUIElement GetUiElement(string resourceName, string elementName)
        {
            UIElementInfo elementInfo = UIElementInfoProvider.GetUIElementInfo(resourceName, elementName);
            return elementInfo.ActLike<IUIElement>();
        }
    }
}

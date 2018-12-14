// <copyright file="RemoveCmsPermissionFromRoleBusiness.cs" company="Chris Crutchfield">
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
using ImpromptuInterface;
using PoshKentico.Core.Services.Configuration.Roles;
using PoshKentico.Core.Services.Development.Modules;

namespace PoshKentico.Business.Configuration.Roles
{
    /// <summary>
    /// Business Layer of Remove-CmsPermissionFromRole cmdlet.
    /// </summary>
    [Export(typeof(RemoveCmsPermissionFromRoleBusiness))]
    public class RemoveCmsPermissionFromRoleBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the Role Service.  Populated by MEF.
        /// </summary>
        [Import]
        public IRoleService RoleService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Remove a module permission to a role.
        /// </summary>
        /// <param name="role">The role to remove a module permission from.</param>
        /// <param name="permissionNames">The permission names of the module <see cref="IResource"/>.</param>
        /// <param name="resourceName">The resource name of the module <see cref="IResource"/>.</param>
        /// <param name="className">The class name of the module <see cref="IResource"/>.</param>
        public void RemovePermissionFromRole(IRole role, string[] permissionNames, string resourceName, string className = null)
        {
            var module = new
            {
                PermissionNames = permissionNames,
                ResourceName = resourceName,
                ClassName = className,
            };
            this.RoleService.RemoveModulePermissionFromRole(module.ActLike<IResource>(), role);
        }
        #endregion
    }
}

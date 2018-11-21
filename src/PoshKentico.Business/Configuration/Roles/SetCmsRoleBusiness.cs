// <copyright file="SetCmsRoleBusiness.cs" company="Chris Crutchfield">
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
using CMS.SiteProvider;
using ImpromptuInterface;
using PoshKentico.Core.Services.Configuration.Roles;

namespace PoshKentico.Business.Configuration.Roles
{
    /// <summary>
    /// Business Layer of Set-CMSRole cmdlet.
    /// </summary>
    [Export(typeof(SetCmsRoleBusiness))]
    public class SetCmsRoleBusiness : CmdletBusinessBase
    {
        #region Propreties

        /// <summary>
        /// Gets or sets a reference to the Role Service. Populated by MEF.
        /// </summary>
        [Import]
        public IRoleService RoleService { get; set; }

        #endregion

        #region

        /// <summary>
        /// Sets the role <see cref="IRole"/> in the CMS System.
        /// </summary>
        /// <param name="role">The <see cref="IRole"/> to set.</param>
        /// <returns>The updated role.</returns>
        public IRole Set(IRole role)
        {
            return this.RoleService.SetRole(role);
        }

        /// <summary>
        /// Sets the role <see cref="IRole"/> in the CMS System.
        /// </summary>
        /// <param name="roleDisplayName">The Display Name for the new Role.</param>
        /// <param name="roleName">The Role Name for the new Role.</param>
        /// <param name="siteID">The Site ID for the new Role.</param>
        /// <returns>The newly created <see cref="IRole"/>.</returns>
        public IRole Set(string roleDisplayName, string roleName, int siteID)
        {
            var data = new
            {
                // Sets the role properties
                RoleDisplayName = roleDisplayName,
                RoleName = roleName,
                SiteID = siteID,
            };

            return this.RoleService.SetRole(data.ActLike<IRole>(), false);
        }

        #endregion
    }
}
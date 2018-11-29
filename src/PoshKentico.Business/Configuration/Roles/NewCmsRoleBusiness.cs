// <copyright file="NewCmsRoleBusiness.cs" company="Chris Crutchfield">
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
using ImpromptuInterface;
using PoshKentico.Core.Services.Configuration.Roles;

namespace PoshKentico.Business.Configuration.Roles
{
    /// <summary>
    /// Business Layer of New-CMSRole cmdlet.
    /// </summary>
    [Export(typeof(NewCmsRoleBusiness))]
    public class NewCmsRoleBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the Role Service. Populated by MEF.
        /// </summary>
        [Import]
        public IRoleService RoleService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new <see cref="IRole"/> in the CMS System.
        /// </summary>
        /// <param name="roleDisplayName">The Display Name for the new Role.</param>
        /// <param name="roleName">The Role Name for the new Role.</param>
        /// <param name="siteID">The Site ID for the new Role.</param>
        /// <returns>The newly created <see cref="IRole"/>.</returns>
        public IRole CreateRole(string roleDisplayName, string roleName, int siteID)
        {
            TextInfo txtInfo = new CultureInfo("en-us", false).TextInfo;
            var newRoleName = string.IsNullOrEmpty(roleName) ? txtInfo.ToTitleCase(roleDisplayName).Replace(" ", string.Empty) : roleName;

            var data = new
            {
                // Sets the role properties
                RoleDisplayName = roleDisplayName,
                RoleName = newRoleName,
                SiteID = siteID,
            };

            return this.RoleService.CreateRole(data.ActLike<IRole>());
        }

        #endregion
    }
}
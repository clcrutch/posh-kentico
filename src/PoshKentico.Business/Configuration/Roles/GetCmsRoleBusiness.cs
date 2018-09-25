// <copyright file="GetCmsRoleBusiness.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Configuration.Roles;

namespace PoshKentico.Business.Configuration.Roles
{
    /// <summary>
    /// Business Layer of Get-CMSRole cmdlet.
    /// </summary>
    [Export(typeof(GetCmsRoleBusiness))]
    public class GetCmsRoleBusiness : CmdletBusinessBase
    {
        #region Propreties

        /// <summary>
        /// Gets or sets a reference to the Role Service. Populated by MEF.
        /// </summary>
        [Import]
        public IRoleService RoleService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a list of all of the <see cref="IRole"/> in the CMS System.
        /// </summary>
        /// <returns>A list of all of the <see cref="IRole"/>.</returns>
        public IEnumerable<IRole> GetRoles()
        {
            return this.RoleService.Roles;
        }

        /// <summary>
        /// Gets a list of the <see cref="IRole"/> which match the supplied IDs.
        /// </summary>
        /// <param name="ids">The IDs of the <see cref="IRole"/> to return.</param>
        /// <returns>A list of the <see cref="IRole"/> which match the supplied IDs.</returns>
        public IEnumerable<IRole> GetRoles(params int[] ids)
        {
            var roles = from id in ids select this.RoleService.GetRole(id);

            return (from role in roles
                    where role != null
                    select role).ToArray();
        }

        /// <summary>
        /// Gets a list of all of the <see cref="IRole"/> which match the specified criteria.
        /// </summary>
        /// <param name="roleName">The role name which to match the roles to.</param>
        /// <param name="siteID">The site id which to match the roles to.</param>
        /// <param name="exact">A boolean which indicates if the match should be exact.</param>
        /// <returns>A list of all of the <see cref="IRole"/> which match the specified criteria.</returns>
        public IEnumerable<IRole> GetRoles(string roleName, int siteID, bool exact)
        {
            if (exact)
            {
                var roles = (from c in this.RoleService.Roles
                             where c.RoleName.ToLowerInvariant().Equals(roleName, StringComparison.InvariantCultureIgnoreCase)
                             select c).ToArray();
                return GetRolesFromID(siteID, roles);
            }
            else
            {
                var roles = (from c in this.RoleService.Roles
                             where c.RoleName.ToLowerInvariant().Contains(roleName.ToLowerInvariant())
                             select c).ToArray();

                return GetRolesFromID(siteID, roles);
            }
        }

        private static IEnumerable<IRole> GetRolesFromID(int siteID, IRole[] roles)
        {
            if (siteID == -1)
            {
                return roles;
            }
            else
            {
                return roles.Select(x => x).Where(x => x.SiteID == siteID);
            }
        }

        #endregion

    }
}

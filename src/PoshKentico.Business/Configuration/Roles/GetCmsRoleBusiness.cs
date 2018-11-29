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
using System.Text.RegularExpressions;
using CMS.SiteProvider;
using ImpromptuInterface;
using PoshKentico.Business.Configuration.Sites;
using PoshKentico.Core.Services.Configuration.Roles;
using PoshKentico.Core.Services.Configuration.Sites;
using PoshKentico.Core.Services.Configuration.Users;

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

        /// <summary>
        /// Gets or sets a reference to the Site Business.
        /// </summary>
        [Import]
        public GetCmsSiteBusiness SiteService { get; set; }

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
        /// <param name="siteName">The site name of the site which to match the roles to. If no site match, return roles that not assigne to any site.</param>
        /// <param name="isRegex">A boolean which indicates if the match should be regex.</param>
        /// <returns>A list of all of the <see cref="IRole"/> which match the specified criteria.</returns>
        public IEnumerable<IRole> GetRoles(string roleName, string siteName, bool isRegex)
        {
            List<IRole> roles = new List<IRole>();
            if (!string.IsNullOrEmpty(siteName))
            {
                var sites = this.SiteService.GetSites(siteName, isRegex);
                foreach (var site in sites)
                {
                    roles.AddRange(this.GetRoles(roleName, isRegex, site.SiteID));
                }
            }
            else
            {
                roles.AddRange(this.GetRoles(roleName, isRegex));
            }

            return roles;
        }

        /// <summary>
        /// Gets a list of all of the <see cref="IRole"/> which match the specified criteria.
        /// </summary>
        /// <param name="roleName">The role name which to match the roles to.</param>
        /// <param name="isRegex">A boolean which indicates if the match should be regex.</param>
        /// <param name="siteID">The site id which to match the roles to. If no site match, return roles that not assigne to any site.</param>
        /// <returns>A list of all of the <see cref="IRole"/> which match the specified criteria.</returns>
        public IEnumerable<IRole> GetRoles(string roleName, bool isRegex, int siteID = 0)
        {
            Regex regex = null;

            if (isRegex)
            {
                regex = new Regex(roleName, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            else
            {
                regex = new Regex($"^{roleName.Replace("*", ".*")}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }

            if (siteID != 0)
            {
                var matched = from f in this.RoleService.Roles
                          where regex.IsMatch(f.RoleName) && f.SiteID == siteID
                          select f;

                return matched.ToArray();
            }
            else
            {
                var matched = from f in this.RoleService.Roles
                          where regex.IsMatch(f.RoleName)
                          select f;

                return matched.ToArray();
            }
        }

        /// <summary>
        /// Gets a list of all of the <see cref="IRole"/> from the specified user in the CMS System.
        /// </summary>
        /// <param name="userName">The user to retrive all users from.</param>
        /// <returns>A list of all of the <see cref="IRole"/> that belong to the user.</returns>
        public IEnumerable<IRole> GetRolesFromUser(string userName)
        {
            var user = new
            {
                UserName = userName,
            };

            return this.RoleService.GetRolesFromUser(user.ActLike<IUser>());
        }

        /// <summary>
        /// Gets a list of all of the <see cref="IRole"/> from the specified user in the CMS System.
        /// </summary>
        /// <param name="user">The user to retrive all users from.</param>
        /// <returns>A list of all of the <see cref="IRole"/> that belong to the user.</returns>
        public IEnumerable<IRole> GetRolesFromUser(IUser user)
        {
            return this.RoleService.GetRolesFromUser(user);
        }
        #endregion

    }
}

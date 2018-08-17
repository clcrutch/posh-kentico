// <copyright file="SetCmsRoleNoLogBusiness.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Configuration.Staging;

namespace PoshKentico.Business.Configuration.Staging
{
    /// <summary>
    /// Business Layer for the Set-CMSRoleNoLog cmdlet.
    /// </summary>
    [Export(typeof(SetCmsRoleNoLogBusiness))]
    public class SetCmsRoleNoLogBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the Staging Service.  Populated by MEF.
        /// </summary>
        [Import]
        public IStagingService StagingService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the <see cref="IRole"/> a new role without logging any staging tasks in the CMS System.
        /// </summary>
        /// <param name="role">The <see cref="IRole"/> to sync.</param>
        public void SetNoLogRole(IRole role)
        {
            this.StagingService.SetNoLoggingRole(role);
        }

        /// <summary>
        /// Sets the <see cref="IRole"/> a new role without logging any staging tasks in the CMS System.
        /// </summary>
        /// <param name="displayName">the role display name for the new role</param>
        /// <param name="roleName">The role name for the new role</param>
        /// <param name="roleSiteId">The role site id for the new role</param>
        public void SetNoLogRole(string displayName, string roleName, int roleSiteId)
        {
            var data = new
            {
                RoleName = roleName,
                RoleSiteID = roleSiteId,
                RoleDisplayName = displayName,
            };
            this.StagingService.SetNoLoggingRole(data.ActLike<IRole>());
        }

        #endregion
    }
}

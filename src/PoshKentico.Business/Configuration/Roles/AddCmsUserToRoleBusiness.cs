// <copyright file="AddCmsUserToRoleBusiness.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Configuration.Users;

namespace PoshKentico.Business.Configuration.Roles
{
    /// <summary>
    /// Business Layer for the Add-CMSUserToRole cmdlet.
    /// </summary>
    [Export(typeof(AddCmsUserToRoleBusiness))]
    public class AddCmsUserToRoleBusiness : CmdletBusinessBase
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
        /// Add a user to a role.
        /// </summary>
        /// <param name="userName">The user name of the user to add to the specified role.</param>
        /// <param name="role">The role to add a user to.</param>
        public void AddUserToRole(string userName, IRole role)
        {
            var user = new
            {
                UserName = userName,
            };
            this.RoleService.AddUserToRole(user.ActLike<IUser>(), role);
        }
        #endregion
    }
}

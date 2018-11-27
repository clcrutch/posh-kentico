// <copyright file="RemoveCmsRoleBusiness.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Configuration.Roles;

namespace PoshKentico.Business.Configuration.Roles
{
    /// <summary>
    /// Business Layer for the Remove-CMSRole cmdlet.
    /// </summary>
    [Export(typeof(RemoveCmsRoleBusiness))]
    public class RemoveCmsRoleBusiness : CmdletBusinessBase
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
        /// Removes a the specified role <see cref="IRole"/> in the CMS System.
        /// </summary>
        /// <param name="role">The <see cref="IRole"/> to remove.</param>
        public void RemoveRole(IRole role)
        {
            if (this.OutputService.ShouldProcess(role.RoleName, "Remove the role from Kentico."))
            {
                this.RoleService.DeleteRole(role);
            }
        }

        #endregion
    }
}

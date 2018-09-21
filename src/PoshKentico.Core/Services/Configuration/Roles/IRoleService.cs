// <copyright file="IRoleService.cs" company="Chris Crutchfield">
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

using System.Collections.Generic;

namespace PoshKentico.Core.Services.Configuration.Roles
{
    /// <summary>
    /// Service for providing access to a CMS Roles.
    /// </summary>
    public interface IRoleService
    {
        #region Properties

        /// <summary>
        /// Gets a list of all of the <see cref="IRole"/> provided by the CMS System.
        /// </summary>
        IEnumerable<IRole> Roles { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the role.
        /// </summary>
        /// <param name="role">The <see cref="IRole"/> to create.</param>
        /// <returns>The newly created role.</returns>
        IRole CreateRole(IRole role);

        /// <summary>
        /// Sets the <see cref="IRole"/>.
        /// </summary>
        /// <param name="role">The <see cref="IRole"/> to update to.</param>
        /// <param name="isReplace">To indicate if replace the complete object or update only the properties.</param>
        /// <returns>The updated <see cref="IRole"/>.</returns>
        IRole SetRole(IRole role, bool isReplace = true);
        #endregion
    }
}

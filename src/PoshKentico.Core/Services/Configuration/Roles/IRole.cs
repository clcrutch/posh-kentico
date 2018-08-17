// <copyright file="IRole.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Core.Services.Configuration.Roles
{
    /// <summary>
    /// Represents a Role Object.
    /// </summary>
    public interface IRole
    {
        #region Properties

        /// <summary>
        /// Gets the display name for the role.
        /// </summary>
        string RoleDisplayName { get; }

        /// <summary>
        /// Gets the role name.
        /// </summary>
        string RoleName { get; }

        /// <summary>
        /// Gets the site id
        /// </summary>
        int SiteID { get; }
        #endregion
    }
}

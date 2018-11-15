// <copyright file="IUser.cs" company="Chris Crutchfield">
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

using CMS.Base;

namespace PoshKentico.Core.Services.Configuration.Users
{
    /// <summary>
    /// Represents a User Object.
    /// </summary>
    public interface IUser
    {
        #region Properties

        /// <summary>
        /// Gets the full name for the user.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Gets the user name for the user.
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Gets the email for the user.
        /// </summary>
        string Email { get; }

        /// <summary>
        /// Gets the preferred culture code for the user.
        /// </summary>
        string PreferredCultureCode { get; }

        /// <summary>
        /// Gets the site independent privilege level for the user.
        /// </summary>
        UserPrivilegeLevelEnum SiteIndependentPrivilegeLevel { get; }

        #endregion
    }
}

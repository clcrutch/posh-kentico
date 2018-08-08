// <copyright file="ISite.cs" company="Chris Crutchfield">
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

using CMS.SiteProvider;

namespace PoshKentico.Core.Services.Configuration.Sites
{
    /// <summary>
    /// Represents a Site Object.
    /// </summary>
    public interface ISite
    {
        #region Properties

        /// <summary>
        /// Gets the display name for the site.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Gets the site name.
        /// </summary>
        string SiteName { get; }

        /// <summary>
        /// Gets the site status.
        /// </summary>
        SiteStatusEnum Status { get; }

        /// <summary>
        /// Gets the domain name for the site.
        /// </summary>
        string DomainName { get; }

        #endregion

    }
}

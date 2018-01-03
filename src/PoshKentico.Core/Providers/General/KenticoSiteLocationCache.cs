// <copyright file="KenticoSiteLocationCache.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Core.Providers.General
{
    /// <summary>
    /// Model used for caching the Kentico site location.
    /// </summary>
    internal class KenticoSiteLocationCache
    {
        #region Properties

        /// <summary>
        /// Gets or sets the file path for the Kentico site.
        /// </summary>
        public string SiteLocation { get; set; }

        /// <summary>
        /// Gets or sets the connection string for the Kentico database.
        /// </summary>
        public string ConnectionString { get; set; }

        #endregion

    }
}

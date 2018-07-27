// <copyright file="ISiteService.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Core.Services.Configuration
{
    /// <summary>
    /// Service for providing access to a CMS sites.
    /// </summary>
    public interface ISiteService
    {
        #region Properties

        /// <summary>
        /// Gets a list of all of the <see cref="ISite"/> provided by the CMS System.
        /// </summary>
        IEnumerable<ISite> Sites { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the <see cref="ISite"/>.
        /// </summary>
        /// <param name="site">The <see cref="ISite"/> to create.</param>
        /// <returns>The newly created <see cref="ISite"/>.</returns>
        ISite Create(ISite site);

        /// <summary>
        /// Deletes the specified <see cref="ISite"/>.
        /// </summary>
        /// <param name="site">The <see cref="ISite"/> to delete.</param>
        void Delete(ISite site);

        /// <summary>
        /// Gets the <see cref="ISite"/> which matches the supplied ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="ISite"/> to return.</param>
        /// <returns>The <see cref="ISite"/> which matches the ID, else null.</returns>
        ISite GetSite(int id);

        /// <summary>
        /// Updates the <see cref="ISite"/>.
        /// </summary>
        /// <param name="site">The <see cref="ISite"/> to update.</param>
        void Update(ISite site);

        /// <summary>
        /// Starts the specified <see cref="ISite"/>.
        /// </summary>
        /// <param name="site">The <see cref="ISite"/> to start.</param>
        void Start(ISite site);

        /// <summary>
        /// Stops the specified <see cref="ISite"/>.
        /// </summary>
        /// <param name="site">The <see cref="ISite"/> to stop.</param>
        void Stop(ISite site);

        /// <summary>
        /// Assigns the culture to the site
        /// </summary>
        /// <param name="site">the <see cref="ISite"/> to assign a culture to. </param>
        /// <param name="cultureName">the culture name </param>
        void AddSiteCulture(ISite site, string cultureName);

        /// <summary>
        /// Removes the culture from the site
        /// </summary>
        /// <param name="site">the <see cref="ISite"/> to remove a culture from. </param>
        /// <param name="cultureName">the culture name </param>
        void RemoveSiteCulture(ISite site, string cultureName);
        #endregion
    }
}

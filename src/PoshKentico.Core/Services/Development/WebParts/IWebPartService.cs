// <copyright file="IWebPartService.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Core.Services.Development.WebParts
{
    /// <summary>
    /// Service for providing access to a CMS webparts.
    /// </summary>
    public interface IWebPartService
    {
        #region Properties

        /// <summary>
        /// Gets a list of all of the <see cref="IWebPart"/> provided by the CMS System.
        /// </summary>
        IEnumerable<IWebPart> WebParts { get; }

        /// <summary>
        /// Gets a list of all of the <see cref="IWebPartCategory"/> provided by the CMS System.
        /// </summary>
        IEnumerable<IWebPartCategory> WebPartCategories { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the <see cref="IWebPartCategory"/>.
        /// </summary>
        /// <param name="webPartCategory">The <see cref="IWebPartCategory"/> to create.</param>
        /// <returns>The newly created <see cref="IWebPartCategory"/>.</returns>
        IWebPartCategory Create(IWebPartCategory webPartCategory);

        /// <summary>
        /// Deletes the specified <see cref="IWebPartCategory"/>.
        /// </summary>
        /// <param name="webPartCategory">The <see cref="IWebPartCategory"/> to delete.</param>
        void Delete(IWebPartCategory webPartCategory);

        /// <summary>
        /// Gets a list of <see cref="IWebPartCategory"/> that have <paramref name="parentWebPartCategory"/> as the parent.
        /// </summary>
        /// <param name="parentWebPartCategory">The parent <see cref="IWebPartCategory"/> to the desired web part categories.</param>
        /// <returns>A list of <see cref="IWebPartCategory"/> that have <paramref name="parentWebPartCategory"/> as the parent.</returns>
        IEnumerable<IWebPartCategory> GetWebPartCategories(IWebPartCategory parentWebPartCategory);

        /// <summary>
        /// Gets the <see cref="IWebPartCategory"/> which matches the supplied ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="IWebPartCategory"/> to return.</param>
        /// <returns>The <see cref="IWebPartCategory"/> which matches the ID, else null.</returns>
        IWebPartCategory GetWebPartCategory(int id);

        /// <summary>
        /// Gets a list of <see cref="IWebPart"/> which have <paramref name="webPartCategory"/> as their parent category.
        /// </summary>
        /// <param name="webPartCategory">The <see cref="IWebPartCategory"/> which is the category for the desired list of <see cref="IWebPartCategory"/>.</param>
        /// <returns>A list of <see cref="IWebPart"/> which are related to <paramref name="webPartCategory"/>.</returns>
        IEnumerable<IWebPart> GetWebParts(IWebPartCategory webPartCategory);

        /// <summary>
        /// Updates the <see cref="IWebPartCategory"/>.
        /// </summary>
        /// <param name="webPartCategory">The <see cref="IWebPartCategory"/> to update.</param>
        void Update(IWebPartCategory webPartCategory);

        #endregion

    }
}

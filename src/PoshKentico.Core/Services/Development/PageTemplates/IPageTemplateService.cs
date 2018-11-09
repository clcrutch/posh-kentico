// <copyright file="IPageTemplateService.cs" company="Chris Crutchfield">
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

using System;
using System.Collections.Generic;

namespace PoshKentico.Core.Services.Development.PageTemplates
{
    /// <summary>
    /// Service for providing access to a CMS pagetemplates.
    /// </summary>
    public interface IPageTemplateService
    {
        #region Properties

        /// <summary>
        /// Gets a list of all of the <see cref="IPageTemplate"/> provided by the CMS System.
        /// </summary>
        IEnumerable<IPageTemplate> PageTemplates { get; }

        /// <summary>
        /// Gets a list of all of the <see cref="IPageTemplateCategory"/> provided by the CMS System.
        /// </summary>
        IEnumerable<IPageTemplateCategory> PageTemplateCategories { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a field to a <see cref="IPageTemplate"/>.
        /// </summary>
        /// <param name="field">The <see cref="IPageTemplateField"/> to add.</param>
        /// <param name="pageTemplate">The <see cref="IPageTemplate"/> to attach the field to.</param>
        /// <returns>The newly created <see cref="IPageTemplateField"/>.</returns>
        IPageTemplateField AddField(IPageTemplateField field, IPageTemplate pageTemplate);

        /// <summary>
        /// Creates the <see cref="IPageTemplateCategory"/>.
        /// </summary>
        /// <param name="pageTemplateCategory">The <see cref="IPageTemplateCategory"/> to create.</param>
        /// <returns>The newly created <see cref="IPageTemplateCategory"/>.</returns>
        IPageTemplateCategory Create(IPageTemplateCategory pageTemplateCategory);

        /// <summary>
        /// Creates the <see cref="IPageTemplate"/>.
        /// </summary>
        /// <param name="pageTemplate">The <see cref="IPageTemplate"/> to create.</param>
        /// <returns>The newly created <see cref="IPageTemplate"/>.</returns>
        IPageTemplate Create(IPageTemplate pageTemplate);

        /// <summary>
        /// Deletes the specified <see cref="IPageTemplateCategory"/>.
        /// </summary>
        /// <param name="pageTemplateCategory">The <see cref="IPageTemplateCategory"/> to delete.</param>
        void Delete(IPageTemplateCategory pageTemplateCategory);

        /// <summary>
        /// Deletes the specified <see cref="IPageTemplate"/>.
        /// </summary>
        /// <param name="pageTemplate">The <see cref="IPageTemplate"/> to delete.</param>
        void Delete(IPageTemplate pageTemplate);

        /// <summary>
        /// Gets a list of <see cref="IPageTemplateCategory"/> that have <paramref name="parentPageTemplateCategory"/> as the parent.
        /// </summary>
        /// <param name="parentPageTemplateCategory">The parent <see cref="IPageTemplateCategory"/> to the desired page template categories.</param>
        /// <returns>A list of <see cref="IPageTemplateCategory"/> that have <paramref name="parentPageTemplateCategory"/> as the parent.</returns>
        IEnumerable<IPageTemplateCategory> GetPageTemplateCategories(IPageTemplateCategory parentPageTemplateCategory);

        /// <summary>
        /// Gets the <see cref="IPageTemplateCategory"/> which matches the supplied ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="IPageTemplateCategory"/> to return.</param>
        /// <returns>The <see cref="IPageTemplateCategory"/> which matches the ID, else null.</returns>
        IPageTemplateCategory GetPageTemplateCategory(int id);

        /// <summary>
        /// Gets the <see cref="IPageTemplateField"/> associated with the supplied <see cref="IPageTemplate"/>.
        /// </summary>
        /// <param name="pageTemplate">The <see cref="IPageTemplate"/> to get the list of fields for.</param>
        /// <returns>A list of the <see cref="IPageTemplateField"/> that is associated with the supplied <see cref="IPageTemplate"/>.</returns>
        IEnumerable<IPageTemplateField> GetPageTemplateFields(IPageTemplate pageTemplate);

        /// <summary>
        /// Gets a list of <see cref="IPageTemplate"/> which have <paramref name="pageTemplateCategory"/> as their parent category.
        /// </summary>
        /// <param name="pageTemplateCategory">The <see cref="IPageTemplateCategory"/> which is the category for the desired list of <see cref="IPageTemplateCategory"/>.</param>
        /// <returns>A list of <see cref="IPageTemplate"/> which are related to <paramref name="pageTemplateCategory"/>.</returns>
        IEnumerable<IPageTemplate> GetPageTemplates(IPageTemplateCategory pageTemplateCategory);

        /// <summary>
        /// Removes a <see cref="IPageTemplateField"/> from a <see cref="IPageTemplate"/>.
        /// </summary>
        /// <param name="field">The <see cref="IPageTemplateField"/> to remove.</param>
        void RemoveField(IPageTemplateField field);

        /// <summary>
        /// Updates the <see cref="IPageTemplateCategory"/>.
        /// </summary>
        /// <param name="pageTemplateCategory">The <see cref="IPageTemplateCategory"/> to update.</param>
        void Update(IPageTemplateCategory pageTemplateCategory);

        /// <summary>
        /// Updates the <see cref="IPageTemplate"/>.
        /// </summary>
        /// <param name="pageTemplate">The <see cref="IPageTemplate"/> to update.</param>
        void Update(IPageTemplate pageTemplate);

        /// <summary>
        /// Updates the <see cref="IPageTemplateField"/>.
        /// </summary>
        /// <param name="field">The <see cref="IPageTemplateField"/> to update.</param>
        void Update(IPageTemplateField field);

        #endregion

    }
}

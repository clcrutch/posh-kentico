// <copyright file="KenticoWebPartService.cs" company="Chris Crutchfield">
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
using System.ComponentModel.Composition;
using System.Linq;
using CMS.PortalEngine;
using ImpromptuInterface;
using PoshKentico.Core.Services.Development.WebParts;

namespace PoshKentico.Core.Providers.Development.WebParts
{
    /// <summary>
    /// Implementation of <see cref="IWebPartService"/> that uses Kentico.
    /// </summary>
    [Export(typeof(IWebPartService))]
    public class KenticoWebPartService : IWebPartService
    {
        #region Properties

        /// <summary>
        /// Gets a list of all of the <see cref="IWebPart"/> provided by Kentico.
        /// </summary>
        public IEnumerable<IWebPart> WebParts => (from wp in WebPartInfoProvider.GetWebParts()
                                                  select Impromptu.ActLike<IWebPart>(wp as WebPartInfo)).ToArray();

        /// <summary>
        /// Gets a list of all of the <see cref="IWebPartCategory"/> provided by Kentico.
        /// </summary>
        public IEnumerable<IWebPartCategory> WebPartCategories => (from c in WebPartCategoryInfoProvider.GetCategories()
                                                                   select Impromptu.ActLike<IWebPartCategory>(c as WebPartCategoryInfo)).ToArray();

        #endregion

        #region Methods

        /// <summary>
        /// Creates the <see cref="IWebPartCategory"/>.
        /// </summary>
        /// <param name="webPartCategory">The <see cref="IWebPartCategory"/> to create.</param>
        /// <returns>The newly created <see cref="IWebPartCategory"/>.</returns>
        public IWebPartCategory Create(IWebPartCategory webPartCategory)
        {
            var category = new WebPartCategoryInfo
            {
                CategoryDisplayName = webPartCategory.CategoryDisplayName,
                CategoryName = webPartCategory.CategoryName,
                CategoryImagePath = webPartCategory.CategoryImagePath,
                CategoryParentID = webPartCategory.CategoryParentID,
            };

            WebPartCategoryInfoProvider.SetWebPartCategoryInfo(category);

            return category.ActLike<IWebPartCategory>();
        }

        /// <summary>
        /// Deletes the specified <see cref="IWebPartCategory"/>.
        /// </summary>
        /// <param name="webPartCategory">The <see cref="IWebPartCategory"/> to delete.</param>
        public void Delete(IWebPartCategory webPartCategory)
        {
            WebPartCategoryInfoProvider.DeleteCategoryInfo(webPartCategory.CategoryID);
        }

        /// <summary>
        /// Gets the <see cref="IWebPartCategory"/> which matches the supplied ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="IWebPartCategory"/> to return.</param>
        /// <returns>The <see cref="IWebPartCategory"/> which matches the ID, else null.</returns>
        public IWebPartCategory GetWebPartCategory(int id)
        {
            return (WebPartCategoryInfoProvider.GetWebPartCategoryInfoById(id) as WebPartCategoryInfo)?.ActLike<IWebPartCategory>();
        }

        /// <summary>
        /// Updates the <see cref="IWebPartCategory"/>.
        /// </summary>
        /// <param name="webPartCategory">The <see cref="IWebPartCategory"/> to update.</param>
        public void Update(IWebPartCategory webPartCategory)
        {
            var category = WebPartCategoryInfoProvider.GetWebPartCategoryInfoById(webPartCategory.CategoryID);

            if (category == null)
            {
                return;
            }

            category.CategoryDisplayName = webPartCategory.CategoryDisplayName;
            category.CategoryName = webPartCategory.CategoryName;
            category.CategoryImagePath = webPartCategory.CategoryImagePath;
            category.CategoryParentID = webPartCategory.CategoryParentID;

            WebPartCategoryInfoProvider.SetWebPartCategoryInfo(category);
        }

        #endregion

    }
}

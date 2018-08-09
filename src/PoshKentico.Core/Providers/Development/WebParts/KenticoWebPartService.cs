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

using System;
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

        /// <inheritdoc />
        public IEnumerable<IWebPart> WebParts => (from wp in WebPartInfoProvider.GetWebParts()
                                                  select Impromptu.ActLike<IWebPart>(wp as WebPartInfo)).ToArray();

        /// <inheritdoc />
        public IEnumerable<IWebPartCategory> WebPartCategories => (from c in WebPartCategoryInfoProvider.GetCategories()
                                                                   select Impromptu.ActLike<IWebPartCategory>(c as WebPartCategoryInfo)).ToArray();

        #endregion

        #region Methods

        /// <inheritdoc />
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

        /// <inheritdoc />
        public void Delete(IWebPartCategory webPartCategory) =>
            WebPartCategoryInfoProvider.DeleteCategoryInfo(webPartCategory.CategoryID);

        /// <inheritdoc />
        public IEnumerable<IWebPartCategory> GetWebPartCategories(IWebPartCategory parentWebPartCategory) =>
            (from c in this.WebPartCategories
             where c.CategoryParentID == parentWebPartCategory.CategoryID
             select c).ToArray();

        /// <inheritdoc />
        public IWebPartCategory GetWebPartCategory(int id) =>
            (WebPartCategoryInfoProvider.GetWebPartCategoryInfoById(id) as WebPartCategoryInfo)?.ActLike<IWebPartCategory>();

        /// <inheritdoc />
        public IEnumerable<IWebPart> GetWebParts(IWebPartCategory webPartCategory) =>
            (from wp in this.WebParts
             where wp.WebPartCategoryID == webPartCategory.CategoryID
             select wp).ToArray();

        /// <inheritdoc />
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

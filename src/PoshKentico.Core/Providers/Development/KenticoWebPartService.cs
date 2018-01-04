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
using PoshKentico.Core.Services.Development;

namespace PoshKentico.Core.Providers.Development
{
    /// <summary>
    /// Implementation of <see cref="IWebPartService"/> that uses Kentico.
    /// </summary>
    [Export(typeof(IWebPartService))]
    public class KenticoWebPartService : IWebPartService
    {
        #region Properties

        /// <summary>
        /// Gets a list of all of the <see cref="IWebPartCategory"/> provided by Kentico.
        /// </summary>
        public IEnumerable<IWebPartCategory> WebPartCategories => (from c in WebPartCategoryInfoProvider.GetCategories()
                                                                   select Impromptu.ActLike<IWebPartCategory>(c as WebPartCategoryInfo)).ToArray();

        #endregion

        #region Methods

        public IEnumerable<IWebPartCategory> GetWebPartCategories(params int[] ids)
        {
            var webPartCategories = from id in ids
                                    select WebPartCategoryInfoProvider.GetWebPartCategoryInfoById(id);

            return (from wpc in webPartCategories
                    where wpc != null
                    select Impromptu.ActLike<IWebPartCategory>(wpc as WebPartCategoryInfo)).ToArray();
        }

        public IWebPartCategory Save(IWebPartCategory webPartCategory)
        {
            var category = WebPartCategoryInfoProvider.GetWebPartCategoryInfoById(webPartCategory.CategoryID);

            if (category == null)
            {
                category = new WebPartCategoryInfo()
                {
                    CategoryDisplayName = webPartCategory.CategoryDisplayName,
                    CategoryName = webPartCategory.CategoryName,
                    CategoryImagePath = webPartCategory.CategoryImagePath,
                    CategoryParentID = webPartCategory.CategoryParentID,
                };
            }
            else
            {
                category.CategoryDisplayName = webPartCategory.CategoryDisplayName;
                category.CategoryName = webPartCategory.CategoryName;
                category.CategoryImagePath = webPartCategory.CategoryImagePath;
                category.CategoryParentID = webPartCategory.CategoryParentID;
            }

            WebPartCategoryInfoProvider.SetWebPartCategoryInfo(category);

            return category.ActLike<IWebPartCategory>();
        }

        #endregion

    }
}

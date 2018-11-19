// <copyright file="NewCMSPageTemplateCategoryBusiness.cs" company="Chris Crutchfield">
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
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using PoshKentico.Core.Services.Development.PageTemplates;

namespace PoshKentico.Business.Development.PageTemplates
{
    /// <summary>
    /// Business layer for the New-CMSPageTemplateCategory cmdlet.
    /// </summary>
    [Export(typeof(NewCMSPageTemplateCategoryBusiness))]
    public class NewCMSPageTemplateCategoryBusiness : PageTemplateBusinessBase
    {
        #region Methods

        /// <summary>
        /// Creates a new PageTemplateCategory in the CMS System.
        /// </summary>
        /// <param name="path">The path for the new PageTemplateCategory.</param>
        /// <param name="displayName">The display name for the PageTemplateCategory.</param>
        /// <param name="imagePath">The image path for the new PageTemplateCategory.</param>
        /// <returns>The newly created PageTemplateCategory.</returns>
        public IPageTemplateCategory CreatePageTemplateCategory(string path, string displayName, string imagePath)
        {
            var name = path.Substring(path.LastIndexOf('/') + 1);
            var basePath = path.Substring(0, path.LastIndexOf('/'));

            if (string.IsNullOrWhiteSpace(basePath))
            {
                basePath = "/";
            }

            var parent = this.GetCategoryFromPath(basePath);

            if (string.IsNullOrEmpty(displayName))
            {
                displayName = name;
            }

            var data = new PageTemplateCategory
            {
                CategoryName = name,
                CategoryPath = path,
                DisplayName = displayName,
                CategoryImagePath = imagePath,
                ParentId = parent.CategoryId,
                CategoryId = -1,
            };

            return this.PageTemplateService.Create(data);
        }

        #endregion

        #region Classes

        [ExcludeFromCodeCoverage]
        private class PageTemplateCategory : IPageTemplateCategory
        {
            public string CodeName { get; set; }

            public string DisplayName { get; set; }

            public int CategoryId { get; set; }

            public Guid CategoryGUID { get; set; }

            public string CategoryImagePath { get; set; }

            public string CategoryName { get; set; }

            public int ParentId { get; set; }

            public string CategoryPath { get; set; }
        }

        #endregion

    }
}

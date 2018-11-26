// <copyright file="NewCMSPageTemplateBusiness.cs" company="Chris Crutchfield">
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
using System.Diagnostics.CodeAnalysis;
using PoshKentico.Core.Services.Development.PageTemplates;
using PoshKentico.Core.Services.Development.WebParts;

namespace PoshKentico.Business.Development.PageTemplates
{
    /// <summary>
    /// Business layer fo the New-CMSPageTemplate cmdlet.
    /// </summary>
    [Export(typeof(NewCMSPageTemplateBusiness))]
    public class NewCMSPageTemplateBusiness : PageTemplateBusinessBase
    {
        #region Methods

        /// <summary>
        /// Creates a <see cref="IPageTemplate"/> at the specified path.
        /// </summary>
        /// <param name="path">The path to create the <see cref="IPageTemplate"/> at.</param>
        /// <param name="fileName">The file name for the underlying class file.</param>
        /// <param name="displayName">The display name for the <see cref="IPageTemplate"/>.</param>
        /// <param name="layout">Page template layout.</param>
        /// <param name="iconClass">Page template icon class defining the page template thumbnail.</param>
        /// <param name="css">Page template CSS.</param>
        /// <param name="isReusable">Gets or sets flag whether page template is reusable.</param>
        /// <returns>The newly created <see cref="IPageTemplate"/>.</returns>
        public IPageTemplate CreatePageTemplate(string path, string fileName, string displayName, string layout, string iconClass, string css, bool isReusable)
        {
            var name = path.Substring(path.LastIndexOf('/') + 1);
            var basePath = path.Substring(0, path.LastIndexOf('/'));

            if (string.IsNullOrWhiteSpace(basePath))
            {
                basePath = "/";
            }

            var parent = this.GetCategoryFromPath(basePath);

            return this.CreatePageTemplate(name, fileName, displayName, layout, iconClass, css, isReusable, parent);
        }

        /// <summary>
        /// Creates a <see cref="IPageTemplate"/> with the specified name under the specified <see cref="IPageTemplateCategory"/>.
        /// </summary>
        /// <param name="name">The name for the <see cref="IPageTemplate"/>.</param>
        /// <param name="fileName">The file name for the underlying class file.</param>
        /// <param name="displayName">The display name for the <see cref="IPageTemplate"/>.</param>
        /// <param name="layout">Page template layout.</param>
        /// <param name="iconClass">Page template icon class defining the page template thumbnail.</param>
        /// <param name="css">Page template CSS.</param>
        /// <param name="isReusable">Gets or sets flag whether page template is reusable.</param>
        /// <param name="pageTemplateCategory">The <see cref="IPageTemplateCategory"/> to create the <see cref="IPageTemplate"/> under.</param>
        /// <returns>The newly created <see cref="IPageTemplate"/>.</returns>
        public virtual IPageTemplate CreatePageTemplate(string name, string fileName, string displayName, string layout, string iconClass, string css, bool isReusable, IPageTemplateCategory pageTemplateCategory)
        {
            if (string.IsNullOrEmpty(displayName))
            {
                displayName = name;
            }

            var data = new PageTemplate
            {
                DisplayName = displayName,
                FileName = fileName,
                CodeName = name,
                PageTemplateLayout = layout,
                PageTemplateIconClass = iconClass,
                PageTemplateCSS = css,
                IsReusable = isReusable,

                CategoryID = pageTemplateCategory.CategoryId,
            };

            return this.PageTemplateService.Create(data);
        }

        #endregion

        #region Classes

        [ExcludeFromCodeCoverage]
        private class PageTemplate : IPageTemplate
        {
            public int CategoryID { get; set; }

            public int PageTemplateSiteID { get; set; }

            public int PageTemplateId { get; set; }

            public string DisplayName { get; set; }

            public string FileName { get; set; }

            public string Description { get; set; }

            public string PageTemplates { get; set; }

            public string CodeName { get; set; }

            public string PageTemplateProperties { get; set; }

            public string PageTemplateLayout { get; set; }

            public string PageTemplateIconClass { get; set; }

            public string PageTemplateCSS { get; set; }

            public bool IsReusable { get; set; }

            public List<IWebPartZoneInstance> WebPartZones { get; set; }
        }

        #endregion

    }
}

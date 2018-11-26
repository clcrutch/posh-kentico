// <copyright file="RemoveCMSPageTemplateCategoryBusiness.cs" company="Chris Crutchfield">
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
using System.Linq;
using PoshKentico.Core.Services.Development.PageTemplates;

namespace PoshKentico.Business.Development.PageTemplates
{
    /// <summary>
    /// Business layer for the Remove-CMSPageTemplateCategory cmdlet.
    /// </summary>
    [Export(typeof(RemoveCMSPageTemplateCategoryBusiness))]
    public class RemoveCMSPageTemplateCategoryBusiness : PageTemplateBusinessBase
    {
        #region Methods

        /// <summary>
        /// Deletes the specified <see cref="IPageTemplateCategory"/>.  Throws exceptions if there are children.
        /// </summary>
        /// <param name="pageTemplateCategory">The PageTemplate category to delete.</param>
        /// <param name="recurse">Indicates whether PageTemplate categories and page templates should be removed recursively.</param>
        public void RemovePageTemplateCategory(IPageTemplateCategory pageTemplateCategory, bool recurse)
        {
            var pageTemplates = this.PageTemplateService.GetPageTemplates(pageTemplateCategory);
            if (pageTemplates.Any())
            {
                if (recurse)
                {
                    foreach (var pageTemplate in pageTemplates)
                    {
                        if (this.OutputService.ShouldProcess(pageTemplate.DisplayName, "Remove the page template from Kentico."))
                        {
                            this.PageTemplateService.Delete(pageTemplate);
                        }
                    }
                }
                else
                {
                    throw new Exception($"Web Part Category {pageTemplateCategory.DisplayName} has Web Parts associated.  Failed to delete.");
                }
            }

            var pageTemplateCategories = this.PageTemplateService.GetPageTemplateCategories(pageTemplateCategory);
            if (pageTemplateCategories.Any())
            {
                if (recurse)
                {
                    foreach (var category in pageTemplateCategories)
                    {
                        this.RemovePageTemplateCategory(category, recurse);
                    }
                }
                else
                {
                    throw new Exception($"Web Part Category {pageTemplateCategory.DisplayName} has Web Parts Categories associated.  Failed to delete.");
                }
            }

            if (this.OutputService.ShouldProcess(pageTemplateCategory.DisplayName, "Remove the page template category from Kentico."))
            {
                this.PageTemplateService.Delete(pageTemplateCategory);
            }
        }

        #endregion

    }
}

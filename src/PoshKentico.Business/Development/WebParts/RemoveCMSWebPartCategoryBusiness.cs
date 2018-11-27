// <copyright file="RemoveCMSWebPartCategoryBusiness.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Development.WebParts;

namespace PoshKentico.Business.Development.WebParts
{
    /// <summary>
    /// Business layer for the Remove-CMSWebPartCategory cmdlet.
    /// </summary>
    [Export(typeof(RemoveCMSWebPartCategoryBusiness))]
    public class RemoveCMSWebPartCategoryBusiness : WebPartBusinessBase
    {
        #region Methods

        /// <summary>
        /// Deletes the specified <see cref="IWebPartCategory"/>.  Throws exceptions if there are children.
        /// </summary>
        /// <param name="webPartCategory">The webpart category to delete.</param>
        /// <param name="recurse">Indicates whether webpart categories and web parts should be removed recursively.</param>
        public void RemoveWebPartCategory(IWebPartCategory webPartCategory, bool recurse)
        {
            var webParts = this.WebPartService.GetWebParts(webPartCategory);
            if (webParts.Any())
            {
                if (recurse)
                {
                    foreach (var webpart in webParts)
                    {
                        if (this.OutputService.ShouldProcess(webpart.WebPartDisplayName, "Remove the web part from Kentico."))
                        {
                            this.WebPartService.Delete(webpart);
                        }
                    }
                }
                else
                {
                    throw new Exception($"Web Part Category {webPartCategory.CategoryDisplayName} has Web Parts associated.  Failed to delete.");
                }
            }

            var webPartCategories = this.WebPartService.GetWebPartCategories(webPartCategory);
            if (webPartCategories.Any())
            {
                if (recurse)
                {
                    foreach (var category in webPartCategories)
                    {
                        this.RemoveWebPartCategory(category, recurse);
                    }
                }
                else
                {
                    throw new Exception($"Web Part Category {webPartCategory.CategoryDisplayName} has Web Parts Categories associated.  Failed to delete.");
                }
            }

            if (this.OutputService.ShouldProcess(webPartCategory.CategoryDisplayName, "Remove the web part category from Kentico."))
            {
                this.WebPartService.Delete(webPartCategory);
            }
        }

        #endregion

    }
}

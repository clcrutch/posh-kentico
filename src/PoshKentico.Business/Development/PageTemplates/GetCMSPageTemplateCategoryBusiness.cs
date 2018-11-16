// <copyright file="GetCMSPageTemplateCategoryBusiness.cs" company="Chris Crutchfield">
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
using System.Text.RegularExpressions;
using PoshKentico.Core.Services.Development.PageTemplates;

namespace PoshKentico.Business.Development.PageTemplates
{
    /// <summary>
    /// Business layer for the Get-CMSPageTemplateCategory cmdlet.
    /// </summary>
    [Export(typeof(GetCMSPageTemplateCategoryBusiness))]
    public class GetCMSPageTemplateCategoryBusiness : PageTemplateBusinessBase
    {
        #region Methods

        /// <summary>
        /// Gets a list of all of the <see cref="IPageTemplateCategory"/>.
        /// </summary>
        /// <returns>A list of all of the <see cref="IPageTemplateCategory"/>.</returns>
        public IEnumerable<IPageTemplateCategory> GetPageTemplateCategories() => this.PageTemplateService.PageTemplateCategories;

        /// <summary>
        /// Gets a list of all of the <see cref="IPageTemplateCategory"/> which match the specified criteria.
        /// </summary>
        /// <param name="matchString">The string which to match the pagetemplate categories to.</param>
        /// <param name="isRegex">Indicates whether <paramref name="matchString"/> is a regular expression.</param>
        /// <param name="recurse">Indicates whether pagetemplate categories should be returned recursively.</param>
        /// <returns>A list of all of the <see cref="IPageTemplateCategory"/> which match the specified criteria.</returns>
        public virtual IEnumerable<IPageTemplateCategory> GetPageTemplateCategories(string matchString, bool isRegex, bool recurse)
        {
            Regex regex = null;

            if (isRegex)
            {
                regex = new Regex(matchString, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            else
            {
                regex = new Regex($"^{matchString.Replace("*", ".*")}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }

            var matched = from c in this.PageTemplateService.PageTemplateCategories
                          where regex.IsMatch(c.CategoryName) ||
                              regex.IsMatch(c.DisplayName)
                          select c;

            if (recurse)
            {
                return this.GetRecursePageTemplateCategories(matched);
            }
            else
            {
                return matched.ToArray();
            }
        }

        /// <summary>
        /// Gets a list of the <see cref="IPageTemplateCategory"/> which are children of the <paramref name="parentPageTemplateCategory"/>.
        /// </summary>
        /// <param name="parentPageTemplateCategory">The <see cref="IPageTemplateCategory"/> which is parent to the categories to find.</param>
        /// <param name="recurse">Indicates whether pagetemplate categories should be returned recursively.</param>
        /// <returns>A list of the <see cref="IPageTemplateCategory"/> which are children to the supplied <paramref name="parentPageTemplateCategory"/>.</returns>
        public IEnumerable<IPageTemplateCategory> GetPageTemplateCategories(IPageTemplateCategory parentPageTemplateCategory, bool recurse)
        {
            var categories = this.PageTemplateService.GetPageTemplateCategories(parentPageTemplateCategory);

            if (recurse)
            {
                return this.GetRecursePageTemplateCategories(categories);
            }
            else
            {
                return categories;
            }
        }

        /// <summary>
        /// Gets a list of the <see cref="IPageTemplateCategory"/> which match the supplied IDs.
        /// </summary>
        /// <param name="ids">The IDs of the <see cref="IPageTemplateCategory"/> to return.</param>
        /// <param name="recurse">Indicates whether pageTemplate categories should be returned recursively.</param>
        /// <returns>A list of the <see cref="IPageTemplateCategory"/> which match the supplied IDs.</returns>
        public IEnumerable<IPageTemplateCategory> GetPageTemplateCategories(int[] ids, bool recurse)
        {
            var pageTemplateCategories = from id in ids
                                    select this.PageTemplateService.GetPageTemplateCategory(id);

            var nonNullCategories = from wpc in pageTemplateCategories
                                    where wpc != null
                                    select wpc;

            if (recurse)
            {
                return this.GetRecursePageTemplateCategories(nonNullCategories);
            }
            else
            {
                return nonNullCategories.ToArray();
            }
        }

        /// <summary>
        /// Gets a list of page template categories by path.
        /// </summary>
        /// <param name="path">The path to get the list of page template categories.</param>
        /// <param name="recurse">Indicates if the page template category children should be returned as well.</param>
        /// <returns>A list of all of the page template categories found at the specified path.</returns>
        public IEnumerable<IPageTemplateCategory> GetPageTemplateCategories(string path, bool recurse)
        {
            var categories = from c in this.PageTemplateService.PageTemplateCategories
                             where c.CategoryPath.Equals(path, StringComparison.InvariantCultureIgnoreCase)
                             select c;

            if (recurse)
            {
                return this.GetRecursePageTemplateCategories(categories);
            }
            else
            {
                return categories.ToArray();
            }
        }

        /// <summary>
        /// Gets the page template category for the current page template.
        /// </summary>
        /// <param name="pageTemplate">The pageTemplate to get the category for.</param>
        /// <returns>The page template category.</returns>
        public IPageTemplateCategory GetPageTemplateCategory(IPageTemplate pageTemplate) =>
            (from c in this.PageTemplateService.PageTemplateCategories
             where c.CategoryId == pageTemplate.CategoryID
             select c).SingleOrDefault();

        private IEnumerable<IPageTemplateCategory> GetRecursePageTemplateCategories(IEnumerable<IPageTemplateCategory> pageTemplateCategories)
        {
            return pageTemplateCategories
                .Select(wp => this.GetPageTemplateCategories(wp, true))
                .SelectMany(c => c)
                .Concat(pageTemplateCategories)
                .ToArray();
        }

        #endregion

    }
}

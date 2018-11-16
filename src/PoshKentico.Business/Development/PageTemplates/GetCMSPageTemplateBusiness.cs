// <copyright file="GetCMSPageTemplateBusiness.cs" company="Chris Crutchfield">
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
    /// Base class for all Cmdlet Business objects which depend on the <see cref="IPageTemplateService"/>.
    /// </summary>
    [Export(typeof(GetCMSPageTemplateBusiness))]
    public class GetCMSPageTemplateBusiness : PageTemplateBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to <see cref="GetCMSPageTemplateCategoryBusiness"/>. Populated by MEF.
        /// </summary>
        [Import]
        public GetCMSPageTemplateCategoryBusiness GetCMSPageTemplateCategoryBusiness { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a list of all of the <see cref="IPageTemplate"/>.
        /// </summary>
        /// <returns>A list of all of the <see cref="IPageTemplate"/>.</returns>
        public IEnumerable<IPageTemplate> GetPageTemplates() => this.PageTemplateService.PageTemplates;

        /// <summary>
        /// Gets the <see cref="IPageTemplate"/> at the specified path.
        /// </summary>
        /// <param name="path">The path to look at for the desired page template.</param>
        /// <returns>The pagetemplate found at the desired path.</returns>
        public IPageTemplate GetPageTemplate(string path)
        {
            var name = path.Substring(path.LastIndexOf('/') + 1);
            var basePath = path.Substring(0, path.LastIndexOf('/'));

            if (string.IsNullOrWhiteSpace(basePath))
            {
                basePath = "/";
            }

            var parent = this.GetCategoryFromPath(basePath);

            return (from wp in this.GetPageTemplatesByCategory(parent)
                    where wp.DisplayName == name
                    select wp).SingleOrDefault();
        }

        /// <summary>
        /// Gets the <see cref="IPageTemplate"/> that match the provided <paramref name="matchString"/>.
        /// </summary>
        /// <param name="matchString">The string which to match the pagetemplates to.</param>
        /// <param name="isRegex">Indicates whether <paramref name="matchString"/> is a regular expression.</param>
        /// <returns>A list of <see cref="IPageTemplate"/> matching the <paramref name="matchString"/>.</returns>
        public IEnumerable<IPageTemplate> GetPageTemplates(string matchString, bool isRegex)
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

            return (from wp in this.PageTemplateService.PageTemplates
                    where regex.IsMatch(wp.DisplayName) ||
                        regex.IsMatch(wp.CodeName)
                    select wp).ToArray();
        }

        /// <summary>
        /// Gets a list of <see cref="IPageTemplate"/> that are within the <paramref name="pageTemplateCategory"/>.
        /// </summary>
        /// <param name="pageTemplateCategory">The <see cref="IPageTemplateCategory"/> that contains the desired <see cref="IPageTemplate"/>.</param>
        /// <returns>A list of <see cref="IPageTemplate"/> that are within the <paramref name="pageTemplateCategory"/>.</returns>
        public IEnumerable<IPageTemplate> GetPageTemplatesByCategory(IPageTemplateCategory pageTemplateCategory) => this.PageTemplateService.GetPageTemplates(pageTemplateCategory);

        /// <summary>Gets a list of <see cref="IPageTemplate"/> that are within the <see cref="IPageTemplateCategory"/> matching the <paramref name="matchString"/>.</summary>
        /// <param name="matchString">The string which to match the pagetemplate categories to.</param>
        /// <param name="isRegex">Indicates whether <paramref name="matchString"/> is a regular expression.</param>
        /// <returns>A list of <see cref="IPageTemplate"/> which are contained by the <see cref="IPageTemplateCategory"/> matching the <paramref name="matchString"/>.</returns>
        public IEnumerable<IPageTemplate> GetPageTemplatesByCategories(string matchString, bool isRegex)
        {
            var categories = this.GetCMSPageTemplateCategoryBusiness.GetPageTemplateCategories(matchString, isRegex, false);

            var ids = new HashSet<int>(from c in categories
                                       select c.CategoryId);

            var items = (from pt in this.PageTemplateService.PageTemplates
                    where ids.Contains(pt.CategoryID)
                    select pt).ToArray();

            return items;
        }
    }

    #endregion
}

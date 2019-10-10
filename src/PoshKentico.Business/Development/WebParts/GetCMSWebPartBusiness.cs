// <copyright file="GetCMSWebPartBusiness.cs" company="Chris Crutchfield">
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
using System.Text.RegularExpressions;
using CMS.PortalEngine;
using PoshKentico.Core.Services.Development;
using PoshKentico.Core.Services.Development.WebParts;

namespace PoshKentico.Business.Development.WebParts
{
    /// <summary>
    /// Business layer of the Get-CMSWebPart cmdlet.
    /// </summary>
    [Export(typeof(GetCMSWebPartBusiness))]
    public class GetCMSWebPartBusiness : WebPartBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to <see cref="GetCMSWebPartCategoryBusiness"/>. Populated by MEF.
        /// </summary>
        [Import]
        public GetCMSWebPartCategoryBusiness GetCMSWebPartCategoryBusiness { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the <see cref="IControl{T}"/> at the specified path.
        /// </summary>
        /// <param name="path">The path to look at for the desired web part.</param>
        /// <returns>The webpart found at the desired path.</returns>
        public IControl<WebPartInfo> GetWebPart(string path)
        {
            var name = path.Substring(path.LastIndexOf('/') + 1);
            var basePath = path.Substring(0, path.LastIndexOf('/'));

            if (string.IsNullOrWhiteSpace(basePath))
            {
                basePath = "/";
            }

            var parent = this.GetCategoryFromPath(basePath);

            return (from wp in this.GetWebPartsByCategory(parent)
                    where wp.Name == name
                    select wp).SingleOrDefault();
        }

        /// <summary>
        /// Gets the <see cref="IWebPart"/> associated with <see cref="IWebPartField"/>.
        /// </summary>
        /// <param name="field">The <see cref="IWebPartField"/> associated with the desired <see cref="IWebPart"/>.</param>
        /// <returns>Returns the <see cref="IWebPart"/> associated with the <see cref="IWebPartField"/>.</returns>
        public IWebPart GetWebPart(IWebPartField field)
        {
            return field.WebPart;
        }

        /// <summary>
        /// Gets a list of all of the <see cref="IControl{T}"/>.
        /// </summary>
        /// <returns>A list of all of the <see cref="IControl{T}"/>.</returns>
        public IEnumerable<IControl<WebPartInfo>> GetWebParts() => this.WebPartService.Controls;

        /// <summary>
        /// Gets the <see cref="IControl{T}"/> that match the provided <paramref name="matchString"/>.
        /// </summary>
        /// <param name="matchString">The string which to match the webparts to.</param>
        /// <param name="isRegex">Indicates whether <paramref name="matchString"/> is a regular expression.</param>
        /// <returns>A list of <see cref="IControl{T}"/> matching the <paramref name="matchString"/>.</returns>
        public IEnumerable<IControl<WebPartInfo>> GetWebParts(string matchString, bool isRegex)
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

            return (from wp in this.WebPartService.Controls
                    where regex.IsMatch(wp.DisplayName) ||
                        regex.IsMatch(wp.Name)
                    select wp).ToArray();
        }

        /// <summary>Gets a list of <see cref="IControl{T}"/> that are within the <see cref="IControlCategory{T}"/> matching the <paramref name="matchString"/>.</summary>
        /// <param name="matchString">The string which to match the webpart categories to.</param>
        /// <param name="isRegex">Indicates whether <paramref name="matchString"/> is a regular expression.</param>
        /// <returns>A list of <see cref="IControl{T}"/> which are contained by the <see cref="IControlCategory{T}"/> matching the <paramref name="matchString"/>.</returns>
        public IEnumerable<IControl<WebPartInfo>> GetWebPartsByCategories(string matchString, bool isRegex)
        {
            var categories = this.GetCMSWebPartCategoryBusiness.GetWebPartCategories(matchString, isRegex, false);

            var ids = new HashSet<int>(from c in categories
                                       select c.ID);

            return (from wp in this.WebPartService.Controls
                    where ids.Contains(wp.CategoryID)
                    select wp).ToArray();
        }

        /// <summary>
        /// Gets a list of <see cref="IControl{T}"/> that are within the <paramref name="controlCategory"/>.
        /// </summary>
        /// <param name="controlCategory">The <see cref="IControlCategory{T}"/> that contains the desired <see cref="IControl{T}"/>.</param>
        /// <returns>A list of <see cref="IControl{T}"/> that are within the <paramref name="controlCategory"/>.</returns>
        public IEnumerable<IControl<WebPartInfo>> GetWebPartsByCategory(IControlCategory<WebPartCategoryInfo> controlCategory) =>
            this.WebPartService.GetWebParts(controlCategory);

        #endregion

    }
}

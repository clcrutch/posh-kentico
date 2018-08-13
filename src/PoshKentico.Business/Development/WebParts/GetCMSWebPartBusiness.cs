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
using PoshKentico.Core.Services.Development.WebParts;

namespace PoshKentico.Business.Development.WebParts
{
    /// <summary>
    /// Business layer fo the Get-CMSWebPart cmdlet.
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

        public IWebPart GetWebPart(string path)
        {
            var name = path.Substring(path.LastIndexOf('/') + 1);
            var basePath = path.Substring(0, path.LastIndexOf('/'));

            if (string.IsNullOrWhiteSpace(basePath))
            {
                basePath = "/";
            }

            var parent = this.GetCategoryFromPath(basePath);

            return (from wp in this.GetWebPartsByCategory(parent)
                    where wp.WebPartName == name
                    select wp).SingleOrDefault();
        }

        /// <summary>
        /// Gets a list of all of the <see cref="IWebPart"/>.
        /// </summary>
        /// <returns>A list of all of the <see cref="IWebPart"/>.</returns>
        public IEnumerable<IWebPart> GetWebParts() => this.WebPartService.WebParts;

        /// <summary>
        /// Gets the <see cref="IWebPart"/> that match the provided <paramref name="matchString"/>.
        /// </summary>
        /// <param name="matchString">The string which to match the webparts to.</param>
        /// <param name="isRegex">Indicates whether <paramref name="matchString"/> is a regular expression.</param>
        /// <returns>A list of <see cref="IWebPart"/> matching the <paramref name="matchString"/>.</returns>
        public IEnumerable<IWebPart> GetWebParts(string matchString, bool isRegex)
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

            return (from wp in this.WebPartService.WebParts
                    where regex.IsMatch(wp.WebPartDisplayName) ||
                        regex.IsMatch(wp.WebPartName)
                    select wp).ToArray();
        }

        /// <summary>
        /// Gets a list of <see cref="IWebPart"/> that are within the <paramref name="webPartCategory"/>.
        /// </summary>
        /// <param name="webPartCategory">The <see cref="IWebPartCategory"/> that contains the desired <see cref="IWebPart"/>.</param>
        /// <returns>A list of <see cref="IWebPart"/> that are within the <paramref name="webPartCategory"/>.</returns>
        public IEnumerable<IWebPart> GetWebPartsByCategory(IWebPartCategory webPartCategory) => this.WebPartService.GetWebParts(webPartCategory);

        /// <summary>Gets a list of <see cref="IWebPart"/> that are within the <see cref="IWebPartCategory"/> matching the <paramref name="matchString"/>.</summary>
        /// <param name="matchString">The string which to match the webpart categories to.</param>
        /// <param name="isRegex">Indicates whether <paramref name="matchString"/> is a regular expression.</param>
        /// <returns>A list of <see cref="IWebPart"/> which are contained by the <see cref="IWebPartCategory"/> matching the <paramref name="matchString"/>.</returns>
        public IEnumerable<IWebPart> GetWebPartsByCategory(string matchString, bool isRegex)
        {
            var categories = this.GetCMSWebPartCategoryBusiness.GetWebPartCategories(matchString, isRegex, false);

            var ids = new HashSet<int>(from c in categories
                                       select c.CategoryID);

            return (from wp in this.WebPartService.WebParts
                    where ids.Contains(wp.WebPartCategoryID)
                    select wp).ToArray();
        }

        #endregion

    }
}

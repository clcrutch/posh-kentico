// <copyright file="GetCMSWebPartCategoryBusiness.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Development;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Business.Development
{
    /// <summary>
    /// Business layer for the Get-CMSWebPartCategory cmdlet.
    /// </summary>
    [Export(typeof(GetCMSWebPartCategoryBusiness))]
    public class GetCMSWebPartCategoryBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the CMS Application Service.  Populated by MEF.
        /// </summary>
        [Import]
        public ICmsApplicationService CmsApplicationService { get; set; }

        /// <summary>
        /// Gets or sets a reference to the WebPart Service.  Populated by MEF.
        /// </summary>
        [Import]
        public IWebPartService WebPartService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a list of all of the <see cref="WebPartCategoryInfo"/>.
        /// </summary>
        /// <returns>A list of all of the <see cref="WebPartCategoryInfo"/>.</returns>
        public IEnumerable<WebPartCategoryInfo> GetWebPartCategories()
        {
            this.CmsApplicationService.Initialize(true, this.WriteVerbose, this.WriteDebug);

            return this.WebPartService.WebPartCategories;
        }

        /// <summary>
        /// Gets a list of all of the <see cref="WebPartCategoryInfo"/> which match the specified criteria.
        /// </summary>
        /// <param name="matchString">The string which to match the webparts to.</param>
        /// <param name="exact">A boolean which indicates if the match should be exact.</param>
        /// <returns>A list of all of the <see cref="WebPartCategoryInfo"/> which match the specified criteria.</returns>
        public IEnumerable<WebPartCategoryInfo> GetWebPartCategories(string matchString, bool exact)
        {
            this.CmsApplicationService.Initialize(true, this.WriteVerbose, this.WriteDebug);

            if (exact)
            {
                return (from c in this.WebPartService.WebPartCategories
                        where c.CategoryName.ToLowerInvariant().Equals(matchString, StringComparison.InvariantCultureIgnoreCase) ||
                            c.CategoryDisplayName.ToLowerInvariant().Equals(matchString, StringComparison.InvariantCultureIgnoreCase) ||
                            c.CategoryPath.ToLowerInvariant().Equals(matchString, StringComparison.InvariantCultureIgnoreCase)
                        select c).ToArray();
            }
            else
            {
                var lowerMatchString = matchString.ToLowerInvariant();

                return (from c in this.WebPartService.WebPartCategories
                        where c.CategoryName.ToLowerInvariant().Contains(lowerMatchString) ||
                           c.CategoryDisplayName.ToLowerInvariant().Contains(lowerMatchString) ||
                           c.CategoryPath.ToLowerInvariant().StartsWith(lowerMatchString)
                        select c).ToArray();
            }
        }

        #endregion

    }
}

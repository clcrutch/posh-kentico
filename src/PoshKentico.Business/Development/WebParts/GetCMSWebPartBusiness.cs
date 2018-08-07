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
using PoshKentico.Core.Services.Development.WebParts;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Business.Development.WebParts
{
    /// <summary>
    /// Business layer fo the Get-CMSWebPart cmdlet.
    /// </summary>
    [Export(typeof(GetCMSWebPartBusiness))]
    public class GetCMSWebPartBusiness : CmdletBusinessBase
    {
        #region Properties

        [Import]
        public GetCMSWebPartCategoryBusiness GetCMSWebPartCategoryBusiness { get; set; }

        /// <summary>
        /// Gets or sets a reference to the WebPart Service.  Populated by MEF.
        /// </summary>
        [Import]
        public IWebPartService WebPartService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a list of all of the <see cref="IWebPart"/>.
        /// </summary>
        /// <returns>A list of all of the <see cref="IWebPart"/>.</returns>
        public IEnumerable<IWebPart> GetWebParts() => this.WebPartService.WebParts;

        public IEnumerable<IWebPart> GetWebPartsByCategory(IWebPartCategory webPartCategory) => this.WebPartService.GetWebParts(webPartCategory);

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

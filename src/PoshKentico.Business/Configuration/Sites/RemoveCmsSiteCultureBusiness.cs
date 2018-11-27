// <copyright file="RemoveCmsSiteCultureBusiness.cs" company="Chris Crutchfield">
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

using System.ComponentModel.Composition;
using PoshKentico.Core.Services.Configuration;
using PoshKentico.Core.Services.Configuration.Sites;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Business.Configuration.Sites
{
    /// <summary>
    /// Business layer for the Remove-CMSSiteCulture cmdlet.
    /// </summary>
    [Export(typeof(RemoveCmsSiteCultureBusiness))]
    public class RemoveCmsSiteCultureBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the Site Service.  Populated by MEF.
        /// </summary>
        [Import]
        public ISiteService SiteService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Remove a culture to a site.
        /// </summary>
        /// <param name="site">The site to remove culture to.</param>
        /// <param name="cultureCode">The culture code for the culture to remove to the site.</param>
        public void RemoveCulture(ISite site, string cultureCode)
        {
            this.SiteService.RemoveSiteCulture(site, cultureCode);
        }
        #endregion

    }
}

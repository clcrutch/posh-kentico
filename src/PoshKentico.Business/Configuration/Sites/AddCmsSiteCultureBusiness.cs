// <copyright file="AddCmsSiteCultureBusiness.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.General;

namespace PoshKentico.Business.Configuration.Sites
{
    /// <summary>
    /// Business layer for the Add-CMSSiteCulture cmdlet.
    /// </summary>
    [Export(typeof(AddCmsSiteCultureBusiness))]
    public class AddCmsSiteCultureBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the CMS Application Service.  Populated by MEF.
        /// </summary>
        [Import]
        public ICmsApplicationService CmsApplicationService { get; set; }

        /// <summary>
        /// Gets or sets a reference to the Site Service.  Populated by MEF.
        /// </summary>
        [Import]
        public ISiteService SiteService { get; set; }

        /// <summary>
        /// Gets or sets a reference to the <see cref="GetCmsSiteBusiness"/> used to get the site to add culture to.  Populated by MEF.
        /// </summary>
        [Import]
        public GetCmsSiteBusiness GetCmsSiteBusiness { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Add a culture to a site
        /// </summary>
        /// <param name="site">the site to add culture to</param>
        /// <param name="cultureCode">the culture code for the culture to add to the site</param>
        public void AddCulture(ISite site, string cultureCode)
        {
            this.CmsApplicationService.Initialize(true, this.WriteDebug, this.WriteVerbose);

            this.AddSiteCulture(site, cultureCode);
        }

        /// <summary>
        /// Adds the culture to a <see cref="ISite"/> in the CMS System.
        /// </summary>
        /// <param name="matchString">the string which to match the site to.</param>
        /// <param name="exact">A boolean which indicates if the match should be exact.</param>
        /// <param name="cultureCode">the culture code for the culture to add to the site</param>
        public void AddCulture(string matchString, bool exact, string cultureCode)
        {
            this.CmsApplicationService.Initialize(true, this.WriteDebug, this.WriteVerbose);

            foreach (var site in this.GetCmsSiteBusiness.GetSites(matchString, exact))
            {
                this.AddSiteCulture(site, cultureCode);
            }
        }

        /// <summary>
        /// Adds the culture to a <see cref="ISite"/> in the CMS System.
        /// </summary>
        /// <param name="ids">The IDs of the <see cref="ISite"/> to add culture to.</param>
        /// <param name="cultureCode">the culture code for the culture to add to the site</param>
        public void AddCulture(int[] ids, string cultureCode)
        {
            this.CmsApplicationService.Initialize(true, this.WriteDebug, this.WriteVerbose);

            foreach (var site in this.GetCmsSiteBusiness.GetSites(ids))
            {
                this.AddSiteCulture(site, cultureCode);
            }
        }

        /// <summary>
        /// Add a culture to a site
        /// </summary>
        /// <param name="site">the site to add culture to</param>
        /// <param name="cultureCode">the culture code for the culture to add to the site</param>
        private void AddSiteCulture(ISite site, string cultureCode)
        {
            this.SiteService.AddSiteCulture(site, cultureCode);
        }
        #endregion

    }
}

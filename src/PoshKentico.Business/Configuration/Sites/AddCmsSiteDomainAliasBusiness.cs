// <copyright file="AddCmsSiteDomainAliasBusiness.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Business.Configuration.Sites
{
    /// <summary>
    /// Business layer for the Add-CMSSiteDomainAlias cmdlet.
    /// </summary>
    [Export(typeof(AddCmsSiteDomainAliasBusiness))]
    public class AddCmsSiteDomainAliasBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the Site Service.  Populated by MEF.
        /// </summary>
        [Import]
        public ISiteService SiteService { get; set; }

        /// <summary>
        /// Gets or sets a reference to the <see cref="GetCmsSiteBusiness"/> used to get the site to add domain alias to.  Populated by MEF.
        /// </summary>
        [Import]
        public GetCmsSiteBusiness GetCmsSiteBusiness { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Add a domain alias to a site
        /// </summary>
        /// <param name="site">the site to add domain alias to</param>
        /// <param name="aliasName">the domain alias code for the domain alias to add to the site</param>
        public void AddDomainAlias(ISite site, string aliasName)
        {
            this.AddSiteDomainAlias(site, aliasName);
        }

        /// <summary>
        /// Adds the domain alias to a <see cref="ISite"/> in the CMS System.
        /// </summary>
        /// <param name="matchString">the string which to match the site to.</param>
        /// <param name="exact">A boolean which indicates if the match should be exact.</param>
        /// <param name="aliasName">the domain alias code for the domain alias to add to the site</param>
        public void AddDomainAlias(string matchString, bool exact, string aliasName)
        {
            foreach (var site in this.GetCmsSiteBusiness.GetSites(matchString, exact))
            {
                this.AddSiteDomainAlias(site, aliasName);
            }
        }

        /// <summary>
        /// Adds the domain alias to a <see cref="ISite"/> in the CMS System.
        /// </summary>
        /// <param name="ids">The IDs of the <see cref="ISite"/> to add domain alias to.</param>
        /// <param name="aliasName">the domain alias code for the domain alias to add to the site</param>
        public void AddDomainAlias(int[] ids, string aliasName)
        {
            foreach (var site in this.GetCmsSiteBusiness.GetSites(ids))
            {
                this.AddSiteDomainAlias(site, aliasName);
            }
        }

        /// <summary>
        /// Add a domain alias to a site
        /// </summary>
        /// <param name="site">the site to add domain alias to</param>
        /// <param name="aliasName">the domain alias code for the domain alias to add to the site</param>
        private void AddSiteDomainAlias(ISite site, string aliasName)
        {
            this.SiteService.AddSiteDomainAlias(site, aliasName);
        }
        #endregion

    }
}

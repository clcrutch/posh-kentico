// <copyright file="GetCmsSiteDomainAliasBusiness.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Configuration;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Business.Configuration.Sites
{
    /// <summary>
    /// Business layer for the Get-CMSSiteDomainAlias cmdlet.
    /// </summary>
    [Export(typeof(GetCmsSiteDomainAliasBusiness))]
    public class GetCmsSiteDomainAliasBusiness : CmdletBusinessBase
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
        /// Gets or sets a reference to the <see cref="GetCmsSiteBusiness"/> used to get the site to get Domain Alias from.  Populated by MEF.
        /// </summary>
        [Import]
        public GetCmsSiteBusiness GetCmsSiteBusiness { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Gets the Domain Aliases of the specified site.
        /// </summary>
        /// <param name="site">the site to get DomainAlias from.</param>
        /// <returns>Returns the list containing the Domain Aliases of the specified site.</returns>
        public IEnumerable<ISiteDomainAlias> GetDomainAliases(ISite site)
        {
            this.CmsApplicationService.Initialize(true, this.WriteDebug, this.WriteVerbose);

            return this.GetSiteDomainAliases(site);
        }

        /// <summary>
        /// Gets the Domain Aliases of the specified site.
        /// </summary>
        /// <param name="siteName">the site name of the site to get DomainAlias from.</param>
        /// <returns>Returns the list containing the Domain Aliases of the specified site.</returns>
        public IEnumerable<ISiteDomainAlias> GetDomainAliases(string siteName)
        {
            this.CmsApplicationService.Initialize(true, this.WriteDebug, this.WriteVerbose);

            var site = this.SiteService.GetSite(siteName);

            return this.GetSiteDomainAliases(site);
        }

        /// <summary>
        /// Gets the Domain Aliases of the specified site.
        /// </summary>
        /// <param name="site">the site to get DomainAlias from.</param>
        /// <returns>Returns the list containing the Domain Aliases of the specified site.</returns>
        private IEnumerable<ISiteDomainAlias> GetSiteDomainAliases(ISite site)
        {
            return this.SiteService.GetDomainAliases(site);
        }
        #endregion

    }
}

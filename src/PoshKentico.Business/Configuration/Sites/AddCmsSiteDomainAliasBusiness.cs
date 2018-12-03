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
using PoshKentico.Core.Services.Configuration.Sites;

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
        #endregion

        #region Methods

        /// <summary>
        /// Add a domain alias to a site.
        /// </summary>
        /// <param name="site">The site to add domain alias to.</param>
        /// <param name="aliasName">The domain alias code for the domain alias to add to the site.</param>
        public void AddDomainAlias(ISite site, string aliasName)
        {
            this.SiteService.AddSiteDomainAlias(site, aliasName);
        }
        #endregion

    }
}

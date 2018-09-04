// <copyright file="SetCmsSiteBusiness.cs" company="Chris Crutchfield">
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
using CMS.SiteProvider;
using ImpromptuInterface;
using PoshKentico.Core.Services.Configuration.Sites;

namespace PoshKentico.Business.Configuration.Sites
{
    /// <summary>
    /// Business Layer for the Set-CMSSite cmdlet.
    /// </summary>
    [Export(typeof(SetCmsSiteBusiness))]
    public class SetCmsSiteBusiness : CmdletBusinessBase
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
        /// Sets the <see cref="ISite"/> in the CMS System.
        /// </summary>
        /// <param name="site">The <see cref="ISite"/> to set.</param>
        public void Set(ISite site)
        {
            this.SiteService.Update(site);
        }

        /// <summary>
        /// Sets the <see cref="ISite"/> in the CMS System.
        /// </summary>
        /// <param name="displayName">The Display Name for site to update.</param>
        /// <param name="siteName">The Site Name for site to update.</param>
        /// <param name="status">The Status for site to update.</param>
        /// <param name="domainName">The Domain Name for site to update.</param>
        public void Set(string displayName, string siteName, SiteStatusEnum status, string domainName)
        {
            var site = new
            {
                DisplayName = displayName,
                SiteName = siteName,
                Status = status,
                DomainName = domainName,
            };

            this.SiteService.Update(site.ActLike<ISite>());
        }

        #endregion

    }
}

// <copyright file="KenticoSiteService.cs" company="Chris Crutchfield">
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
using CMS.SiteProvider;
using ImpromptuInterface;
using PoshKentico.Core.Services.Configuration;

namespace PoshKentico.Core.Providers.Configuration
{
    /// <summary>
    /// Implementation of <see cref="ISiteService"/> that uses Kentico.
    /// </summary>
    [Export(typeof(ISiteService))]
    public class KenticoSiteService : ISiteService
    {
        #region Properties

        /// <inheritdoc/>
        public IEnumerable<ISite> Sites => (from c in SiteInfoProvider.GetSites()
                                              select Impromptu.ActLike<ISite>(c as SiteInfo)).ToArray();

        #endregion

        #region Methods

        /// <inheritdoc/>
        public ISite Create(ISite site)
        {
            var siteInfo = new SiteInfo
            {
                DisplayName = site.DisplayName,
                SiteName = site.SiteName,
                Status = site.Status,
                DomainName = site.DomainName,
            };

            SiteInfoProvider.SetSiteInfo(siteInfo);

            return siteInfo.ActLike<ISite>();
        }

        /// <inheritdoc/>
        public ISite GetSite(int id)
        {
            return (SiteInfoProvider.GetSiteInfo(id) as SiteInfo)?.ActLike<ISite>();
        }

        /// <inheritdoc/>
        public void Delete(ISite site)
        {
            // Gets the site
            SiteInfo deleteSite = SiteInfoProvider.GetSiteInfo(site.SiteName);

            if (deleteSite != null)
            {
                // Deletes the site
                SiteInfoProvider.DeleteSiteInfo(deleteSite);
            }
        }

        /// <inheritdoc/>
        public void Update(ISite site)
        {
            // Gets the site
            SiteInfo updateSite = SiteInfoProvider.GetSiteInfo(site.SiteName);
            if (updateSite != null)
            {
                // Updates the site properties
                updateSite.DisplayName = site.DisplayName;
                updateSite.DomainName = site.DomainName;
                updateSite.SiteName = site.SiteName;
                updateSite.Status = site.Status;

                // Saves the modified site to the database
                SiteInfoProvider.SetSiteInfo(updateSite);
            }
        }

        /// <inheritdoc/>
        public void Start(ISite site)
        {
            // Gets the site
            SiteInfo siteToStart = SiteInfoProvider.GetSiteInfo(site.SiteName);
            if (siteToStart != null)
            {
                // Starts the site
                SiteInfoProvider.RunSite(siteToStart.SiteName);
            }
        }

        /// <inheritdoc/>
        public void Stop(ISite site)
        {
            // Gets the site
            SiteInfo siteToStop = SiteInfoProvider.GetSiteInfo(site.SiteName);
            if (siteToStop != null)
            {
                // Stops the site
                SiteInfoProvider.StopSite(siteToStop.SiteName);
            }
        }

        #endregion
    }
}

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

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CMS.Localization;
using CMS.Membership;
using CMS.SiteProvider;
using ImpromptuInterface;
using PoshKentico.Core.Services.Configuration.Localization;
using PoshKentico.Core.Services.Configuration.Sites;
using PoshKentico.Core.Services.Configuration.Users;

namespace PoshKentico.Core.Providers.Configuration.Sites
{
    /// <summary>
    /// Implementation of <see cref="ISiteService"/> that uses Kentico.
    /// </summary>
    [ExcludeFromCodeCoverage]
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

            // Verifies that the site is unique
            if (SiteInfoProvider.GetSiteInfo(site.SiteName) == null)
            {
                // Saves the site to the database
                SiteInfoProvider.SetSiteInfo(siteInfo);
            }
            else
            {
                // A site with the same name already exists
                throw new Exception("A site with the same name already exists!");
            }

            return siteInfo.ActLike<ISite>();
        }

        /// <inheritdoc/>
        public ISite GetSite(int id)
        {
            return (SiteInfoProvider.GetSiteInfo(id) as SiteInfo)?.ActLike<ISite>();
        }

        /// <inheritdoc/>
        public ISite GetSite(string siteName)
        {
            return (SiteInfoProvider.GetSiteInfo(siteName) as SiteInfo)?.ActLike<ISite>();
        }

        /// <inheritdoc/>
        public IEnumerable<ISite> GetSite(IUser user)
        {
            // Gets the user
            UserInfo existingUser = UserInfoProvider.GetUserInfo(user.UserName);

            if (existingUser != null)
            {
                // Gets the sites to which the user is assigned
                var userSiteIDs = UserSiteInfoProvider.GetUserSites().Column("SiteID").WhereEquals("UserID", existingUser.UserID);
                var sites = SiteInfoProvider.GetSites().WhereIn("SiteID", userSiteIDs);

                // Loops through the sites
                return (from c in sites select Impromptu.ActLike<ISite>(c as SiteInfo)).ToArray();
            }
            else
            {
                // A user with the specified name not exist on the site
                throw new Exception(string.Format("A user with the user name {0} does not exist.", user.UserName));
            }
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
            else
            {
                // A site with the specified name not exists
                throw new Exception(string.Format("A site with the site name {0} does not exist.", site.SiteName));
            }
        }

        /// <inheritdoc/>
        public ISite Update(ISite site, bool isReplace = true)
        {
            // Gets the site
            SiteInfo updateSite = SiteInfoProvider.GetSiteInfo(site.SiteName);
            if (updateSite != null)
            {
                if (isReplace)
                {
                    updateSite = site.UndoActLike();
                }
                else
                {
                    // Updates the site properties
                    updateSite.DisplayName = site.DisplayName;
                    updateSite.DomainName = site.DomainName;
                    updateSite.SiteName = site.SiteName;
                    updateSite.Status = site.Status;
                }

                // Saves the modified site to the database
                SiteInfoProvider.SetSiteInfo(updateSite);
            }
            else
            {
                // A site with the specified name not exists
                throw new Exception(string.Format("A site with the site name {0} does not exist.", site.SiteName));
            }

            return updateSite.ActLike<ISite>();
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
            else
            {
                // A site with the specified name not exists
                throw new Exception(string.Format("A site with the site name {0} does not exist.", site.SiteName));
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
            else
            {
                // A site with the specified name not exists
                throw new Exception(string.Format("A site with the site name {0} does not exist.", site.SiteName));
            }
        }

        /// <inheritdoc/>
        public void AddSiteCulture(ISite site, string cultureCode)
        {
            // Gets the site and culture objects
            SiteInfo siteToWork = SiteInfoProvider.GetSiteInfo(site.SiteName);
            CultureInfo cultureToWork = CultureInfoProvider.GetCultureInfo(cultureCode);

            if ((siteToWork != null) && (cultureToWork != null))
            {
                // Assigns the culture to the site
                CultureSiteInfoProvider.AddCultureToSite(cultureToWork.CultureID, siteToWork.SiteID);
            }
            else
            {
                // A site with the specified name not exists or the cultureCode not exists.
                throw new Exception(string.Format("A site with the site name {0} does not exist or the culture code {1} not exists.", site.SiteName, cultureCode));
            }
        }

        /// <inheritdoc/>
        public void RemoveSiteCulture(ISite site, string cultureCode)
        {
            // Gets the site and culture objects
            SiteInfo siteToWork = SiteInfoProvider.GetSiteInfo(site.SiteName);
            CultureInfo cultureToWork = CultureInfoProvider.GetCultureInfo(cultureCode);

            if ((siteToWork != null) && (cultureToWork != null))
            {
                // Removes the culture from the site
                CultureSiteInfoProvider.RemoveCultureFromSite(cultureToWork.CultureID, siteToWork.SiteID);
            }
            else
            {
                // A site with the specified name not exists or the cultureCode not exists.
                throw new Exception(string.Format("A site with the site name {0} does not exist or the culture code {1} not exists.", site.SiteName, cultureCode));
            }
        }

        /// <inheritdoc />
        public IEnumerable<ICulture> GetSiteCultures(ISite site)
        {
            List<ICulture> cultures = new List<ICulture>();
            SiteInfo siteToWork = SiteInfoProvider.GetSiteInfo(site.SiteName);

            if (siteToWork != null)
            {
                var items = CultureSiteInfoProvider.GetSiteCultures(site.SiteName)?.Items;

                foreach (var item in items)
                {
                    cultures.Add(item.ActLike<ICulture>());
                }
            }
            else
            {
                // A site with the specified name not exists
                throw new Exception(string.Format("A site with the site name {0} does not exist.", site.SiteName));
            }

            return cultures;
        }

        /// <inheritdoc/>
        public void AddSiteDomainAlias(ISite site, string aliasName)
        {
            // Gets the site object
            SiteInfo siteToWork = SiteInfoProvider.GetSiteInfo(site.SiteName);

            if (siteToWork != null)
            {
                // Creates a new site domain alias object
                SiteDomainAliasInfo newAlias = new SiteDomainAliasInfo
                {
                    SiteDomainAliasName = aliasName,

                    // Assigns the domain alias to the site
                    SiteID = siteToWork.SiteID,
                };

                // Saves the site domain alias to the database
                SiteDomainAliasInfoProvider.SetSiteDomainAliasInfo(newAlias);
            }
            else
            {
                // A site with the specified name not exists
                throw new Exception(string.Format("A site with the site name {0} does not exist.", site.SiteName));
            }
        }

        /// <inheritdoc/>
        public void RemoveSiteDomainAlias(ISite site, string aliasName)
        {
            // Gets the site object
            SiteInfo siteToWork = SiteInfoProvider.GetSiteInfo(site.SiteName);

            if (siteToWork != null)
            {
                // Gets the specified domain alias for the site
                SiteDomainAliasInfo deleteAlias = SiteDomainAliasInfoProvider.GetSiteDomainAliasInfo(aliasName, siteToWork.SiteID);

                // Deletes the site domain alias
                SiteDomainAliasInfoProvider.DeleteSiteDomainAliasInfo(deleteAlias);
            }
            else
            {
                // A site with the specified name not exists
                throw new Exception(string.Format("A site with the site name {0} does not exist.", site.SiteName));
            }
        }

        /// <inheritdoc/>
        public IEnumerable<ISiteDomainAlias> GetDomainAliases(ISite site)
        {
            List<ISiteDomainAlias> aliases = new List<ISiteDomainAlias>();

            // Gets the site object
            SiteInfo siteToWork = SiteInfoProvider.GetSiteInfo(site.SiteName);

            if (siteToWork != null)
            {
                // Gets all the domain alias for the site
                var items = SiteDomainAliasInfoProvider.GetDomainAliases(site.SiteName)?.Items;

                foreach (var item in items)
                {
                    aliases.Add(item.ActLike<ISiteDomainAlias>());
                }
            }
            else
            {
                // A site with the specified name not exists
                throw new Exception(string.Format("A site with the site name {0} does not exist.", site.SiteName));
            }

            return aliases;
        }
        #endregion
    }
}

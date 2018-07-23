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

        /// <summary>
        /// Gets a list of all of the <see cref="ISite"/> provided by Kentico.
        /// </summary>
        public IEnumerable<ISite> Sites => (from c in SiteInfoProvider.GetSites()
                                              select Impromptu.ActLike<ISite>(c as SiteInfo)).ToArray();

        #endregion

        #region Methods

        /// <summary>
        /// Creates the <see cref="ISite"/>.
        /// </summary>
        /// <param name="site">The <see cref="ISite"/> to create.</param>
        /// <returns>The newly created <see cref="ISite"/>.</returns>
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

        /// <summary>
        /// Gets the <see cref="ISite"/> which matches the supplied ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="ISite"/> to return.</param>
        /// <returns>The <see cref="ISite"/> which matches the ID, else null.</returns>
        public ISite GetSite(int id)
        {
            return (SiteInfoProvider.GetSiteInfo(id) as SiteInfo)?.ActLike<ISite>();
        }

        /// <summary>
        /// Deletes the specified <see cref="ISite"/>.
        /// </summary>
        /// <param name="site">The <see cref="ISite"/> to delete.</param>
        public void Delete(ISite site)
        {
            SiteInfoProvider.DeleteSiteInfo(site.SiteName);
        }

        /// <summary>
        /// Updates the <see cref="ISite"/>.
        /// </summary>
        /// <param name="site">The <see cref="ISite"/> to update.</param>
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

        #endregion
    }
}

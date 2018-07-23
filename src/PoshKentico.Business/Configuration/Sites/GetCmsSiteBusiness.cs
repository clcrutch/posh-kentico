
using PoshKentico.Core.Services.Configuration;
using PoshKentico.Core.Services.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Business.Configuration.Sites
{
    /// <summary>
    /// Business layer for the Get-CMSSites cmdlet.
    /// </summary>
    [Export(typeof(GetCmsSiteBusiness))]
    public class GetCmsSiteBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the CMS Application Service.  Populated by MEF.
        /// </summary>
        [Import]
        public ICmsApplicationService CmsApplicationService { get; set; }

        /// <summary>
        /// Gets or sets a reference to the WebPart Service.  Populated by MEF.
        /// </summary>
        [Import]
        public ISiteService SiteService { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Gets a list of all of the <see cref="ISite"/>.
        /// </summary>
        /// <returns>A list of all of the <see cref="ISite"/>.</returns>
        public IEnumerable<ISite> GetSites()
        {
            this.CmsApplicationService.Initialize(true, this.WriteVerbose, this.WriteDebug);

            return this.SiteService.Sites;
        }

        /// <summary>
        /// Gets a list of all of the <see cref="ISite"/> which match the specified criteria.
        /// </summary>
        /// <param name="matchString">The string which to match the webparts to.</param>
        /// <param name="exact">A boolean which indicates if the match should be exact.</param>
        /// <returns>A list of all of the <see cref="ISite"/> which match the specified criteria.</returns>
        public IEnumerable<ISite> GetSites(string matchString, bool exact)
        {
            this.CmsApplicationService.Initialize(true, this.WriteVerbose, this.WriteDebug);

            if (exact)
            {
                return (from c in this.SiteService.Sites
                        where c.DisplayName.ToLowerInvariant().Equals(matchString, StringComparison.InvariantCultureIgnoreCase) ||
                            c.SiteName.ToLowerInvariant().Equals(matchString, StringComparison.InvariantCultureIgnoreCase) ||
                            c.DomainName.ToLowerInvariant().Equals(matchString, StringComparison.InvariantCultureIgnoreCase)
                        select c).ToArray();
            }
            else
            {
                var lowerMatchString = matchString.ToLowerInvariant();

                return (from c in this.SiteService.Sites
                        where c.DisplayName.ToLowerInvariant().Contains(lowerMatchString) ||
                           c.SiteName.ToLowerInvariant().Contains(lowerMatchString) ||
                           c.DomainName.ToLowerInvariant().StartsWith(lowerMatchString)
                        select c).ToArray();
            }
        }

        /// <summary>
        /// Gets a list of the <see cref="ISite"/> which match the supplied IDs.
        /// </summary>
        /// <param name="ids">The IDs of the <see cref="ISite"/> to return.</param>
        /// <returns>A list of the <see cref="ISite"/> which match the supplied IDs.</returns>
        public IEnumerable<ISite> GetSites(params int[] ids)
        {
            this.CmsApplicationService.Initialize(true, this.WriteVerbose, this.WriteDebug);

            var sites = from id in ids select this.SiteService.GetSite(id);

            return (from site in sites
                    where site != null
                    select site).ToArray();
        }

        #endregion
    }
}

using CMS.SiteProvider;
using ImpromptuInterface;
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
    /// Business layer for the New-CMSSites cmdlet.
    /// </summary>
    [Export(typeof(NewCmsSiteBusiness))]
    public class NewCmsSiteBusiness : CmdletBusinessBase
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
        /// Creates a new <see cref="ISite"/> in the CMS System.
        /// </summary>
        /// <param name="displayName">The Display Name for the new Site</param>
        /// <param name="siteName">The Site Name for the new Site</param>
        /// <param name="status">The Status for the new Site</param>
        /// <param name="domainName">The Domain Name for the new Site</param>
        /// <returns>A list of all of the <see cref="ISite"/>.</returns>
        public ISite CreateSite(string displayName, string siteName, string status, string domainName)
        {
            this.CmsApplicationService.Initialize(true, this.WriteVerbose, this.WriteDebug);

            var data = new
            {
                DisplayName = displayName,
                SiteName = siteName,
                Status = (SiteStatusEnum)Enum.Parse(typeof(SiteStatusEnum), status),
                DomainName = domainName,
            };

            return this.SiteService.Create(data.ActLike<ISite>());
        }

        #endregion
    }
}

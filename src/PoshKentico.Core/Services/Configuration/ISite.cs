using CMS.SiteProvider;

namespace PoshKentico.Core.Services.Configuration
{
    /// <summary>
    /// Represents a Site Object.
    /// </summary>
    public interface ISite
    {
        #region Properties

        /// <summary>
        /// Gets the display name for the site.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Gets the site name.
        /// </summary>
        string SiteName { get; }

        /// <summary>
        /// Gets the site status.
        /// </summary>
        SiteStatusEnum Status { get; }

        /// <summary>
        /// Gets the domain name for the site.
        /// </summary>
        string DomainName { get; }

        #endregion

    }
}

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using CMS.SiteProvider;
using ImpromptuInterface;
using PoshKentico.Business.Configuration.Sites;
using PoshKentico.Core.Services.Configuration;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.Sites
{
    /// <summary>
    /// <para type="synopsis">Gets the sites selected by the provided input.</para>
    /// <para type="description">Gets the sites selected by the provided input.  This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">Without parameters, this command returns all sites.</para>
    /// <para type="description">With parameters, this command returns the sites that match the criteria.</para>
    /// <example>
    ///     <para>Get all the sites.</para>
    ///     <code>Get-CmsSite</code>
    /// </example>
    /// <example>
    ///     <para>Get all sites with a category name "*bas*", display name "*bas*", or a path "bas*".</para>
    ///     <code>Get-CmsSite bas</code>
    /// </example>
    /// <example>
    ///     <para>Get all sites with a category name "basic", display name "basic", or path "basic"</para>
    ///     <code>Get-CmsSite basic -Exact</code>
    /// </example>
    /// <example>
    ///     <para>Get all the sites with the specified IDs.</para>
    ///     <code>Get-CmsSite -ID 5,304,5</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Get, "CMSSite", DefaultParameterSetName = NONE)]
    [OutputType(typeof(SiteInfo))]
    [Alias("gsite")]
    public class GetCmsSiteCmdlet : MefCmdlet
    {
        #region Constants

        private const string NONE = "None";
        private const string DISPLAYNAME = "Dislpay Name";
        private const string IDSETNAME = "ID";

        #endregion
        #region Properties

        /// <summary>
        /// <para type="description">The category name, display name, or path of the webpart category.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = DISPLAYNAME)]
        public string DisplayName { get; set; }

        /// <summary>
        /// <para type="description">The display name for the newly created site.</para>
        /// <para type="description">If null, then the display name is used for the site name.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 1, ValueFromPipeline = true, ParameterSetName = DISPLAYNAME)]
        public string SiteName { get; set; }

        /// <summary>
        /// <para type="description">The domain name for the newly created site.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 2, ValueFromPipeline = true, ParameterSetName = DISPLAYNAME)]
        public string DomainName { get; set; }

        /// <summary>
        /// <para type="description">If set, the match is exact,</para>
        /// <para type="description">else the match performs a contains for display name and category name and starts with for path.</para>
        /// </summary>
        [Parameter(ParameterSetName = DISPLAYNAME)]
        public SwitchParameter Exact { get; set; }

        /// <summary>
        /// <para type="description">The IDs of the web part category to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = IDSETNAME)]
        public int[] ID { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this web part. Populated by MEF.
        /// </summary>
        [Import]
        public GetCmsSiteBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<ISite> sites = null;

            switch (this.ParameterSetName)
            {
                case DISPLAYNAME:
                    sites = this.BusinessLayer.GetSites(this.DisplayName, this.Exact.ToBool());
                    break;
                case IDSETNAME:
                    sites = this.BusinessLayer.GetSites(this.ID);
                    break;
                case NONE:
                    sites = this.BusinessLayer.GetSites();
                    break;
            }

            foreach (var site in sites)
            {
                this.WriteObject(site.UndoActLike());
            }
        }

        #endregion
    }
}

// <copyright file="StartCmsSiteCmdlet.cs" company="Chris Crutchfield">
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
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using CMS.SiteProvider;
using ImpromptuInterface;
using PoshKentico.Business.Configuration.Sites;
using PoshKentico.Core.Services.Configuration.Sites;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.Sites
{
    /// <summary>
    /// <para type="synopsis">Starts a site.</para>
    /// <para type="description">Starts a site.</para>
    /// <example>
    ///     <para>Starts a site contains a site name "*bas*", display name "*bas*", or a domain name "bas*".</para>
    ///     <code>Start-CMSSite -SiteName "bas"</code>
    /// </example>
    /// <example>
    ///     <para>Starts a site with a site name "basic", display name "basic", or a domain name "basic".</para>
    ///     <code>Start-CMSSite -Site "basic" -RegularExpression</code>
    /// </example>
    /// <example>
    ///     <para>Starts a site.</para>
    ///     <code>$site| Start-CMSSite</code>
    /// </example>
    /// <example>
    ///     <para>Starts all the sites with the specified IDs.</para>
    ///     <code>Start-CMSSite -SiteIds 1,2,3</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet("Start", "CMSSite")]
    [Alias("startsite")]
    public class StartCmsSiteCmdlet : GetCmsSiteCmdlet
    {
        #region Constants

        private const string SITEOBJECTSET = "Object";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">A reference to the site to start.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = SITEOBJECTSET)]
        [Alias("Site")]
        public SiteInfo SiteToStart { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this site.  Populated by MEF.
        /// </summary>
        [Import]
        public StartCmsSiteBusiness StartBusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            if (this.ParameterSetName == SITEOBJECTSET)
            {
                this.ActOnObject(this.SiteToStart.ActLike<ISite>());
            }
            else
            {
                base.ProcessRecord();
            }
        }

        /// <inheritdoc />
        protected override void ActOnObject(ISite site)
        {
            if (site == null)
            {
                return;
            }

            this.StartBusinessLayer.Start(site);
        }

        #endregion
    }
}

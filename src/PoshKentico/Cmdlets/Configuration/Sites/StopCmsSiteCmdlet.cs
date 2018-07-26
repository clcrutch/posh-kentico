// <copyright file="StopCmsSiteCmdlet.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Configuration;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.Sites
{
    /// <summary>
    /// <para type="synopsis">Stops a site.</para>
    /// <para type="description">Stops a site.</para>
    /// <example>
    ///     <para>Stops a site contains a site name "*bas*", display name "*bas*", or a domain name "bas*".</para>
    ///     <code>Stop-CMSSite -SiteName "bas"</code>
    /// </example>
    /// <example>
    ///     <para>Stops a site with a site name "basic", display name "basic", or a domain name "basic".</para>
    ///     <code>Stop-CMSSite -Site "basic" -EXACT</code>
    /// </example>
    /// <example>
    ///     <para>Stops a site.</para>
    ///     <code>$site| Stop-CMSSite</code>
    /// </example>
    /// <example>
    ///     <para>Stops all the sites with the specified IDs.</para>
    ///     <code>Stop-CMSSite -ID 1,2,3</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet("Stop", "CMSSite")]
    [Alias("stasite")]
    public class StopCmsSiteCmdlet : MefCmdlet
    {
        #region Constants

        private const string OBJECTSET = "Object";
        private const string PROPERTYSET = "Property";
        private const string IDSETNAME = "ID";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">A reference to the site to stop.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = OBJECTSET)]
        [Alias("Site")]
        public SiteInfo SiteToStart { get; set; }

        /// <summary>
        /// <para type="description">The site name for the site to stop.</para>
        /// <para type="description">Site name cannot be blank.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PROPERTYSET)]
        public string SiteName { get; set; }

        /// <summary>
        /// <para type="description">If set, the match is exact,</para>
        /// <para type="description">else the match performs a contains for site name.</para>
        /// </summary>
        [Parameter(ParameterSetName = PROPERTYSET)]
        public SwitchParameter Exact { get; set; }

        /// <summary>
        /// <para type="description">The IDs of the web part category to stop.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = IDSETNAME)]
        public int[] ID { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this site.  Populated by MEF.
        /// </summary>
        [Import]
        public StopCMSSiteBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case OBJECTSET:
                    this.BusinessLayer.Stop(this.SiteToStart.ActLike<ISite>());
                    break;
                case PROPERTYSET:
                    this.BusinessLayer.Stop(this.SiteName, this.Exact);
                    break;
                case IDSETNAME:
                    this.BusinessLayer.Stop(this.ID);
                    break;
            }
        }

        #endregion
    }
}

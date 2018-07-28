// <copyright file="RemoveCmsSiteCmdlet.cs" company="Chris Crutchfield">
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
    /// <para type="synopsis">Deletes a site.</para>
    /// <para type="description">Deletes a site.</para>
    /// <example>
    ///     <para>Deletes all sites contains a site name "*bas*", display name "*bas*", or a domain name "*bas*".</para>
    ///     <code>Remove-CMSSite -SiteName "bas"</code>
    /// </example>
    /// <example>
    ///     <para>Deletes all sites with a site name "basic", display name "basic", or a domain name "basic".</para>
    ///     <code>Remove-CMSSite -SiteName "basic" -Exact</code>
    /// </example>
    /// <example>
    ///     <para>Deletes a site.</para>
    ///     <code>$site | Remove-CMSSite</code>
    /// </example>
    /// <example>
    ///     <para>Delete all the sites with the specified IDs.</para>
    ///     <code>Remove-CMSSite -ID 1,2,3</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSSite", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [Alias("rsite")]
    public class RemoveCmsSiteCmdlet : MefCmdlet
    {
        #region Constants

        private const string OBJECTSET = "Object";
        private const string SITENAMESET = "Property";
        private const string IDSETNAME = "ID";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">A reference to the site to remove.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = OBJECTSET)]
        [Alias("Site")]
        public SiteInfo SiteToRemove { get; set; }

        /// <summary>
        /// <para type="description">The site name for the site to remove.</para>
        /// <para type="description">Site name cannot be blank.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = SITENAMESET)]
        public string SiteName { get; set; }

        /// <summary>
        /// <para type="description">If set, the match is exact,</para>
        /// <para type="description">else the match performs a contains for site name.</para>
        /// </summary>
        [Parameter(ParameterSetName = SITENAMESET)]
        public SwitchParameter Exact { get; set; }

        /// <summary>
        /// <para type="description">The IDs of the web part category to delete.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = IDSETNAME)]
        public int[] ID { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this site.  Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCmsSiteBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case OBJECTSET:
                    this.BusinessLayer.Remove(this.SiteToRemove.ActLike<ISite>());
                    break;
                case SITENAMESET:
                    this.BusinessLayer.Remove(this.SiteName, this.Exact.ToBool());
                    break;
                case IDSETNAME:
                    this.BusinessLayer.Remove(this.ID);
                    break;
            }
        }

        #endregion

    }
}

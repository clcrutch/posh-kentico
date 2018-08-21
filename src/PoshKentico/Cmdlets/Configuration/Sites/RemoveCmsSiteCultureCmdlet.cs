// <copyright file="RemoveCmsSiteCultureCmdlet.cs" company="Chris Crutchfield">
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
    /// <para type="synopsis">Removes a culture to a specified site.</para>
    /// <para type="description">Removes a culture to a specified site based off of the provided input.</para>
    /// <example>
    ///     <para>Remove a culture with culture code "cul" from a specified site specifying the site name "*bas*", display name "*bas*", or a domain name "*bas*".</para>
    ///     <code>Remove-CMSSiteCulture -SiteName "*bas*" -CultureCode "cul"</code>
    /// </example>
    /// <example>
    ///     <para>Remove a culture with culture code "cul" from a specified site specifying the site name "basic", display name "basic", or a domain name "basic".</para>
    ///     <code>Remove-CMSSiteCulture -SiteName "basic" -EXACT -CultureCode "cul"</code>
    /// </example>
    /// <example>
    ///     <para>Remove a culture with culture code "cul" from a site.</para>
    ///     <code>$site | Remove-CMSSiteCulture</code>
    /// </example>
    /// <example>
    ///     <para>Remove a culture with culture code "cul" with the specified IDs.</para>
    ///     <code>Remove-CMSSiteCulture -ID 1,2,3</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSSiteCulture")]
    [Alias("rcsite")]
    public class RemoveCmsSiteCultureCmdlet : MefCmdlet
    {
        #region Constants

        private const string OBJECTSET = "Object";
        private const string SITENAMESET = "Property";
        private const string IDSETNAME = "ID";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The site name for the site.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string SiteName { get; set; }

        /// <summary>
        /// <para type="description">A reference to the site.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = OBJECTSET)]
        [Alias("Site")]
        public SiteInfo SiteToRemove { get; set; }

        /// <summary>
        /// <para type="description">If set, the match is exact,</para>
        /// <para type="description">else the match performs a contains for site name.</para>
        /// </summary>
        [Parameter(ParameterSetName = SITENAMESET)]
        public SwitchParameter Exact { get; set; }

        /// <summary>
        /// <para type="description">The IDs of the site.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = IDSETNAME)]
        public int[] ID { get; set; }

        /// <summary>
        /// <para type="description">The IDs of the site.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        public string CultureCode { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for adding culture to this site.  Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCmsSiteCultureBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case OBJECTSET:
                    this.BusinessLayer.RemoveCulture(this.SiteToRemove.ActLike<ISite>(), this.CultureCode);
                    break;
                case SITENAMESET:
                    this.BusinessLayer.RemoveCulture(this.SiteName, this.Exact.ToBool(), this.CultureCode);
                    break;
                case IDSETNAME:
                    this.BusinessLayer.RemoveCulture(this.ID, this.CultureCode);
                    break;
            }
        }

        #endregion

    }
}

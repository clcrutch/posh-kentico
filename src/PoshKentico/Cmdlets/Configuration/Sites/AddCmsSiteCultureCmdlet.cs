// <copyright file="AddCmsSiteCultureCmdlet.cs" company="Chris Crutchfield">
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
    /// <para type="synopsis">Adds a culture to a specified site.</para>
    /// <para type="description">Adds a culture to a specified site based off of the provided input.</para>
    /// <para type="description">This cmdlet returns the newly modified site when the -PassThru switch is used.</para>
    /// <example>
    ///     <para>Add a culture with culture name "cul" to a specified site specifying the site name "*bas*", display name "*bas*", or a domain name "*bas*".</para>
    ///     <code>Add-CMSSiteCulture -SiteName "*bas*" -CultureName "cul"</code>
    /// </example>
    /// <example>
    ///     <para>Add a culture with culture name "cul" to a specified site specifying the site name "basic", display name "basic", or a domain name "basic".</para>
    ///     <code>Add-CMSSiteCulture -SiteName "basic" -EXACT -CultureName "cul"</code>
    /// </example>
    /// <example>
    ///     <para>Add a culture with culture name "cul" to a site.</para>
    ///     <code>$site | Add-CMSSiteCulture</code>
    /// </example>
    /// <example>
    ///     <para>Add a culture with culture name "cul" with the specified IDs.</para>
    ///     <code>Add-CMSSiteCulture -ID 1,2,3</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Add, "CMSSite")]
    [Alias("acsite")]
    public class AddCmsSiteCultureCmdlet : MefCmdlet
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
        public SiteInfo SiteToAdd { get; set; }

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
        public string CultureName { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for adding culture to this site.  Populated by MEF.
        /// </summary>
        [Import]
        public AddCmsSiteCultureBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case OBJECTSET:
                    this.BusinessLayer.AddCulture(this.SiteToAdd.ActLike<ISite>(), this.CultureName);
                    break;
                case SITENAMESET:
                    this.BusinessLayer.AddCulture(this.SiteName, this.Exact.ToBool(), this.CultureName);
                    break;
                case IDSETNAME:
                    this.BusinessLayer.AddCulture(this.ID, this.CultureName);
                    break;
            }
        }

        #endregion

    }
}

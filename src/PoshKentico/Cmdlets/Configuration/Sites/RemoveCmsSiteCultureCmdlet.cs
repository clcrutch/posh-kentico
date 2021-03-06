﻿// <copyright file="RemoveCmsSiteCultureCmdlet.cs" company="Chris Crutchfield">
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
    /// <para type="synopsis">Removes site cultures from specified sites.</para>
    /// <para type="description">Removes site cultures from specified sites based off of the provided input.</para>
    /// <example>
    ///     <para>Remove cultures with culture code "en-US, en-AU" from specified sites specifying the site name "basic", display name "basic", or a domain name "basic".</para>
    ///     <code>Remove-CMSSiteCulture -SiteName "basic" -CultureCodes "en-US, en-AU"</code>
    /// </example>
    /// <example>
    ///     <para>Remove cultures with culture code "en-US, en-AU" from specified sites specifying the site name "basic", display name "basic", or a domain name "basic".</para>
    ///     <code>Remove-CMSSiteCulture -SiteName "*basic*"  -CultureCodes "en-US, en-AU" -RegularExpression</code>
    /// </example>
    /// <example>
    ///     <para>Remove cultures with culture code "en-US, en-AU" to a specified site.</para>
    ///     <code>$site | Remove-CMSSiteCulture -CultureCodes "en-US, en-AU"</code>
    /// </example>
    /// <example>
    ///     <para>Remove cultures with culture code "en-US, en-AU" with the specified site IDs.</para>
    ///     <code>Remove-CMSSiteCulture -SiteIds 1,2,3 -CultureCodes "en-US, en-AU"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSSiteCulture", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [Alias("rscul")]
    public class RemoveCmsSiteCultureCmdlet : GetCmsSiteCmdlet
    {
        #region Constants

        private const string SITEOBJECTSET = "Object";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">A reference to the site.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = SITEOBJECTSET)]
        [Alias("Site")]
        public SiteInfo SiteToRemove { get; set; }

        /// <summary>
        /// <para type="description">The IDs of the site.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = NONE)]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = DISPLAYNAME)]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = IDSETNAME)]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = USEROBJECT)]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = SITEOBJECTSET)]
        public string[] CultureCodes { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for Removeing culture to this site.  Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCmsSiteCultureBusiness RemoveBusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            if (this.ParameterSetName == SITEOBJECTSET)
            {
                this.ActOnObject(this.SiteToRemove.ActLike<ISite>());
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

            foreach (string code in this.CultureCodes)
            {
                this.RemoveBusinessLayer.RemoveCulture(site, code);
            }
        }

        #endregion

    }
}

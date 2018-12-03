// <copyright file="AddCmsSiteDomainAliasCmdlet.cs" company="Chris Crutchfield">
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
    /// <para type="synopsis">Adds domain aliases to a specified site.</para>
    /// <para type="description">Adds domain aliases to a specified site based off of the provided input.</para>
    /// <example>
    ///     <para>Add domain aliases with alias name "alias" to a specified site specifying the site name "*bas*", display name "*bas*", or a domain name "*bas*".</para>
    ///     <code>Add-CMSSiteDomainAlias -SiteName "*bas*" -AliasNames "alias"</code>
    /// </example>
    /// <example>
    ///     <para>Add domain aliases with alias name "alias" to a specified site specifying the site name "basic", display name "basic", or a domain name "basic".</para>
    ///     <code>Add-CMSSiteDomainAlias -SiteName "basic" -AliasNames "alias" -RegularExpression </code>
    /// </example>
    /// <example>
    ///     <para>Add domain aliases with alias name "alias" to a site.</para>
    ///     <code>$site | Add-CMSSiteDomainAlias -AliasNames "alias"</code>
    /// </example>
    /// <example>
    ///     <para>Add domain aliases with alias name "alias" with the specified site IDs.</para>
    ///     <code>Add-CMSSiteDomainAlias -SiteIds 1,2,3 -AliasNames "alias"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Add, "CMSSiteDomainAlias", DefaultParameterSetName = NONE)]
    [Alias("aatosite")]
    public class AddCmsSiteDomainAliasCmdlet : GetCmsSiteCmdlet
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
        public SiteInfo SiteToAdd { get; set; }

        /// <summary>
        /// <para type="description">The alias to add to the site.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = NONE)]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = DISPLAYNAME)]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = IDSETNAME)]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = USEROBJECT)]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = SITEOBJECTSET)]
        public string[] AliasNames { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for adding domain alias to this site.  Populated by MEF.
        /// </summary>
        [Import]
        public AddCmsSiteDomainAliasBusiness AddBusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            if (this.ParameterSetName == SITEOBJECTSET)
            {
                this.ActOnObject(this.SiteToAdd.ActLike<ISite>());
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

            foreach (string alias in this.AliasNames)
            {
                this.AddBusinessLayer.AddDomainAlias(site, alias);
            }
        }
        #endregion

    }
}

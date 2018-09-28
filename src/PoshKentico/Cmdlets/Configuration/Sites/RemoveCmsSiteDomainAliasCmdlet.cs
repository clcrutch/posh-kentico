// <copyright file="RemoveCmsSiteDomainAliasCmdlet.cs" company="Chris Crutchfield">
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
    /// <para type="synopsis">Removes a domain alias to a specified site.</para>
    /// <para type="description">Removes a domain alias to a specified site based off of the provided input.</para>
    /// <example>
    ///     <para>Remove a domain alias with alias name "alias" to a specified site specifying the site name "*bas*", display name "*bas*", or a domain name "*bas*".</para>
    ///     <code>Remove-CMSSiteDomainAlias -SiteName "*bas*" -AliasName "alias"</code>
    /// </example>
    /// <example>
    ///     <para>Remove a domain alias with alias name "alias" to a specified site specifying the site name "basic", display name "basic", or a domain name "basic".</para>
    ///     <code>Remove-CMSSiteDomainAlias -SiteName "basic" -EXACT -AliasName "alias"</code>
    /// </example>
    /// <example>
    ///     <para>Remove a domain alias with alias name "alias" to a site.</para>
    ///     <code>$site | Remove-CMSSiteDomainAlias -AliasName "alias"</code>
    /// </example>
    /// <example>
    ///     <para>Remove a domain alias with alias name "alias" with the specified site IDs.</para>
    ///     <code>Remove-CMSSiteDomainAlias -ID 1,2,3 -AliasName "alias"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSSiteDomainAlias", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public class RemoveCmsSiteDomainAliasCmdlet : MefCmdlet
    {
        #region Constants

        private const string OBJECTSET = "Object";
        private const string SITENAMESET = "Property";
        private const string IDSETNAME = "ID";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">A reference to the site.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = OBJECTSET)]
        [Alias("Site")]
        public SiteInfo SiteToRemove { get; set; }

        /// <summary>
        /// <para type="description">The site name for the site.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = SITENAMESET)]
        public string SiteName { get; set; }

        /// <summary>
        /// <para type="description">If set, the match is exact, else the match performs a contains for site name.</para>
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
        public string AliasName { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for removing domain alias to this site.  Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCmsSiteDomainAliasBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case OBJECTSET:
                    this.BusinessLayer.RemoveDomainAlias(this.SiteToRemove.ActLike<ISite>(), this.AliasName);
                    break;
                case SITENAMESET:
                    this.BusinessLayer.RemoveDomainAlias(this.SiteName, this.Exact.ToBool(), this.AliasName);
                    break;
                case IDSETNAME:
                    this.BusinessLayer.RemoveDomainAlias(this.ID, this.AliasName);
                    break;
            }
        }

        #endregion

    }
}

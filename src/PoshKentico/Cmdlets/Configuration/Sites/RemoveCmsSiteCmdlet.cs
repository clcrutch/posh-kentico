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
using PoshKentico.Core.Services.Configuration.Sites;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.Sites
{
    /// <summary>
    /// <para type="synopsis">Deletes a site.</para>
    /// <para type="description">Deletes a site.</para>
    /// <example>
    ///     <para>Deletes all sites contains a site name "basic", display name "basic", or a domain name "basic".</para>
    ///     <code>Remove-CMSSite -SiteName "basic"</code>
    /// </example>
    /// <example>
    ///     <para>Deletes all sites with a site name "*basic*", display name "*basic*", or a domain name "*basic*".</para>
    ///     <code>Remove-CMSSite -SiteName "*basic*" -RegularExpression</code>
    /// </example>
    /// <example>
    ///     <para>Deletes a site.</para>
    ///     <code>$site | Remove-CMSSite</code>
    /// </example>
    /// <example>
    ///     <para>Delete all the sites with the specified IDs.</para>
    ///     <code>Remove-CMSSite -SiteIds 1,2,3</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSSite", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [Alias("rsite")]
    public class RemoveCmsSiteCmdlet : GetCmsSiteCmdlet
    {
        #region Constants

        private const string SITEOBJECTSET = "Object";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">A reference to the site to remove.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = SITEOBJECTSET)]
        [Alias("Site")]
        public SiteInfo SiteToRemove { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this site.  Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCmsSiteBusiness RemoveBusinessLayer { get; set; }

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

            this.RemoveBusinessLayer.Remove(site);
        }
        #endregion

    }
}

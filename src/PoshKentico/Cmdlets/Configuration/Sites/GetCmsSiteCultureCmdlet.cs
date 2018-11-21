// <copyright file="GetCmsSiteCultureCmdlet.cs" company="Chris Crutchfield">
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

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using CMS.SiteProvider;
using ImpromptuInterface;
using PoshKentico.Business.Configuration.Sites;
using PoshKentico.Core.Services.Configuration.Localization;
using PoshKentico.Core.Services.Configuration.Sites;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.Sites
{
    /// <summary>
    /// <para type="synopsis">Gets the cultures of the specified site.</para>
    /// <para type="description">Gets the cultures of the specified site based off of the provided input.</para>
    /// <example>
    ///     <para>Get all the sites.</para>
    ///     <code>Get-CMSSiteCulture</code>
    /// </example>
    /// <example>
    ///     <para>Get all sites with a display name "basic", site name "basic", or a domain name "basic".</para>
    ///     <code>Get-CMSSiteCulture basic</code>
    /// </example>
    /// <example>
    ///     <para>Get all sites with a display name "basic", site name "basic", or domain name "basic"</para>
    ///     <code>Get-CMSSiteCulture *basic* -RegularExpression</code>
    /// </example>
    /// <example>
    ///     <para>Get all the sites with the specified IDs.</para>
    ///     <code>Get-CMSSiteCulture -SiteIds 5,304,5</code>
    /// </example>
    /// <example>
    ///     <para>Get all the sites with the specified user.</para>
    ///     <code>Get-CMSSiteCulture -User $user</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Get, "CMSSiteCulture", DefaultParameterSetName = NONE)]
    [OutputType(typeof(CMS.Localization.CultureInfo[]))]
    [Alias("gscul")]
    public class GetCmsSiteCultureCmdlet : GetCmsSiteCmdlet
    {
        #region Properties

        /// <summary>
        ///  Gets or sets the Business Layer for adding culture to this site.  Populated by MEF.
        /// </summary>
        [Import]
        public GetCmsSiteCultureBusiness GetBusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ActOnObject(ISite site)
        {
            if (site == null)
            {
                return;
            }

            var cultures = this.GetBusinessLayer.GetCultures(site);

            foreach (var cul in cultures)
            {
                this.WriteObject(cul.UndoActLike());
            }
        }

        #endregion

    }
}

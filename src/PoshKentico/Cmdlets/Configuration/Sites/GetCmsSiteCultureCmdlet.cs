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
    /// <para type="description">This cmdlet returns the list of cultures when the -PassThru switch is used.</para>
    /// <example>
    ///     <para>Gets the cultures of a site specifying the site name "basic".</para>
    ///     <code>Get-CMSSiteCulture -SiteName "basic" </code>
    /// </example>
    /// <example>
    ///     <para>Gets the cultures of a site passing the site to the cmdlet.</para>
    ///     <code>$site | Get-CMSSiteCulture</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Get, "CMSSiteCulture")]
    [Alias("gscul")]
    public class GetCmsSiteCultureCmdlet : MefCmdlet
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
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = SITENAMESET)]
        public string SiteName { get; set; }

        /// <summary>
        /// <para type="description">A reference to the site.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = OBJECTSET)]
        [Alias("Site")]
        public SiteInfo SiteToWork { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for adding culture to this site.  Populated by MEF.
        /// </summary>
        [Import]
        public GetCmsSiteCultureBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<ICulture> cultures = null;

            switch (this.ParameterSetName)
            {
                case OBJECTSET:
                    cultures = this.BusinessLayer.GetCultures(this.SiteToWork.ActLike<ISite>());
                    break;
                case SITENAMESET:
                    cultures = this.BusinessLayer.GetCultures(this.SiteName);
                    break;
            }

            foreach (var culture in cultures)
            {
                this.WriteObject(culture.UndoActLike());
            }
        }

        #endregion

    }
}

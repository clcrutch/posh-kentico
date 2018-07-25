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
    ///     <para>Deletes a site.</para>
    ///     <code>Remove-CMSSite -SiteName "My site name to delete"</code>
    /// </example>
    /// <example>
    ///     <para>Deletes a site.</para>
    ///     <code>Remove-CMSSite -Site "My site to delete"</code>
    /// </example>
    /// <example>
    ///     <para>Deletes a site.</para>
    ///     <code>$webPartCategory | Remove-CMSSite</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSSite", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [Alias("rsite")]
    public class RemoveCmsSiteCmdlet : MefCmdlet
    {
        #region Constants

        private const string OBJECTSET = "Object";
        private const string PROPERTYSET = "Property";
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
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PROPERTYSET)]
        public string SiteName { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this site.  Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCMSSiteBusiness BusinessLayer { get; set; }

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
                case PROPERTYSET:
                    this.BusinessLayer.Remove(this.SiteName);
                    break;
            }
        }

        #endregion

    }
}

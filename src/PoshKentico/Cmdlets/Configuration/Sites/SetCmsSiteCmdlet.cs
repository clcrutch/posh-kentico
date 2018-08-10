// <copyright file="SetCmsSiteCmdlet.cs" company="Chris Crutchfield">
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
    /// <para type="synopsis">Sets a site.</para>
    /// <para type="description">Sets a new site based off of the provided input.</para>
    /// <para type="description">This cmdlet returns the site to update when the -PassThru switch is used.</para>
    /// <example>
    ///     <para>Set site specifying an existing site.</para>
    ///     <code>Set-CMSSite -Site "My Desired Site"</code>
    /// </example>
    /// <example>
    ///     <para>Set site specifying an existing site.</para>
    ///     <code>$site | Set-CMSSite</code>
    /// </example>
    /// <example>
    ///     <para>Set site specifying the display name, site name, status, and domain name.</para>
    ///     <code>Set-CMSSite -DisplayName "My Test Name" -SiteName "My Site Name" -Status "Running or Stopped" -DomainName "My Domain Name"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Set, "CMSSite")]
    [OutputType(typeof(SiteInfo[]), ParameterSetName = new string[] { PASSTHRU })]
    [Alias("ssite")]
    public class SetCmsSiteCmdlet : MefCmdlet
    {
        #region Constants

        private const string PASSTHRU = "PassThru";
        private const string OBJECTSET = "Object";
        private const string PROPERTYSET = "Property";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">A reference to the site to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = OBJECTSET)]
        [Alias("Site")]
        public SiteInfo SiteToSet { get; set; }

        /// <summary>
        /// <para type="description">The display name for the site to update.</para>
        /// <para type="description">Site display name cannot be blank.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PROPERTYSET)]
        public string DisplayName { get; set; }

        /// <summary>
        /// <para type="description">The site name for the site to update.</para>
        /// <para type="description">Site name cannot be blank.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = PROPERTYSET)]
        public string SiteName { get; set; }

        /// <summary>
        /// <para type="description">The status for the site to update. </para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = PROPERTYSET)]
        public SiteStatusEnum Status { get; set; }

        /// <summary>
        /// <para type="description">The domain name for the site to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 3, ParameterSetName = PROPERTYSET)]
        public string DomainName { get; set; }

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the site to update.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = PASSTHRU)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this site.  Populated by MEF.
        /// </summary>
        [Import]
        public SetCmsSiteBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case OBJECTSET:
                    this.BusinessLayer.Set(this.SiteToSet.ActLike<ISite>());
                    break;
                case PROPERTYSET:
                    this.SiteToSet = new SiteInfo
                    {
                        DisplayName = this.DisplayName,
                        SiteName = this.SiteName,
                        Status = this.Status,
                        DomainName = this.DomainName,
                    };
                    this.BusinessLayer.Set(this.SiteToSet.ActLike<ISite>());
                    break;
            }

            if (this.PassThru.ToBool())
            {
                this.WriteObject(this.SiteToSet);
            }
        }

        #endregion
    }
}

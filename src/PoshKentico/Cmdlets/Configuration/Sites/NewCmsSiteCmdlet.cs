// <copyright file="NewCmsSiteCmdlet.cs" company="Chris Crutchfield">
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
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.Sites
{
    /// <summary>
    /// <para type="synopsis">Creates a new site.</para>
    /// <para type="description">Creates a new site based off of the provided input.</para>
    /// <para type="description">This cmdlet returns the newly created site when the -PassThru switch is used.</para>
    /// <example>
    ///     <para>Create a new site specifying the display name and domain name.</para>
    ///     <code>New-CMSSite -DisplayName "My Test Name" -DomainName "My Domain Name"</code>
    /// </example>
    /// <example>
    ///     <para>Create a new site specifying the display name.</para>
    ///     <code>New-CMSSite -DisplayName "My Test Name" -SiteName "My Site Name" -Status "Running or Stopped" -DomainName "My Domain Name"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.New, "CMSSite")]
    [OutputType(typeof(SiteInfo), ParameterSetName = new string[] { PASSTHRU })]
    [Alias("nsite")]
    public class NewCmsSiteCmdlet : MefCmdlet
    {
        #region Constants

        private const string PASSTHRU = "PassThru";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The display name for the newly created site.</para>
        /// <para type="description">Site display name cannot be blank.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string DisplayName { get; set; }

        /// <summary>
        /// <para type="description">The display name for the newly created site.</para>
        /// <para type="description">If null, then the display name is used for the site name.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 1)]
        public string SiteName { get; set; }

        /// <summary>
        /// <para type="description">The status for the newly create site. </para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 2)]
        public string Status { get; set; }

        /// <summary>
        /// <para type="description">The domain name for the newly created site.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 3)]
        public string DomainName { get; set; }

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the newly created site.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = PASSTHRU)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this web part.  Populated by MEF.
        /// </summary>
        [Import]
        public NewCmsSiteBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            var newSite = this.BusinessLayer.CreateSite(this.DisplayName, this.SiteName, this.Status, this.DomainName);

            if (this.PassThru.ToBool())
            {
                this.WriteObject(newSite.UndoActLike());
            }
        }

        #endregion
    }
}

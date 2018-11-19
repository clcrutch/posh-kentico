// <copyright file="SetCmsMediaLibrarySecurityOptionCmdlet.cs" company="Chris Crutchfield">
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
using CMS.Helpers;
using CMS.MediaLibrary;
using ImpromptuInterface;
using PoshKentico.Business.ContentManagement.MediaLibraries;
using PoshKentico.Core.Services.ContentManagement.MediaLibraries;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.ContentManagement.MediaLibraries
{
    /// <summary>
    /// <para type="synopsis">Sets the media libraries security option by the provided input.</para>
    /// <para type="description">Sets the media libraries security option by the provided input. </para>
    /// <para type="description">Without parameters, this command sets all libraries.</para>
    /// <para type="description">With parameters, this command sets the libraries that match the criteria.</para>
    /// <example>
    ///     <para>Set the security option of all the libraries.</para>
    ///     <code>Set-CMSMediaLibrarySecurityOption -SecurityProperty Access -SecurityAccess AllUsers</code>
    /// </example>
    /// <example>
    ///     <para>Set the security option of all libraries with a site ID 1 and display name "*tes*".</para>
    ///     <code>Set-CMSMediaLibrarySecurityOption -SiteID 1 -DisplayName bas -SecurityProperty Access -SecurityAccess AllUsers</code>
    /// </example>
    /// <example>
    ///     <para>Set the security option of all libraries with a site ID 1 and display name "test"</para>
    ///     <code>Set-CMSMediaLibrarySecurityOption -SiteID 1 -DisplayName "test" -Exact -SecurityProperty Access -SecurityAccess AllUsers</code>
    /// </example>
    /// <example>
    ///     <para>Set the security option of all libraries with a site $site and display name "*tes*".</para>
    ///     <code>Set-CMSMediaLibrarySecurityOption -Site $site -DisplayName bas -SecurityProperty Access -SecurityAccess AllUsers</code>
    /// </example>
    /// <example>
    ///     <para>Set the security option of all libraries with a site $site and display name "test"</para>
    ///     <code>Set-CMSMediaLibrarySecurityOption -Site $site -DisplayName "test" -Exact -SecurityProperty Access -SecurityAccess AllUsers</code>
    /// </example>
    /// <example>
    ///     <para>Set the security option of all the libraries with the specified IDs.</para>
    ///     <code>Set-CMSMediaLibrarySecurityOption -ID 1,3 -SecurityProperty Access -SecurityAccess AllUsers</code>
    /// </example>
    /// <example>
    ///     <para>Set the security option of all the libraries with the specified IDs.</para>
    ///     <code>Set-CMSMediaLibrarySecurityOption -MediaLibrary $library -SecurityProperty Access -SecurityAccess AllUsers</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Set, "CMSMediaLibrarySecurityOption")]
    [Alias("smlsopt")]
    public class SetCmsMediaLibrarySecurityOptionCmdlet : GetCmsMediaLibraryCmdlet
    {
        #region Constants

        private const string MEDIALIBRARY = "Media Library";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The field to remove from Kentico.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = MEDIALIBRARY)]
        public MediaLibraryInfo MediaLibrary { get; set; }

        /// <summary>
        /// <para type="description">The security property enum for the media library.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public SecurityPropertyEnum SecurityProperty { get; set; }

        /// <summary>
        /// <para type="description">The security access enum for the media library.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public SecurityAccessEnum SecurityAccess { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this web part. Populated by MEF.
        /// </summary>
        [Import]
        public SetCmsMediaLibrarySecurityOptionBusiness SetOptionBusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            if (this.ParameterSetName == MEDIALIBRARY)
            {
                this.ActOnObject(this.MediaLibrary.ActLike<IMediaLibrary>());
            }
            else
            {
                base.ProcessRecord();
            }
        }

        /// <inheritdoc/>
        protected override void ActOnObject(IMediaLibrary library)
        {
            if (this.MediaLibrary != null)
            {
                library = this.MediaLibrary.ActLike<IMediaLibrary>();
            }

            this.SetOptionBusinessLayer.SetMediaLibrarySecurityOption(library, this.SecurityProperty, this.SecurityAccess);
        }

        #endregion
    }
}

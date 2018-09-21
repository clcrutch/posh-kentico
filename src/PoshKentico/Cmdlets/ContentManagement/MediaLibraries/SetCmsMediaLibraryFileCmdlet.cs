// <copyright file="SetCmsMediaLibraryFileCmdlet.cs" company="Chris Crutchfield">
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
using CMS.MediaLibrary;
using ImpromptuInterface;
using PoshKentico.Business.ContentManagement.MediaLibraries;
using PoshKentico.Core.Services.ContentManagement.MediaLibraries;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.ContentManagement.MediaLibraries
{
    /// <summary>
    /// <para type="synopsis">Sets a media file.</para>
    /// <para type="description">Sets a media file based off of the provided input.</para>
    /// <para type="description">This cmdlet returns the updated library file when the -PassThru switch is used.</para>
    /// <example>
    ///     <para>Set library specifying an existing library.</para>
    ///     <code>Set-CMSMediaLibraryFile -MediaFile $libraryFile</code>
    /// </example>
    /// <example>
    ///     <para>Set library specifying an existing library.</para>
    ///     <code>$libraryFile | Set-CMSMediaLibraryFile</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Set, "CMSMediaLibraryFile")]
    [OutputType(typeof(MediaFileInfo))]
    [Alias("smlfil")]
    public class SetCmsMediaLibraryFileCmdlet : MefCmdlet
    {
        #region Constants

        private const string PASSTHRU = "PassThru";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the media library file.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = PASSTHRU)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        /// <para type="description">The media library file to set.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public MediaFileInfo MediaFile { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this media library file. Populated by MEF.
        /// </summary>
        [Import]
        public SetCmsMediaLibraryFileBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            this.BusinessLayer.Set(this.MediaFile.ActLike<IMediaFile>());

            if (this.PassThru.ToBool())
            {
                this.WriteObject(this.MediaFile);
            }
        }

        #endregion

    }
}

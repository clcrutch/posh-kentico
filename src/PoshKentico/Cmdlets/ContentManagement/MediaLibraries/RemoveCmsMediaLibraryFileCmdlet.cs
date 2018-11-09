// <copyright file="RemoveCmsMediaLibraryFileCmdlet.cs" company="Chris Crutchfield">
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
    /// <para type="synopsis">Removes a list of library files.</para>
    /// <para type="description">Removes a list of library files based off of the provided input.</para>
    ///  <example>
    ///     <para>Remove the specified library file.</para>
    ///     <code>$libraryFile | Remove-CMSMediaLibraryFile</code>
    /// </example>
    ///  <example>
    ///     <para>Remove the specified library file.</para>
    ///     <code>Remove-CMSMediaLibraryFile -MediaFile $libraryFile</code>
    /// </example>
    /// <example>
    ///     <para>Remove the list of library files specifying an existing library, and with '*.png*' file extension and  from folder "*NewFolder*".</para>
    ///     <code>Remove-CMSMediaLibraryFile -MediaLibrary $library -Extension ".png" -FilePath "NewFolder"</code>
    /// </example>
    /// <example>
    ///     <para>Remove the list of library files specifying an existing library, and with '.png' file extension and  from folder "NewFolder". </para>
    ///     <code>Remove-CMSMediaLibraryFile -MediaLibrary $library -Extension ".png" -FilePath "NewFolder" -EXACT</code>
    /// </example>
    /// <example>
    ///     <para>Remove the list of library files specifying an existing library, and with '*.png*' file extension and  from folder "*NewFolder*".</para>
    ///     <code>$library | Remove-CMSMediaLibraryFile -Extension ".png" -FilePath "NewFolder"</code>
    /// </example>
    /// <example>
    ///     <para>Remove the list of library files specifying an existing library, and with '.png' file extension and  from folder "NewFolder". </para>
    ///     <code>$library | Remove-CMSMediaLibraryFile -Extension ".png" -FilePath "NewFolder" -EXACT</code>
    /// </example>
    /// <example>
    ///     <para>Remove the list of library files specifying an existing library, and with '*.png*' file extension and  from folder "*NewFolder*".</para>
    ///     <code>Remove-CMSMediaLibraryFile -LibraryID 1 -Extension ".png" -FilePath "NewFolder"</code>
    /// </example>
    /// <example>
    ///     <para>Remove the list of library files specifying an existing library, and with '.png' file extension and  from folder "NewFolder". </para>
    ///     <code>Remove-CMSMediaLibraryFile -LibraryID 1 -Extension ".png" -FilePath "NewFolder" -EXACT</code>
    /// </example>
    ///  <example>
    ///     <para>Remove all the library files with the specified IDs.</para>
    ///     <code>Remove-CMSMediaLibraryFile -ID 1,3</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSMediaLibraryFile", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [Alias("rmlfil")]
    public class RemoveCmsMediaLibraryFileCmdlet : GetCmsMediaLibraryFileCmdlet
    {
        #region Constants

        private const string MEDIAFILE = "Media File";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The field to remove from Kentico.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = MEDIAFILE)]
        public MediaFileInfo MediaFile { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this web part. Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCmsMediaLibraryFileBusiness RemoveBusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            if (this.ParameterSetName == MEDIAFILE)
            {
                this.ActOnObject(this.MediaFile.ActLike<IMediaFile>());
            }
            else
            {
                base.ProcessRecord();
            }
        }

        /// <inheritdoc/>
        protected override void ActOnObject(IMediaFile file)
        {
            if (this.MediaFile != null)
            {
                file = this.MediaFile.ActLike<IMediaFile>();
            }

            this.RemoveBusinessLayer.RemoveMediaFile(file);
        }

        #endregion
    }
}

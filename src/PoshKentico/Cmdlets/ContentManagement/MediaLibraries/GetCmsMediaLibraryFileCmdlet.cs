// <copyright file="GetCmsMediaLibraryFileCmdlet.cs" company="Chris Crutchfield">
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

using System;
using System.Collections.Generic;
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
    /// <para type="synopsis">Gets a list of library files.</para>
    /// <para type="description">Gets a list of library files based off of the provided input.</para>
    /// <para type="description">This cmdlet returns the library files when the -PassThru switch is used.</para>
    /// <example>
    ///     <para>Get the list of library files specifying an existing library, and with '*.png*' file extension and  from folder "*NewFolder*".</para>
    ///     <code>Get-CMSMediaLibraryFile -MediaLibrary $library -Extension ".png" -FilePath "NewFolder"</code>
    /// </example>
    /// <example>
    ///     <para>Get the list of library files specifying an existing library, and with '.png' file extension and  from folder "NewFolder". </para>
    ///     <code>Get-CMSMediaLibraryFile -MediaLibrary $library -FilePath "NewFolder/Image.png" -EXACT</code>
    /// </example>
    /// <example>
    ///     <para>Get the list of library files specifying an existing library, and with '*.png*' file extension and  from folder "*NewFolder*".</para>
    ///     <code>$library | Get-CMSMediaLibraryFile -Extension ".png" -FilePath "NewFolder"</code>
    /// </example>
    /// <example>
    ///     <para>Get the list of library files specifying an existing library, and with '.png' file extension and  from folder "NewFolder". </para>
    ///     <code>$library | Get-CMSMediaLibraryFile -FilePath "NewFolder/Image.png" -EXACT</code>
    /// </example>
    /// <example>
    ///     <para>Get the list of library files specifying an existing library, and with '*.png*' file extension and  from folder "*NewFolder*".</para>
    ///     <code>Get-CMSMediaLibraryFile -LibraryID 1 -Extension ".png" -FilePath "NewFolder"</code>
    /// </example>
    /// <example>
    ///     <para>Get the list of library files specifying an existing library, and with '.png' file extension and  from folder "NewFolder". </para>
    ///     <code>Get-CMSMediaLibraryFile -LibraryID 1 -FilePath "NewFolder/Image.png" -EXACT</code>
    /// </example>
    ///  <example>
    ///     <para>Get all the library files with the specified IDs.</para>
    ///     <code>Get-CMSMediaLibraryFile -ID 1,3</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Get, "CMSMediaLibraryFile", DefaultParameterSetName = NONE)]
    [OutputType(typeof(MediaFileInfo[]))]
    [Alias("gmlfil")]
    public class GetCmsMediaLibraryFileCmdlet : MefCmdlet
    {
        #region Constants

        private const string NONE = "None";
        private const string OBJECTSET = "Object";
        private const string PROPERTYSET = "Property";
        private const string IDSETNAME = "ID";

        #endregion
        #region Properties

        /// <summary>
        /// <para type="description">The associalted library for the library files to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = OBJECTSET)]
        public MediaLibraryInfo MediaLibrary { get; set; }

        /// <summary>
        /// <para type="description">The library ID of the library files to retrive.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = PROPERTYSET)]
        public int LibraryID { get; set; }

        /// <summary>
        /// <para type="description">The file extension of the library files to retrive.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Extension { get; set; }

        /// <summary>
        /// <para type="description">The file path of the library files to retrive.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string FilePath { get; set; }

        /// <summary>
        /// <para type="description">If set, the match is exact, else the match performs a contains for file extension and file path.</para>
        /// </summary>
        [Parameter(ParameterSetName = PROPERTYSET)]
        [Parameter(ParameterSetName = OBJECTSET)]
        public SwitchParameter Exact { get; set; }

        /// <summary>
        /// <para type="description">The IDs of the library file to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = IDSETNAME)]
        public int[] ID { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this media library service. Populated by MEF.
        /// </summary>
        [Import]
        public GetCmsMediaLibraryFileBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<IMediaFile> files = null;

            switch (this.ParameterSetName)
            {
                case OBJECTSET:
                    files = this.BusinessLayer.GetMediaFiles(this.MediaLibrary.LibraryID, this.Extension, this.FilePath, this.Exact.ToBool());
                    break;
                case PROPERTYSET:
                    files = this.BusinessLayer.GetMediaFiles(this.LibraryID, this.Extension, this.FilePath, this.Exact.ToBool());
                    break;
                case IDSETNAME:
                    files = this.BusinessLayer.GetMediaFiles(this.ID);
                    break;
                case NONE:
                    files = this.BusinessLayer.GetMediaFiles();
                    break;
            }

            foreach (var file in files)
            {
                this.ActOnObject(file);
            }
        }

        /// <summary>
        /// When overridden in a child class, operates on the specified action.
        /// </summary>
        /// <param name="file">The media library file to operate on.</param>
        protected virtual void ActOnObject(IMediaFile file)
        {
            this.WriteObject(file.UndoActLike());
        }
        #endregion
    }
}

// <copyright file="NewCmsMediaLibraryFileCmdlet.cs" company="Chris Crutchfield">
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
using System.IO;
using System.Management.Automation;
using CMS.MediaLibrary;
using ImpromptuInterface;
using PoshKentico.Business.ContentManagement.MediaLibraries;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.ContentManagement.MediaLibraries
{
    /// <summary>
    /// <para type="synopsis">Creates a media library file.</para>
    /// <para type="description">Creates a media library file based off of the provided input.</para>
    /// <example>
    ///     <para>Create a new media library file specifying the associated site id and library name for the library, and the new media file name.</para>
    ///     <code>New-CMSMediaLibraryFile -SiteID 1 -LibraryName "Name" -LocalFile $file -FileName "Image" -FileTitle "File title" -FilePath "NewFolder/Image/" -FileDescription "Description"</code>
    /// </example>
    /// <example>
    ///     <para>Create a new media library file specifying the associated library $library and the new media file name.</para>
    ///     <code>$file | New-CMSMediaLibraryFile -Library $library -FileName "Image" -FileTitle "File title" -FilePath "NewFolder/Image/" -FileDescription "Description"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.New, "CMSMediaLibraryFile")]
    [OutputType(typeof(MediaFileInfo))]
    [Alias("nmlfil")]
    public class NewCmsMediaLibraryFileCmdlet : MefCmdlet
    {
        #region Constants

        private const string OBJECTSET = "Object";
        private const string PROPERTYSET = "Property";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The associalted library for the new media file.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = OBJECTSET)]
        public MediaLibraryInfo Library { get; set; }

        /// <summary>
        /// <para type="description">The associalted library site id for the new media file.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PROPERTYSET)]
        public int SiteID { get; set; }

        /// <summary>
        /// <para type="description">The library name for the new media file.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = PROPERTYSET)]
        public string LibraryName { get; set; }

        /// <summary>
        /// <para type="description">The file name for the new media file.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public FileInfo LocalFile { get; set; }

        /// <summary>
        /// <para type="description">The file name for the new media file.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string FileName { get; set; }

        /// <summary>
        /// <para type="description">The file title for the new media file.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string FileTitle { get; set; }

        /// <summary>
        /// <para type="description">The file path for the new media file.</para>
        /// </summary>
        [Parameter(Mandatory = true)]
        public string FilePath { get; set; }

        /// <summary>
        /// <para type="description">The file description for the new media file.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string FileDescription { get; set; }

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the newly created library.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this library.  Populated by MEF.
        /// </summary>
        [Import]
        public NewCmsMediaLibraryFileBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            int siteID = this.ParameterSetName == PROPERTYSET ? this.SiteID : this.Library.LibrarySiteID;
            string libraryName = this.ParameterSetName == PROPERTYSET ? this.LibraryName : this.Library.LibraryName;

            var mediaFile = this.BusinessLayer.CreateMediaLibraryFile(siteID, libraryName, this.LocalFile.FullName, this.FileName, this.FileTitle, this.FileDescription, this.FilePath);

            if (this.PassThru.ToBool())
            {
                this.WriteObject(mediaFile.UndoActLike());
            }
        }

        #endregion
    }
}

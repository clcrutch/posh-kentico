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
using System.Management.Automation;
using CMS.MediaLibrary;
using PoshKentico.Business.ContentManagement.MediaLibraries;

namespace PoshKentico.Cmdlets.ContentManagement.MediaLibraries
{
    /// <summary>
    /// <para type="synopsis">Creates a media library file.</para>
    /// <para type="description">Creates a media library file based off of the provided input.</para>
    /// <example>
    ///     <para>Create a new media library file specifying the associated site id and library name for the library, and the new media file name.</para>
    ///     <code>New-CMSMediaLibraryFile -SiteID 1 -LibraryName "Name" -LocalFilePath "C:\Files\images\Image.png" -FileName "Image" -FileTitle "File title" -FilePath "NewFolder/Image/" -FileDescription "Description"</code>
    /// </example>
    /// <example>
    ///     <para>Create a new media library file specifying the associated library $library and the new media file name.</para>
    ///     <code>$library | New-CMSMediaLibraryFile -LocalFilePath "C:\Files\images\Image.png" -FileName "Image" -FileTitle "File title" -FilePath "NewFolder/Image/" -FileDescription "Description"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.New, "CMSMediaLibraryFile")]
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
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = OBJECTSET)]
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
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = PROPERTYSET)]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = OBJECTSET)]
        public string LocalFilePath { get; set; }

        /// <summary>
        /// <para type="description">The file name for the new media file.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 3, ParameterSetName = PROPERTYSET)]
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = OBJECTSET)]
        public string FileName { get; set; }

        /// <summary>
        /// <para type="description">The file title for the new media file.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 4, ParameterSetName = PROPERTYSET)]
        [Parameter(Mandatory = true, Position = 3, ParameterSetName = OBJECTSET)]
        public string FileTitle { get; set; }

        /// <summary>
        /// <para type="description">The file path for the new media file.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 5, ParameterSetName = PROPERTYSET)]
        [Parameter(Mandatory = true, Position = 4, ParameterSetName = OBJECTSET)]
        public string FilePath { get; set; }

        /// <summary>
        /// <para type="description">The file description for the new media file.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 6, ParameterSetName = PROPERTYSET)]
        [Parameter(Mandatory = false, Position = 5, ParameterSetName = OBJECTSET)]
        public string FileDescription { get; set; }

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the newly created library.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = OBJECTSET)]
        [Parameter(Mandatory = false, ParameterSetName = PROPERTYSET)]
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

            this.BusinessLayer.CreateMediaLibraryFile(siteID, libraryName, this.LocalFilePath, this.FileName, this.FileTitle, this.FileDescription, this.FilePath);
        }

        #endregion
    }
}

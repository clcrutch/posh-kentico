// <copyright file="NewCmsMediaLibraryFileBusiness.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.ContentManagement.MediaLibraries;

namespace PoshKentico.Business.ContentManagement.MediaLibraries
{
    /// <summary>
    /// Business layer for the New-CMSMediaLibraryFile cmdlet.
    /// </summary>
    [Export(typeof(NewCmsMediaLibraryFileBusiness))]
    public class NewCmsMediaLibraryFileBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the Media Library Service.  Populated by MEF.
        /// </summary>
        [Import]
        public IMediaLibraryService MediaLibraryService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a media library file <see cref="IMediaFile"/> within the <see cref="IMediaLibrary"/>.
        /// </summary>
        /// <param name="librarySiteID">The site id of the <see cref="IMediaLibrary"/>to retrive for creating the new file.</param>
        /// <param name="libraryName">The name of the <see cref="IMediaLibrary"/> to retrive for creating the new file.</param>
        /// <param name = "localFilePath" >The local file path for the <see cref="IMediaFile"/>.</param>
        /// <param name="fileName">The file name for the <see cref="IMediaFile"/>.</param>
        /// <param name="fileTitle">The file title for the <see cref="IMediaFile"/>.</param>
        /// <param name="fileDesc">The file description for the <see cref="IMediaFile"/>.</param>
        /// <param name="filePath">The file path for the <see cref="IMediaFile"/>.</param>
        /// <returns>The newly created <see cref="IMediaFile"/>.</returns>
        public IMediaFile CreateMediaLibraryFile(
                                                int librarySiteID,
                                                string libraryName,
                                                string localFilePath,
                                                string fileName,
                                                string fileTitle,
                                                string fileDesc,
                                                string filePath)
        {
            return this.MediaLibraryService.CreateMediaFile(librarySiteID, libraryName, localFilePath, fileName, fileTitle, fileDesc, filePath);
        }

        #endregion
    }
}

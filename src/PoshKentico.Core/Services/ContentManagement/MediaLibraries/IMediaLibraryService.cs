// <copyright file="IMediaLibraryService.cs" company="Chris Crutchfield">
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

using System.Collections.Generic;
using CMS.MediaLibrary;

namespace PoshKentico.Core.Services.ContentManagement.MediaLibraries
{
    /// <summary>
    /// Service for providing access to a CMS Media Library.
    /// </summary>
    public interface IMediaLibraryService
    {
        #region Properties

        /// <summary>
        /// Gets a list of all of the <see cref="IMediaLibrary"/> provided by the CMS System.
        /// </summary>
        IEnumerable<IMediaLibrary> MediaLibraries { get; }

        /// <summary>
        /// Gets a list of all of the <see cref="IMediaFile"/> provided by the CMS System.
        /// </summary>
        IEnumerable<IMediaFile> MediaFiles { get; }
        #endregion

        #region Methods

        /// <summary>
        /// Creates the <see cref="IMediaLibrary"/>.
        /// </summary>
        /// <param name="library">The <see cref="IMediaLibrary"/> to create.</param>
        /// <returns>The newly created <see cref="IMediaLibrary"/>.</returns>
        IMediaLibrary CreateMediaLibrary(IMediaLibrary library);

        /// <summary>
        /// Gets the <see cref="IMediaLibrary"/> which matches the supplied ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="IMediaLibrary"/> to return.</param>
        /// <returns>The <see cref="IMediaLibrary"/> which matches the ID, else null.</returns>
        IMediaLibrary GetMediaLibrary(int id);

        /// <summary>
        /// Gets the <see cref="IMediaLibrary"/> which matches the supplied library name.
        /// </summary>
        /// <param name="librarySiteID">The site id of the <see cref="IMediaLibrary"/>to return.</param>
        /// <param name="libraryName">The name of the <see cref="IMediaLibrary"/> to return.</param>
        /// <returns>the media library info object.</returns>
        IMediaLibrary GetMediaLibrary(int librarySiteID, string libraryName);

        /// <summary>
        /// Updates the <see cref="IMediaLibrary"/>.
        /// </summary>
        /// <param name="library">The <see cref="IMediaLibrary"/> to update.</param>
        /// <param name="isReplace">to indicate if replace the complete object or update only the properties.</param>
        /// <returns>The updated library.</returns>
        IMediaLibrary UpdateMediaLibrary(IMediaLibrary library, bool isReplace = true);

        /// <summary>
        /// Deletes the specified <see cref="IMediaLibrary"/>.
        /// </summary>
        /// <param name="library">The <see cref="IMediaLibrary"/> to delete.</param>
        void DeleteMediaLibrary(IMediaLibrary library);

        /// <summary>
        /// Creates a media library folder within the <see cref="IMediaLibrary"/>.
        /// </summary>
        /// <param name="librarySiteID">The site id of the <see cref="IMediaLibrary"/>to retrive for creating the new folder.</param>
        /// <param name="libraryName">The name of the <see cref="IMediaLibrary"/> to retrive for creating the new folder.</param>
        /// <param name="folderName">The new folder name to create within the media library.</param>
        void CreateMediaFolder(int librarySiteID, string libraryName, string folderName);

        /// <summary>
        /// Creates a media library file within the <see cref="IMediaLibrary"/>.
        /// </summary>
        /// <param name="librarySiteID">The site id of the <see cref="IMediaLibrary"/>to retrive for creating the new file.</param>
        /// <param name="libraryName">The name of the <see cref="IMediaLibrary"/> to retrive for creating the new file.</param>
        /// <param name = "localFilePath" >The local file path for the <see cref="IMediaFile"/>.</param>
        /// <param name="fileName">The file name for the <see cref="IMediaFile"/>.</param>
        /// <param name="fileTitle">The file title for the <see cref="IMediaFile"/>.</param>
        /// <param name="fileDesc">The file description for the <see cref="IMediaFile"/>.</param>
        /// <param name="filePath">The file path for the <see cref="IMediaFile"/>.</param>
        /// <returns>The newly created <see cref="IMediaFile"/>.</returns>
        IMediaFile CreateMediaFile(int librarySiteID, string libraryName, string localFilePath, string fileName, string fileTitle, string fileDesc, string filePath);

        /// <summary>
        /// Gets the <see cref="IMediaFile"/> which matches the supplied ID.
        /// </summary>
        /// <param name="mediaFileId">The media file ID of the <see cref="IMediaFile"/>.</param>
        /// <returns>The <see cref="IMediaFile"/> which matches the ID, else null.</returns>
        IMediaFile GetMediaFile(int mediaFileId);

        /// <summary>
        /// Gets the <see cref="IMediaFile"/> which matches the supplied library <see cref="IMediaLibrary"/> and file path.
        /// </summary>
        /// <param name="library">The specified <see cref="IMediaLibrary"/> to look for the media file.</param>
        /// <param name="folder">The folder of the media file.</param>
        /// <param name="fileName">The file name of the media file.</param>
        /// <returns>The list of <see cref="IMediaFile"/> which matches the input, else null.</returns>
        IMediaFile GetMediaFile(IMediaLibrary library, string folder, string fileName);

        /// <summary>
        /// Updates the <see cref="IMediaFile"/>.
        /// </summary>
        /// <param name="file">The <see cref="IMediaFile"/> to update.</param>
        /// <returns>The updated media file.</returns>
        IMediaFile UpdateMediaFile(IMediaFile file);

        /// <summary>
        /// Deletes the specified <see cref="IMediaFile"/>.
        /// </summary>
        /// <param name="file">The <see cref="IMediaFile"/> to delete.</param>
        void DeleteMediaFile(IMediaFile file);

        /// <summary>
        /// Deletes a media library folder within the <see cref="IMediaLibrary"/>.
        /// </summary>
        /// <param name="librarySiteID">The site id of the <see cref="IMediaLibrary"/>to retrive for creating the new folder.</param>
        /// <param name="libraryName">The name of the <see cref="IMediaLibrary"/> to retrive for creating the new folder.</param>
        /// <param name="folderName">The new folder name to create within the media library.</param>
        void DeleteMediaFolder(int librarySiteID, string libraryName, string folderName);
        #endregion

    }
}

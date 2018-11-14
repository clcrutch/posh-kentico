// <copyright file="KenticoMediaLibraryService.cs" company="Chris Crutchfield">
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
using System.Linq;
using CMS.Helpers;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using ImpromptuInterface;
using PoshKentico.Core.Services.ContentManagement.MediaLibraries;

namespace PoshKentico.Core.Configuration.ContentManagement.MediaLibraries
{
    /// <summary>
    /// Implementation of <see cref="IMediaLibraryService"/> that uses Kentico.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Export(typeof(IMediaLibraryService))]
    public class KenticoMediaLibraryService : IMediaLibraryService
    {
        #region Properties

        /// <inheritdoc/>
        public IEnumerable<IMediaLibrary> MediaLibraries => (from c in MediaLibraryInfoProvider.GetMediaLibraries()
                                                            select Impromptu.ActLike<IMediaLibrary>(c as MediaLibraryInfo)).ToArray();

        /// <inheritdoc/>
        public IEnumerable<IMediaFile> MediaFiles => (from f in MediaFileInfoProvider.GetMediaFiles()
                                                      select Impromptu.ActLike<IMediaFile>(SetFileBinary(f))).ToArray();

        #endregion

        #region Methods

        /// <inheritdoc/>
        public IMediaLibrary CreateMediaLibrary(IMediaLibrary library)
        {
            // Creates a new media library object
            MediaLibraryInfo newLibrary = new MediaLibraryInfo
            {
                // Sets the library properties
                LibraryDisplayName = library.LibraryDisplayName,
                LibraryName = library.LibraryName,
                LibraryDescription = library.LibraryDescription,
                LibraryFolder = library.LibraryFolder,
                LibrarySiteID = library.LibrarySiteID,
            };

            // Saves the new media library to the database
            MediaLibraryInfoProvider.SetMediaLibraryInfo(newLibrary);

            return newLibrary.ActLike<IMediaLibrary>();
        }

        /// <inheritdoc/>
        public string CreateMediaFolder(int librarySiteID, string libraryName, string folderName)
        {
            // Gets the media library
            string siteName = SiteInfoProvider.GetSiteName(librarySiteID);
            var existingLibrary = MediaLibraryInfoProvider.GetMediaLibraryInfo(libraryName, siteName);

            if (existingLibrary != null)
            {
                // Creates the "NewFolder" folder within the media library
                MediaLibraryInfoProvider.CreateMediaLibraryFolder(siteName, existingLibrary.LibraryID, folderName);
            }

            return folderName;
        }

        /// <inheritdoc/>
        public IMediaFile CreateMediaFile(int librarySiteID, string libraryName, string localFilePath, string fileName, string fileTitle, string fileDesc, string folder)
        {
            // Gets the media library
            string siteName = SiteInfoProvider.GetSiteName(librarySiteID);
            var existingLibrary = MediaLibraryInfoProvider.GetMediaLibraryInfo(libraryName, siteName);

            if (existingLibrary != null)
            {
                // Prepares a CMS.IO.FileInfo object representing the local file
                CMS.IO.FileInfo file = CMS.IO.FileInfo.New(localFilePath);
                if (file != null)
                {
                    // Creates a new media library file object
                    MediaFileInfo mediaFile = new MediaFileInfo(localFilePath, existingLibrary.LibraryID)
                    {
                        // Sets the media library file properties
                        FileName = fileName,
                        FileTitle = fileTitle,
                        FileDescription = fileDesc,
                        FilePath = $"{folder}/{fileName}", // Sets the path within the media library's folder structure
                        FileExtension = file.Extension,
                        FileMimeType = MimeTypeHelper.GetMimetype(file.Extension),
                        FileSiteID = librarySiteID,
                        FileLibraryID = existingLibrary.LibraryID,
                        FileSize = file.Length,
                    };

                    // Saves the media library file
                    MediaFileInfoProvider.SetMediaFileInfo(mediaFile);

                    return mediaFile.ActLike<IMediaFile>();
                }
            }

            return null;
        }

        /// <inheritdoc/>
        public IMediaLibrary GetMediaLibrary(int id)
        {
            return (MediaLibraryInfoProvider.GetMediaLibraryInfo(id) as MediaLibraryInfo)?.ActLike<IMediaLibrary>();
        }

        /// <inheritdoc/>
        public IMediaLibrary GetMediaLibrary(int librarySiteID, string libraryName)
        {
            string siteName = SiteInfoProvider.GetSiteName(librarySiteID);
            return (MediaLibraryInfoProvider.GetMediaLibraryInfo(libraryName, siteName) as MediaLibraryInfo)?.ActLike<IMediaLibrary>();
        }

        /// <inheritdoc/>
        public IMediaFile GetMediaFile(int mediaFileId)
        {
            MediaFileInfo file = MediaFileInfoProvider.GetMediaFileInfo(mediaFileId);
            file = SetFileBinary(file);
            return file?.ActLike<IMediaFile>();
        }

        /// <inheritdoc/>
        public IMediaFile GetMediaFile(IMediaLibrary library, string folder, string fileName)
        {
            MediaFileInfo mediaFile = null;

            // Gets the media library
            var existingLibrary = GetMediaLibrary(library);

            if (existingLibrary != null)
            {
                // Gets the media file
                mediaFile = MediaFileInfoProvider.GetMediaFileInfo(existingLibrary.LibraryID, $"{folder}/{fileName}");
            }

            return mediaFile.ActLike<IMediaFile>();
        }

        /// <inheritdoc/>
        public IMediaLibrary UpdateMediaLibrary(IMediaLibrary library, bool isReplace = true)
        {
            var updateLibrary = GetMediaLibrary(library);
            if (updateLibrary != null)
            {
                if (isReplace)
                {
                    updateLibrary = library.UndoActLike();
                }
                else
                {
                    // Updates the library properties
                    updateLibrary.LibraryDisplayName = library.LibraryDisplayName ?? updateLibrary.LibraryDisplayName;
                    updateLibrary.LibraryDescription = library.LibraryDescription ?? updateLibrary.LibraryDescription;
                    updateLibrary.LibraryFolder = library.LibraryFolder ?? updateLibrary.LibraryFolder;
                }

                // Saves the updated library to the database
                MediaLibraryInfoProvider.SetMediaLibraryInfo(updateLibrary);
            }

            return updateLibrary.ActLike<IMediaLibrary>();
        }

        /// <inheritdoc/>
        public IMediaFile UpdateMediaFile(IMediaFile file)
        {
            MediaFileInfo mediaFileInfo = MediaFileInfoProvider.GetMediaFileInfo(file.FileID);

            if (mediaFileInfo == null)
            {
                return null;
            }

            MediaFileInfoProvider.SetMediaFileInfo(mediaFileInfo);

            return mediaFileInfo.ActLike<IMediaFile>();
        }

        /// <inheritdoc/>
        public void DeleteMediaLibrary(IMediaLibrary library)
        {
            // Gets the media library
            var deleteLibrary = GetMediaLibrary(library);

            if (deleteLibrary != null)
            {
                // Deletes the media library
                MediaLibraryInfoProvider.DeleteMediaLibraryInfo(deleteLibrary);
            }
        }

        /// <inheritdoc/>
        public void DeleteMediaFile(IMediaFile file)
        {
            MediaFileInfo mediaFileInfo = MediaFileInfoProvider.GetMediaFileInfo(file.FileID);

            if (mediaFileInfo == null)
            {
                return;
            }

            // Deletes the media file
            MediaFileInfoProvider.DeleteMediaFileInfo(mediaFileInfo);
        }

        /// <inheritdoc/>
        public void DeleteMediaFolder(int librarySiteID, string libraryName, string folderName)
        {
            // Gets the media library
            string siteName = SiteInfoProvider.GetSiteName(librarySiteID);
            var existingLibrary = MediaLibraryInfoProvider.GetMediaLibraryInfo(libraryName, siteName);

            if (existingLibrary != null)
            {
                // Deletes the "NewFolder" folder within the media library
                MediaLibraryInfoProvider.DeleteMediaLibraryFolder(siteName, existingLibrary.LibraryID, folderName, false);
            }
        }

        /// <inheritdoc/>
        public void SetMediaLibrarySecurityOption(IMediaLibrary library, SecurityPropertyEnum option, SecurityAccessEnum securityAccess)
        {
            // Gets the media library
            var existingLibrary = GetMediaLibrary(library);

            if (existingLibrary != null)
            {
                // Get security property name from enum
                string propName = Enum.GetName(typeof(SecurityPropertyEnum), option);

                // Set security property value using reflection
                existingLibrary.GetType().GetProperty(propName).SetValue(existingLibrary, securityAccess);

                // Saves the updated media library to the database
                MediaLibraryInfoProvider.SetMediaLibraryInfo(existingLibrary);
            }
        }

        /// <summary>
        /// Gets the media library info object.
        /// </summary>
        /// <param name="library">the interface <see cref="IMediaLibrary"/>.</param>
        /// <returns>the media library info object.</returns>
        private static MediaLibraryInfo GetMediaLibrary(IMediaLibrary library)
        {
            string siteName = SiteInfoProvider.GetSiteName(library.LibrarySiteID);
            return MediaLibraryInfoProvider.GetMediaLibraryInfo(library.LibraryName, siteName);
        }

        /// <summary>
        /// Sets the media library file binary.
        /// </summary>
        /// <param name="file">the file to set the binary. </param>
        /// <returns>the updated media library info object.</returns>
        private static MediaFileInfo SetFileBinary(MediaFileInfo file)
        {
            if (file != null)
            {
                var siteName = SiteInfoProvider.GetSiteName(file.FileSiteID);
                var path = $"{CMS.Base.SystemContext.WebApplicationPhysicalPath}\\{siteName}\\media\\Images\\{file.FilePath}";
                if (CMS.IO.File.Exists(path))
                {
                    file.FileBinary = CMS.IO.File.ReadAllBytes(path);
                }
            }

            return file;
        }
        #endregion

    }
}

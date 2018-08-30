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

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using ImpromptuInterface;
using PoshKentico.Core.Services.ContentManagement.MediaLibraries;

namespace PoshKentico.Core.Configuration.ContentManagement.MediaLibraries
{
    /// <summary>
    /// Implementation of <see cref="IMediaLibraryService"/> that uses Kentico.
    /// </summary>
    [Export(typeof(IMediaLibraryService))]
    public class KenticoMediaLibraryService : IMediaLibraryService
    {
        #region Properties

        /// <summary>
        /// Gets a list of all of the <see cref="IMediaLibrary"/> provided by the CMS System.
        /// </summary>
        public IEnumerable<IMediaLibrary> MediaLibrarys => (from c in MediaLibraryInfoProvider.GetMediaLibraries()
                                              select Impromptu.ActLike<IMediaLibrary>(c as MediaLibraryInfo)).ToArray();

        #endregion

        #region Methods

        /// <inheritdoc/>
        public IMediaLibrary Create(IMediaLibrary library)
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
        public IMediaLibrary GetMediaLibrary(int id)
        {
            return (MediaLibraryInfoProvider.GetMediaLibraryInfo(id) as MediaLibraryInfo)?.ActLike<IMediaLibrary>();
        }

        /// <inheritdoc/>
        public IMediaLibrary GetMediaLibrary(string libraryName, string librarySiteName)
        {
            return (MediaLibraryInfoProvider.GetMediaLibraryInfo(libraryName, librarySiteName) as MediaLibraryInfo)?.ActLike<IMediaLibrary>();
        }

        /// <inheritdoc/>
        public IMediaLibrary Update(IMediaLibrary library, bool isReplace = true)
        {
            string siteName = SiteInfoProvider.GetSiteName(library.LibrarySiteID);
            var updateLibrary = MediaLibraryInfoProvider.GetMediaLibraryInfo(library.LibraryName, siteName);
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
                }

                // Saves the updated library to the database
                MediaLibraryInfoProvider.SetMediaLibraryInfo(updateLibrary);
            }

            return updateLibrary.ActLike<IMediaLibrary>();
        }

        /// <inheritdoc/>
        public void Delete(IMediaLibrary library)
        {
            // Gets the media library
            string siteName = SiteInfoProvider.GetSiteName(library.LibrarySiteID);
            var deleteLibrary = MediaLibraryInfoProvider.GetMediaLibraryInfo(library.LibraryName, siteName);

            if (deleteLibrary != null)
            {
                // Deletes the media library
                MediaLibraryInfoProvider.DeleteMediaLibraryInfo(deleteLibrary);
            }
        }

        #endregion

    }
}

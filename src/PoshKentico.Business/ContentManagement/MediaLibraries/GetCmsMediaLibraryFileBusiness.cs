// <copyright file="GetCmsMediaLibraryFileBusiness.cs" company="Chris Crutchfield">
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
using System.Linq;
using PoshKentico.Core.Services.ContentManagement.MediaLibraries;

namespace PoshKentico.Business.ContentManagement.MediaLibraries
{
    /// <summary>
    /// Business Layer for Get-CMSMediaLibraryFile Cmdlet.
    /// </summary>
    [Export(typeof(GetCmsMediaLibraryFileBusiness))]
    public class GetCmsMediaLibraryFileBusiness : CmdletBusinessBase
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
        /// Gets a list of all of the <see cref="IMediaFile"/> in the CMS System.
        /// </summary>
        /// <returns>A list of all of the <see cref="IMediaFile"/>.</returns>
        public IEnumerable<IMediaFile> GetMediaFiles()
        {
            return this.MediaLibraryService.MediaFiles;
        }

        /// <summary>
        /// Gets a list of all of the <see cref="IMediaFile"/> which match the specified criteria.
        /// </summary>
        /// <param name="libraryID">The library id to match the files to.</param>
        /// <param name="extension">The file extension which to match the files to.</param>
        /// <param name="filePath">The string which to match the files to.</param>
        /// <param name="exact">A boolean which indicates if the match should be exact.</param>
        /// <returns>A list of all of the <see cref="IMediaFile"/> which match the specified criteria.</returns>
        public IEnumerable<IMediaFile> GetMediaFiles(int libraryID, string extension, string filePath, bool exact)
        {
            if (exact)
            {
                var res = this.MediaLibraryService.MediaFiles.Select(c => c).Where(c => c.FileLibraryID == libraryID);
                if (extension != null)
                {
                    res = res.Select(x => x).Where(x => x.FileExtension.ToLowerInvariant() == extension.ToLowerInvariant());
                }

                if (filePath != null)
                {
                    res = res.Select(x => x).Where(x => x.FilePath.ToLowerInvariant() == filePath.ToLowerInvariant());
                }

                return res;
            }
            else
            {
                var lowerExtension = extension == null ? string.Empty : extension.ToLowerInvariant();
                var lowerFilePath = filePath == null ? string.Empty : filePath.ToLowerInvariant();

                return (from c in this.MediaLibraryService.MediaFiles
                        where c.FileLibraryID == libraryID && (
                            c.FileExtension.ToLowerInvariant().Contains(lowerExtension) &&
                            c.FilePath.ToLowerInvariant().Contains(lowerFilePath))
                        select c).ToArray();
            }
        }

        /// <summary>
        /// Gets a list of the <see cref="IMediaFile"/> which match the supplied IDs.
        /// </summary>
        /// <param name="ids">The IDs of the <see cref="IMediaFile"/> to return.</param>
        /// <returns>A list of the <see cref="IMediaFile"/> which match the supplied IDs.</returns>
        public IEnumerable<IMediaFile> GetMediaFiles(params int[] ids)
        {
            var files = from id in ids select this.MediaLibraryService.GetMediaFile(id);

            return (from file in files
                    where file != null
                    select file).ToArray();
        }

        #endregion

    }
}

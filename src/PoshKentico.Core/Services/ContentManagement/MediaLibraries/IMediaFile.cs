// <copyright file="IMediaFile.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Core.Services.ContentManagement.MediaLibraries
{
    /// <summary>
    /// Represents a Media Library File Object.
    /// </summary>
    public interface IMediaFile
    {
        /// <summary>
        /// Gets the media file name.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Gets the media file title.
        /// </summary>
        string FileTitle { get; }

        /// <summary>
        /// Gets the media file path.
        /// </summary>
        string FilePath { get; }

        /// <summary>
        /// Gets the media file description.
        /// </summary>
        string FileDescription { get; }

        /// <summary>
        /// Gets the media file extention.
        /// </summary>
        string FileExtension { get; }

        /// <summary>
        /// Gets the mime type for the media file.
        /// </summary>
        string FileMimeType { get; }

        /// <summary>
        /// Gets the file size for the media file.
        /// </summary>
        long FileSize { get; }

        /// <summary>
        /// Gets the file id for the media file.
        /// </summary>
        int FileID { get; }

        /// <summary>
        /// Gets the site id for the media file.
        /// </summary>
        int FileSiteID { get; }

        /// <summary>
        /// Gets the media library id for the media file.
        /// </summary>
        int FileLibraryID { get; }

        /// <summary>
        /// Gets the file binary for the media file.
        /// </summary>
        byte[] FileBinary { get; }
    }
}

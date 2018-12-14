﻿// <copyright file="IMediaLibrary.cs" company="Chris Crutchfield">
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
    /// Represents a Media Library Object.
    /// </summary>
    public interface IMediaLibrary
    {
        /// <summary>
        /// Gets the display name for the media library.
        /// </summary>
        string LibraryDisplayName { get; }

        /// <summary>
        /// Gets the media library name.
        /// </summary>
        string LibraryName { get; }

        /// <summary>
        /// Gets the media library description.
        /// </summary>
        string LibraryDescription { get; }

        /// <summary>
        /// Gets the media library folder.
        /// </summary>
        string LibraryFolder { get; }

        /// <summary>
        /// Gets the site id for the media library.
        /// </summary>
        int LibrarySiteID { get; }

        /// <summary>
        /// Gets the library id for the media library.
        /// </summary>
        int LibraryID { get; }
    }
}

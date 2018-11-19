// <copyright file="MediaLibraryMock.cs" company="Chris Crutchfield">
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

using System.Diagnostics.CodeAnalysis;
using CMS.Helpers;
using PoshKentico.Core.Services.ContentManagement.MediaLibraries;

namespace PoshKentico.Tests.ContentManagement.MediaLibraries
{
    [ExcludeFromCodeCoverage]
    public class MediaLibraryMock : IMediaLibrary
    {
        /// <summary>
        /// Gets or Sets the display name for the media library.
        /// </summary>
        public string LibraryDisplayName { get; set; }

        /// <summary>
        /// Gets or Sets the media library name.
        /// </summary>
        public string LibraryName { get; set; }

        /// <summary>
        /// Gets or Sets the media library description.
        /// </summary>
        public string LibraryDescription { get; set; }

        /// <summary>
        /// Gets or Sets the media library folder.
        /// </summary>
        public string LibraryFolder { get; set; }

        /// <summary>
        /// Gets or Sets the site id for the media library.
        /// </summary>
        public int LibrarySiteID { get; set; }

        /// <summary>
        /// Gets or Sets the library id for the media library.
        /// </summary>
        public int LibraryID { get; set; }

        /// <summary>
        /// Gets or Sets whether the access to library is allowed.
        /// </summary>
        public SecurityAccessEnum Access { get; set; }
    }
}

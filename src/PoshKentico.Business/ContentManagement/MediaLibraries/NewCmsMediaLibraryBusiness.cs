// <copyright file="NewCmsMediaLibraryBusiness.cs" company="Chris Crutchfield">
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
using ImpromptuInterface;
using PoshKentico.Core.Services.ContentManagement.MediaLibraries;

namespace PoshKentico.Business.ContentManagement.MediaLibraries
{
    /// <summary>
    /// Business layer for the New-CMSMediaLibrary cmdlet.
    /// </summary>
    [Export(typeof(NewCmsMediaLibraryBusiness))]
    public class NewCmsMediaLibraryBusiness : CmdletBusinessBase
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
        /// Creates a new Media Library in the CMS System.
        /// </summary>
        /// <param name="displayName">The display name of the media library</param>
        /// <param name="name">The name of the media library</param>
        /// <param name="description">the description of the media library</param>
        /// <param name="folder">the folder of the media library</param>
        /// <param name="siteID">the site id of the media library</param>
        /// <returns>The newly created Media Library.</returns>
        public IMediaLibrary CreateMediaLibrary(string displayName, string name, string description, string folder, int siteID)
        {
            var data = new
            {
                LibraryDisplayName = displayName,
                LibraryName = name,
                LibraryDescription = description,
                LibraryFolder = folder,
                LibrarySiteID = siteID,
            };

            return this.MediaLibraryService.CreateMediaLibrary(data.ActLike<IMediaLibrary>());
        }

        #endregion

    }
}

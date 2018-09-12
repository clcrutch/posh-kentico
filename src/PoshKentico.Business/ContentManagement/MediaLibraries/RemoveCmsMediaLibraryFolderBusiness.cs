// <copyright file="RemoveCmsMediaLibraryFolderBusiness.cs" company="Chris Crutchfield">
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
    /// Business layer for the Remove-CMSMediaLibraryFolder cmdlet.
    /// </summary>
    [Export(typeof(RemoveCmsMediaLibraryFolderBusiness))]
    public class RemoveCmsMediaLibraryFolderBusiness : CmdletBusinessBase
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
        /// Removes a new Media Library Folder in the CMS System.
        /// </summary>
        /// <param name="siteID">the site id of the media library.</param>
        /// <param name="name">The name of the media library.</param>
        /// <param name="folder">The name of the newly created folder.</param>
        public void RemoveMediaLibraryFolder(int siteID, string name, string folder)
        {
            this.MediaLibraryService.DeleteMediaFolder(siteID, name, folder);
        }

        #endregion
    }
}

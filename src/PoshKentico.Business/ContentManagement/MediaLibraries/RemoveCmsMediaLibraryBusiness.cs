// <copyright file="RemoveCmsMediaLibraryBusiness.cs" company="Chris Crutchfield">
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
    /// Business Layer for Remove-CMSMediaLibrary cmdlet.
    /// </summary>
    [Export(typeof(RemoveCmsMediaLibraryBusiness))]
    public class RemoveCmsMediaLibraryBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the Media Library Service.  Populated by MEF.
        /// </summary>
        [Import]
        public IMediaLibraryService MediaLibraryService { get; set; }

        /// <summary>
        /// Gets or sets a reference to the <see cref="GetCmsMediaLibraryBusiness"/> used to get the library to delete.  Populated by MEF.
        /// </summary>
        [Import]
        public GetCmsMediaLibraryBusiness GetCmsMediaLibraryBusiness { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes the <see cref="IMediaLibrary"/> in the CMS System.
        /// </summary>
        /// <param name="library">The <see cref="IMediaLibrary"/> to set.</param>
        public void Remove(IMediaLibrary library)
        {
            this.RemoveMediaLibrary(library);
        }

        /// <summary>
        /// Deletes the <see cref="IMediaLibrary"/> in the CMS System.
        /// </summary>
        /// <param name="ids">The IDs of the <see cref="IMediaLibrary"/> to delete.</param>
        public void Remove(params int[] ids)
        {
            foreach (var library in this.GetCmsMediaLibraryBusiness.GetMediaLibraries(ids))
            {
                this.RemoveMediaLibrary(library);
            }
        }

        /// <summary>
        /// Deletes the <see cref="IMediaLibrary"/> in the CMS System.
        /// </summary>
        /// <param name="siteID">The site id of the library to return.</param>
        /// <param name="matchString">the string which to match the library to.</param>
        /// <param name="exact">A boolean which indicates if the match should be exact.</param>
        public void Remove(int siteID, string matchString, bool exact)
        {
            foreach (var library in this.GetCmsMediaLibraryBusiness.GetMediaLibraries(siteID, matchString, exact))
            {
                this.RemoveMediaLibrary(library);
            }
        }

        /// <summary>
        /// Deletes the specified <see cref="IMediaLibrary"/> in the CMS System.
        /// </summary>
        /// <param name="library">The <see cref="IMediaLibrary"/> to set.</param>
        private void RemoveMediaLibrary(IMediaLibrary library)
        {
            if (this.OutputService.ShouldProcess(library.LibraryName, "delete"))
            {
                this.MediaLibraryService.DeleteMediaLibrary(library);
            }
        }

        #endregion
    }
}

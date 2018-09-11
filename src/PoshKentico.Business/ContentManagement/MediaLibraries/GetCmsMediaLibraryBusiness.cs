// <copyright file="GetCmsMediaLibraryBusiness.cs" company="Chris Crutchfield">
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
    /// Business Layer for Get-CMSMediaLibray cmdlet.
    /// </summary>
    [Export(typeof(GetCmsMediaLibraryBusiness))]
    public class GetCmsMediaLibraryBusiness : CmdletBusinessBase
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
        /// Gets a list of all of the <see cref="IMediaLibrary"/> in the CMS System.
        /// </summary>
        /// <returns>A list of all of the <see cref="IMediaLibrary"/>.</returns>
        public IEnumerable<IMediaLibrary> GetMediaLibraries()
        {
            return this.MediaLibraryService.MediaLibraries;
        }

        /// <summary>
        /// Gets a list of all of the <see cref="IMediaLibrary"/> which match the specified criteria.
        /// </summary>
        /// <param name="siteID">The site id to match the libraries to.</param>
        /// <param name="matchString">The string which to match the libraries to.</param>
        /// <param name="exact">A boolean which indicates if the match should be exact.</param>
        /// <returns>A list of all of the <see cref="IMediaLibrary"/> which match the specified criteria.</returns>
        public IEnumerable<IMediaLibrary> GetMediaLibraries(int siteID, string matchString, bool exact)
        {
            if (exact)
            {
                return (from c in this.MediaLibraryService.MediaLibraries
                        where c.LibrarySiteID == siteID && (
                            c.LibraryDisplayName.ToLowerInvariant().Equals(matchString, StringComparison.InvariantCultureIgnoreCase) ||
                            c.LibraryName.ToLowerInvariant().Equals(matchString, StringComparison.InvariantCultureIgnoreCase))
                        select c).ToArray();
            }
            else
            {
                var lowerMatchString = matchString.ToLowerInvariant();

                return (from c in this.MediaLibraryService.MediaLibraries
                        where c.LibrarySiteID == siteID && (
                            c.LibraryDisplayName.ToLowerInvariant().Contains(lowerMatchString) ||
                            c.LibraryName.ToLowerInvariant().Contains(lowerMatchString))
                        select c).ToArray();
            }
        }

        /// <summary>
        /// Gets a list of the <see cref="IMediaLibrary"/> which match the supplied IDs.
        /// </summary>
        /// <param name="ids">The IDs of the <see cref="IMediaLibrary"/> to return.</param>
        /// <returns>A list of the <see cref="IMediaLibrary"/> which match the supplied IDs.</returns>
        public IEnumerable<IMediaLibrary> GetMediaLibraries(params int[] ids)
        {
            var libraries = from id in ids select this.MediaLibraryService.GetMediaLibrary(id);

            return (from library in libraries
                    where library != null
                    select library).ToArray();
        }

        #endregion
    }
}

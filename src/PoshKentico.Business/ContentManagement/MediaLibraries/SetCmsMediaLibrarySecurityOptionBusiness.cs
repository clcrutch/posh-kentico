// <copyright file="SetCmsMediaLibrarySecurityOptionBusiness.cs" company="Chris Crutchfield">
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
using CMS.Helpers;
using PoshKentico.Core.Services.ContentManagement.MediaLibraries;

namespace PoshKentico.Business.ContentManagement.MediaLibraries
{
    /// <summary>
    /// Business Layer of Set-CmsMediaLibrarySecurityOption cmdlet.
    /// </summary>
    [Export(typeof(SetCmsMediaLibrarySecurityOptionBusiness))]
    public class SetCmsMediaLibrarySecurityOptionBusiness : CmdletBusinessBase
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
        /// Sets the security options for a media library.
        /// </summary>
        /// <param name="library">The specified <see cref="IMediaLibrary"/> to look for setting the security option.</param>
        /// <param name="option">The security option <see cref="SecurityPropertyEnum"/>.</param>
        /// <param name="securityAccess">The security acess enum <see cref="SecurityAccessEnum"/>.</param>
        public void SetMediaLibrarySecurityOption(IMediaLibrary library, SecurityPropertyEnum option, SecurityAccessEnum securityAccess)
        {
            this.MediaLibraryService.SetMediaLibrarySecurityOption(library, option, securityAccess);
        }

        #endregion
    }
}

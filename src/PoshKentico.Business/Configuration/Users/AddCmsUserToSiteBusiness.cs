﻿// <copyright file="AddCmsUserToSiteBusiness.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Configuration.Sites;
using PoshKentico.Core.Services.Configuration.Users;

namespace PoshKentico.Business.Configuration.Users
{
    /// <summary>
    /// Business Layer for Set-CMSUserToSite cmdlet.
    /// </summary>
    [Export(typeof(AddCmsUserToSiteBusiness))]
    public class AddCmsUserToSiteBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the User Service. Populated by MEF.
        /// </summary>
        [Import]
        public IUserService UserService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a user <see cref="IUser"/> to the specified site<see cref="ISite"/> in the CMS System.
        /// </summary>
        /// <param name="user">The user to add to a site.</param>
        /// <param name="siteName">The site name to add a user to.</param>
        public void AddUserToSite(IUser user, string siteName)
        {
            this.UserService.AddUserToSite(user, siteName);
        }

        #endregion
    }
}

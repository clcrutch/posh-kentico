// <copyright file="SetCmsUserBusiness.cs" company="Chris Crutchfield">
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
using CMS.Base;
using ImpromptuInterface;
using PoshKentico.Core.Services.Configuration.Users;

namespace PoshKentico.Business.Configuration.Users
{
    /// <summary>
    /// Business Layer for the Set-CMSUser cmdlet.
    /// </summary>
    [Export(typeof(SetCmsUserBusiness))]
    public class SetCmsUserBusiness : CmdletBusinessBase
    {
        #region Propreties

        /// <summary>
        /// Gets or sets a reference to the User Service. Populated by MEF.
        /// </summary>
        [Import]
        public IUserService UserService { get; set; }

        #endregion

        #region

        /// <summary>
        /// Sets the <see cref="IUser"/> in the CMS System.
        /// </summary>
        /// <param name="user">The <see cref="IUser"/> to set.</param>
        /// <returns>The updated user.</returns>
        public IUser Set(IUser user)
        {
            return this.UserService.SetUser(user);
        }

        /// <summary>
        /// Creates a new <see cref="IUser"/> in the CMS System.
        /// </summary>
        /// <param name="userName">The User Name for the new User.</param>
        /// <param name="fullName">The Full Name for the new User.</param>
        /// <param name="email">The Email for the new User.</param>
        /// <param name="preferredCultureCode">The Preferred Culture Code for the new User.</param>
        /// <param name="privilegeLevel">The User Privilege Level for the new User.</param>
        /// <returns>The newly created <see cref="IUser"/>.</returns>
        public IUser Set(
                        string userName,
                        string fullName,
                        string email,
                        string preferredCultureCode,
                        UserPrivilegeLevelEnum privilegeLevel)
        {
            var data = new
            {
                // Sets the user properties
                FullName = fullName,
                UserName = userName,
                Email = email,
                PreferredCultureCode = preferredCultureCode,
                SiteIndependentPrivilegeLevel = privilegeLevel,
            };

            return this.UserService.SetUser(data.ActLike<IUser>(), false);
        }

        #endregion
    }
}

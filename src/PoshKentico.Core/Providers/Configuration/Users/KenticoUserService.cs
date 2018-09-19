// <copyright file="KenticoUserService.cs" company="Chris Crutchfield">
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
using CMS.Base;
using CMS.Membership;
using ImpromptuInterface;
using PoshKentico.Core.Services.Configuration.Users;

namespace PoshKentico.Core.Providers.Configuration.Users
{
    /// <summary>
    /// Implementation of <see cref="IUserService"/> that uses Kentico.
    /// </summary>
    [Export(typeof(IUserService))]
    public class KenticoUserService : IUserService
    {
        /// <inheritdoc/>
        public IEnumerable<IUser> Users => (from c in UserInfoProvider.GetUsers()
                                                select Impromptu.ActLike<IUser>(c as UserInfo)).ToArray();

        /// <inheritdoc/>
        public IUser CreateUser(IUser user)
        {
            // Creates a new user object
            UserInfo newUser = new UserInfo
            {
                // Sets the user properties
                FullName = user.FullName,
                UserName = user.UserName,
                Email = user.Email,
                PreferredCultureCode = user.PreferredCultureCode,
                SiteIndependentPrivilegeLevel = user.SiteIndependentPrivilegeLevel,
            };

            // Saves the user to the database
            UserInfoProvider.SetUserInfo(newUser);

            return newUser.ActLike<IUser>();
        }

        /// <inheritdoc/>
        public IUser SetUser(IUser user, bool isReplace = true)
        {
            UserInfo existingUser = UserInfoProvider.GetUserInfo(user.UserName);

            if (existingUser != null)
            {
                // Updates the user's properties
                if (isReplace)
                {
                    existingUser = user.UndoActLike();
                }
                else
                {
                    existingUser.FullName = user.FullName ?? existingUser.FullName;
                    existingUser.Email = user.Email ?? existingUser.FullName;
                    existingUser.PreferredCultureCode = user.PreferredCultureCode ?? existingUser.PreferredCultureCode;
                    existingUser.SiteIndependentPrivilegeLevel = user.SiteIndependentPrivilegeLevel;
                }

                // Saves the changes to the database
                UserInfoProvider.SetUserInfo(existingUser);
            }

            return existingUser.ActLike<IUser>();
        }

        /// <inheritdoc/>
        public IUser GetUser(int id)
        {
            return (UserInfoProvider.GetUserInfo(id) as UserInfo)?.ActLike<IUser>();
        }

        /// <inheritdoc/>
        public IUser GetUser(string userName)
        {
            return (UserInfoProvider.GetUserInfo(userName) as UserInfo)?.ActLike<IUser>();
        }

        /// <inheritdoc/>
        public void DeleteUser(IUser user)
        {
            // Gets the user
            UserInfo existingUser = UserInfoProvider.GetUserInfo(user.UserName);

            if (existingUser != null)
            {
                // Deletes the user
                UserInfoProvider.DeleteUser(existingUser);
            }
        }

        /// <inheritdoc/>
        public void AddUserToSite(IUser user, string siteName)
        {
            // Gets the user
            UserInfo existingUser = UserInfoProvider.GetUserInfo(user.UserName);

            if (existingUser != null)
            {
                // Adds the user to the site
                UserInfoProvider.AddUserToSite(existingUser.UserName, siteName);
            }
        }

        /// <inheritdoc/>
        public void RemoveUserFromSite(IUser user, string siteName)
        {
            // Gets the user
            UserInfo existingUser = UserInfoProvider.GetUserInfo(user.UserName);

            if (existingUser != null)
            {
                // Removes the user from the site
                UserInfoProvider.RemoveUserFromSite(existingUser.UserName, siteName);
            }
        }
    }
}

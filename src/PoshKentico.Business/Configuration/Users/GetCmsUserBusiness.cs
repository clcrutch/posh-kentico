﻿// <copyright file="GetCmsUserBusiness.cs" company="Chris Crutchfield">
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
using System.Text.RegularExpressions;
using PoshKentico.Core.Services.Configuration.Users;

namespace PoshKentico.Business.Configuration.Users
{
    /// <summary>
    /// Business Layer of the Get-CMSUser cmdlet.
    /// </summary>
    [Export(typeof(GetCmsUserBusiness))]
    public class GetCmsUserBusiness : CmdletBusinessBase
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
        /// Gets a list of all of the <see cref="IUser"/> in the CMS System.
        /// </summary>
        /// <returns>A list of all of the <see cref="IUser"/>.</returns>
        public IEnumerable<IUser> GetUsers()
        {
            return this.UserService.Users;
        }

        /// <summary>
        /// Gets a list of the <see cref="IUser"/> which match the supplied IDs.
        /// </summary>
        /// <param name="ids">The IDs of the <see cref="IUser"/> to return.</param>
        /// <returns>A list of the <see cref="IUser"/> which match the supplied IDs.</returns>
        public IEnumerable<IUser> GetUsers(params int[] ids)
        {
            var users = from id in ids select this.UserService.GetUser(id);

            return (from user in users
                    where user != null
                    select user).ToArray();
        }

        /// <summary>
        /// Gets a list of all of the <see cref="IUser"/> which match the specified criteria.
        /// </summary>
        /// <param name="userName">The user name which to match the users to.</param>
        /// <param name="isRegex">A boolean which indicates if the match should be exact.</param>
        /// <returns>A list of all of the <see cref="IUser"/> which match the specified criteria.</returns>
        public IEnumerable<IUser> GetUsers(string userName, bool isRegex)
        {
            Regex regex = null;

            if (isRegex)
            {
                regex = new Regex(userName, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            else
            {
                regex = new Regex($"^{userName.Replace("*", ".*")}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }

            var matched = from f in this.UserService.Users
                          where regex.IsMatch(f.UserName)
                          select f;

            return matched.ToArray();
        }

        /// <summary>
        /// Gets all users <see cref="IUser"/> from a specified role which match the specified criteria.
        /// </summary>
        /// <param name="roleName">The role name of the role.</param>
        /// <param name="siteID">The SiteID of the role.</param>
        /// <returns>A list of users that belong to the specified role.</returns>
        public IEnumerable<IUser> GetUsersFromRole(string roleName, int siteID)
        {
            return this.UserService.GetUsersFromRole(roleName, siteID);
        }

        #endregion
    }
}

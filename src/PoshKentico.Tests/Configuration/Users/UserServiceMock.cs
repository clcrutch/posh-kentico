// <copyright file="UserServiceMock.cs" company="Chris Crutchfield">
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
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Moq;
using PoshKentico.Core.Services.Configuration.Roles;
using PoshKentico.Core.Services.Configuration.Users;

namespace PoshKentico.Tests.Configuration.Users
{
    [ExcludeFromCodeCoverage]
    public class UserServiceMock : IUserService
    {
        private IUser userMock;

        public IEnumerable<IUser> Users => throw new NotImplementedException();

        public IEnumerable<IUserRole> UserRoles => throw new NotImplementedException();

        public void AddUserToSite(IUser user, string siteName)
        {
            throw new NotImplementedException();
        }

        public IUser CreateUser(IUser newUser)
        {
            this.userMock = newUser;

            var testUser = new Mock<IUser>();
            testUser.SetupGet(x => x.UserName).Returns(newUser.UserName);
            testUser.SetupGet(x => x.FullName).Returns(newUser.FullName);
            testUser.SetupGet(x => x.Email).Returns(newUser.Email);
            testUser.SetupGet(x => x.PreferredCultureCode).Returns(newUser.PreferredCultureCode);
            testUser.SetupGet(x => x.SiteIndependentPrivilegeLevel).Returns(newUser.SiteIndependentPrivilegeLevel);

            return testUser.Object;
        }

        public void VerifyCreate(IUser expectedUser)
        {
            this.userMock.UserName.Should().Be(expectedUser.UserName);
            this.userMock.FullName.Should().Be(expectedUser.FullName);
            this.userMock.Email.Should().Be(expectedUser.Email);
            this.userMock.PreferredCultureCode.Should().Be(expectedUser.PreferredCultureCode);
            this.userMock.SiteIndependentPrivilegeLevel.Should().Be(expectedUser.SiteIndependentPrivilegeLevel);
        }

        public void DeleteUser(IUser user)
        {
            throw new NotImplementedException();
        }

        public IUser GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public IUser GetUser(string userName)
        {
            throw new NotImplementedException();
        }

        public void RemoveUserFromSite(IUser user, string siteName)
        {
            throw new NotImplementedException();
        }

        public IUser SetUser(IUser user, bool isReplace = true)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IUser> GetUsersFromRole(string roleName, int siteID)
        {
            throw new NotImplementedException();
        }
    }
}

// <copyright file="GetCmsUserTests.cs" company="Chris Crutchfield">
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

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Configuration.Users;
using PoshKentico.Core.Services.Configuration.Users;
using PoshKentico.Tests.Helpers;

namespace PoshKentico.Tests.Configuration.Users
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class GetCmsUserTests
    {
        [Test]
        public void GetCmsUser_None()
        {
            var userServiceMock = new Mock<IUserService>();

            var businessLayer = new GetCmsUserBusiness()
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                UserService = userServiceMock.Object,
            };

            businessLayer.GetUsers().Should().BeEmpty();

            userServiceMock.VerifyGet(x => x.Users);
        }

        [Test]
        public void GetCmsUser_All()
        {
            var userServiceMock = new Mock<IUserService>();

            var users = new List<IUser>();
            var userMock1 = new Mock<IUser>();
            userMock1.SetupGet(x => x.UserName).Returns("NewUser1");
            userMock1.SetupGet(x => x.Email).Returns("newuser1@localhost");
            users.Add(userMock1.Object);

            var userMock2 = new Mock<IUser>();
            userMock2.SetupGet(x => x.UserName).Returns("NewUser2");
            userMock2.SetupGet(x => x.Email).Returns("newuser2@localhost");
            users.Add(userMock2.Object);

            userServiceMock.SetupGet(x => x.Users).Returns(users);

            var businessLayer = new GetCmsUserBusiness()
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                UserService = userServiceMock.Object,
            };

            businessLayer.GetUsers().Should().NotBeNullOrEmpty().And.HaveCount(2);
        }

        [Test]
        public void GetCmsUser_IDs()
        {
            var userServiceMock = new Mock<IUserService>();

            var users = new List<IUser>();
            var userMock1 = new Mock<IUser>();
            userMock1.SetupGet(x => x.UserName).Returns("NewUser1");
            userMock1.SetupGet(x => x.Email).Returns("newuser1@localhost");
            users.Add(userMock1.Object);

            var userMock2 = new Mock<IUser>();
            userMock2.SetupGet(x => x.UserName).Returns("NewUser2");
            userMock2.SetupGet(x => x.Email).Returns("newuser2@localhost");
            users.Add(userMock2.Object);

            userServiceMock.Setup(x => x.GetUser(1)).Returns(userMock1.Object);
            userServiceMock.Setup(x => x.GetUser(2)).Returns(userMock2.Object);

            var businessLayer = new GetCmsUserBusiness()
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                UserService = userServiceMock.Object,
            };

            businessLayer.GetUsers(4).Should().BeEmpty();

            businessLayer.GetUsers(2).Should().NotBeNullOrEmpty().And.HaveCount(1);

            businessLayer.GetUsers(1, 2, 3).Should().NotBeNullOrEmpty().And.HaveCount(2);

            userServiceMock.Verify(x => x.GetUser(1));
            userServiceMock.Verify(x => x.GetUser(2));
            userServiceMock.Verify(x => x.GetUser(3));
        }

        [Test]
        public void GetCmsUser_ExactTrue()
        {
            var userServiceMock = new Mock<IUserService>();

            var users = new List<IUser>();
            var userMock1 = new Mock<IUser>();
            userMock1.SetupGet(x => x.UserName).Returns("NewUser1");
            userMock1.SetupGet(x => x.Email).Returns("newuser1@localhost");
            users.Add(userMock1.Object);

            var userMock2 = new Mock<IUser>();
            userMock2.SetupGet(x => x.UserName).Returns("NewUser2");
            userMock2.SetupGet(x => x.Email).Returns("newuser2@localhost");
            users.Add(userMock2.Object);

            userServiceMock.SetupGet(x => x.Users).Returns(users);

            var businessLayer = new GetCmsUserBusiness()
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                UserService = userServiceMock.Object,
            };

            businessLayer.GetUsers("NewUser1", true).Should().NotBeNullOrEmpty().And.HaveCount(1);
            businessLayer.GetUsers("NewUser", true).Should().NotBeNullOrEmpty().And.HaveCount(2);
        }

        [Test]
        public void GetCmsUser_ExactFalse()
        {
            var userServiceMock = new Mock<IUserService>();

            var users = new List<IUser>();
            var userMock1 = new Mock<IUser>();
            userMock1.SetupGet(x => x.UserName).Returns("NewUser1");
            userMock1.SetupGet(x => x.Email).Returns("newuser1@localhost");
            users.Add(userMock1.Object);

            var userMock2 = new Mock<IUser>();
            userMock2.SetupGet(x => x.UserName).Returns("NewUser2");
            userMock2.SetupGet(x => x.Email).Returns("newuser2@localhost");
            users.Add(userMock2.Object);

            userServiceMock.SetupGet(x => x.Users).Returns(users);

            var businessLayer = new GetCmsUserBusiness()
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                UserService = userServiceMock.Object,
            };

            businessLayer.GetUsers("NewUser1", false).Should().NotBeNullOrEmpty().And.HaveCount(1);
            businessLayer.GetUsers("NewUser", false).Should().BeEmpty();
        }

        [Test]
        public void GetCmsUser_FromRole()
        {
            var userServiceMock = new Mock<IUserService>();

            var businessLayer = new GetCmsUserBusiness()
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                UserService = userServiceMock.Object,
            };

            string roleName = "testRole";
            int siteID = 2;

            businessLayer.GetUsersFromRole(roleName, siteID);
            userServiceMock.Verify(x => x.GetUsersFromRole(roleName, siteID));
        }
    }
}

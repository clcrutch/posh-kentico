// <copyright file="SetCmsUserTests.cs" company="Chris Crutchfield">
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

using System.Diagnostics.CodeAnalysis;
using CMS.Base;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Configuration.Users;
using PoshKentico.Core.Services.Configuration.Users;

namespace PoshKentico.Tests.Configuration.Users
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class SetCmsUserTests
    {
        [Test]
        public void SetCmsUser_Properties()
        {
            var userServiceMock = new Mock<IUserService>();

            var userMock1 = new Mock<IUser>();
            userMock1.SetupGet(x => x.UserName).Returns("MyUser1");
            userMock1.SetupGet(x => x.FullName).Returns("New User1");
            userMock1.SetupGet(x => x.Email).Returns("newuser1@localhost");
            userMock1.SetupGet(x => x.PreferredCultureCode).Returns("en-uk");
            userMock1.SetupGet(x => x.SiteIndependentPrivilegeLevel).Returns(UserPrivilegeLevelEnum.Editor);

            var businessLayer = new SetCmsUserBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                UserService = userServiceMock.Object,
            };

            var result = businessLayer.Set("MyUser1", "My User1", "user1@localhost", "en-us", UserPrivilegeLevelEnum.Admin);

            userServiceMock.Verify(x => x.SetUser(
               It.Is<IUser>(i => i.FullName == "My User1"
                               && i.Email == "user1@localhost"
                               && i.PreferredCultureCode == "en-us"
                               && i.SiteIndependentPrivilegeLevel == UserPrivilegeLevelEnum.Admin), false));
        }

        [Test]
        public void SetCmsUser_Object()
        {
            var userServiceMock = new Mock<IUserService>();

            var userMock1 = new Mock<IUser>();
            userMock1.SetupGet(x => x.UserName).Returns("MyUser1");
            userMock1.SetupGet(x => x.FullName).Returns("New User1");
            userMock1.SetupGet(x => x.Email).Returns("newuser1@localhost");
            userMock1.SetupGet(x => x.PreferredCultureCode).Returns("en-uk");
            userMock1.SetupGet(x => x.SiteIndependentPrivilegeLevel).Returns(UserPrivilegeLevelEnum.Editor);

            var businessLayer = new SetCmsUserBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                UserService = userServiceMock.Object,
            };

            var result = businessLayer.Set(userMock1.Object);

            userServiceMock.Verify(x => x.SetUser(userMock1.Object, true));
        }
    }
}

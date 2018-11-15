// <copyright file="NewCmsUserTests.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Tests.Configuration.Users
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class NewCmsUserTests
    {
        [Test]
        public void NewCmsUser()
        {
            var userServiceMock = new Mock<UserServiceMock>();

            var businessLayer = new NewCmsUserBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                UserService = userServiceMock.Object,
            };

            var result = businessLayer.CreateUser("MyUser1", "My User1", "user1@localhost", "en-us", UserPrivilegeLevelEnum.Admin);

            userServiceMock.Object.VerifyCreate(result);

            result.UserName.Should().Be("MyUser1");
            result.FullName.Should().Be("My User1");
            result.Email.Should().Be("user1@localhost");
            result.PreferredCultureCode.Should().Be("en-us");
            result.SiteIndependentPrivilegeLevel.Should().Be(UserPrivilegeLevelEnum.Admin);
        }
    }
}

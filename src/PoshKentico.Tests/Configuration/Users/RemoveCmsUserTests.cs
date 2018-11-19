// <copyright file="RemoveCmsUserTests.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Tests.Configuration.Users
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class RemoveCmsUserTests
    {
        [Test]
        public void RemoveCmsUserTest_Object()
        {
            var userServiceMock = new Mock<IUserService>();
            bool shouldProcessCalled = false;

            var userMock1 = new Mock<IUser>();
            var userMock2 = new Mock<IUser>();

            var businessLayer = new RemoveCmsUserBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                UserService = userServiceMock.Object,
                ShouldProcess = (x, y) =>
                {
                    shouldProcessCalled = true;
                    return true;
                },
            };

            businessLayer.RemoveUsers(userMock1.Object);
            userServiceMock.Verify(x => x.DeleteUser(userMock1.Object));

            businessLayer.RemoveUsers(userMock2.Object);
            userServiceMock.Verify(x => x.DeleteUser(userMock2.Object));

            shouldProcessCalled.Should().BeTrue();
        }
    }
}

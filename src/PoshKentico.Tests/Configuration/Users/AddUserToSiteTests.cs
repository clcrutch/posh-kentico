// <copyright file="AddUserToSiteTests.cs" company="Chris Crutchfield">
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
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Configuration.Users;
using PoshKentico.Core.Services.Configuration.Users;

namespace PoshKentico.Tests.Configuration.Users
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class AddUserToSiteTests
    {
        [Test]
        public void AddUserToSiteTest()
        {
            var userServiceMock = new Mock<IUserService>();

            var userMock1 = new Mock<IUser>();

            var businessLayer = new AddCmsUserToSiteBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                UserService = userServiceMock.Object,
            };

            businessLayer.AddUserToSite(userMock1.Object, "test site");

            userServiceMock.Verify(x => x.AddUserToSite(userMock1.Object, "test site"));
        }
    }
}

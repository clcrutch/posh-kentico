// <copyright file="NewCmsStagingBusinessTests.cs" company="Chris Crutchfield">
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

using CMS.Synchronization;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Tests.Configuration.Staging;

namespace PoshKentico.Business.Configuration.Staging
{
    [TestFixture]
    public class NewCmsStagingTests
    {
        [TestCase]
        public void CreateServerTest_WithoutServerName()
        {
            var serviceServiceMock = new Mock<StagingServiceMock>();

            var businessLayer = new NewCmsServerBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                StagingService = serviceServiceMock.Object,
            };

            var result = businessLayer.CreateServer("My Server 1", string.Empty, "localhost", ServerAuthenticationEnum.UserName, true, null, null, 12);

            serviceServiceMock.Object.VerifyCreate(result);

            result.ServerName.Should().Be("MyServer1");
        }

        [TestCase]
        public void CreateServerTest_WithServerName()
        {
            var serviceServiceMock = new Mock<StagingServiceMock>();

            var businessLayer = new NewCmsServerBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                StagingService = serviceServiceMock.Object,
            };

            var result = businessLayer.CreateServer("My Server 2", "server2", "localhost", ServerAuthenticationEnum.UserName, true, null, null, 12);

            serviceServiceMock.Object.VerifyCreate(result);

            result.ServerName.Should().Be("server2");
        }
    }
}
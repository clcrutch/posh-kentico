// <copyright file="GetCmsServerTests.cs" company="Chris Crutchfield">
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
using PoshKentico.Business.Configuration.Staging;
using PoshKentico.Core.Services.Configuration.Staging;

namespace PoshKentico.Tests.Configuration.Staging
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class GetCmsServerTests
    {
        [TestCase]
        public void GetServerTest_NoParameters_None()
        {
            var stagingServiceMock = new Mock<IStagingService>();

            var businessLayer = new GetCmsServerBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                StagingService = stagingServiceMock.Object,
            };

            businessLayer.GetServers().Should().BeEmpty();

            stagingServiceMock.VerifyGet(x => x.Servers);
        }

        [TestCase]
        public void GetServerTest_NoParameters_All()
        {
            var serverServiceMock = new Mock<IStagingService>();

            var servers = new List<IServer>();
            var serverMock1 = new Mock<IServer>();
            serverMock1.SetupGet(x => x.ServerDisplayName).Returns("My Server1");
            serverMock1.SetupGet(x => x.ServerSiteID).Returns(9);
            servers.Add(serverMock1.Object);

            var serverMock2 = new Mock<IServer>();
            serverMock2.SetupGet(x => x.ServerDisplayName).Returns("My Server2");
            serverMock2.SetupGet(x => x.ServerSiteID).Returns(12);
            servers.Add(serverMock2.Object);

            serverServiceMock.SetupGet(x => x.Servers).Returns(servers);

            var businessLayer = new GetCmsServerBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                StagingService = serverServiceMock.Object,
            };

            businessLayer.GetServers().Should().NotBeNullOrEmpty().And.HaveCount(2);

            serverServiceMock.VerifyGet(x => x.Servers);
        }

        [TestCase]
        public void GetServerTest_MatchString_ExactFalse()
        {
            var serverServiceMock = new Mock<IStagingService>();

            var servers = new List<IServer>();
            var serverMock1 = new Mock<IServer>();
            serverMock1.SetupGet(x => x.ServerDisplayName).Returns("my Server1");
            serverMock1.SetupGet(x => x.ServerName).Returns("MyServer1");
            serverMock1.SetupGet(x => x.ServerSiteID).Returns(9);
            servers.Add(serverMock1.Object);

            var serverMock2 = new Mock<IServer>();
            serverMock2.SetupGet(x => x.ServerDisplayName).Returns("my server2");
            serverMock2.SetupGet(x => x.ServerName).Returns("myserver2");
            serverMock2.SetupGet(x => x.ServerSiteID).Returns(12);
            servers.Add(serverMock2.Object);

            var serverMock3 = new Mock<IServer>();
            serverMock3.SetupGet(x => x.ServerDisplayName).Returns("your server3");
            serverMock3.SetupGet(x => x.ServerName).Returns("yourserver3");
            serverMock3.SetupGet(x => x.ServerSiteID).Returns(12);
            servers.Add(serverMock3.Object);

            serverServiceMock.SetupGet(x => x.Servers).Returns(servers);

            var businessLayer = new GetCmsServerBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                StagingService = serverServiceMock.Object,
            };

            businessLayer.GetServers(12, "server", false).Should().NotBeNullOrEmpty().And.HaveCount(2);

            businessLayer.GetServers(12, "my", false).Should().NotBeNullOrEmpty().And.HaveCount(1);

            businessLayer.GetServers(12, "your", false).Should().NotBeNullOrEmpty().And.HaveCount(1);

            serverServiceMock.VerifyGet(x => x.Servers);
        }

        [TestCase]
        public void GetServerTest_MatchString_ExactTrue()
        {
            var serverServiceMock = new Mock<IStagingService>();

            var servers = new List<IServer>();
            var serverMock1 = new Mock<IServer>();
            serverMock1.SetupGet(x => x.ServerDisplayName).Returns("My Server1");
            serverMock1.SetupGet(x => x.ServerName).Returns("MyServer1");
            serverMock1.SetupGet(x => x.ServerSiteID).Returns(9);
            servers.Add(serverMock1.Object);

            var serverMock2 = new Mock<IServer>();
            serverMock2.SetupGet(x => x.ServerDisplayName).Returns("your server2");
            serverMock2.SetupGet(x => x.ServerName).Returns("yourserver2");
            serverMock2.SetupGet(x => x.ServerSiteID).Returns(12);
            servers.Add(serverMock2.Object);

            var serverMock3 = new Mock<IServer>();
            serverMock3.SetupGet(x => x.ServerDisplayName).Returns("your server3");
            serverMock3.SetupGet(x => x.ServerName).Returns("yourserver3");
            serverMock3.SetupGet(x => x.ServerSiteID).Returns(12);
            servers.Add(serverMock3.Object);

            serverServiceMock.SetupGet(x => x.Servers).Returns(servers);

            var businessLayer = new GetCmsServerBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                StagingService = serverServiceMock.Object,
            };

            businessLayer.GetServers(9, "server", true).Should().BeEmpty();

            businessLayer.GetServers(12, "your server2", true).Should().NotBeNullOrEmpty().And.HaveCount(1);

            serverServiceMock.VerifyGet(x => x.Servers);
        }

        [TestCase]
        public void GetServerTest_IDs()
        {
            var serverServiceMock = new Mock<IStagingService>();

            var servers = new List<IServer>();
            var serverMock1 = new Mock<IServer>();
            serverMock1.SetupGet(x => x.ServerDisplayName).Returns("My Server1");
            serverMock1.SetupGet(x => x.ServerName).Returns("MyServer1");
            serverMock1.SetupGet(x => x.ServerSiteID).Returns(9);
            servers.Add(serverMock1.Object);

            var serverMock2 = new Mock<IServer>();
            serverMock2.SetupGet(x => x.ServerDisplayName).Returns("your server2");
            serverMock2.SetupGet(x => x.ServerName).Returns("yourserver2");
            serverMock2.SetupGet(x => x.ServerSiteID).Returns(12);
            servers.Add(serverMock2.Object);

            var serverMock3 = new Mock<IServer>();
            serverMock3.SetupGet(x => x.ServerDisplayName).Returns("your server3");
            serverMock3.SetupGet(x => x.ServerName).Returns("yourserver3");
            serverMock3.SetupGet(x => x.ServerSiteID).Returns(12);
            servers.Add(serverMock3.Object);

            serverServiceMock.Setup(x => x.GetServer(1)).Returns(serverMock1.Object);
            serverServiceMock.Setup(x => x.GetServer(2)).Returns(serverMock2.Object);
            serverServiceMock.Setup(x => x.GetServer(3)).Returns(serverMock3.Object);

            var businessLayer = new GetCmsServerBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                StagingService = serverServiceMock.Object,
            };

            businessLayer.GetServers(4).Should().BeEmpty();

            businessLayer.GetServers(2).Should().NotBeNullOrEmpty().And.HaveCount(1);

            businessLayer.GetServers(1, 2, 3).Should().NotBeNullOrEmpty().And.HaveCount(3);

            serverServiceMock.Verify(x => x.GetServer(1));
            serverServiceMock.Verify(x => x.GetServer(2));
            serverServiceMock.Verify(x => x.GetServer(3));
        }
    }
}

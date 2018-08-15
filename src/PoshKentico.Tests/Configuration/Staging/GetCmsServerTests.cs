using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Configuration.Staging;
using PoshKentico.Core.Services.Configuration.Staging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Tests.Configuration.Staging
{
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

using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Configuration.Staging;
using PoshKentico.Core.Services.Configuration.Staging;

namespace PoshKentico.Tests.Configuration.Staging
{
    [TestFixture]
    public class RemoveCmsStagingTaskTests
    {
        [TestCase]
        public void RemoveStagingTaskTest_SpecifiedObject()
        {
            var serverServiceMock = new Mock<IStagingService>();

            var businessLayer = new RemoveCmsStagingTaskBusiness
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                StagingService = serverServiceMock.Object,
            };

            var serverMock1 = new Mock<IServer>();
            serverMock1.SetupGet(x => x.ServerDisplayName).Returns("my Server1");
            serverMock1.SetupGet(x => x.ServerName).Returns("MyServer1");
            serverMock1.SetupGet(x => x.ServerSiteID).Returns(9);

            businessLayer.RemoveStaging(serverMock1.Object);

            serverServiceMock.Verify(x => x.DeleteStagingTask(serverMock1.Object));
        }

        [TestCase]
        public void RemoveStagingTaskTest_SpecifiedProperties()
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

            var businessLayer = new RemoveCmsStagingTaskBusiness
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                StagingService = serverServiceMock.Object,
            };

            serverServiceMock.Setup(x => x.GetServer("myserver1", 9)).Returns(serverMock2.Object);

            businessLayer.RemoveStaging("myserver1", 9);

            serverServiceMock.Verify(x => x.DeleteStagingTask(
                It.Is<IServer>(i => i.ServerName == "myserver1" && i.ServerSiteID == 9)));

            serverServiceMock.Setup(x => x.GetServer("myserver2", 12)).Returns(serverMock2.Object);

            businessLayer.RemoveStaging("myserver2", 12);

            serverServiceMock.Verify(x => x.DeleteStagingTask(
                It.Is<IServer>(i => i.ServerName == "myserver2" && i.ServerSiteID == 12)));

            serverServiceMock.VerifyNoOtherCalls();
        }
    }
}

// <copyright file="SyncCmsStagingTaskTests.cs" company="Chris Crutchfield">
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
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Configuration.Staging;
using PoshKentico.Core.Services.Configuration.Staging;

namespace PoshKentico.Tests.Configuration.Staging
{
    [TestFixture]
    public class SyncCmsStagingTaskTests
    {
        [TestCase]
        public void SyncStagingTaskTest_SpecifiedObject()
        {
            var serverServiceMock = new Mock<IStagingService>();

            var businessLayer = new SyncCmsStagingTaskBusiness
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                StagingService = serverServiceMock.Object,
            };

            var serverMock1 = new Mock<IServer>();
            serverMock1.SetupGet(x => x.ServerDisplayName).Returns("my Server1");
            serverMock1.SetupGet(x => x.ServerName).Returns("MyServer1");
            serverMock1.SetupGet(x => x.ServerSiteID).Returns(9);

            businessLayer.SyncStaging(serverMock1.Object);

            serverServiceMock.Verify(x => x.SynchronizeStagingTask(serverMock1.Object));
        }

        [TestCase]
        public void SyncStagingTaskTest_SpecifiedProperties()
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

            var businessLayer = new SyncCmsStagingTaskBusiness
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                StagingService = serverServiceMock.Object,
            };

            serverServiceMock.Setup(x => x.GetServer("myserver1", 9)).Returns(serverMock2.Object);

            businessLayer.SyncStaging("myserver1", 9);

            serverServiceMock.Verify(x => x.SynchronizeStagingTask(
                It.Is<IServer>(i => i.ServerName == "myserver1" && i.ServerSiteID == 9)));

            serverServiceMock.Setup(x => x.GetServer("myserver2", 12)).Returns(serverMock2.Object);

            businessLayer.SyncStaging("myserver2", 12);

            serverServiceMock.Verify(x => x.SynchronizeStagingTask(
                It.Is<IServer>(i => i.ServerName == "myserver2" && i.ServerSiteID == 12)));

            serverServiceMock.VerifyNoOtherCalls();
        }
    }
}

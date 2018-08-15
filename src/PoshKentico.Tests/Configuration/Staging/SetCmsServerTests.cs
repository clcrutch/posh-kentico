// <copyright file="SetCmsServerTests.cs" company="Chris Crutchfield">
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
using CMS.Synchronization;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Configuration.Staging;
using PoshKentico.Core.Services.Configuration.Staging;

namespace PoshKentico.Tests.Configuration.Staging
{
    [TestFixture]
    public class SetCmsServerTests
    {
        [TestCase]
        public void SetServerTest_SpecifiedObject()
        {
            var serverServiceMock = new Mock<IStagingService>();

            var businessLayer = new SetCmsServerBusiness
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                StagingService = serverServiceMock.Object,
            };

            var serverMock1 = new Mock<IServer>();
            serverMock1.SetupGet(x => x.ServerDisplayName).Returns("my Server1");
            serverMock1.SetupGet(x => x.ServerName).Returns("MyServer1");
            serverMock1.SetupGet(x => x.ServerSiteID).Returns(9);

            businessLayer.Set(serverMock1.Object);

            serverServiceMock.Verify(x => x.Update(serverMock1.Object));
        }

        [TestCase]
        public void SetServerTest_SpecifiedProperties()
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

            var businessLayer = new SetCmsServerBusiness
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                StagingService = serverServiceMock.Object,
            };

            businessLayer.Set("myserver2", 12, "My Modified Server2", "localhost", ServerAuthenticationEnum.UserName, null, null, null);

            serverServiceMock.Verify(x => x.Update(
                It.Is<IServer>(i => i.ServerDisplayName == "My Modified Server2"
                && i.ServerAuthentication == ServerAuthenticationEnum.UserName && i.ServerURL == "localhost")));

            businessLayer.Set("myserver2", 12, "My Modified Server2", null, ServerAuthenticationEnum.UserName, true, "admin", "password");

            serverServiceMock.Verify(x => x.Update(
                It.Is<IServer>(i => i.ServerUsername == "admin" && i.ServerPassword == "password")));
        }
    }
}

// <copyright file="RemoveCmsServerTests.cs" company="Chris Crutchfield">
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
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Configuration.Staging;
using PoshKentico.Core.Providers.General;
using PoshKentico.Core.Services.Configuration.Staging;
using PoshKentico.Tests.Helpers;

namespace PoshKentico.Tests.Configuration.Staging
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class RemoveCmsServerTests
    {
        [TestCase]
        public void RemoveServerTest_Object()
        {
            var serverServiceMock = new Mock<IStagingService>();

            var serverMock1 = new Mock<IServer>();
            serverMock1.SetupGet(x => x.ServerDisplayName).Returns("My Server1");
            serverMock1.SetupGet(x => x.ServerName).Returns("MyServer1");
            serverMock1.SetupGet(x => x.ServerSiteID).Returns(9);

            var serverMock2 = new Mock<IServer>();
            serverMock2.SetupGet(x => x.ServerDisplayName).Returns("your server2");
            serverMock2.SetupGet(x => x.ServerName).Returns("yourserver2");
            serverMock2.SetupGet(x => x.ServerSiteID).Returns(12);

            var serverMock3 = new Mock<IServer>();
            serverMock3.SetupGet(x => x.ServerDisplayName).Returns("your server3");
            serverMock3.SetupGet(x => x.ServerName).Returns("yourserver3");
            serverMock3.SetupGet(x => x.ServerSiteID).Returns(12);

            var outputService = OutputServiceHelper.GetPassThruOutputService();
            PassThruOutputService.ShouldProcessFunction = (x, y) => true;

            var businessLayer = new RemoveCmsServerBusiness()
            {
                OutputService = outputService,
                StagingService = serverServiceMock.Object,
            };

            businessLayer.Remove(serverMock1.Object);
            businessLayer.Remove(serverMock2.Object);
            businessLayer.Remove(serverMock3.Object);
            serverServiceMock.Verify(x => x.Delete(serverMock1.Object));
            serverServiceMock.Verify(x => x.Delete(serverMock2.Object));
            serverServiceMock.Verify(x => x.Delete(serverMock3.Object));
        }
    }
}

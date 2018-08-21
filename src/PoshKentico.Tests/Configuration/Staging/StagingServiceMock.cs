// <copyright file="StagingServiceMock.cs" company="Chris Crutchfield">
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

using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using PoshKentico.Core.Services.Configuration.Roles;
using PoshKentico.Core.Services.Configuration.Staging;

namespace PoshKentico.Tests.Configuration.Staging
{
    public class StagingServiceMock : IStagingService
    {
        private IServer serverMock;

        IEnumerable<IServer> IStagingService.Servers => throw new NotImplementedException();

        IServer IStagingService.Create(IServer newServer)
        {
            this.serverMock = newServer;

            var testServer = new Mock<IServer>();
            testServer.SetupGet(x => x.ServerDisplayName).Returns(newServer.ServerDisplayName);
            testServer.SetupGet(x => x.ServerName).Returns(newServer.ServerName);
            testServer.SetupGet(x => x.ServerSiteID).Returns(newServer.ServerSiteID);
            testServer.SetupGet(x => x.ServerURL).Returns(newServer.ServerURL);
            testServer.SetupGet(x => x.ServerAuthentication).Returns(newServer.ServerAuthentication);
            testServer.SetupGet(x => x.ServerEnabled).Returns(newServer.ServerEnabled);
            testServer.SetupGet(x => x.ServerUsername).Returns(newServer.ServerUsername);
            testServer.SetupGet(x => x.ServerPassword).Returns(newServer.ServerPassword);

            return testServer.Object;
        }

        public void VerifyCreate(IServer expectedServer)
        {
            this.serverMock.ServerDisplayName.Should().Be(expectedServer.ServerDisplayName);
            this.serverMock.ServerName.Should().Be(expectedServer.ServerName);
            this.serverMock.ServerSiteID.Should().Be(expectedServer.ServerSiteID);
            this.serverMock.ServerURL.Should().Be(expectedServer.ServerURL);
            this.serverMock.ServerAuthentication.Should().Be(expectedServer.ServerAuthentication);
            this.serverMock.ServerEnabled.Should().Be(expectedServer.ServerEnabled);
            this.serverMock.ServerUsername.Should().Be(expectedServer.ServerUsername);
            this.serverMock.ServerPassword.Should().Be(expectedServer.ServerPassword);
        }

        void IStagingService.Delete(IServer server)
        {
            throw new NotImplementedException();
        }

        IServer IStagingService.GetServer(int id)
        {
            throw new NotImplementedException();
        }

        IServer IStagingService.GetServer(string serverName, int serverServerId)
        {
            throw new NotImplementedException();
        }

        void IStagingService.Update(IServer server)
        {
            throw new NotImplementedException();
        }

        public string SynchronizeStagingTask(IServer server)
        {
            throw new NotImplementedException();
        }

        public void DeleteStagingTask(IServer server)
        {
            throw new NotImplementedException();
        }

        public void SetLoggingRole(IRole role, string taskGroupName)
        {
            throw new NotImplementedException();
        }

        public void SetNoLoggingRole(IRole role)
        {
            throw new NotImplementedException();
        }
    }
}

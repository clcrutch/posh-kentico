// <copyright file="RemoveCmsRoleTests.cs" company="Chris Crutchfield">
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
using PoshKentico.Business.Configuration.Roles;
using PoshKentico.Core.Providers.General;
using PoshKentico.Core.Services.Configuration.Roles;
using PoshKentico.Tests.Helpers;

namespace PoshKentico.Tests.Configuration.Roles
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class RemoveCmsRoleTests
    {
        [Test]
        public void RemoveRoleTest()
        {
            var roleServiceMock = new Mock<IRoleService>();

            var roleMock1 = new Mock<IRole>();
            roleMock1.SetupGet(x => x.RoleName).Returns("My Role1");
            roleMock1.SetupGet(x => x.SiteID).Returns(1);

            var roleMock2 = new Mock<IRole>();
            roleMock1.SetupGet(x => x.RoleName).Returns("My Role2");
            roleMock1.SetupGet(x => x.SiteID).Returns(2);

            var outputService = OutputServiceHelper.GetPassThruOutputService();
            PassThruOutputService.ShouldProcessFunction = (x, y) => true;

            var businessLayer = new RemoveCmsRoleBusiness()
            {
                OutputService = outputService,

                RoleService = roleServiceMock.Object,
            };

            businessLayer.RemoveRole(roleMock1.Object);

            roleServiceMock.Verify(x => x.DeleteRole(roleMock1.Object));
        }
    }
}

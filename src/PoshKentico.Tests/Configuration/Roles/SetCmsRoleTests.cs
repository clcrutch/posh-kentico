// <copyright file="SetCmsRoleTests.cs" company="Chris Crutchfield">
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
using PoshKentico.Business.Configuration.Roles;
using PoshKentico.Core.Services.Configuration.Roles;
using PoshKentico.Tests.Helpers;

namespace PoshKentico.Tests.Configuration.Roles
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class SetCmsRoleTests
    {
        [TestCase]
        public void SetRoleTest_SpecifiedObject()
        {
            var roleServiceMock = new Mock<IRoleService>();

            var businessLayer = new SetCmsRoleBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                RoleService = roleServiceMock.Object,
            };

            var roleMock1 = new Mock<IRole>();
            roleMock1.SetupGet(x => x.RoleName).Returns("My Role1");
            roleMock1.SetupGet(x => x.SiteID).Returns(1);

            businessLayer.Set(roleMock1.Object);

            roleServiceMock.Verify(x => x.SetRole(roleMock1.Object, true));
        }

        [TestCase]
        public void SetRoleTest_SpecifiedProperties()
        {
            var roleServiceMock = new Mock<IRoleService>();

            var roles = new List<IRole>();
            var roleMock1 = new Mock<IRole>();
            roleMock1.SetupGet(x => x.RoleName).Returns("My Role1");
            roleMock1.SetupGet(x => x.SiteID).Returns(1);
            roles.Add(roleMock1.Object);

            var roleMock2 = new Mock<IRole>();
            roleMock2.SetupGet(x => x.RoleName).Returns("My Role2");
            roleMock2.SetupGet(x => x.SiteID).Returns(2);
            roles.Add(roleMock2.Object);

            roleServiceMock.SetupGet(x => x.Roles).Returns(roles);

            var businessLayer = new SetCmsRoleBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                RoleService = roleServiceMock.Object,
            };

            businessLayer.Set("My Role3", "MyRole1", 1);

            roleServiceMock.Verify(x => x.SetRole(
                It.Is<IRole>(i => i.RoleDisplayName == "My Role3" && i.RoleName == "MyRole1"
                && i.SiteID == 1), false));
        }
    }
}

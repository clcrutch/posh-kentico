// <copyright file="GetCmsRoleTests.cs" company="Chris Crutchfield">
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
using PoshKentico.Business.Configuration.Roles;
using PoshKentico.Core.Services.Configuration.Roles;
using PoshKentico.Tests.Helpers;

namespace PoshKentico.Tests.Configuration.Roles
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class GetCmsRoleTests
    {
        [Test]
        public void GetCmsRoleTest()
        {
            var roleServiceMock = new Mock<IRoleService>();

            var businessLayer = new GetCmsRoleBusiness()
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                RoleService = roleServiceMock.Object,
            };

            businessLayer.GetRoles().Should().BeEmpty();

            roleServiceMock.VerifyGet(x => x.Roles);
        }

        [Test]
        public void GetRoleTest_NoParameters_All()
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

            var businessLayer = new GetCmsRoleBusiness()
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                RoleService = roleServiceMock.Object,
            };

            businessLayer.GetRoles().Should().NotBeNullOrEmpty().And.HaveCount(2);

            roleServiceMock.VerifyGet(x => x.Roles);
        }

        [Test]
        public void GetRoleTest_MatchString_IsRegexTrue()
        {
            var roleServiceMock = new Mock<IRoleService>();

            var roles = new List<IRole>();
            var roleMock1 = new Mock<IRole>();
            roleMock1.SetupGet(x => x.RoleName).Returns("My Role1");
            roleMock1.SetupGet(x => x.SiteID).Returns(1);
            roles.Add(roleMock1.Object);

            var roleMock2 = new Mock<IRole>();
            roleMock2.SetupGet(x => x.RoleName).Returns("your role2");
            roleMock2.SetupGet(x => x.SiteID).Returns(2);
            roles.Add(roleMock2.Object);

            roleServiceMock.SetupGet(x => x.Roles).Returns(roles);

            var businessLayer = new GetCmsRoleBusiness()
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                RoleService = roleServiceMock.Object,
            };

            businessLayer.GetRoles("role", true).Should().NotBeNullOrEmpty().And.HaveCount(2);

            businessLayer.GetRoles("my", true).Should().NotBeNullOrEmpty().And.HaveCount(1);

            businessLayer.GetRoles("your", true).Should().NotBeNullOrEmpty().And.HaveCount(1);

            roleServiceMock.VerifyGet(x => x.Roles);
        }

        [Test]
        public void GetRoleTest_MatchString_IsRegexFalse()
        {
            var roleServiceMock = new Mock<IRoleService>();

            var roles = new List<IRole>();
            var roleMock1 = new Mock<IRole>();
            roleMock1.SetupGet(x => x.RoleName).Returns("My Role1");
            roleMock1.SetupGet(x => x.SiteID).Returns(1);
            roles.Add(roleMock1.Object);

            var roleMock2 = new Mock<IRole>();
            roleMock2.SetupGet(x => x.RoleName).Returns("your role2");
            roleMock2.SetupGet(x => x.SiteID).Returns(2);
            roles.Add(roleMock2.Object);

            roleServiceMock.SetupGet(x => x.Roles).Returns(roles);

            var businessLayer = new GetCmsRoleBusiness()
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                RoleService = roleServiceMock.Object,
            };

            businessLayer.GetRoles("role", false).Should().BeEmpty();

            businessLayer.GetRoles("your role2", false).Should().NotBeNullOrEmpty().And.HaveCount(1);

            roleServiceMock.VerifyGet(x => x.Roles);
        }

        [Test]
        public void GetRoleTest_IDs()
        {
            var roleServiceMock = new Mock<IRoleService>();

            var roles = new List<IRole>();
            var roleMock1 = new Mock<IRole>();
            roleMock1.SetupGet(x => x.RoleName).Returns("My Role1");
            roleMock1.SetupGet(x => x.RoleName).Returns("MyRole1");
            roleMock1.SetupGet(x => x.SiteID).Returns(1);
            roles.Add(roleMock1.Object);

            var roleMock2 = new Mock<IRole>();
            roleMock2.SetupGet(x => x.RoleName).Returns("your role2");
            roleMock2.SetupGet(x => x.RoleName).Returns("yourrole2");
            roleMock2.SetupGet(x => x.SiteID).Returns(2);
            roles.Add(roleMock2.Object);

            roleServiceMock.Setup(x => x.GetRole(1)).Returns(roleMock1.Object);
            roleServiceMock.Setup(x => x.GetRole(2)).Returns(roleMock2.Object);

            var businessLayer = new GetCmsRoleBusiness()
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                RoleService = roleServiceMock.Object,
            };

            businessLayer.GetRoles(3).Should().BeEmpty();

            businessLayer.GetRoles(2).Should().NotBeNullOrEmpty().And.HaveCount(1);

            businessLayer.GetRoles(1, 2, 3).Should().NotBeNullOrEmpty().And.HaveCount(2);

            roleServiceMock.Verify(x => x.GetRole(1));
            roleServiceMock.Verify(x => x.GetRole(2));
            roleServiceMock.Verify(x => x.GetRole(3));
        }
    }
}

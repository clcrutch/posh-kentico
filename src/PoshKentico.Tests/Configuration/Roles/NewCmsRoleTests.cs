// <copyright file="NewCmsRoleTests.cs" company="Chris Crutchfield">
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

using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Configuration.Roles;
using PoshKentico.Core.Services.Configuration.Roles;
using PoshKentico.Tests.Helpers;

namespace PoshKentico.Tests.Configuration.Roles
{
    [TestFixture]
    public class NewCmsRoleTests
    {
        [Test]
        public void NewCmsRoleTest()
        {
            string displayName = "My Role1";
            string roleName = "MyRole1";
            int siteID = 0;

            var roleMock1 = new Mock<IRole>();
            roleMock1.Setup(x => x.RoleDisplayName).Returns(displayName);

            IRole passedRole = null;
            var roleServiceMock = new Mock<IRoleService>();
            roleServiceMock.Setup(x => x.CreateRole(It.IsAny<IRole>())).Callback<IRole>(x => passedRole = x).Returns(roleMock1.Object);

            var businessLayer = new NewCmsRoleBusiness()
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                RoleService = roleServiceMock.Object,
            };

            var result = businessLayer.CreateRole(displayName, roleName, siteID);

            result.Should().NotBeNull();

            passedRole.RoleDisplayName.Should().Be(displayName);
            passedRole.RoleName.Should().Be(roleName);
            passedRole.SiteID.Should().Be(siteID);
        }
    }
}

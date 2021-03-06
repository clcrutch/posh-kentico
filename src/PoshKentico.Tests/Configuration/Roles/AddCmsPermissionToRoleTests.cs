﻿// <copyright file="AddCmsPermissionToRoleTests.cs" company="Chris Crutchfield">
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
using ImpromptuInterface;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Configuration.Roles;
using PoshKentico.Core.Services.Configuration.Roles;
using PoshKentico.Core.Services.Development.Modules;
using PoshKentico.Tests.Helpers;

namespace PoshKentico.Tests.Configuration.Roles
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class AddCmsPermissionToRoleTests
    {
        [Test]
        public void AddCmsPermissionToRoleTest()
        {
            var roleServiceMock = new Mock<IRoleService>();

            var businessLayer = new AddCmsPermissionToRoleBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                RoleService = roleServiceMock.Object,
            };

            var roleMock1 = new Mock<IRole>();
            roleMock1.SetupGet(x => x.RoleName).Returns("My Role1");
            roleMock1.SetupGet(x => x.SiteID).Returns(1);

            string[] permissionNames = new string[] { "Read", "Modify" };
            string resourceName = "test";
            string className = null;

            businessLayer.AddPermissionToRole(roleMock1.Object, permissionNames, resourceName, className);

            roleServiceMock.Verify(x => x.AddModulePermissionToRole(It.IsAny<IResource>(), roleMock1.Object));
        }
    }
}

// <copyright file="RemoveCmsUiElementFromRoleTests.cs" company="Chris Crutchfield">
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
    public class RemoveCmsUiElementFromRoleTests
    {
        [TestCase]
        public void RemoveCmsUiElementFromRoleTest()
        {
            var roleServiceMock = new Mock<IRoleService>();

            var businessLayer = new RemoveCmsUIElementFromRoleBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                RoleService = roleServiceMock.Object,
            };

            var roleMock1 = new Mock<IRole>();
            roleMock1.SetupGet(x => x.RoleName).Returns("My Role1");
            roleMock1.SetupGet(x => x.SiteID).Returns(1);

            string resourceName = "CMS.Design";
            string elementName = "Design";
            int resourceID = 22;
            var elem = new
            {
                ElementName = elementName,
                ElementResourceID = resourceID,
            };

            roleServiceMock.Setup(x => x.GetUiElement(resourceName, elementName)).Returns(elem.ActLike<IUIElement>());

            businessLayer.RemoveUiElementFromRole(roleMock1.Object, resourceName, elementName);

            roleServiceMock.Verify(x => x.GetUiElement(resourceName, elementName));

            roleServiceMock.Verify(x => x.RemoveUiElementFromRole(It.IsAny<IUIElement>(), roleMock1.Object));
        }
    }
}

// <copyright file="NewCMSWebPartCategoryTests.cs" company="Chris Crutchfield">
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
using CMS.PortalEngine;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development;
using PoshKentico.Core.Services.Development.WebParts;
using PoshKentico.Tests.Helpers;

namespace PoshKentico.Tests.Development.WebParts
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class NewCMSWebPartCategoryTests
    {
        [TestCase]
        public void ShouldCreateWebPartCategory()
        {
            IControlCategory<WebPartCategoryInfo> passedCategory = null;

            var name = "Category";
            var path = "/Category";
            var displayName = "Display Name";
            var imagePath = "/image/path";

            var webPartCategoryMock1 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock1
                .Setup(x => x.Path)
                .Returns(path);
            webPartCategoryMock1
                .Setup(x => x.DisplayName)
                .Returns(displayName);
            webPartCategoryMock1
                .Setup(x => x.ImagePath)
                .Returns(imagePath);
            var webPartCategoryMock2 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock2
                .Setup(x => x.ID)
                .Returns(20);
            webPartCategoryMock2
                .Setup(x => x.Path)
                .Returns("/");

            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.Categories)
                .Returns(new IControlCategory<WebPartCategoryInfo>[] { webPartCategoryMock1.Object, webPartCategoryMock2.Object });
            webPartServiceMock
                .Setup(x => x.Create(It.IsAny<IControlCategory<WebPartCategoryInfo>>()))
                .Callback<IControlCategory<WebPartCategoryInfo>>(x => passedCategory = x)
                .Returns(webPartCategoryMock1.Object);

            var businessLayer = new NewCMSWebPartCategoryBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
                ControlService = webPartServiceMock.Object,
            };

            var results = businessLayer.CreateWebPartCategory(path, displayName, imagePath);
            results
                .Should()
                .NotBeNull();
            results
                .Should().BeEquivalentTo(webPartCategoryMock1.Object);

            passedCategory.Name.Should().Be(name);
            passedCategory.Path.Should().Be(path);
            passedCategory.DisplayName.Should().Be(displayName);
            passedCategory.ImagePath.Should().Be(imagePath);
            passedCategory.ParentID.Should().Be(20);
        }

        [TestCase]
        public void ShouldCreateWebPartCategoryDisplayNameNull()
        {
            IControlCategory<WebPartCategoryInfo> passedCategory = null;

            var name = "Category";
            var path = "/Category";
            var imagePath = "/image/path";

            var webPartCategoryMock1 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock1
                .Setup(x => x.Path)
                .Returns(path);
            webPartCategoryMock1
                .Setup(x => x.DisplayName)
                .Returns("Category");
            webPartCategoryMock1
                .Setup(x => x.ImagePath)
                .Returns(imagePath);
            var webPartCategoryMock2 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock2
                .Setup(x => x.ID)
                .Returns(20);
            webPartCategoryMock2
                .Setup(x => x.Path)
                .Returns("/");

            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.Categories)
                .Returns(new IControlCategory<WebPartCategoryInfo>[] { webPartCategoryMock1.Object, webPartCategoryMock2.Object });
            webPartServiceMock
                .Setup(x => x.Create(It.IsAny<IControlCategory<WebPartCategoryInfo>>()))
                .Callback<IControlCategory<WebPartCategoryInfo>>(x => passedCategory = x)
                .Returns(webPartCategoryMock1.Object);

            var businessLayer = new NewCMSWebPartCategoryBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
                ControlService = webPartServiceMock.Object,
            };

            var results = businessLayer.CreateWebPartCategory(path, null, imagePath);
            results
                .Should()
                .NotBeNull();
            results
                .Should().BeEquivalentTo(webPartCategoryMock1.Object);

            passedCategory.Name.Should().Be(name);
            passedCategory.Path.Should().Be(path);
            passedCategory.DisplayName.Should().Be(name);
            passedCategory.ImagePath.Should().Be(imagePath);
            passedCategory.ParentID.Should().Be(20);
        }
    }
}

// <copyright file="NewCMSWebPartTests.cs" company="Chris Crutchfield">
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
    public class NewCMSWebPartTests
    {
        [TestCase]
        public void ShouldCreateWebPartWithPath()
        {
            var fileName = "filename.acsx";
            var displayName = "Display Name";

            var webPartCategoryMock = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock
                .Setup(x => x.Path)
                .Returns("/Category");

            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.Categories)
                .Returns(new IControlCategory<WebPartCategoryInfo>[]
                {
                    webPartCategoryMock.Object,
                });

            var businessLayerMock = new Mock<NewCMSWebPartBusiness>();
            businessLayerMock
                .Setup(x => x.ControlService)
                .Returns(webPartServiceMock.Object);

            businessLayerMock
                .Object
                .CreateWebPart("/Category/WebPart", fileName, displayName);

            businessLayerMock.Verify(x => x.CreateWebPart("WebPart", fileName, displayName, webPartCategoryMock.Object));
        }

        [TestCase]
        public void ShouldCreateWebPartWithRootPath()
        {
            var fileName = "filename.acsx";
            var displayName = "Display Name";

            var webPartCategoryMock = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock
                .Setup(x => x.Path)
                .Returns("/");

            var webPartMock = new Mock<IWebPart>();

            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.Categories)
                .Returns(new IControlCategory<WebPartCategoryInfo>[]
                {
                    webPartCategoryMock.Object,
                });

            var businessLayerMock = new Mock<NewCMSWebPartBusiness>();
            businessLayerMock
                .Setup(x => x.ControlService)
                .Returns(webPartServiceMock.Object);

            businessLayerMock
                .Setup(x => x.CreateWebPart("WebPart", fileName, displayName, webPartCategoryMock.Object))
                .Returns(webPartMock.Object);

            var results = businessLayerMock
                            .Object
                            .CreateWebPart("/WebPart", fileName, displayName);
            results
                .Should()
                .NotBeNull();

            results
                .Should().BeEquivalentTo(webPartMock.Object);

            businessLayerMock.Verify(x => x.CreateWebPart("WebPart", fileName, displayName, webPartCategoryMock.Object));
        }

        [TestCase]
        public void ShouldCreateWebPartWithName()
        {
            IWebPart passedWebPart = null;

            var fileName = "filename.acsx";
            var displayName = "Display Name";
            var name = "WebPart";

            var webPartCategoryMock = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock
                .Setup(x => x.ID)
                .Returns(66);

            var webPartMock = new Mock<IWebPart>();
            webPartMock
                .Setup(x => x.DisplayName)
                .Returns(displayName);
            webPartMock
                .Setup(x => x.FileName)
                .Returns(fileName);
            webPartMock
                .Setup(x => x.Name)
                .Returns(name);

            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.Create(It.IsAny<IWebPart>()))
                .Callback<IWebPart>(x => passedWebPart = x)
                .Returns(webPartMock.Object);

            var businessLayer = new NewCMSWebPartBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
                ControlService = webPartServiceMock.Object,
            };

            var results = businessLayer.CreateWebPart(name, fileName, displayName, webPartCategoryMock.Object);

            results
                .Should()
                .NotBeNull();

            results
                .Should().BeEquivalentTo(webPartMock.Object);

            passedWebPart.DisplayName.Should().Be(displayName);
            passedWebPart.FileName.Should().Be(fileName);
            passedWebPart.Name.Should().Be(name);
            passedWebPart.CategoryID.Should().Be(66);
        }

        [TestCase]
        public void ShouldCreateWebPartWithNameAndNullDisplayName()
        {
            IWebPart passedWebPart = null;

            var fileName = "filename.acsx";
            var name = "WebPart";

            var webPartCategoryMock = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock
                .Setup(x => x.ID)
                .Returns(66);

            var webPartMock = new Mock<IWebPart>();
            webPartMock
                .Setup(x => x.DisplayName)
                .Returns(name);
            webPartMock
                .Setup(x => x.FileName)
                .Returns(fileName);
            webPartMock
                .Setup(x => x.Name)
                .Returns(name);

            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.Create(It.IsAny<IWebPart>()))
                .Callback<IWebPart>(x => passedWebPart = x)
                .Returns(webPartMock.Object);

            var businessLayer = new NewCMSWebPartBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
                ControlService = webPartServiceMock.Object,
            };

            var results = businessLayer.CreateWebPart(name, fileName, null, webPartCategoryMock.Object);

            results
                .Should()
                .NotBeNull();

            results
                .Should().BeEquivalentTo(webPartMock.Object);

            passedWebPart.DisplayName.Should().Be(name);
            passedWebPart.FileName.Should().Be(fileName);
            passedWebPart.Name.Should().Be(name);
            passedWebPart.CategoryID.Should().Be(66);
        }
    }
}

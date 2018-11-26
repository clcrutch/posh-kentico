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
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Development.WebParts;
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

            var webPartCategoryMock = new Mock<IWebPartCategory>();
            webPartCategoryMock
                .Setup(x => x.CategoryPath)
                .Returns("/Category");

            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.WebPartCategories)
                .Returns(new IWebPartCategory[]
                {
                    webPartCategoryMock.Object,
                });

            var businessLayerMock = new Mock<NewCMSWebPartBusiness>();
            businessLayerMock
                .Setup(x => x.WebPartService)
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

            var webPartCategoryMock = new Mock<IWebPartCategory>();
            webPartCategoryMock
                .Setup(x => x.CategoryPath)
                .Returns("/");

            var webPartMock = new Mock<IWebPart>();

            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.WebPartCategories)
                .Returns(new IWebPartCategory[]
                {
                    webPartCategoryMock.Object,
                });

            var businessLayerMock = new Mock<NewCMSWebPartBusiness>();
            businessLayerMock
                .Setup(x => x.WebPartService)
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

            var webPartCategoryMock = new Mock<IWebPartCategory>();
            webPartCategoryMock
                .Setup(x => x.CategoryID)
                .Returns(66);

            var webPartMock = new Mock<IWebPart>();
            webPartMock
                .Setup(x => x.WebPartDisplayName)
                .Returns(displayName);
            webPartMock
                .Setup(x => x.WebPartFileName)
                .Returns(fileName);
            webPartMock
                .Setup(x => x.WebPartName)
                .Returns(name);

            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.Create(It.IsAny<IWebPart>()))
                .Callback<IWebPart>(x => passedWebPart = x)
                .Returns(webPartMock.Object);

            var businessLayer = new NewCMSWebPartBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
                WebPartService = webPartServiceMock.Object,
            };

            var results = businessLayer.CreateWebPart(name, fileName, displayName, webPartCategoryMock.Object);

            results
                .Should()
                .NotBeNull();

            results
                .Should().BeEquivalentTo(webPartMock.Object);

            passedWebPart.WebPartDisplayName.Should().Be(displayName);
            passedWebPart.WebPartFileName.Should().Be(fileName);
            passedWebPart.WebPartName.Should().Be(name);
            passedWebPart.WebPartCategoryID.Should().Be(66);
        }

        [TestCase]
        public void ShouldCreateWebPartWithNameAndNullDisplayName()
        {
            IWebPart passedWebPart = null;

            var fileName = "filename.acsx";
            var name = "WebPart";

            var webPartCategoryMock = new Mock<IWebPartCategory>();
            webPartCategoryMock
                .Setup(x => x.CategoryID)
                .Returns(66);

            var webPartMock = new Mock<IWebPart>();
            webPartMock
                .Setup(x => x.WebPartDisplayName)
                .Returns(name);
            webPartMock
                .Setup(x => x.WebPartFileName)
                .Returns(fileName);
            webPartMock
                .Setup(x => x.WebPartName)
                .Returns(name);

            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.Create(It.IsAny<IWebPart>()))
                .Callback<IWebPart>(x => passedWebPart = x)
                .Returns(webPartMock.Object);

            var businessLayer = new NewCMSWebPartBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
                WebPartService = webPartServiceMock.Object,
            };

            var results = businessLayer.CreateWebPart(name, fileName, null, webPartCategoryMock.Object);

            results
                .Should()
                .NotBeNull();

            results
                .Should().BeEquivalentTo(webPartMock.Object);

            passedWebPart.WebPartDisplayName.Should().Be(name);
            passedWebPart.WebPartFileName.Should().Be(fileName);
            passedWebPart.WebPartName.Should().Be(name);
            passedWebPart.WebPartCategoryID.Should().Be(66);
        }
    }
}

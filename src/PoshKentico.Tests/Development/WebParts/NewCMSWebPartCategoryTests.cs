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
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;

namespace PoshKentico.Tests.Development.WebParts
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class NewCMSWebPartCategoryTests
    {
        [TestCase]
        public void ShouldCreateWebPartCategory()
        {
            IWebPartCategory passedCategory = null;

            var name = "Category";
            var path = "/Category";
            var displayName = "Display Name";
            var imagePath = "/image/path";

            var webPartCategoryMock1 = new Mock<IWebPartCategory>();
            webPartCategoryMock1
                .Setup(x => x.CategoryPath)
                .Returns(path);
            webPartCategoryMock1
                .Setup(x => x.CategoryDisplayName)
                .Returns(displayName);
            webPartCategoryMock1
                .Setup(x => x.CategoryImagePath)
                .Returns(imagePath);
            var webPartCategoryMock2 = new Mock<IWebPartCategory>();
            webPartCategoryMock2
                .Setup(x => x.CategoryID)
                .Returns(20);
            webPartCategoryMock2
                .Setup(x => x.CategoryPath)
                .Returns("/");

            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.WebPartCategories)
                .Returns(new IWebPartCategory[] { webPartCategoryMock1.Object, webPartCategoryMock2.Object });
            webPartServiceMock
                .Setup(x => x.Create(It.IsAny<IWebPartCategory>()))
                .Callback<IWebPartCategory>(x => passedCategory = x)
                .Returns(webPartCategoryMock1.Object);

            var businessLayer = new NewCMSWebPartCategoryBusiness
            {
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
            };

            var results = businessLayer.CreateWebPartCategory(path, displayName, imagePath);
            results
                .Should()
                .NotBeNull();
            results
                .Should().BeEquivalentTo(webPartCategoryMock1.Object);

            passedCategory.CategoryName.Should().Be(name);
            passedCategory.CategoryPath.Should().Be(path);
            passedCategory.CategoryDisplayName.Should().Be(displayName);
            passedCategory.CategoryImagePath.Should().Be(imagePath);
            passedCategory.CategoryParentID.Should().Be(20);
        }

        [TestCase]
        public void ShouldCreateWebPartCategoryDisplayNameNull()
        {
            IWebPartCategory passedCategory = null;

            var name = "Category";
            var path = "/Category";
            var imagePath = "/image/path";

            var webPartCategoryMock1 = new Mock<IWebPartCategory>();
            webPartCategoryMock1
                .Setup(x => x.CategoryPath)
                .Returns(path);
            webPartCategoryMock1
                .Setup(x => x.CategoryDisplayName)
                .Returns("Category");
            webPartCategoryMock1
                .Setup(x => x.CategoryImagePath)
                .Returns(imagePath);
            var webPartCategoryMock2 = new Mock<IWebPartCategory>();
            webPartCategoryMock2
                .Setup(x => x.CategoryID)
                .Returns(20);
            webPartCategoryMock2
                .Setup(x => x.CategoryPath)
                .Returns("/");

            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.WebPartCategories)
                .Returns(new IWebPartCategory[] { webPartCategoryMock1.Object, webPartCategoryMock2.Object });
            webPartServiceMock
                .Setup(x => x.Create(It.IsAny<IWebPartCategory>()))
                .Callback<IWebPartCategory>(x => passedCategory = x)
                .Returns(webPartCategoryMock1.Object);

            var businessLayer = new NewCMSWebPartCategoryBusiness
            {
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
            };

            var results = businessLayer.CreateWebPartCategory(path, null, imagePath);
            results
                .Should()
                .NotBeNull();
            results
                .Should().BeEquivalentTo(webPartCategoryMock1.Object);

            passedCategory.CategoryName.Should().Be(name);
            passedCategory.CategoryPath.Should().Be(path);
            passedCategory.CategoryDisplayName.Should().Be(name);
            passedCategory.CategoryImagePath.Should().Be(imagePath);
            passedCategory.CategoryParentID.Should().Be(20);
        }
    }
}

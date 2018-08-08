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

using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Tests.Development.WebParts
{
    [TestFixture]
    public class NewCMSWebPartCategoryTests
    {
        [TestCase]
        public void CreateWebPart_ParentIsRoot_DisplayNameNull()
        {
            var webPartServiceMock = new Mock<WebPartServiceMock>();

            var webPartCategories = new List<IWebPartCategory>();

            var rootCatMock = new Mock<IWebPartCategory>();
            rootCatMock.SetupGet(x => x.CategoryDisplayName).Returns("/");
            rootCatMock.SetupGet(x => x.CategoryName).Returns("/");
            rootCatMock.SetupGet(x => x.CategoryPath).Returns("/");
            rootCatMock.SetupGet(x => x.CategoryID).Returns(355);
            webPartCategories.Add(rootCatMock.Object);

            var catMock1 = new Mock<IWebPartCategory>();
            catMock1.SetupGet(x => x.CategoryDisplayName).Returns("My Category");
            catMock1.SetupGet(x => x.CategoryName).Returns("MyCategory");
            catMock1.SetupGet(x => x.CategoryPath).Returns("/my/category");
            webPartCategories.Add(catMock1.Object);

            var catMock2 = new Mock<IWebPartCategory>();
            catMock2.SetupGet(x => x.CategoryDisplayName).Returns("Ny Category");
            catMock2.SetupGet(x => x.CategoryName).Returns("NyCategory");
            catMock2.SetupGet(x => x.CategoryPath).Returns("/my/category2");
            webPartCategories.Add(catMock2.Object);

            webPartServiceMock.SetupGet(x => x.WebPartCategories).Returns(webPartCategories);

            var businessLayer = new NewCMSWebPartCategoryBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                WebPartService = webPartServiceMock.Object,
            };

            var result = businessLayer.CreateWebPart("/Test", null, "/imagepath");

            webPartServiceMock.Object.VerifyCreate("Test", "Test", "/Test", "/imagepath", 355);

            result.CategoryName.Should().Be("TestCat");
        }

        [TestCase]
        public void CreateWebPart_ParentIsRoot_DisplayNameNotNull()
        {
            var webPartServiceMock = new Mock<WebPartServiceMock>();

            var webPartCategories = new List<IWebPartCategory>();

            var rootCatMock = new Mock<IWebPartCategory>();
            rootCatMock.SetupGet(x => x.CategoryDisplayName).Returns("/");
            rootCatMock.SetupGet(x => x.CategoryName).Returns("/");
            rootCatMock.SetupGet(x => x.CategoryPath).Returns("/");
            rootCatMock.SetupGet(x => x.CategoryID).Returns(355);
            webPartCategories.Add(rootCatMock.Object);

            var catMock1 = new Mock<IWebPartCategory>();
            catMock1.SetupGet(x => x.CategoryDisplayName).Returns("My Category");
            catMock1.SetupGet(x => x.CategoryName).Returns("MyCategory");
            catMock1.SetupGet(x => x.CategoryPath).Returns("/my/category");
            webPartCategories.Add(catMock1.Object);

            var catMock2 = new Mock<IWebPartCategory>();
            catMock2.SetupGet(x => x.CategoryDisplayName).Returns("Ny Category");
            catMock2.SetupGet(x => x.CategoryName).Returns("NyCategory");
            catMock2.SetupGet(x => x.CategoryPath).Returns("/my/category2");
            webPartCategories.Add(catMock2.Object);

            webPartServiceMock.SetupGet(x => x.WebPartCategories).Returns(webPartCategories);

            var businessLayer = new NewCMSWebPartCategoryBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                WebPartService = webPartServiceMock.Object,
            };

            var result = businessLayer.CreateWebPart("/Test", "My Test", "/imagepath");

            webPartServiceMock.Object.VerifyCreate("My Test", "Test", "/Test", "/imagepath", 355);

            result.CategoryName.Should().Be("TestCat");
        }

        [TestCase]
        public void CreateWebPart_ParentIsNotRoot_DisplayNameNull()
        {
            var webPartServiceMock = new Mock<WebPartServiceMock>();

            var webPartCategories = new List<IWebPartCategory>();

            var rootCatMock = new Mock<IWebPartCategory>();
            rootCatMock.SetupGet(x => x.CategoryDisplayName).Returns("/");
            rootCatMock.SetupGet(x => x.CategoryName).Returns("/");
            rootCatMock.SetupGet(x => x.CategoryPath).Returns("/");
            rootCatMock.SetupGet(x => x.CategoryID).Returns(355);
            webPartCategories.Add(rootCatMock.Object);

            var catMock1 = new Mock<IWebPartCategory>();
            catMock1.SetupGet(x => x.CategoryDisplayName).Returns("My");
            catMock1.SetupGet(x => x.CategoryName).Returns("My");
            catMock1.SetupGet(x => x.CategoryPath).Returns("/My");
            catMock1.SetupGet(x => x.CategoryID).Returns(400);
            webPartCategories.Add(catMock1.Object);

            webPartServiceMock.SetupGet(x => x.WebPartCategories).Returns(webPartCategories);

            var businessLayer = new NewCMSWebPartCategoryBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                WebPartService = webPartServiceMock.Object,
            };

            var result = businessLayer.CreateWebPart("/My/Test", null, "/imagepath");

            webPartServiceMock.Object.VerifyCreate("Test", "Test", "/My/Test", "/imagepath", 400);

            result.CategoryName.Should().Be("TestCat");
        }

        [TestCase]
        public void CreateWebPart_ParentIsNotRoot_DisplayNameNotNull()
        {
            var webPartServiceMock = new Mock<WebPartServiceMock>();

            var webPartCategories = new List<IWebPartCategory>();

            var rootCatMock = new Mock<IWebPartCategory>();
            rootCatMock.SetupGet(x => x.CategoryDisplayName).Returns("/");
            rootCatMock.SetupGet(x => x.CategoryName).Returns("/");
            rootCatMock.SetupGet(x => x.CategoryPath).Returns("/");
            rootCatMock.SetupGet(x => x.CategoryID).Returns(355);
            webPartCategories.Add(rootCatMock.Object);

            var catMock1 = new Mock<IWebPartCategory>();
            catMock1.SetupGet(x => x.CategoryDisplayName).Returns("My");
            catMock1.SetupGet(x => x.CategoryName).Returns("My");
            catMock1.SetupGet(x => x.CategoryPath).Returns("/My");
            catMock1.SetupGet(x => x.CategoryID).Returns(400);
            webPartCategories.Add(catMock1.Object);

            webPartServiceMock.SetupGet(x => x.WebPartCategories).Returns(webPartCategories);

            var businessLayer = new NewCMSWebPartCategoryBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                WebPartService = webPartServiceMock.Object,
            };

            var result = businessLayer.CreateWebPart("/My/Test", "My Test", "/imagepath");

            webPartServiceMock.Object.VerifyCreate("My Test", "Test", "/My/Test", "/imagepath", 400);

            result.CategoryName.Should().Be("TestCat");
        }

        public abstract class WebPartServiceMock : IWebPartService
        {
            private IWebPartCategory webPartCategory;

            public abstract IEnumerable<IWebPart> WebParts { get; }

            public abstract IEnumerable<IWebPartCategory> WebPartCategories { get; }

            public IWebPartCategory Create(IWebPartCategory webPartCategory)
            {
                this.webPartCategory = webPartCategory;

                var testCat = new Mock<IWebPartCategory>();
                testCat.SetupGet(x => x.CategoryDisplayName).Returns("TestCat");
                testCat.SetupGet(x => x.CategoryName).Returns("TestCat");
                testCat.SetupGet(x => x.CategoryPath).Returns("/TestCat");

                return testCat.Object;
            }

            public abstract void Delete(IWebPartCategory webPartCategory);

            public abstract IWebPartCategory GetWebPartCategory(int id);

            public void VerifyCreate(string displayName, string name, string path, string imagePath, int parentId)
            {
                this.webPartCategory.CategoryDisplayName.Should().Be(displayName);
                this.webPartCategory.CategoryName.Should().Be(name);
                this.webPartCategory.CategoryPath.Should().Be(path);
                this.webPartCategory.CategoryImagePath.Should().Be(imagePath);
                this.webPartCategory.CategoryParentID.Should().Be(parentId);
            }

            public abstract void Update(IWebPartCategory webPartCategory);
        }
    }
}

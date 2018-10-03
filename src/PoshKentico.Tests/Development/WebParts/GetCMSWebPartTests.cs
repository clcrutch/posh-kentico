// <copyright file="GetCMSWebPartTests.cs" company="Chris Crutchfield">
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
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;

namespace PoshKentico.Tests.Development.WebParts
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class GetCMSWebPartTests
    {
        [TestCase]
        public void ShouldGetWebPartFromPath()
        {
            // Setup web part category
            var webPartCategoryMock = new Mock<IWebPartCategory>();
            webPartCategoryMock
                .Setup(x => x.CategoryID)
                .Returns(15);
            webPartCategoryMock
                .Setup(x => x.CategoryPath)
                .Returns("/Category");
            var webPartCategoryObj = webPartCategoryMock.Object;

            // Setup web part
            var webPartMock = new Mock<IWebPart>();
            webPartMock
                .Setup(x => x.WebPartCategoryID)
                .Returns(15);
            webPartMock
                .Setup(x => x.WebPartName)
                .Returns("WebPart");
            var webPartObj = webPartMock.Object;

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.WebPartCategories)
                .Returns(new IWebPartCategory[]
                {
                    webPartCategoryObj,
                });
            webPartServiceMock
                .Setup(x => x.GetWebParts(webPartCategoryObj))
                .Returns(new IWebPart[] { webPartObj });

            // Setup business layer
            var businessLayer = new GetCMSWebPartBusiness
            {
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
            };

            var result = businessLayer.GetWebPart("/Category/WebPart");

            result
                .Should()
                .NotBeNull();

            result
                .Should().BeEquivalentTo(webPartObj);
        }

        [TestCase]
        public void ShouldGetWebPartFromNoPath()
        {
            // Setup web part category
            var webPartCategoryMock = new Mock<IWebPartCategory>();
            webPartCategoryMock
                .Setup(x => x.CategoryID)
                .Returns(15);
            webPartCategoryMock
                .Setup(x => x.CategoryPath)
                .Returns("/");
            var webPartCategoryObj = webPartCategoryMock.Object;

            // Setup web part
            var webPartMock = new Mock<IWebPart>();
            webPartMock
                .Setup(x => x.WebPartCategoryID)
                .Returns(15);
            webPartMock
                .Setup(x => x.WebPartName)
                .Returns("WebPart");
            var webPartObj = webPartMock.Object;

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.WebPartCategories)
                .Returns(new IWebPartCategory[]
                {
                    webPartCategoryObj,
                });
            webPartServiceMock
                .Setup(x => x.GetWebParts(webPartCategoryObj))
                .Returns(new IWebPart[] { webPartObj });

            // Setup business layer
            var businessLayer = new GetCMSWebPartBusiness
            {
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
            };

            var result = businessLayer.GetWebPart("/WebPart");

            result
                .Should()
                .NotBeNull();

            result
                .Should().BeEquivalentTo(webPartObj);
        }

        [TestCase]
        public void ShouldGetWebPartByField()
        {
            // Setup web part
            var webPartMock = new Mock<IWebPart>();
            var webPartObj = webPartMock.Object;

            // Setup web part field
            var webPartFieldMock = new Mock<IWebPartField>();
            webPartFieldMock
                .Setup(x => x.WebPart)
                .Returns(webPartObj);

            // Setup business layer
            var businessLayer = new GetCMSWebPartBusiness
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
            };
            var result = businessLayer.GetWebPart(webPartFieldMock.Object);

            result
                .Should()
                .NotBeNull();

            result
                .Should().BeEquivalentTo(webPartObj);
        }

        [TestCase]
        public void ShouldGetAllWebParts()
        {
            // Setup web part
            var webPartMock = new Mock<IWebPart>();
            var webPartObj = webPartMock.Object;

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.WebParts)
                .Returns(new IWebPart[] { webPartObj });

            // Setup business layer
            var businessLayer = new GetCMSWebPartBusiness
            {
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
            };
            var result = businessLayer.GetWebParts();

            result
                .Should()
                .NotBeNullOrEmpty();

            result
                .Single()
                .Should()
                .NotBeNull();

            result
                .Single()
                .Should().BeEquivalentTo(webPartObj);
        }

        [TestCase]
        public void ShouldGetWebPartsByMatchStringWithoutRegex()
        {
            // Setup web part
            var webPartMock = new Mock<IWebPart>();
            webPartMock
                .Setup(x => x.WebPartDisplayName)
                .Returns("aDisplaya");
            webPartMock
                .Setup(x => x.WebPartName)
                .Returns("dTests");
            var webPartObj = webPartMock.Object;

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.WebParts)
                .Returns(new IWebPart[] { webPartObj });

            // Setup business layer
            var businessLayer = new GetCMSWebPartBusiness
            {
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
            };

            // Test Display Name
            var results = businessLayer.GetWebParts("*Display*", false);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Single()
                .Should().BeEquivalentTo(webPartObj);

            results = businessLayer.GetWebParts("Display*", false);
            results
                .Should()
                .BeEmpty();

            // Test Name
            results = businessLayer.GetWebParts("*test*", false);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Single()
                .Should().BeEquivalentTo(webPartObj);

            results = businessLayer.GetWebParts("test*", false);
            results
                .Should()
                .BeEmpty();
        }

        [TestCase]
        public void ShouldGetWebPartsByMatchStringWithRegex()
        {
            // Setup web part
            var webPartMock = new Mock<IWebPart>();
            webPartMock
                .Setup(x => x.WebPartDisplayName)
                .Returns("aDisplaya");
            webPartMock
                .Setup(x => x.WebPartName)
                .Returns("dTests");
            var webPartObj = webPartMock.Object;

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.WebParts)
                .Returns(new IWebPart[] { webPartObj });

            // Setup business layer
            var businessLayer = new GetCMSWebPartBusiness
            {
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
            };

            // Test Display name
            var results = businessLayer.GetWebParts("[a-z]Display(a)+", true);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Single()
                .Should().BeEquivalentTo(webPartObj);

            results = businessLayer.GetWebParts("[a-z]NotDisplay(a)+", true);
            results
                .Should()
                .BeEmpty();

            // Test Name
            results = businessLayer.GetWebParts("[a-z]Test(s)+", true);
            results
                .Should()
                .NotBeNullOrEmpty();
            results.Single()
                .Should().BeEquivalentTo(webPartObj);

            results = businessLayer.GetWebParts("[a-z]nottest(s)+", true);
            results
                .Should()
                .BeEmpty();
        }

        [TestCase]
        public void ShouldGetWebPartsByCategoryFromCategory()
        {
            // Setup web part category
            var webPartCategoryMock = new Mock<IWebPartCategory>();
            webPartCategoryMock
                .Setup(x => x.CategoryID)
                .Returns(15);
            var webPartCategoryObj = webPartCategoryMock.Object;

            // Setup web part
            var webPartMock = new Mock<IWebPart>();
            webPartMock
                .Setup(x => x.WebPartCategoryID)
                .Returns(15);
            var webPartObj = webPartMock.Object;

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetWebParts(webPartCategoryObj))
                .Returns(new IWebPart[] { webPartObj });

            // Setup business layer
            var businessLayer = new GetCMSWebPartBusiness
            {
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
            };

            var result = businessLayer.GetWebPartsByCategory(webPartCategoryObj);

            result
                .Should()
                .NotBeNullOrEmpty();

            result
                .Single()
                .Should().BeEquivalentTo(webPartObj);
        }

        [TestCase]
        public void ShouldGetWebPartsByCategory()
        {
            // Setup web part category
            var webPartCategoryMock = new Mock<IWebPartCategory>();
            webPartCategoryMock
                .Setup(x => x.CategoryID)
                .Returns(15);
            webPartCategoryMock
                .Setup(x => x.CategoryPath)
                .Returns("/Category");
            var webPartCategoryObj = webPartCategoryMock.Object;

            // Setup web part
            var webPartMock = new Mock<IWebPart>();
            webPartMock
                .Setup(x => x.WebPartCategoryID)
                .Returns(15);
            var webPartObj = webPartMock.Object;

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.WebParts)
                .Returns(new IWebPart[] { webPartObj });

            // Setup category business layer
            var categoryBusinessLayerMock = new Mock<GetCMSWebPartCategoryBusiness>();
            categoryBusinessLayerMock
                .Setup(x => x.GetWebPartCategories("*cate*", false, false))
                .Returns(new IWebPartCategory[] { webPartCategoryObj });

            // Setup business layer
            var businessLayer = new GetCMSWebPartBusiness
            {
                GetCMSWebPartCategoryBusiness = categoryBusinessLayerMock.Object,
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
            };

            var result = businessLayer.GetWebPartsByCategories("*cate*", false);

            result
                .Should()
                .NotBeNullOrEmpty();

            result
                .Single()
                .Should().BeEquivalentTo(webPartObj);
        }

        [TestCase]
        public void ShouldGetWebPartCategoriesFromIDsWithoutRecurse()
        {
            // Setup web part category
            var webPartCategoryMock1 = new Mock<IWebPartCategory>();
            webPartCategoryMock1
                .Setup(x => x.CategoryID)
                .Returns(15);
            var webPartCategoryMock2 = new Mock<IWebPartCategory>();
            webPartCategoryMock2
                .Setup(x => x.CategoryID)
                .Returns(16);
            var webPartCategoryMock3 = new Mock<IWebPartCategory>();
            webPartCategoryMock3
                .Setup(x => x.CategoryID)
                .Returns(17);
            webPartCategoryMock3
                .Setup(x => x.CategoryParentID)
                .Returns(16);

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetWebPartCategory(It.IsAny<int>()))
                .Returns<IWebPartCategory>(null);
            webPartServiceMock
                .Setup(x => x.GetWebPartCategory(15))
                .Returns(webPartCategoryMock1.Object);
            webPartServiceMock
                .Setup(x => x.GetWebPartCategory(16))
                .Returns(webPartCategoryMock2.Object);
            webPartServiceMock
                .Setup(x => x.GetWebPartCategory(17))
                .Returns(webPartCategoryMock3.Object);

            var businessLayer = new GetCMSWebPartCategoryBusiness
            {
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
            };

            var results = businessLayer.GetWebPartCategories(new int[] { 15, 16, 18 }, false);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Should()
                .Contain(new IWebPartCategory[] { webPartCategoryMock1.Object, webPartCategoryMock2.Object });
            results
                .Should()
                .NotContainNulls();
        }

        [TestCase]
        public void ShouldGetWebPartCategoriesFromIDsWithRecurse()
        {
            // Setup web part category
            var webPartCategoryMock1 = new Mock<IWebPartCategory>();
            webPartCategoryMock1
                .Setup(x => x.CategoryID)
                .Returns(15);
            var webPartCategoryMock2 = new Mock<IWebPartCategory>();
            webPartCategoryMock2
                .Setup(x => x.CategoryID)
                .Returns(16);
            var webPartCategoryMock3 = new Mock<IWebPartCategory>();
            webPartCategoryMock3
                .Setup(x => x.CategoryID)
                .Returns(17);
            webPartCategoryMock3
                .Setup(x => x.CategoryParentID)
                .Returns(16);

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetWebPartCategory(It.IsAny<int>()))
                .Returns<IWebPartCategory>(null);
            webPartServiceMock
                .Setup(x => x.GetWebPartCategory(15))
                .Returns(webPartCategoryMock1.Object);
            webPartServiceMock
                .Setup(x => x.GetWebPartCategory(16))
                .Returns(webPartCategoryMock2.Object);
            webPartServiceMock
                .Setup(x => x.GetWebPartCategory(17))
                .Returns(webPartCategoryMock3.Object);
            webPartServiceMock
                .Setup(x => x.GetWebPartCategories(webPartCategoryMock2.Object))
                .Returns(new IWebPartCategory[] { webPartCategoryMock3.Object });

            var businessLayer = new GetCMSWebPartCategoryBusiness
            {
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
            };

            var results = businessLayer.GetWebPartCategories(new int[] { 15, 16, 18 }, true);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Should()
                .Contain(new IWebPartCategory[] { webPartCategoryMock1.Object, webPartCategoryMock2.Object, webPartCategoryMock3.Object });
            results
                .Should()
                .NotContainNulls();
        }

        [TestCase]
        public void ShouldGetWebPartCategoriesFromParentCategoryWithoutRecurse()
        {
            // Setup web part category
            var webPartCategoryMock1 = new Mock<IWebPartCategory>();
            webPartCategoryMock1
                .Setup(x => x.CategoryID)
                .Returns(15);
            var webPartCategoryMock2 = new Mock<IWebPartCategory>();
            webPartCategoryMock2
                .Setup(x => x.CategoryID)
                .Returns(16);
            var webPartCategoryMock3 = new Mock<IWebPartCategory>();
            webPartCategoryMock3
                .Setup(x => x.CategoryID)
                .Returns(17);
            webPartCategoryMock3
                .Setup(x => x.CategoryParentID)
                .Returns(16);

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetWebPartCategory(It.IsAny<int>()))
                .Returns<IWebPartCategory>(null);
            webPartServiceMock
                .Setup(x => x.GetWebPartCategory(15))
                .Returns(webPartCategoryMock1.Object);
            webPartServiceMock
                .Setup(x => x.GetWebPartCategory(16))
                .Returns(webPartCategoryMock2.Object);
            webPartServiceMock
                .Setup(x => x.GetWebPartCategory(17))
                .Returns(webPartCategoryMock3.Object);
            webPartServiceMock
                .Setup(x => x.GetWebPartCategories(webPartCategoryMock2.Object))
                .Returns(new IWebPartCategory[] { webPartCategoryMock3.Object });

            // Setup business layer
            var businessLayer = new GetCMSWebPartCategoryBusiness
            {
                WebPartService = webPartServiceMock.Object,
            };

            // Test business layer
            var results = businessLayer.GetWebPartCategories(webPartCategoryMock2.Object, false);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Should()
                .Contain(new IWebPartCategory[] { webPartCategoryMock3.Object });
            results
                .Should()
                .NotContainNulls();
        }

        [TestCase]
        public void ShouldGetWebPartCategoriesFromParentCategoryWithRecurse()
        {
            // Setup web part category
            var webPartCategoryMock1 = new Mock<IWebPartCategory>();
            webPartCategoryMock1
                .Setup(x => x.CategoryID)
                .Returns(15);
            var webPartCategoryMock2 = new Mock<IWebPartCategory>();
            webPartCategoryMock2
                .Setup(x => x.CategoryID)
                .Returns(16);
            var webPartCategoryMock3 = new Mock<IWebPartCategory>();
            webPartCategoryMock3
                .Setup(x => x.CategoryID)
                .Returns(17);
            webPartCategoryMock3
                .Setup(x => x.CategoryParentID)
                .Returns(16);
            var webPartCategoryMock4 = new Mock<IWebPartCategory>();
            webPartCategoryMock4
                .Setup(x => x.CategoryID)
                .Returns(18);
            webPartCategoryMock4
                .Setup(x => x.CategoryParentID)
                .Returns(17);

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetWebPartCategory(It.IsAny<int>()))
                .Returns<IWebPartCategory>(null);
            webPartServiceMock
                .Setup(x => x.GetWebPartCategory(15))
                .Returns(webPartCategoryMock1.Object);
            webPartServiceMock
                .Setup(x => x.GetWebPartCategory(16))
                .Returns(webPartCategoryMock2.Object);
            webPartServiceMock
                .Setup(x => x.GetWebPartCategory(17))
                .Returns(webPartCategoryMock3.Object);
            webPartServiceMock
                .Setup(x => x.GetWebPartCategories(webPartCategoryMock2.Object))
                .Returns(new IWebPartCategory[] { webPartCategoryMock3.Object });
            webPartServiceMock
                .Setup(x => x.GetWebPartCategories(webPartCategoryMock3.Object))
                .Returns(new IWebPartCategory[] { webPartCategoryMock4.Object });

            // Setup business layer
            var businessLayer = new GetCMSWebPartCategoryBusiness
            {
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
            };

            // Test business layer
            var results = businessLayer.GetWebPartCategories(webPartCategoryMock2.Object, true);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Should()
                .Contain(new IWebPartCategory[] { webPartCategoryMock3.Object, webPartCategoryMock4.Object });
            results
                .Should()
                .NotContainNulls();
        }

        [TestCase]
        public void ShouldGetWebPartCategoriesByPathWithoutRecurse()
        {
            // Setup web part category
            var webPartCategoryMock1 = new Mock<IWebPartCategory>();
            webPartCategoryMock1
                .Setup(x => x.CategoryPath)
                .Returns("/Category1");
            var webPartCategoryMock2 = new Mock<IWebPartCategory>();
            webPartCategoryMock2
                .Setup(x => x.CategoryPath)
                .Returns("/Category2");

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.WebPartCategories)
                .Returns(new IWebPartCategory[]
                {
                    webPartCategoryMock1.Object,
                    webPartCategoryMock2.Object,
                });

            // Setup business layer
            var businessLayer = new GetCMSWebPartCategoryBusiness
            {
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
            };

            // Test business layer
            var results = businessLayer.GetWebPartCategories("/Category1", false);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Single()
                .Should().BeEquivalentTo(webPartCategoryMock1.Object);
        }

        [TestCase]
        public void ShouldGetWebPartCategoriesByPathWithRecurse()
        {
            // Setup web part category
            var webPartCategoryMock1 = new Mock<IWebPartCategory>();
            webPartCategoryMock1
                .Setup(x => x.CategoryPath)
                .Returns("/Category1");
            var webPartCategoryMock2 = new Mock<IWebPartCategory>();
            webPartCategoryMock2
                .Setup(x => x.CategoryPath)
                .Returns("/Category1/Child1");

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.WebPartCategories)
                .Returns(new IWebPartCategory[]
                {
                    webPartCategoryMock1.Object,
                    webPartCategoryMock2.Object,
                });
            webPartServiceMock
                .Setup(x => x.GetWebPartCategories(webPartCategoryMock1.Object))
                .Returns(new IWebPartCategory[] { webPartCategoryMock2.Object });

            // Setup business layer
            var businessLayer = new GetCMSWebPartCategoryBusiness
            {
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
            };

            // Test business layer
            var results = businessLayer.GetWebPartCategories("/Category1", true);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Should()
                .Contain(new IWebPartCategory[]
                {
                    webPartCategoryMock1.Object,
                    webPartCategoryMock2.Object,
                });
        }

        [TestCase]
        public void ShouldGetWebPartCategoryFromWebPart()
        {
            // Setup web part
            var webPartMock = new Mock<IWebPart>();
            webPartMock
                .Setup(x => x.WebPartCategoryID)
                .Returns(15);

            // Setup web part category
            var webPartCategoryMock1 = new Mock<IWebPartCategory>();
            webPartCategoryMock1
                .Setup(x => x.CategoryID)
                .Returns(15);
            var webPartCategoryMock2 = new Mock<IWebPartCategory>();
            webPartCategoryMock2
                .Setup(x => x.CategoryID)
                .Returns(16);
            var webPartCategoryMock3 = new Mock<IWebPartCategory>();
            webPartCategoryMock3
                .Setup(x => x.CategoryID)
                .Returns(17);
            var webPartCategoryMock4 = new Mock<IWebPartCategory>();
            webPartCategoryMock4
                .Setup(x => x.CategoryID)
                .Returns(18);

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.WebPartCategories)
                .Returns(new IWebPartCategory[]
                {
                    webPartCategoryMock1.Object,
                    webPartCategoryMock2.Object,
                    webPartCategoryMock3.Object,
                });

            // Setup business layer
            var businessLayer = new GetCMSWebPartCategoryBusiness
            {
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
            };

            // Test business layer
            var results = businessLayer.GetWebPartCategory(webPartMock.Object);
            results
                .Should()
                .NotBeNull();
            results
                .Should().BeEquivalentTo(webPartCategoryMock1.Object);
        }
    }
}

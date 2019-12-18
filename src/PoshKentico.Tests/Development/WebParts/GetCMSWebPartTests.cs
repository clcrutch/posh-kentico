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
    public class GetCMSWebPartTests
    {
        [TestCase]
        public void ShouldGetWebPartFromPath()
        {
            // Setup web part category
            var webPartCategoryMock = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock
                .Setup(x => x.ID)
                .Returns(15);
            webPartCategoryMock
                .Setup(x => x.Path)
                .Returns("/Category");
            var webPartCategoryObj = webPartCategoryMock.Object;

            // Setup web part
            var webPartMock = new Mock<IWebPart>();
            webPartMock
                .Setup(x => x.ID)
                .Returns(15);
            webPartMock
                .Setup(x => x.Name)
                .Returns("WebPart");
            var webPartObj = webPartMock.Object;

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.Categories)
                .Returns(new IControlCategory<WebPartCategoryInfo>[]
                {
                    webPartCategoryObj,
                });
            webPartServiceMock
                .Setup(x => x.GetControls(webPartCategoryObj))
                .Returns(new IWebPart[] { webPartObj });

            // Setup business layer
            var businessLayer = this.GetBusinessLayer(webPartServiceMock.Object);

            var result = businessLayer.GetControl("/Category/WebPart");

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
            var webPartCategoryMock = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock
                .Setup(x => x.ID)
                .Returns(15);
            webPartCategoryMock
                .Setup(x => x.Path)
                .Returns("/");
            var webPartCategoryObj = webPartCategoryMock.Object;

            // Setup web part
            var webPartMock = new Mock<IWebPart>();
            webPartMock
                .Setup(x => x.ID)
                .Returns(15);
            webPartMock
                .Setup(x => x.Name)
                .Returns("WebPart");
            var webPartObj = webPartMock.Object;

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.Categories)
                .Returns(new IControlCategory<WebPartCategoryInfo>[]
                {
                    webPartCategoryObj,
                });
            webPartServiceMock
                .Setup(x => x.GetControls(webPartCategoryObj))
                .Returns(new IWebPart[] { webPartObj });

            // Setup business layer
            var businessLayer = this.GetBusinessLayer(webPartServiceMock.Object);

            var result = businessLayer.GetControl("/WebPart");

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
            var webPartFieldMock = new Mock<IControlField<WebPartInfo>>();
            webPartFieldMock
                .Setup(x => x.Control)
                .Returns(webPartObj);

            // Setup business layer
            var businessLayer = this.GetBusinessLayer(null);
            var result = businessLayer.GetControl(webPartFieldMock.Object);

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
                .Setup(x => x.Controls)
                .Returns(new IWebPart[] { webPartObj });

            // Setup business layer
            var businessLayer = this.GetBusinessLayer(webPartServiceMock.Object);
            var result = businessLayer.GetControls();

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
                .Setup(x => x.DisplayName)
                .Returns("aDisplaya");
            webPartMock
                .Setup(x => x.Name)
                .Returns("dTests");
            var webPartObj = webPartMock.Object;

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.Controls)
                .Returns(new IWebPart[] { webPartObj });

            // Setup business layer
            var businessLayer = this.GetBusinessLayer(webPartServiceMock.Object);

            // Test Display Name
            var results = businessLayer.GetControls("*Display*", false);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Single()
                .Should().BeEquivalentTo(webPartObj);

            results = businessLayer.GetControls("Display*", false);
            results
                .Should()
                .BeEmpty();

            // Test Name
            results = businessLayer.GetControls("*test*", false);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Single()
                .Should().BeEquivalentTo(webPartObj);

            results = businessLayer.GetControls("test*", false);
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
                .Setup(x => x.DisplayName)
                .Returns("aDisplaya");
            webPartMock
                .Setup(x => x.Name)
                .Returns("dTests");
            var webPartObj = webPartMock.Object;

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.Controls)
                .Returns(new IWebPart[] { webPartObj });

            // Setup business layer
            var businessLayer = this.GetBusinessLayer(webPartServiceMock.Object);

            // Test Display name
            var results = businessLayer.GetControls("[a-z]Display(a)+", true);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Single()
                .Should().BeEquivalentTo(webPartObj);

            results = businessLayer.GetControls("[a-z]NotDisplay(a)+", true);
            results
                .Should()
                .BeEmpty();

            // Test Name
            results = businessLayer.GetControls("[a-z]Test(s)+", true);
            results
                .Should()
                .NotBeNullOrEmpty();
            results.Single()
                .Should().BeEquivalentTo(webPartObj);

            results = businessLayer.GetControls("[a-z]nottest(s)+", true);
            results
                .Should()
                .BeEmpty();
        }

        [TestCase]
        public void ShouldGetWebPartsByCategoryFromCategory()
        {
            // Setup web part category
            var webPartCategoryMock = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock
                .Setup(x => x.ID)
                .Returns(15);
            var webPartCategoryObj = webPartCategoryMock.Object;

            // Setup web part
            var webPartMock = new Mock<IWebPart>();
            webPartMock
                .Setup(x => x.ID)
                .Returns(15);
            var webPartObj = webPartMock.Object;

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetControls(webPartCategoryObj))
                .Returns(new IWebPart[] { webPartObj });

            // Setup business layer
            var businessLayer = this.GetBusinessLayer(webPartServiceMock.Object);

            var result = businessLayer.GetControlsByCategory(webPartCategoryObj);

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
            var webPartCategoryMock = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock
                .Setup(x => x.ID)
                .Returns(15);
            webPartCategoryMock
                .Setup(x => x.Path)
                .Returns("/Category");
            var webPartCategoryObj = webPartCategoryMock.Object;

            // Setup web part
            var webPartMock = new Mock<IWebPart>();
            webPartMock
                .Setup(x => x.ID)
                .Returns(15);
            var webPartObj = webPartMock.Object;

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.Controls)
                .Returns(new IWebPart[] { webPartObj });

            // Setup category business layer
            var categoryBusinessLayerMock = new Mock<GetCMSWebPartCategoryBusiness>();
            categoryBusinessLayerMock
                .Setup(x => x.GetControlCategories("*cate*", false, false))
                .Returns(new IControlCategory<WebPartCategoryInfo>[] { webPartCategoryObj });

            // Setup business layer
            var businessLayer = this.GetBusinessLayer(webPartServiceMock.Object);
            businessLayer.GetCMSControlCategoryBusiness = categoryBusinessLayerMock.Object;

            var result = businessLayer.GetControlsByCategories("*cate*", false);

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
            var webPartCategoryMock1 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock1
                .Setup(x => x.ID)
                .Returns(15);
            var webPartCategoryMock2 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock2
                .Setup(x => x.ID)
                .Returns(16);
            var webPartCategoryMock3 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock3
                .Setup(x => x.ID)
                .Returns(17);
            webPartCategoryMock3
                .Setup(x => x.ParentID)
                .Returns(16);

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetControlCategory(It.IsAny<int>()))
                .Returns<IControlCategory<WebPartCategoryInfo>>(null);
            webPartServiceMock
                .Setup(x => x.GetControlCategory(15))
                .Returns(webPartCategoryMock1.Object);
            webPartServiceMock
                .Setup(x => x.GetControlCategory(16))
                .Returns(webPartCategoryMock2.Object);
            webPartServiceMock
                .Setup(x => x.GetControlCategory(17))
                .Returns(webPartCategoryMock3.Object);

            var businessLayer = this.GetCategoryBusinessLayer(webPartServiceMock.Object);

            var results = businessLayer.GetControlCategories(new int[] { 15, 16, 18 }, false);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Should()
                .Contain(new IControlCategory<WebPartCategoryInfo>[] { webPartCategoryMock1.Object, webPartCategoryMock2.Object });
            results
                .Should()
                .NotContainNulls();
        }

        [TestCase]
        public void ShouldGetWebPartCategoriesFromIDsWithRecurse()
        {
            // Setup web part category
            var webPartCategoryMock1 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock1
                .Setup(x => x.ID)
                .Returns(15);
            var webPartCategoryMock2 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock2
                .Setup(x => x.ID)
                .Returns(16);
            var webPartCategoryMock3 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock3
                .Setup(x => x.ID)
                .Returns(17);
            webPartCategoryMock3
                .Setup(x => x.ParentID)
                .Returns(16);

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetControlCategory(It.IsAny<int>()))
                .Returns<IControlCategory<WebPartCategoryInfo>>(null);
            webPartServiceMock
                .Setup(x => x.GetControlCategory(15))
                .Returns(webPartCategoryMock1.Object);
            webPartServiceMock
                .Setup(x => x.GetControlCategory(16))
                .Returns(webPartCategoryMock2.Object);
            webPartServiceMock
                .Setup(x => x.GetControlCategory(17))
                .Returns(webPartCategoryMock3.Object);
            webPartServiceMock
                .Setup(x => x.GetControlCategories(webPartCategoryMock2.Object))
                .Returns(new IControlCategory<WebPartCategoryInfo>[] { webPartCategoryMock3.Object });

            var businessLayer = this.GetCategoryBusinessLayer(webPartServiceMock.Object);

            var results = businessLayer.GetControlCategories(new int[] { 15, 16, 18 }, true);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Should()
                .Contain(new IControlCategory<WebPartCategoryInfo>[] { webPartCategoryMock1.Object, webPartCategoryMock2.Object, webPartCategoryMock3.Object });
            results
                .Should()
                .NotContainNulls();
        }

        [TestCase]
        public void ShouldGetWebPartCategoriesFromParentCategoryWithoutRecurse()
        {
            // Setup web part category
            var webPartCategoryMock1 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock1
                .Setup(x => x.ID)
                .Returns(15);
            var webPartCategoryMock2 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock2
                .Setup(x => x.ID)
                .Returns(16);
            var webPartCategoryMock3 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock3
                .Setup(x => x.ID)
                .Returns(17);
            webPartCategoryMock3
                .Setup(x => x.ParentID)
                .Returns(16);

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetControlCategory(It.IsAny<int>()))
                .Returns<IControlCategory<WebPartCategoryInfo>>(null);
            webPartServiceMock
                .Setup(x => x.GetControlCategory(15))
                .Returns(webPartCategoryMock1.Object);
            webPartServiceMock
                .Setup(x => x.GetControlCategory(16))
                .Returns(webPartCategoryMock2.Object);
            webPartServiceMock
                .Setup(x => x.GetControlCategory(17))
                .Returns(webPartCategoryMock3.Object);
            webPartServiceMock
                .Setup(x => x.GetControlCategories(webPartCategoryMock2.Object))
                .Returns(new IControlCategory<WebPartCategoryInfo>[] { webPartCategoryMock3.Object });

            // Setup business layer
            var businessLayer = new GetCMSWebPartCategoryBusiness
            {
                ControlService = webPartServiceMock.Object,
            };

            // Test business layer
            var results = businessLayer.GetControlCategories(webPartCategoryMock2.Object, false);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Should()
                .Contain(new IControlCategory<WebPartCategoryInfo>[] { webPartCategoryMock3.Object });
            results
                .Should()
                .NotContainNulls();
        }

        [TestCase]
        public void ShouldGetWebPartCategoriesFromParentCategoryWithRecurse()
        {
            // Setup web part category
            var webPartCategoryMock1 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock1
                .Setup(x => x.ID)
                .Returns(15);
            var webPartCategoryMock2 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock2
                .Setup(x => x.ID)
                .Returns(16);
            var webPartCategoryMock3 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock3
                .Setup(x => x.ID)
                .Returns(17);
            webPartCategoryMock3
                .Setup(x => x.ParentID)
                .Returns(16);
            var webPartCategoryMock4 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock4
                .Setup(x => x.ID)
                .Returns(18);
            webPartCategoryMock4
                .Setup(x => x.ParentID)
                .Returns(17);

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetControlCategory(It.IsAny<int>()))
                .Returns<IControlCategory<WebPartCategoryInfo>>(null);
            webPartServiceMock
                .Setup(x => x.GetControlCategory(15))
                .Returns(webPartCategoryMock1.Object);
            webPartServiceMock
                .Setup(x => x.GetControlCategory(16))
                .Returns(webPartCategoryMock2.Object);
            webPartServiceMock
                .Setup(x => x.GetControlCategory(17))
                .Returns(webPartCategoryMock3.Object);
            webPartServiceMock
                .Setup(x => x.GetControlCategories(webPartCategoryMock2.Object))
                .Returns(new IControlCategory<WebPartCategoryInfo>[] { webPartCategoryMock3.Object });
            webPartServiceMock
                .Setup(x => x.GetControlCategories(webPartCategoryMock3.Object))
                .Returns(new IControlCategory<WebPartCategoryInfo>[] { webPartCategoryMock4.Object });

            // Setup business layer
            var businessLayer = this.GetCategoryBusinessLayer(webPartServiceMock.Object);

            // Test business layer
            var results = businessLayer.GetControlCategories(webPartCategoryMock2.Object, true);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Should()
                .Contain(new IControlCategory<WebPartCategoryInfo>[] { webPartCategoryMock3.Object, webPartCategoryMock4.Object });
            results
                .Should()
                .NotContainNulls();
        }

        [TestCase]
        public void ShouldGetWebPartCategoriesByPathWithoutRecurse()
        {
            // Setup web part category
            var webPartCategoryMock1 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock1
                .Setup(x => x.Path)
                .Returns("/Category1");
            var webPartCategoryMock2 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock2
                .Setup(x => x.Path)
                .Returns("/Category2");

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.Categories)
                .Returns(new IControlCategory<WebPartCategoryInfo>[]
                {
                    webPartCategoryMock1.Object,
                    webPartCategoryMock2.Object,
                });

            // Setup business layer
            var businessLayer = this.GetCategoryBusinessLayer(webPartServiceMock.Object);

            // Test business layer
            var results = businessLayer.GetControlCategories("/Category1", false);
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
            var webPartCategoryMock1 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock1
                .Setup(x => x.Path)
                .Returns("/Category1");
            var webPartCategoryMock2 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock2
                .Setup(x => x.Path)
                .Returns("/Category1/Child1");

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.Categories)
                .Returns(new IControlCategory<WebPartCategoryInfo>[]
                {
                    webPartCategoryMock1.Object,
                    webPartCategoryMock2.Object,
                });
            webPartServiceMock
                .Setup(x => x.GetControlCategories(webPartCategoryMock1.Object))
                .Returns(new IControlCategory<WebPartCategoryInfo>[] { webPartCategoryMock2.Object });

            // Setup business layer
            var businessLayer = this.GetCategoryBusinessLayer(webPartServiceMock.Object);

            // Test business layer
            var results = businessLayer.GetControlCategories("/Category1", true);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Should()
                .Contain(new IControlCategory<WebPartCategoryInfo>[]
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
                .Setup(x => x.ID)
                .Returns(15);

            // Setup web part category
            var webPartCategoryMock1 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock1
                .Setup(x => x.ID)
                .Returns(15);
            var webPartCategoryMock2 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock2
                .Setup(x => x.ID)
                .Returns(16);
            var webPartCategoryMock3 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock3
                .Setup(x => x.ID)
                .Returns(17);
            var webPartCategoryMock4 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock4
                .Setup(x => x.ID)
                .Returns(18);

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.Categories)
                .Returns(new IControlCategory<WebPartCategoryInfo>[]
                {
                    webPartCategoryMock1.Object,
                    webPartCategoryMock2.Object,
                    webPartCategoryMock3.Object,
                });

            // Setup business layer
            var businessLayer = this.GetCategoryBusinessLayer(webPartServiceMock.Object);

            // Test business layer
            var results = businessLayer.GetControlCategory(webPartMock.Object);
            results
                .Should()
                .NotBeNull();
            results
                .Should().BeEquivalentTo(webPartCategoryMock1.Object);
        }

        private GetCMSWebPartBusiness GetBusinessLayer(IWebPartService webPartService) =>
            new GetCMSWebPartBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
                GetCMSControlCategoryBusiness = this.GetCategoryBusinessLayer(webPartService),
                ControlService = webPartService,
            };

        private GetCMSWebPartCategoryBusiness GetCategoryBusinessLayer(IWebPartService webPartService) =>
            new GetCMSWebPartCategoryBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
                ControlService = webPartService,
            };
    }
}

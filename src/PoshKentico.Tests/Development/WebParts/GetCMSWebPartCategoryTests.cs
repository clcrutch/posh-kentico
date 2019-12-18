// <copyright file="GetCMSWebPartCategoryTests.cs" company="Chris Crutchfield">
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
    public class GetCMSWebPartCategoryTests
    {
        [TestCase]
        public void ShouldGetWebPartCategories()
        {
            // Setup web part category
            var webPartCategoryMock = new Mock<IControlCategory<WebPartCategoryInfo>>();
            var webPartCategoryObj = webPartCategoryMock.Object;

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.Categories)
                .Returns(new IControlCategory<WebPartCategoryInfo>[]
                {
                    webPartCategoryObj,
                });

            var businessLayer = new GetCMSWebPartCategoryBusiness
            {
                ControlService = webPartServiceMock.Object,
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
            };

            var results = businessLayer.GetControlCategories();

            results
                .Should()
                .NotBeNullOrEmpty();

            results
                .Single()
                .Should().BeEquivalentTo(webPartCategoryObj);
        }

        [TestCase]
        public void ShouldGetWebPartCategoriesFromMatchStringWithoutRegexAndWithoutRecurse()
        {
            // Setup web part category
            var webPartCategoryMock1 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock1
                .Setup(x => x.DisplayName)
                .Returns("Display Name");
            webPartCategoryMock1
                .Setup(x => x.Name)
                .Returns("Category");
            var webPartCategoryMock2 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock2
                .Setup(x => x.DisplayName)
                .Returns("Different");
            webPartCategoryMock2
                .Setup(x => x.Name)
                .Returns("Different");

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.Categories)
                .Returns(new IControlCategory<WebPartCategoryInfo>[]
                {
                    webPartCategoryMock1.Object,
                    webPartCategoryMock2.Object,
                });

            var businessLayer = new GetCMSWebPartCategoryBusiness
            {
                ControlService = webPartServiceMock.Object,
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
            };

            // Test Display Name
            var results = businessLayer.GetControlCategories("*spla*", false, false);
            results
                .Should()
                .NotBeNullOrEmpty();

            results
                .Single()
                .Should().BeEquivalentTo(webPartCategoryMock1.Object);

            results = businessLayer.GetControlCategories("spla*", false, false);
            results
                .Should()
                .BeEmpty();

            // Test Name
            results = businessLayer.GetControlCategories("*cate*", false, false);
            results
                .Should()
                .NotBeNullOrEmpty();

            results
                .Single()
                .Should().BeEquivalentTo(webPartCategoryMock1.Object);

            results = businessLayer.GetControlCategories("*cate", false, false);
            results
                .Should()
                .BeEmpty();
        }

        [TestCase]
        public void ShouldGetWebPartCategoriesFromMatchStringWithoutRegexAndWithRecurse()
        {
            // Setup web part category
            var webPartCategoryMock1 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock1
                .Setup(x => x.DisplayName)
                .Returns("Display Name");
            webPartCategoryMock1
                .Setup(x => x.Name)
                .Returns("Category");
            var webPartCategoryMock2 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock2
                .Setup(x => x.DisplayName)
                .Returns("Different");
            webPartCategoryMock2
                .Setup(x => x.Name)
                .Returns("Different");

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

            var businessLayer = new GetCMSWebPartCategoryBusiness
            {
                ControlService = webPartServiceMock.Object,
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
            };

            // Test Display Name
            var results = businessLayer.GetControlCategories("*spla*", false, true);
            results
                .Should()
                .NotBeNullOrEmpty();

            results
                .Count()
                .Should()
                .Be(2);

            results
                .Should()
                .Contain(webPartCategoryMock1.Object);

            results
                .Should()
                .Contain(webPartCategoryMock2.Object);

            results = businessLayer.GetControlCategories("spla*", false, true);
            results
                .Should()
                .BeEmpty();

            // Test Name
            results = businessLayer.GetControlCategories("*cate*", false, true);
            results
                .Should()
                .NotBeNullOrEmpty();

            results
                .Count()
                .Should()
                .Be(2);

            results
                .Should()
                .Contain(webPartCategoryMock1.Object);

            results
                .Should()
                .Contain(webPartCategoryMock2.Object);

            results = businessLayer.GetControlCategories("*cate", false, true);
            results
                .Should()
                .BeEmpty();
        }

        [TestCase]
        public void ShouldGetWebPartCategoriesFromMatchStringWithRegexAndWithoutRecurse()
        {
            // Setup web part category
            var webPartCategoryMock1 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock1
                .Setup(x => x.DisplayName)
                .Returns("Display Name");
            webPartCategoryMock1
                .Setup(x => x.Name)
                .Returns("Category");
            var webPartCategoryMock2 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock2
                .Setup(x => x.DisplayName)
                .Returns("Different");
            webPartCategoryMock2
                .Setup(x => x.Name)
                .Returns("Different");

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.Categories)
                .Returns(new IControlCategory<WebPartCategoryInfo>[]
                {
                    webPartCategoryMock1.Object,
                    webPartCategoryMock2.Object,
                });

            var businessLayer = new GetCMSWebPartCategoryBusiness
            {
                ControlService = webPartServiceMock.Object,
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
            };

            // Test Display Name
            var results = businessLayer.GetControlCategories("y+ Name$", true, false);
            results
                .Should()
                .NotBeNullOrEmpty();

            results
                .Single()
                .Should().BeEquivalentTo(webPartCategoryMock1.Object);

            results = businessLayer.GetControlCategories("^y+ Name$", true, false);
            results
                .Should()
                .BeEmpty();

            // Test Name
            results = businessLayer.GetControlCategories("^cate+", true, false);
            results
                .Should()
                .NotBeNullOrEmpty();

            results
                .Single()
                .Should().BeEquivalentTo(webPartCategoryMock1.Object);

            results = businessLayer.GetControlCategories("cate$", true, false);
            results
                .Should()
                .BeEmpty();
        }

        [TestCase]
        public void ShouldGetWebPartCategoriesFromMatchStringWithtRegexAndWithRecurse()
        {
            // Setup web part category
            var webPartCategoryMock1 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock1
                .Setup(x => x.DisplayName)
                .Returns("Display Name");
            webPartCategoryMock1
                .Setup(x => x.Name)
                .Returns("Category");
            var webPartCategoryMock2 = new Mock<IControlCategory<WebPartCategoryInfo>>();
            webPartCategoryMock2
                .Setup(x => x.DisplayName)
                .Returns("Different");
            webPartCategoryMock2
                .Setup(x => x.Name)
                .Returns("Different");

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

            var businessLayer = new GetCMSWebPartCategoryBusiness
            {
                ControlService = webPartServiceMock.Object,
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
            };

            // Test Display Name
            var results = businessLayer.GetControlCategories("y+ Name$", true, true);
            results
                .Should()
                .NotBeNullOrEmpty();

            results
                .Count()
                .Should()
                .Be(2);

            results
                .Should()
                .Contain(webPartCategoryMock1.Object);

            results
                .Should()
                .Contain(webPartCategoryMock2.Object);

            results = businessLayer.GetControlCategories("^y+ Name$", true, true);
            results
                .Should()
                .BeEmpty();

            // Test Name
            results = businessLayer.GetControlCategories("^cate+", true, true);
            results
                .Should()
                .NotBeNullOrEmpty();

            results
                .Count()
                .Should()
                .Be(2);

            results
                .Should()
                .Contain(webPartCategoryMock1.Object);

            results
                .Should()
                .Contain(webPartCategoryMock2.Object);

            results = businessLayer.GetControlCategories("cate$", true, true);
            results
                .Should()
                .BeEmpty();
        }
    }
}

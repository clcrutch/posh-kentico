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

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Tests.Development.WebParts
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class GetCMSWebPartCategoryTests
    {
        [TestCase]
        public void GetWebPartCategories_NoParameters()
        {
            var applicationServiceMock = new Mock<ICmsApplicationService>();
            var webPartServiceMock = new Mock<IWebPartService>();

            var businessLayer = new GetCMSWebPartCategoryBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                CmsApplicationService = applicationServiceMock.Object,
                WebPartService = webPartServiceMock.Object,
            };

            businessLayer.GetWebPartCategories();

            applicationServiceMock.Verify(x => x.Initialize(true, Assert.NotNull, Assert.NotNull));
            webPartServiceMock.VerifyGet(x => x.WebPartCategories);
        }

        [TestCase]
        public void GetWebPartCategories_MatchString_ExactFalse()
        {
            var applicationServiceMock = new Mock<ICmsApplicationService>();

            var webPartServiceMock = new Mock<IWebPartService>();

            var webPartCategories = new List<IWebPartCategory>();

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

            var businessLayer = new GetCMSWebPartCategoryBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                CmsApplicationService = applicationServiceMock.Object,
                WebPartService = webPartServiceMock.Object,
            };

            businessLayer.GetWebPartCategories("my", false).Should().NotBeNullOrEmpty().And.HaveCount(1);

            applicationServiceMock.Verify(x => x.Initialize(true, Assert.NotNull, Assert.NotNull));
            webPartServiceMock.VerifyGet(x => x.WebPartCategories);

            // Reset to go again.
            applicationServiceMock.ResetCalls();
            webPartServiceMock.ResetCalls();

            businessLayer.GetWebPartCategories("/my", false).Should().NotBeNullOrEmpty().And.HaveCount(2);

            applicationServiceMock.Verify(x => x.Initialize(true, Assert.NotNull, Assert.NotNull));
            webPartServiceMock.VerifyGet(x => x.WebPartCategories);

            // Reset to go again.
            applicationServiceMock.ResetCalls();
            webPartServiceMock.ResetCalls();

            businessLayer.GetWebPartCategories("/ny", false).Should().NotBeNull().And.BeEmpty();

            applicationServiceMock.Verify(x => x.Initialize(true, Assert.NotNull, Assert.NotNull));
            webPartServiceMock.VerifyGet(x => x.WebPartCategories);
        }

        [TestCase]
        public void GetWebPartCategories_MatchString_ExactTrue()
        {
            var applicationServiceMock = new Mock<ICmsApplicationService>();

            var webPartServiceMock = new Mock<IWebPartService>();

            var webPartCategories = new List<IWebPartCategory>();

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

            var businessLayer = new GetCMSWebPartCategoryBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                CmsApplicationService = applicationServiceMock.Object,
                WebPartService = webPartServiceMock.Object,
            };

            businessLayer.GetWebPartCategories("my", true).Should().NotBeNull().And.BeEmpty();

            applicationServiceMock.Verify(x => x.Initialize(true, Assert.NotNull, Assert.NotNull));
            webPartServiceMock.VerifyGet(x => x.WebPartCategories);

            // Reset to go again.
            applicationServiceMock.ResetCalls();
            webPartServiceMock.ResetCalls();

            businessLayer.GetWebPartCategories("/my", true).Should().NotBeNull().And.BeEmpty();

            applicationServiceMock.Verify(x => x.Initialize(true, Assert.NotNull, Assert.NotNull));
            webPartServiceMock.VerifyGet(x => x.WebPartCategories);

            // Reset to go again.
            applicationServiceMock.ResetCalls();
            webPartServiceMock.ResetCalls();

            businessLayer.GetWebPartCategories("my Category", true).Should().NotBeNullOrEmpty().And.HaveCount(1);

            applicationServiceMock.Verify(x => x.Initialize(true, Assert.NotNull, Assert.NotNull));
            webPartServiceMock.VerifyGet(x => x.WebPartCategories);

            // Reset to go again.
            applicationServiceMock.ResetCalls();
            webPartServiceMock.ResetCalls();

            businessLayer.GetWebPartCategories("NyCategory", true).Should().NotBeNullOrEmpty().And.HaveCount(1);

            applicationServiceMock.Verify(x => x.Initialize(true, Assert.NotNull, Assert.NotNull));
            webPartServiceMock.VerifyGet(x => x.WebPartCategories);
        }

        [TestCase]
        public void GetWebPartCategories_IDs()
        {
            var applicationServiceMock = new Mock<ICmsApplicationService>();

            var webPartServiceMock = new Mock<IWebPartService>();

            var catMock1 = new Mock<IWebPartCategory>();
            catMock1.SetupGet(x => x.CategoryDisplayName).Returns("My Category");
            catMock1.SetupGet(x => x.CategoryName).Returns("MyCategory");
            catMock1.SetupGet(x => x.CategoryPath).Returns("/my/category");
            catMock1.SetupGet(x => x.CategoryID).Returns(255);

            var catMock2 = new Mock<IWebPartCategory>();
            catMock2.SetupGet(x => x.CategoryDisplayName).Returns("Ny Category");
            catMock2.SetupGet(x => x.CategoryName).Returns("NyCategory");
            catMock2.SetupGet(x => x.CategoryPath).Returns("/my/category2");
            catMock2.SetupGet(x => x.CategoryID).Returns(101);

            webPartServiceMock.Setup(x => x.GetWebPartCategory(255)).Returns(catMock1.Object);
            webPartServiceMock.Setup(x => x.GetWebPartCategory(101)).Returns(catMock2.Object);

            var businessLayer = new GetCMSWebPartCategoryBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                CmsApplicationService = applicationServiceMock.Object,
                WebPartService = webPartServiceMock.Object,
            };

            businessLayer.GetWebPartCategories(255, 101, 5).Should().NotBeNull().And.HaveCount(2);

            applicationServiceMock.Verify(x => x.Initialize(true, Assert.NotNull, Assert.NotNull));
            webPartServiceMock.Verify(x => x.GetWebPartCategory(255));
            webPartServiceMock.Verify(x => x.GetWebPartCategory(101));
            webPartServiceMock.Verify(x => x.GetWebPartCategory(5));
        }
    }
}

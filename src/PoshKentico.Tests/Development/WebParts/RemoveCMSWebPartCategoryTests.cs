// <copyright file="RemoveCMSWebPartCategoryTests.cs" company="Chris Crutchfield">
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
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Tests.Development.WebParts
{
    [TestFixture]
    public class RemoveCMSWebPartCategoryTests
    {
        [TestCase]
        public void RemoveWebPartCategories_MatchString_ExactFalse()
        {
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

            var getBusinessLayer = new GetCMSWebPartCategoryBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                WebPartService = webPartServiceMock.Object,
            };

            var removeBusinessLayer = new RemoveCMSWebPartCategoryBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
                ShouldProcess = (x, y) => true,

                WebPartService = webPartServiceMock.Object,

                GetCMSWebPartCategoryBusiness = getBusinessLayer,
            };

            removeBusinessLayer.RemoveWebPartCategories("my", false);

            webPartServiceMock.Verify(x => x.Delete(catMock1.Object));
            webPartServiceMock.Verify(x => x.Delete(catMock2.Object), Times.Never);

            // Reset to go again.
            webPartServiceMock.ResetCalls();

            removeBusinessLayer.RemoveWebPartCategories("/my", false);

            webPartServiceMock.Verify(x => x.Delete(catMock1.Object));
            webPartServiceMock.Verify(x => x.Delete(catMock2.Object));

            // Reset to go again.
            webPartServiceMock.ResetCalls();

            removeBusinessLayer.RemoveWebPartCategories("/ny", false);

            webPartServiceMock.Verify(x => x.Delete(catMock1.Object), Times.Never);
            webPartServiceMock.Verify(x => x.Delete(catMock2.Object), Times.Never);
        }

        [TestCase]
        public void RemoveWebPartCategories_MatchString_ExactTrue()
        {
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

            var getBusinessLayer = new GetCMSWebPartCategoryBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                WebPartService = webPartServiceMock.Object,
            };

            var removeBusinessLayer = new RemoveCMSWebPartCategoryBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
                ShouldProcess = (x, y) => true,

                WebPartService = webPartServiceMock.Object,

                GetCMSWebPartCategoryBusiness = getBusinessLayer,
            };

            removeBusinessLayer.RemoveWebPartCategories("my", true);

            webPartServiceMock.Verify(x => x.Delete(catMock1.Object), Times.Never);
            webPartServiceMock.Verify(x => x.Delete(catMock2.Object), Times.Never);

            // Reset to go again.
            webPartServiceMock.ResetCalls();

            removeBusinessLayer.RemoveWebPartCategories("/my", true);

            webPartServiceMock.Verify(x => x.Delete(catMock1.Object), Times.Never);
            webPartServiceMock.Verify(x => x.Delete(catMock2.Object), Times.Never);

            // Reset to go again.
            webPartServiceMock.ResetCalls();

            removeBusinessLayer.RemoveWebPartCategories("my Category", true);

            webPartServiceMock.Verify(x => x.Delete(catMock1.Object));
            webPartServiceMock.Verify(x => x.Delete(catMock2.Object), Times.Never);

            // Reset to go again.
            webPartServiceMock.ResetCalls();

            removeBusinessLayer.RemoveWebPartCategories("NyCategory", true);

            webPartServiceMock.Verify(x => x.Delete(catMock1.Object), Times.Never);
            webPartServiceMock.Verify(x => x.Delete(catMock2.Object));
        }

        [TestCase]
        public void RemoveWebPartCategories_IDs()
        {
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

            var getBusinessLayer = new GetCMSWebPartCategoryBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                WebPartService = webPartServiceMock.Object,
            };

            var removeBusinessLayer = new RemoveCMSWebPartCategoryBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
                ShouldProcess = (x, y) => true,

                WebPartService = webPartServiceMock.Object,

                GetCMSWebPartCategoryBusiness = getBusinessLayer,
            };

            removeBusinessLayer.RemoveWebPartCategories(255, 101, 5);

            webPartServiceMock.Verify(x => x.Delete(catMock1.Object));
            webPartServiceMock.Verify(x => x.Delete(catMock2.Object));
        }

        [TestCase]
        public void RemoveWebPartCategories_ByWebPartCategory()
        {
            var webPartServiceMock = new Mock<IWebPartService>();

            var catMock1 = new Mock<IWebPartCategory>();
            catMock1.SetupGet(x => x.CategoryDisplayName).Returns("My Category");
            catMock1.SetupGet(x => x.CategoryName).Returns("MyCategory");
            catMock1.SetupGet(x => x.CategoryPath).Returns("/my/category");
            catMock1.SetupGet(x => x.CategoryID).Returns(255);

            var getBusinessLayer = new GetCMSWebPartCategoryBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                WebPartService = webPartServiceMock.Object,
            };

            var removeBusinessLayer = new RemoveCMSWebPartCategoryBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
                ShouldProcess = (x, y) => true,

                WebPartService = webPartServiceMock.Object,

                GetCMSWebPartCategoryBusiness = getBusinessLayer,
            };

            removeBusinessLayer.RemoveWebPartCategory(catMock1.Object);

            webPartServiceMock.Verify(x => x.Delete(catMock1.Object));
        }
    }
}

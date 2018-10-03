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

using System;
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
    public class RemoveCMSWebPartCategoryTests
    {
        [TestCase]
        public void ShouldRemoveWebPartCategoryWithoutWebPartsWithoutChildrenWithoutRecurse()
        {
            bool shouldProcessCalled = false;

            var webPartCategoryMock = new Mock<IWebPartCategory>();

            var webPartServiceMock = new Mock<IWebPartService>();

            var businessLayer = new RemoveCMSWebPartCategoryBusiness
            {
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
                ShouldProcess = (x, y) =>
                {
                    shouldProcessCalled = true;

                    return true;
                },
            };

            businessLayer.RemoveWebPartCategory(webPartCategoryMock.Object, false);
            shouldProcessCalled.Should().BeTrue();

            webPartServiceMock
                .Verify(x => x.GetWebParts(webPartCategoryMock.Object));

            webPartServiceMock
                .Verify(x => x.GetWebPartCategories(webPartCategoryMock.Object));

            webPartServiceMock
                .Verify(x => x.Delete(webPartCategoryMock.Object));

            webPartServiceMock
                .VerifyNoOtherCalls();
        }

        [TestCase]
        public void ShouldRemoveWebPartCategoryWithWebPartsWithoutChildrenWithoutRecurse()
        {
            var webPartMock = new Mock<IWebPart>();

            var webPartCategoryMock = new Mock<IWebPartCategory>();

            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetWebParts(webPartCategoryMock.Object))
                .Returns(new IWebPart[] { webPartMock.Object });

            var businessLayer = new RemoveCMSWebPartCategoryBusiness
            {
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
            };

            Action action = () => businessLayer.RemoveWebPartCategory(webPartCategoryMock.Object, false);
            action.Should().Throw<Exception>();

            webPartServiceMock
                .Verify(x => x.GetWebParts(webPartCategoryMock.Object));

            webPartServiceMock
                .VerifyNoOtherCalls();
        }

        [TestCase]
        public void ShouldRemoveWebPartCategoryWithWebPartsWithoutChildrenWithRecurse()
        {
            bool shouldProcessCalled = false;

            var webPartMock = new Mock<IWebPart>();

            var webPartCategoryMock = new Mock<IWebPartCategory>();

            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetWebParts(webPartCategoryMock.Object))
                .Returns(new IWebPart[] { webPartMock.Object });

            var businessLayer = new RemoveCMSWebPartCategoryBusiness
            {
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
                ShouldProcess = (x, y) =>
                {
                    shouldProcessCalled = true;

                    return true;
                },
            };

            Action action = () => businessLayer.RemoveWebPartCategory(webPartCategoryMock.Object, true);
            action.Should().NotThrow<Exception>();
            shouldProcessCalled.Should().BeTrue();

            webPartServiceMock
                .Verify(x => x.GetWebParts(webPartCategoryMock.Object));

            webPartServiceMock
                .Verify(x => x.Delete(webPartMock.Object));

            webPartServiceMock
                .Verify(x => x.GetWebPartCategories(webPartCategoryMock.Object));

            webPartServiceMock
                .Verify(x => x.Delete(webPartCategoryMock.Object));

            webPartServiceMock
                .VerifyNoOtherCalls();
        }

        [TestCase]
        public void ShouldRemoveWebPartCategoryWithoutWebPartsWithChildrenWithoutRecurse()
        {
            var webPartCategoryMock = new Mock<IWebPartCategory>();

            var webPartCategoryChildMock = new Mock<IWebPartCategory>();

            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetWebPartCategories(webPartCategoryMock.Object))
                .Returns(new IWebPartCategory[] { webPartCategoryChildMock.Object });

            var businessLayer = new RemoveCMSWebPartCategoryBusiness
            {
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
            };

            Action action = () => businessLayer.RemoveWebPartCategory(webPartCategoryMock.Object, false);
            action.Should().Throw<Exception>();

            webPartServiceMock
                .Verify(x => x.GetWebParts(webPartCategoryMock.Object));

            webPartServiceMock
                .Verify(x => x.GetWebPartCategories(webPartCategoryMock.Object));

            webPartServiceMock
                .VerifyNoOtherCalls();
        }

        [TestCase]
        public void ShouldRemoveWebPartCategoryWithoutWebPartsWithChildrenWithRecurse()
        {
            bool shouldProcessCalled = false;

            var webPartCategoryMock = new Mock<IWebPartCategory>();

            var webPartCategoryChildMock = new Mock<IWebPartCategory>();

            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetWebPartCategories(webPartCategoryMock.Object))
                .Returns(new IWebPartCategory[] { webPartCategoryChildMock.Object });

            var businessLayer = new RemoveCMSWebPartCategoryBusiness
            {
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
                ShouldProcess = (x, y) =>
                {
                    shouldProcessCalled = true;

                    return true;
                },
            };

            Action action = () => businessLayer.RemoveWebPartCategory(webPartCategoryMock.Object, true);
            action.Should().NotThrow<Exception>();
            shouldProcessCalled.Should().BeTrue();

            webPartServiceMock
                .Verify(x => x.GetWebParts(webPartCategoryMock.Object));

            webPartServiceMock
                .Verify(x => x.GetWebPartCategories(webPartCategoryMock.Object));

            webPartServiceMock
                .Verify(x => x.Delete(webPartCategoryChildMock.Object));

            webPartServiceMock
                .Verify(x => x.GetWebParts(webPartCategoryChildMock.Object));

            webPartServiceMock
                .Verify(x => x.GetWebPartCategories(webPartCategoryChildMock.Object));

            webPartServiceMock
                .Verify(x => x.Delete(webPartCategoryMock.Object));

            webPartServiceMock
                .VerifyNoOtherCalls();
        }
    }
}

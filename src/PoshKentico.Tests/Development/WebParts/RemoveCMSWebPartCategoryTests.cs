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
using CMS.PortalEngine;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Providers.General;
using PoshKentico.Core.Services.Development;
using PoshKentico.Core.Services.Development.WebParts;
using PoshKentico.Tests.Helpers;

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

            var webPartCategoryMock = new Mock<IControlCategory<WebPartCategoryInfo>>();

            var webPartServiceMock = new Mock<IWebPartService>();

            var outputService = OutputServiceHelper.GetPassThruOutputService();
            PassThruOutputService.ShouldProcessFunction = (x, y) =>
            {
                shouldProcessCalled = true;

                return true;
            };

            var businessLayer = new RemoveCMSWebPartCategoryBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
                ControlService = webPartServiceMock.Object,
            };

            businessLayer.RemoveWebPartCategory(webPartCategoryMock.Object, false);
            shouldProcessCalled.Should().BeTrue();

            webPartServiceMock
                .Verify(x => x.GetControls(webPartCategoryMock.Object));

            webPartServiceMock
                .Verify(x => x.GetControlCategories(webPartCategoryMock.Object));

            webPartServiceMock
                .Verify(x => x.Delete(webPartCategoryMock.Object));

            webPartServiceMock
                .VerifyNoOtherCalls();
        }

        [TestCase]
        public void ShouldRemoveWebPartCategoryWithWebPartsWithoutChildrenWithoutRecurse()
        {
            var webPartMock = new Mock<IWebPart>();

            var webPartCategoryMock = new Mock<IControlCategory<WebPartCategoryInfo>>();

            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetControls(webPartCategoryMock.Object))
                .Returns(new IWebPart[] { webPartMock.Object });

            var businessLayer = new RemoveCMSWebPartCategoryBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
                ControlService = webPartServiceMock.Object,
            };

            Action action = () => businessLayer.RemoveWebPartCategory(webPartCategoryMock.Object, false);
            action.Should().Throw<Exception>();

            webPartServiceMock
                .Verify(x => x.GetControls(webPartCategoryMock.Object));

            webPartServiceMock
                .VerifyNoOtherCalls();
        }

        [TestCase]
        public void ShouldRemoveWebPartCategoryWithWebPartsWithoutChildrenWithRecurse()
        {
            bool shouldProcessCalled = false;

            var webPartMock = new Mock<IWebPart>();

            var webPartCategoryMock = new Mock<IControlCategory<WebPartCategoryInfo>>();

            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetControls(webPartCategoryMock.Object))
                .Returns(new IWebPart[] { webPartMock.Object });

            var outputService = OutputServiceHelper.GetPassThruOutputService();
            PassThruOutputService.ShouldProcessFunction = (x, y) =>
            {
                shouldProcessCalled = true;

                return true;
            };

            var businessLayer = new RemoveCMSWebPartCategoryBusiness
            {
                OutputService = outputService,
                ControlService = webPartServiceMock.Object,
            };

            Action action = () => businessLayer.RemoveWebPartCategory(webPartCategoryMock.Object, true);
            action.Should().NotThrow<Exception>();
            shouldProcessCalled.Should().BeTrue();

            webPartServiceMock
                .Verify(x => x.GetControls(webPartCategoryMock.Object));

            webPartServiceMock
                .Verify(x => x.Delete(webPartMock.Object));

            webPartServiceMock
                .Verify(x => x.GetControlCategories(webPartCategoryMock.Object));

            webPartServiceMock
                .Verify(x => x.Delete(webPartCategoryMock.Object));

            webPartServiceMock
                .VerifyNoOtherCalls();
        }

        [TestCase]
        public void ShouldRemoveWebPartCategoryWithoutWebPartsWithChildrenWithoutRecurse()
        {
            var webPartCategoryMock = new Mock<IControlCategory<WebPartCategoryInfo>>();

            var webPartCategoryChildMock = new Mock<IControlCategory<WebPartCategoryInfo>>();

            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetControlCategories(webPartCategoryMock.Object))
                .Returns(new IControlCategory<WebPartCategoryInfo>[] { webPartCategoryChildMock.Object });

            var businessLayer = new RemoveCMSWebPartCategoryBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
                ControlService = webPartServiceMock.Object,
            };

            Action action = () => businessLayer.RemoveWebPartCategory(webPartCategoryMock.Object, false);
            action.Should().Throw<Exception>();

            webPartServiceMock
                .Verify(x => x.GetControls(webPartCategoryMock.Object));

            webPartServiceMock
                .Verify(x => x.GetControlCategories(webPartCategoryMock.Object));

            webPartServiceMock
                .VerifyNoOtherCalls();
        }

        [TestCase]
        public void ShouldRemoveWebPartCategoryWithoutWebPartsWithChildrenWithRecurse()
        {
            bool shouldProcessCalled = false;

            var webPartCategoryMock = new Mock<IControlCategory<WebPartCategoryInfo>>();

            var webPartCategoryChildMock = new Mock<IControlCategory<WebPartCategoryInfo>>();

            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetControlCategories(webPartCategoryMock.Object))
                .Returns(new IControlCategory<WebPartCategoryInfo>[] { webPartCategoryChildMock.Object });

            var outputService = OutputServiceHelper.GetPassThruOutputService();
            PassThruOutputService.ShouldProcessFunction = (x, y) =>
            {
                shouldProcessCalled = true;

                return true;
            };

            var businessLayer = new RemoveCMSWebPartCategoryBusiness
            {
                OutputService = outputService,
                ControlService = webPartServiceMock.Object,
            };

            Action action = () => businessLayer.RemoveWebPartCategory(webPartCategoryMock.Object, true);
            action.Should().NotThrow<Exception>();
            shouldProcessCalled.Should().BeTrue();

            webPartServiceMock
                .Verify(x => x.GetControls(webPartCategoryMock.Object));

            webPartServiceMock
                .Verify(x => x.GetControlCategories(webPartCategoryMock.Object));

            webPartServiceMock
                .Verify(x => x.Delete(webPartCategoryChildMock.Object));

            webPartServiceMock
                .Verify(x => x.GetControls(webPartCategoryChildMock.Object));

            webPartServiceMock
                .Verify(x => x.GetControlCategories(webPartCategoryChildMock.Object));

            webPartServiceMock
                .Verify(x => x.Delete(webPartCategoryMock.Object));

            webPartServiceMock
                .VerifyNoOtherCalls();
        }
    }
}

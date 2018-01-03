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
using PoshKentico.Business.Development;
using PoshKentico.Core.Services.Development;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Tests.Development
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

        [TestCase(true)]
        [TestCase(false)]
        public void GetWebPartCategories_MatchString_Exact(bool exact)
        {
            var applicationServiceMock = new Mock<ICmsApplicationService>();

            var webPartServiceMock = new Mock<IWebPartService>();

            var webPartCategories = new List<IWebPartCategory>();
            webPartServiceMock.SetupGet(x => x.WebPartCategories).Returns(webPartCategories);

            var businessLayer = new GetCMSWebPartCategoryBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                CmsApplicationService = applicationServiceMock.Object,
                WebPartService = webPartServiceMock.Object,
            };

            var results = businessLayer.GetWebPartCategories("my", exact);

            applicationServiceMock.Verify(x => x.Initialize(true, Assert.NotNull, Assert.NotNull));
            webPartServiceMock.VerifyGet(x => x.WebPartCategories);

            results.Should().NotBeNull();
        }
    }
}

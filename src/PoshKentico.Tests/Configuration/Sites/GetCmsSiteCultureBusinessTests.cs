// <copyright file="GetCmsSiteCultureBusinessTests.cs" company="Chris Crutchfield">
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
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Configuration.Sites;
using PoshKentico.Core.Services.Configuration.Sites;

namespace PoshKentico.Tests.Configuration.Sites
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class GetCmsSiteCultureBusinessTests
    {
        [TestCase]
        public void GetSiteCultureTest_Object()
        {
            var siteServiceMock = new Mock<ISiteService>();

            var siteMock1 = new Mock<ISite>();
            siteMock1.SetupGet(x => x.DisplayName).Returns("My Site1");
            siteMock1.SetupGet(x => x.SiteName).Returns("MySite1");
            siteMock1.SetupGet(x => x.DomainName).Returns("localhost1");

            var getBusinessLayer = new GetCmsSiteBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                SiteService = siteServiceMock.Object,
            };

            var businessLayer = new GetCmsSiteCultureBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                SiteService = siteServiceMock.Object,
            };

            businessLayer.GetCultures(siteMock1.Object);

            siteServiceMock.Verify(x => x.GetSiteCultures(siteMock1.Object));
        }

        [TestCase]
        public void GetSiteCultureTest_SiteName()
        {
            var siteServiceMock = new Mock<ISiteService>();

            var siteMock1 = new Mock<ISite>();
            siteMock1.SetupGet(x => x.DisplayName).Returns("My Site1");
            siteMock1.SetupGet(x => x.SiteName).Returns("MySite1");
            siteMock1.SetupGet(x => x.DomainName).Returns("localhost1");

            siteServiceMock.Setup(x => x.GetSite("MySite1")).Returns(siteMock1.Object);

            var getBusinessLayer = new GetCmsSiteBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                SiteService = siteServiceMock.Object,
            };

            var businessLayer = new GetCmsSiteCultureBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                SiteService = siteServiceMock.Object,
            };

            businessLayer.GetCultures(siteMock1.Object.SiteName);

            siteServiceMock.Verify(x => x.GetSiteCultures(siteMock1.Object));
        }
    }
}

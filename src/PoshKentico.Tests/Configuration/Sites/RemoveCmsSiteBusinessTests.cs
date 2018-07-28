// <copyright file="RemoveCmsSiteBusinessTests.cs" company="Chris Crutchfield">
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
using CMS.SiteProvider;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Configuration.Sites;
using PoshKentico.Core.Services.Configuration;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Tests.Configuration.Sites
{
    [TestFixture]
    public class RemoveCmsSiteBusinessTests
    {
        [TestCase]
        public void RemoveSiteTest_Object()
        {
            var applicationServiceMock = new Mock<ICmsApplicationService>();
            var siteServiceMock = new Mock<ISiteService>();

            var sites = new List<ISite>();
            var siteMock1 = new Mock<ISite>();
            siteMock1.SetupGet(x => x.DisplayName).Returns("My Site1");
            siteMock1.SetupGet(x => x.SiteName).Returns("MySite1");
            siteMock1.SetupGet(x => x.DomainName).Returns("localhost1");
            sites.Add(siteMock1.Object);

            var siteMock2 = new Mock<ISite>();
            siteMock2.SetupGet(x => x.DisplayName).Returns("your site2");
            siteMock2.SetupGet(x => x.SiteName).Returns("yoursite2");
            siteMock2.SetupGet(x => x.DomainName).Returns("localhost2");
            sites.Add(siteMock2.Object);

            siteServiceMock.SetupGet(x => x.Sites).Returns(sites);

            var getBusinessLayer = new GetCmsSiteBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                CmsApplicationService = applicationServiceMock.Object,
                SiteService = siteServiceMock.Object,
            };

            var businessLayer = new RemoveCmsSiteBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
                ShouldProcess = (x, y) => true,

                CmsApplicationService = applicationServiceMock.Object,
                SiteService = siteServiceMock.Object,
                GetCmsSiteBusiness = getBusinessLayer,
            };

            businessLayer.Remove(siteMock1.Object);

            applicationServiceMock.Verify(x => x.Initialize(true, Assert.NotNull, Assert.NotNull));
            siteServiceMock.Verify(x => x.Delete(siteMock1.Object));
        }

        [TestCase]
        public void RemoveSiteTest_MatchString_ExactFalse()
        {
            var applicationServiceMock = new Mock<ICmsApplicationService>();
            var siteServiceMock = new Mock<ISiteService>();

            var sites = new List<ISite>();
            var siteMock1 = new Mock<ISite>();
            siteMock1.SetupGet(x => x.DisplayName).Returns("My Site1");
            siteMock1.SetupGet(x => x.SiteName).Returns("MySite1");
            siteMock1.SetupGet(x => x.DomainName).Returns("localhost1");
            sites.Add(siteMock1.Object);

            var siteMock2 = new Mock<ISite>();
            siteMock2.SetupGet(x => x.DisplayName).Returns("your site2");
            siteMock2.SetupGet(x => x.SiteName).Returns("yoursite2");
            siteMock2.SetupGet(x => x.DomainName).Returns("localhost2");
            sites.Add(siteMock2.Object);

            siteServiceMock.SetupGet(x => x.Sites).Returns(sites);

            var getBusinessLayer = new GetCmsSiteBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                CmsApplicationService = applicationServiceMock.Object,
                SiteService = siteServiceMock.Object,
            };

            var businessLayer = new RemoveCmsSiteBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
                ShouldProcess = (x, y) => true,

                CmsApplicationService = applicationServiceMock.Object,
                SiteService = siteServiceMock.Object,
                GetCmsSiteBusiness = getBusinessLayer,
            };

            businessLayer.Remove("site", false);

            applicationServiceMock.Verify(x => x.Initialize(true, Assert.NotNull, Assert.NotNull));
            siteServiceMock.Verify(x => x.Delete(siteMock1.Object));
            siteServiceMock.Verify(x => x.Delete(siteMock2.Object));
        }

        [TestCase]
        public void RemoveSiteTest_MatchString_ExactTrue()
        {
            var applicationServiceMock = new Mock<ICmsApplicationService>();
            var siteServiceMock = new Mock<ISiteService>();

            var sites = new List<ISite>();
            var siteMock1 = new Mock<ISite>();
            siteMock1.SetupGet(x => x.DisplayName).Returns("My Site1");
            siteMock1.SetupGet(x => x.SiteName).Returns("MySite1");
            siteMock1.SetupGet(x => x.DomainName).Returns("localhost1");
            sites.Add(siteMock1.Object);

            var siteMock2 = new Mock<ISite>();
            siteMock2.SetupGet(x => x.DisplayName).Returns("your site2");
            siteMock2.SetupGet(x => x.SiteName).Returns("yoursite2");
            siteMock2.SetupGet(x => x.DomainName).Returns("localhost2");
            sites.Add(siteMock2.Object);

            siteServiceMock.SetupGet(x => x.Sites).Returns(sites);

            var getBusinessLayer = new GetCmsSiteBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                CmsApplicationService = applicationServiceMock.Object,
                SiteService = siteServiceMock.Object,
            };

            var businessLayer = new RemoveCmsSiteBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
                ShouldProcess = (x, y) => true,

                CmsApplicationService = applicationServiceMock.Object,
                SiteService = siteServiceMock.Object,
                GetCmsSiteBusiness = getBusinessLayer,
            };

            businessLayer.Remove("yoursite2", true);

            applicationServiceMock.Verify(x => x.Initialize(true, Assert.NotNull, Assert.NotNull));
            siteServiceMock.Verify(x => x.Delete(siteMock2.Object));
        }

        [TestCase]
        public void RemoveSiteTest_Ids()
        {
            var applicationServiceMock = new Mock<ICmsApplicationService>();
            var siteServiceMock = new Mock<ISiteService>();

            var sites = new List<ISite>();
            var siteMock1 = new Mock<ISite>();
            siteMock1.SetupGet(x => x.DisplayName).Returns("My Site1");
            siteMock1.SetupGet(x => x.SiteName).Returns("MySite1");
            siteMock1.SetupGet(x => x.DomainName).Returns("localhost1");
            sites.Add(siteMock1.Object);

            var siteMock2 = new Mock<ISite>();
            siteMock2.SetupGet(x => x.DisplayName).Returns("your site2");
            siteMock2.SetupGet(x => x.SiteName).Returns("yoursite2");
            siteMock2.SetupGet(x => x.DomainName).Returns("localhost2");
            sites.Add(siteMock2.Object);

            siteServiceMock.Setup(x => x.GetSite(1)).Returns(siteMock1.Object);
            siteServiceMock.Setup(x => x.GetSite(2)).Returns(siteMock2.Object);

            siteServiceMock.SetupGet(x => x.Sites).Returns(sites);

            var getBusinessLayer = new GetCmsSiteBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                CmsApplicationService = applicationServiceMock.Object,
                SiteService = siteServiceMock.Object,
            };

            var businessLayer = new RemoveCmsSiteBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
                ShouldProcess = (x, y) => true,

                CmsApplicationService = applicationServiceMock.Object,
                SiteService = siteServiceMock.Object,
                GetCmsSiteBusiness = getBusinessLayer,
            };

            businessLayer.Remove(2, 3);

            applicationServiceMock.Verify(x => x.Initialize(true, Assert.NotNull, Assert.NotNull));
            siteServiceMock.Verify(x => x.Delete(siteMock2.Object));

        }
    }
}

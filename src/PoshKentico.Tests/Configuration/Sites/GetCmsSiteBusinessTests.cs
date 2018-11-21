// <copyright file="GetCmsSiteBusinessTests.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Configuration.Sites;

namespace PoshKentico.Business.Configuration.Sites
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class GetCmsSiteBusinessTests
    {
        [TestCase]
        public void GetSiteTest_NoParameters_None()
        {
            var siteServiceMock = new Mock<ISiteService>();

            var businessLayer = new GetCmsSiteBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                SiteService = siteServiceMock.Object,
            };

            businessLayer.GetSites().Should().BeEmpty();

            siteServiceMock.VerifyGet(x => x.Sites);
        }

        [TestCase]
        public void GetSiteTest_NoParameters_All()
        {
            var siteServiceMock = new Mock<ISiteService>();

            var sites = new List<ISite>();
            var siteMock1 = new Mock<ISite>();
            siteMock1.SetupGet(x => x.DisplayName).Returns("My Site1");
            siteMock1.SetupGet(x => x.DomainName).Returns("localhost1");
            sites.Add(siteMock1.Object);

            var siteMock2 = new Mock<ISite>();
            siteMock2.SetupGet(x => x.DisplayName).Returns("My Site2");
            siteMock2.SetupGet(x => x.DomainName).Returns("localhost2");
            sites.Add(siteMock2.Object);

            siteServiceMock.SetupGet(x => x.Sites).Returns(sites);

            var businessLayer = new GetCmsSiteBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                SiteService = siteServiceMock.Object,
            };

            businessLayer.GetSites().Should().NotBeNullOrEmpty().And.HaveCount(2);

            siteServiceMock.VerifyGet(x => x.Sites);
        }

        [TestCase]
        public void GetSiteTest_MatchString_IsRegexTrue()
        {
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

            var businessLayer = new GetCmsSiteBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                SiteService = siteServiceMock.Object,
            };

            businessLayer.GetSites("site", true).Should().NotBeNullOrEmpty().And.HaveCount(2);

            businessLayer.GetSites("my", true).Should().NotBeNullOrEmpty().And.HaveCount(1);

            businessLayer.GetSites("your", true).Should().NotBeNullOrEmpty().And.HaveCount(1);

            siteServiceMock.VerifyGet(x => x.Sites);
        }

        [TestCase]
        public void GetSiteTest_MatchString_IsRegexFalse()
        {
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

            var businessLayer = new GetCmsSiteBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                SiteService = siteServiceMock.Object,
            };

            businessLayer.GetSites("site", false).Should().BeEmpty();

            businessLayer.GetSites("your site2", false).Should().NotBeNullOrEmpty().And.HaveCount(1);

            siteServiceMock.VerifyGet(x => x.Sites);
        }

        [TestCase]
        public void GetSiteTest_IDs()
        {
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

            var businessLayer = new GetCmsSiteBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                SiteService = siteServiceMock.Object,
            };

            businessLayer.GetSites(3).Should().BeEmpty();

            businessLayer.GetSites(2).Should().NotBeNullOrEmpty().And.HaveCount(1);

            businessLayer.GetSites(1, 2, 3).Should().NotBeNullOrEmpty().And.HaveCount(2);

            siteServiceMock.Verify(x => x.GetSite(1));
            siteServiceMock.Verify(x => x.GetSite(2));
            siteServiceMock.Verify(x => x.GetSite(3));
        }
    }
}
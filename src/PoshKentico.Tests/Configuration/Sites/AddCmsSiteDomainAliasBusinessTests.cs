// <copyright file="AddCmsSiteDomainAliasBusinessTests.cs" company="Chris Crutchfield">
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
using PoshKentico.Business.Configuration.Sites;
using PoshKentico.Core.Services.Configuration.Sites;

namespace PoshKentico.Tests.Configuration.Sites
{
    [TestFixture]
    public class AddCmsSiteDomainAliasBusinessTests
    {
        [TestCase]
        public void AddSiteDomainAliasTest_Object()
        {
            var siteServiceMock = new Mock<ISiteService>();
            string aliasName = "172.0.0.1";

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

                SiteService = siteServiceMock.Object,
            };

            var businessLayer = new AddCmsSiteDomainAliasBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
                ShouldProcess = (x, y) => true,

                SiteService = siteServiceMock.Object,
                GetCmsSiteBusiness = getBusinessLayer,
            };

            businessLayer.AddDomainAlias(siteMock1.Object, aliasName);

            siteServiceMock.Verify(x => x.AddSiteDomainAlias(siteMock1.Object, aliasName));
        }

        [TestCase]
        public void AddSiteDomainAliasTest_MatchString_ExactFalse()
        {
            var siteServiceMock = new Mock<ISiteService>();
            string aliasName = "172.0.0.1";

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

                SiteService = siteServiceMock.Object,
            };

            var businessLayer = new AddCmsSiteDomainAliasBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                SiteService = siteServiceMock.Object,
                GetCmsSiteBusiness = getBusinessLayer,
            };

            businessLayer.AddDomainAlias("site", false, aliasName);

            siteServiceMock.Verify(x => x.AddSiteDomainAlias(siteMock1.Object, aliasName));
            siteServiceMock.Verify(x => x.AddSiteDomainAlias(siteMock2.Object, aliasName));
        }

        [TestCase]
        public void AddSiteDomainAliasTest_MatchString_ExactTrue()
        {
            var siteServiceMock = new Mock<ISiteService>();
            string aliasName = "172.0.0.1";

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

                SiteService = siteServiceMock.Object,
            };

            var businessLayer = new AddCmsSiteDomainAliasBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
                ShouldProcess = (x, y) => true,

                SiteService = siteServiceMock.Object,
                GetCmsSiteBusiness = getBusinessLayer,
            };

            businessLayer.AddDomainAlias("yoursite2", true, aliasName);

            siteServiceMock.Verify(x => x.AddSiteDomainAlias(siteMock2.Object, aliasName));
        }

        [TestCase]
        public void AddSiteDomainAliasTest_Ids()
        {
            var siteServiceMock = new Mock<ISiteService>();
            string aliasName = "172.0.0.1";

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

                SiteService = siteServiceMock.Object,
            };

            var businessLayer = new AddCmsSiteDomainAliasBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
                ShouldProcess = (x, y) => true,

                SiteService = siteServiceMock.Object,
                GetCmsSiteBusiness = getBusinessLayer,
            };

            int[] ids = new int[] { 2, 3 };

            businessLayer.AddDomainAlias(ids, aliasName);

            siteServiceMock.Verify(x => x.AddSiteDomainAlias(siteMock2.Object, aliasName));
        }
    }
}

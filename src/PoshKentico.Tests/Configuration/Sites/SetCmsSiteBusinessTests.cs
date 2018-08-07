// <copyright file="SetCmsSiteBusinessTests.cs" company="Chris Crutchfield">
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
    public class SetCmsSiteBusinessTests
    {
        [TestCase]
        public void SetSiteTest_SpecifiedObject()
        {
            var siteServiceMock = new Mock<ISiteService>();

            var businessLayer = new SetCmsSiteBusiness
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                SiteService = siteServiceMock.Object,
            };

            var siteMock1 = new Mock<ISite>();
            siteMock1.SetupGet(x => x.DisplayName).Returns("My Site1");
            siteMock1.SetupGet(x => x.SiteName).Returns("MySite1");
            siteMock1.SetupGet(x => x.Status).Returns(SiteStatusEnum.Running);
            siteMock1.SetupGet(x => x.DomainName).Returns("localhost1");

            businessLayer.Set(siteMock1.Object);

            siteServiceMock.Verify(x => x.Update(siteMock1.Object));
        }

        [TestCase]
        public void SetSiteTest_SpecifiedProperties()
        {
            var siteServiceMock = new Mock<ISiteService>();

            var sites = new List<ISite>();
            var siteMock1 = new Mock<ISite>();
            siteMock1.SetupGet(x => x.DisplayName).Returns("My Site1");
            siteMock1.SetupGet(x => x.SiteName).Returns("MySite1");
            siteMock1.SetupGet(x => x.DomainName).Returns("localhost1");
            sites.Add(siteMock1.Object);

            var siteMock2 = new Mock<ISite>();
            siteMock2.SetupGet(x => x.DisplayName).Returns("My Site2");
            siteMock2.SetupGet(x => x.SiteName).Returns("MySite2");
            siteMock2.SetupGet(x => x.DomainName).Returns("localhost2");
            sites.Add(siteMock2.Object);

            siteServiceMock.SetupGet(x => x.Sites).Returns(sites);

            var businessLayer = new SetCmsSiteBusiness
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                SiteService = siteServiceMock.Object,
            };

            businessLayer.Set("My Site3", "MySite1", SiteStatusEnum.Running, "localhost");

            siteServiceMock.Verify(x => x.Update(
                It.Is<ISite>(i => i.DisplayName == "My Site3" && i.SiteName == "MySite1"
                && i.Status == SiteStatusEnum.Running && i.DomainName == "localhost")));
        }
    }
}

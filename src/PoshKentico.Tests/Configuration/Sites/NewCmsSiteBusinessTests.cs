// <copyright file="NewCmsSiteBusinessTests.cs" company="Chris Crutchfield">
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

using CMS.SiteProvider;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace PoshKentico.Business.Configuration.Sites
{
    [TestFixture]
    public class NewCmsSiteBusinessTests
    {
        [TestCase]
        public void CreateSiteTest_WithoutSiteName()
        {
            var siteServiceMock = new Mock<SiteServiceMock>();

            var businessLayer = new NewCmsSiteBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                SiteService = siteServiceMock.Object,
            };

            var result = businessLayer.CreateSite("My Site1", null, SiteStatusEnum.Stopped, "localhost1");

            siteServiceMock.Object.VerifyCreate(result);

            result.SiteName.Should().Be("MySite1");
        }

        [TestCase]
        public void CreateSiteTest_WithSiteName()
        {
            var siteServiceMock = new Mock<SiteServiceMock>();

            var businessLayer = new NewCmsSiteBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                SiteService = siteServiceMock.Object,
            };

            var result = businessLayer.CreateSite("My Site1", "mySite", SiteStatusEnum.Stopped, "localhost1");

            siteServiceMock.Object.VerifyCreate(result);

            result.SiteName.Should().Be("mySite");
        }
    }
}
// <copyright file="StopCmsSiteBusinessTests.cs" company="Chris Crutchfield">
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
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Configuration.Sites;
using PoshKentico.Core.Providers.General;
using PoshKentico.Core.Services.Configuration.Sites;
using PoshKentico.Tests.Helpers;

namespace PoshKentico.Tests.Configuration.Sites
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class StopCmsSiteBusinessTests
    {
        [TestCase]
        public void StopSiteTest()
        {
            var siteServiceMock = new Mock<ISiteService>();

            var siteMock1 = new Mock<ISite>();
            siteMock1.SetupGet(x => x.DisplayName).Returns("My Site1");
            siteMock1.SetupGet(x => x.SiteName).Returns("MySite1");
            siteMock1.SetupGet(x => x.DomainName).Returns("localhost1");

            var siteMock2 = new Mock<ISite>();
            siteMock2.SetupGet(x => x.DisplayName).Returns("your site2");
            siteMock2.SetupGet(x => x.SiteName).Returns("yoursite2");
            siteMock2.SetupGet(x => x.DomainName).Returns("localhost2");

            var outputService = OutputServiceHelper.GetPassThruOutputService();

            var businessLayer = new StopCmsSiteBusiness()
            {
                OutputService = outputService,

                SiteService = siteServiceMock.Object,
            };

            businessLayer.Stop(siteMock1.Object);
            businessLayer.Stop(siteMock2.Object);

            siteServiceMock.Verify(x => x.Stop(siteMock1.Object));
        }
    }
}

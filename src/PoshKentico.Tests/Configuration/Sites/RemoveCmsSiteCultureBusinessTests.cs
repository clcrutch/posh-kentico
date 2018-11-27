// <copyright file="RemoveCmsSiteCultureBusinessTests.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Configuration.Sites;
using PoshKentico.Tests.Helpers;

namespace PoshKentico.Tests.Configuration.Sites
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class RemoveCmsSiteCultureBusinessTests
    {
        [TestCase]
        public void RemoveSiteCultureTest()
        {
            var siteServiceMock = new Mock<ISiteService>();
            string[] cultureCodes = new string[] { "ar-sa", "en-au" };

            var siteMock1 = new Mock<ISite>();
            siteMock1.SetupGet(x => x.DisplayName).Returns("My Site1");
            siteMock1.SetupGet(x => x.SiteName).Returns("MySite1");
            siteMock1.SetupGet(x => x.DomainName).Returns("localhost1");

            var siteMock2 = new Mock<ISite>();
            siteMock2.SetupGet(x => x.DisplayName).Returns("your site2");
            siteMock2.SetupGet(x => x.SiteName).Returns("yoursite2");
            siteMock2.SetupGet(x => x.DomainName).Returns("localhost2");

            var outputService = OutputServiceHelper.GetPassThruOutputService();

            var businessLayer = new RemoveCmsSiteCultureBusiness()
            {
                OutputService = outputService,
                SiteService = siteServiceMock.Object,
            };

            foreach (string cultureCode in cultureCodes)
            {
                businessLayer.RemoveCulture(siteMock1.Object, cultureCode);

                siteServiceMock.Verify(x => x.RemoveSiteCulture(siteMock1.Object, cultureCode));
            }
        }
    }
}

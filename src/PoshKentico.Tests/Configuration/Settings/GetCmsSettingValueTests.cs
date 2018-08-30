// <copyright file="GetCmsSettingValueTests.cs" company="Chris Crutchfield">
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

using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Configuration.Settings;
using PoshKentico.Core.Services.Configuration.Settings;
using PoshKentico.Core.Services.Configuration.Sites;

namespace PoshKentico.Tests.Configuration.Settings
{
    [TestFixture]
    public class GetCmsSettingValueTests
    {
        [TestCase]
        public void GetSettingValueTest_SiteName()
        {
            var settingValueService = new Mock<ISettingValueService>();

            var businessLayer = new GetCmsSettingValueBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                SettingValueService = settingValueService.Object,
            };

            settingValueService.Setup(x => x.GetSettingValue("site", "key")).Returns("value");

            var value = businessLayer.GetSettingValue("site", "key").Should().NotBeNull().And.Equals("value");

            settingValueService.Verify(x => x.GetSettingValue("site", "key"));
        }

        [TestCase]
        public void GetSettingValueTest_Site()
        {
            var settingValueService = new Mock<ISettingValueService>();
            var siteMock = new Mock<ISite>();
            siteMock.Setup(x => x.SiteName).Returns("site");

            var businessLayer = new GetCmsSettingValueBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                SettingValueService = settingValueService.Object,
            };

            settingValueService.Setup(x => x.GetSettingValue("site", "key")).Returns("value");

            var value = businessLayer.GetSettingValue(siteMock.Object.SiteName, "key").Should().NotBeNull().And.Equals("value");

            settingValueService.Verify(x => x.GetSettingValue("site", "key"));
        }

        [TestCase]
        public void GetSettingValueTest_Global()
        {
            var settingValueService = new Mock<ISettingValueService>();
            string siteName = null;

            var businessLayer = new GetCmsSettingValueBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                SettingValueService = settingValueService.Object,
            };

            settingValueService.Setup(x => x.GetSettingValue(null, "key")).Returns("value");

            var value = businessLayer.GetSettingValue(siteName, "key").Should().NotBeNull().And.Equals("value");

            settingValueService.Verify(x => x.GetSettingValue(null, "key"));
        }
    }
}

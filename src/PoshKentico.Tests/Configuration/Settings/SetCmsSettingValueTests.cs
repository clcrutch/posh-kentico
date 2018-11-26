// <copyright file="SetCmsSettingValueTests.cs" company="Chris Crutchfield">
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
using PoshKentico.Business.Configuration.Settings;
using PoshKentico.Core.Services.Configuration.Settings;
using PoshKentico.Core.Services.Configuration.Sites;

namespace PoshKentico.Tests.Configuration.Settings
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class SetCmsSettingValueTests
    {
        [TestCase]
        public void SetSettingValueTest_SiteName()
        {
            var settingValueService = new Mock<ISettingValueService>();

            var businessLayer = new SetCmsSettingValueBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                SettingValueService = settingValueService.Object,
            };

            businessLayer.SetSettingValue("site", "key", "newVal");

            settingValueService.Verify(x => x.SetSettingValue("site", "key", "newVal"));
        }

        [TestCase]
        public void SetSettingValueTest_Site()
        {
            var settingValueService = new Mock<ISettingValueService>();
            var siteMock = new Mock<ISite>();
            siteMock.Setup(x => x.SiteName).Returns("site");

            var businessLayer = new SetCmsSettingValueBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                SettingValueService = settingValueService.Object,
            };

            businessLayer.SetSettingValue(siteMock.Object.SiteName, "key", "newVal");

            settingValueService.Verify(x => x.SetSettingValue("site", "key", "newVal"));
        }

        [TestCase]
        public void SetSettingValueTest_Global()
        {
            var settingValueService = new Mock<ISettingValueService>();
            string siteName = null;

            var businessLayer = new SetCmsSettingValueBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                SettingValueService = settingValueService.Object,
            };

            businessLayer.SetSettingValue(siteName, "key", "newVal");

            settingValueService.Verify(x => x.SetSettingValue(null, "key", "newVal"));
        }
    }
}

// <copyright file="GetCmsWebConfigValueTests.cs" company="Chris Crutchfield">
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
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Configuration.Settings;
using PoshKentico.Core.Services.Configuration.Settings;
using PoshKentico.Tests.Helpers;

namespace PoshKentico.Tests.Configuration.Settings
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class GetCmsWebConfigValueTests
    {
        [TestCase]
        public void GetWebConfigValueTest_Key()
        {
            var settingValueService = new Mock<ISettingValueService>();

            var businessLayer = new GetCmsWebConfigValueBusiness()
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
                SettingValueService = settingValueService.Object,
            };

            settingValueService.Setup(x => x.GetWebConfigValue("key", string.Empty)).Returns("value");

            var value = businessLayer.GetSettingValue("key", string.Empty).Should().NotBeNull().And.Equals("value");

            settingValueService.Verify(x => x.GetWebConfigValue("key", string.Empty));
        }
    }
}

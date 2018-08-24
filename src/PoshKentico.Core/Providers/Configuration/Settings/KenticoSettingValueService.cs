// <copyright file="KenticoSettingValueService.cs" company="Chris Crutchfield">
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

using System;
using System.ComponentModel.Composition;
using CMS.Base;
using CMS.DataEngine;
using CMS.Helpers;
using PoshKentico.Core.Services.Configuration.Settings;

namespace PoshKentico.Core.Providers.Configuration.Settings
{
    /// <summary>
    /// Implementation of <see cref="ISettingValueService"/> that uses Kentico.
    /// </summary>
    [Export(typeof(ISettingValueService))]
    public class KenticoSettingValueService : ISettingValueService
    {
        #region Methods

        /// <inheritdoc/>
        public string GetSettingValue(string siteName, string settingKey)
        {
            return SettingsKeyInfoProvider.GetValue(siteName + "." + settingKey);
        }

        /// <inheritdoc/>
        public string GetWebConfigValue(string settingKey, string culture = "")
        {
            return ValidationHelper.GetString(SettingsHelper.AppSettings[settingKey], culture);
        }

        /// <inheritdoc/>
        public void SetSettingValue(string siteName, string settingKey, object newVal)
        {
            SettingsKeyInfoProvider.SetValue(siteName, settingKey, newVal);
        }
        #endregion
    }
}

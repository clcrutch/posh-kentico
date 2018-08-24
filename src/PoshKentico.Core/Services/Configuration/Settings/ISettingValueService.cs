// <copyright file="ISettingValueService.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Core.Services.Configuration.Settings
{
    /// <summary>
    /// Service for providing values for the given setting key.
    /// </summary>
    public interface ISettingValueService
    {
        /// <summary>
        /// Get the settings value based on the key
        /// </summary>
        /// <param name="settingKey">The key of the setting</param>
        /// <param name="siteName">The site name of the setting</param>
        /// <returns>The value of the setting</returns>
        string GetSettingValue(string settingKey, string siteName);

        /// <summary>
        /// Get the web.config value based on the key
        /// </summary>
        /// <param name="appSettingKey">The key of the web.config setting</param>
        /// <param name="culture">The culture info related to the setting, default is empty string</param>
        /// <returns>The value of the setting</returns>
        string GetWebConfigValue(string appSettingKey, string culture = "");

        /// <summary>
        /// Set the settings value based on the key
        /// </summary>
        /// <param name="siteName">The site name of the setting</param>
        /// /// <param name="settingKey">The key of the setting</param>
        /// <param name="newVal">The new value of the setting</param>
        void SetSettingValue(string siteName, string settingKey, object newVal);
    }
}

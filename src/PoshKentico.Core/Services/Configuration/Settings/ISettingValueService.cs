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
        /// <param name="siteName">The site name of the setting</param>
        /// <param name="settingKey">The key of the setting</param>
        /// <returns>The value of the setting</returns>
        string GetSettingValue(string siteName, string settingKey);
    }
}

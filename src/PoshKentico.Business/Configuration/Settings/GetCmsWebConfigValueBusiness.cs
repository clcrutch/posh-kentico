// <copyright file="GetCmsWebConfigValueBusiness.cs" company="Chris Crutchfield">
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

using System.ComponentModel.Composition;
using PoshKentico.Core.Services.Configuration.Settings;

namespace PoshKentico.Business.Configuration.Settings
{
    /// <summary>
    /// Business layer for the Get-CMSWebConfigValue cmdlet.
    /// </summary>
    [Export(typeof(GetCmsWebConfigValueBusiness))]
    public class GetCmsWebConfigValueBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the Setting Service.  Populated by MEF.
        /// </summary>
        [Import]
        public ISettingValueService SettingValueService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Get a web.config setting value.
        /// </summary>
        /// <param name="appSettingKey">The key of the web.config setting.</param>
        /// <param name="defaultValue">The default value to return if no key is matched.</param>
        /// <returns>The value of the setting.</returns>
        public object GetSettingValue(string appSettingKey, string defaultValue = "")
        {
            return this.SettingValueService.GetWebConfigValue(appSettingKey, defaultValue);
        }

        #endregion
    }
}

// <copyright file="SetCmsSettingValueBusiness.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Configuration.Sites;

namespace PoshKentico.Business.Configuration.Settings
{
    /// <summary>
    /// Business layer for the Set-CMSSettingValue cmdlet.
    /// </summary>
    [Export(typeof(SetCmsSettingValueBusiness))]
    public class SetCmsSettingValueBusiness : CmdletBusinessBase
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
        /// Set a new setting value from a site with provided key.
        /// </summary>
        /// <param name="site">The site to set the setting to.</param>
        /// <param name="settingKey">The setting key associated with the setting.</param>
        /// <param name="newVal">The new value of the setting.</param>
        public void SetSettingValue(ISite site, string settingKey, object newVal)
        {
           this.SettingValueService.SetSettingValue(site.SiteName, settingKey, newVal);
        }

        /// <summary>
        /// Set a new setting value from a site with provided key.
        /// </summary>
        /// <param name="siteName">The site name of the setting.</param>
        /// <param name="settingKey">The key of the setting.</param>
        /// <param name="newVal">The new value of the setting.</param>
        public void SetSettingValue(string siteName, string settingKey, object newVal)
        {
            this.SettingValueService.SetSettingValue(siteName, settingKey, newVal);
        }

        #endregion
    }
}

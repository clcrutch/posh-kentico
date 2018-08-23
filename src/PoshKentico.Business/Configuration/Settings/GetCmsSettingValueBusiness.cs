// <copyright file="GetCmsSettingValueBusiness.cs" company="Chris Crutchfield">
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
using PoshKentico.Business.Configuration.Sites;
using PoshKentico.Core.Services.Configuration.Settings;
using PoshKentico.Core.Services.Configuration.Sites;

namespace PoshKentico.Business.Configuration.Settings
{
    /// <summary>
    /// Business layer for the Set-CMSSettingValue cmdlet.
    /// </summary>
    [Export(typeof(GetCmsSettingValueBusiness))]
    public class GetCmsSettingValueBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the Site Service.  Populated by MEF.
        /// </summary>
        [Import]
        public ISettingValueService SettingValueService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Get a setting value from a site
        /// </summary>
        /// <param name="site">the site to get the setting from</param>
        /// <param name="key">the setting key associated with the setting</param>
        /// <returns>the setting value</returns>
        public object GetSettingValue(ISite site, string key)
        {
            return this.SettingValueService.GetSettingValue(site.SiteName, key);
        }

        /// <summary>
        /// Get a setting value from a site <see cref="ISite"/> in the CMS System.
        /// </summary>
        /// <param name="siteName">the site name of the site to get the setting from</param>
        /// <param name="key">the setting key associated with the setting</param>
        /// <returns>the setting value</returns>
        public object GetSettingValue(string siteName, string key)
        {
            return this.SettingValueService.GetSettingValue(siteName, key);
        }

        #endregion
    }
}

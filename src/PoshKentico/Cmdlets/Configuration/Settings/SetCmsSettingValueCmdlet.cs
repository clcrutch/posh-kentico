// <copyright file="SetCmsSettingValueCmdlet.cs" company="Chris Crutchfield">
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
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using CMS.SiteProvider;
using ImpromptuInterface;
using PoshKentico.Business.Configuration.Settings;
using PoshKentico.Core.Services.Configuration.Sites;

namespace PoshKentico.Cmdlets.Configuration.Settings
{
    /// <summary>
    /// <para type="synopsis">Sets the setting values by the provided setting key.</para>
    /// <para type="description">Sets the setting values by the provided setting key. </para>
    /// /// <example>
    ///     <para>Set setting values  with a new value "new val", for setting with a site, and setting key "key".</para>
    ///     <code>$site | Set-CMSSettingValue -Key "my key" -Value "new val"</code>
    /// </example>
    /// <example>
    ///     <para>Set setting values with a new value "new val", for setting with site name "site", and setting key "key".</para>
    ///     <code>Set-CMSSettingValue -SiteName "my site" -Key "my key" -Value "new val"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Set, "CMSSettingValue", DefaultParameterSetName = NONE)]
    public class SetCmsSettingValueCmdlet : MefCmdlet
    {
        #region Constants

        private const string NONE = "None";
        private const string OBJECTSET = "Object";
        private const string PROPERTYSET = "Property";

        #endregion
        #region Properties

        /// <summary>
        /// <para type="description">A reference to the site to set setting for.</para>
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, ParameterSetName = OBJECTSET)]
        public SiteInfo Site { get; set; }

        /// <summary>
        /// <para type="description">The site name of the site to set setting for.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = PROPERTYSET)]
        public string SiteName { get; set; }

        /// <summary>
        /// <para type="description">The key of the setting to set value for.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = OBJECTSET)]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = PROPERTYSET)]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = NONE)]
        public string Key { get; set; }

        /// <summary>
        /// <para type="description">The new value of the setting to set value for.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = OBJECTSET)]
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = PROPERTYSET)]
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = NONE)]
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this setting service. Populated by MEF.
        /// </summary>
        [Import]
        public SetCmsSettingValueBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case OBJECTSET:
                    this.BusinessLayer.SetSettingValue(this.Site.ActLike<ISite>(), this.Key, this.Value);
                    break;
                case PROPERTYSET:
                    this.BusinessLayer.SetSettingValue(this.SiteName, this.Key, this.Value);
                    break;
                case NONE:
                    this.BusinessLayer.SetSettingValue(this.SiteName, this.Key, this.Value);
                    break;
            }
        }

        #endregion
    }
}

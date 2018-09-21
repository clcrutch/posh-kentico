// <copyright file="GetCmsWebConfigValueCmdlet.cs" company="Chris Crutchfield">
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
using PoshKentico.Business.Configuration.Settings;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.Settings
{
    /// <summary>
    /// <para type="synopsis">Gets the web.config setting value by the provided setting key.</para>
    /// <para type="description">Gets the web.config setting values by the provided setting key or default value if not. </para>
    /// <example>
    ///     <para>Get web.config setting values with a key.</para>
    ///     <code>Get-CMSSettingValue -Key "my key"</code>
    /// </example>
    /// <example>
    ///     <para>Get web.config setting values with a key and a culture.</para>
    ///     <code>Get-CMSSettingValue -Key "my key" -Default "the default value"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Get, "CMSWebConfigValue")]
    [Alias("gwcval")]
    public class GetCmsWebConfigValueCmdlet : MefCmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">The key of the web.config setting to retrive value from.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true)]
        public string Key { get; set; }

        /// <summary>
        /// <para type="description">The default value for the setting.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 1)]
        public string Default { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this setting service. Populated by MEF.
        /// </summary>
        [Import]
        public GetCmsWebConfigValueBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            var value = this.BusinessLayer.GetSettingValue(this.Key, this.Default);

            this.WriteObject(value);
        }

        #endregion
    }
}

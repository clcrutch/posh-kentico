// <copyright file="RemoveCmsServerCmslet.cs" company="Chris Crutchfield">
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
using PoshKentico.Business.Configuration.Staging;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.Staging
{
    /// <summary>
    /// <para type="synopsis">Removes the servers selected by the provided input.</para>
    /// <para type="description">Removes the servers selected by the provided input.  This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <example>
    ///     <para>Remove all servers with a display name "*bas*", or server name "*bas*".</para>
    ///     <code>Remove-CMSServer bas</code>
    /// </example>
    /// <example>
    ///     <para>Remove all servers with a site id 5,  and a display name "*bas*" or server name "*bas*".</para>
    ///     <code>Remove-CMSServer -SiteID 5 -ServerName "bas"</code>
    /// </example>
    /// <example>
    ///     <para>Remove all servers associalted with site $site with a display name "*bas*", or server name "*bas*"</para>
    ///     <code>$site | Remove-CMSServer bas</code>
    /// </example>
    /// <example>
    ///     <para>Remove all servers with a display name "basic", or server name "basic"</para>
    ///     <code>Remove-CMSServer basic -Exact</code>
    /// </example>
    /// <example>
    ///     <para>Remove all servers with a site id 5, and a display name "basic" or server name "basic"</para>
    ///     <code>Remove-CMSServer -SiteID 5 -ServerName "basic" -Exact</code>
    /// </example>
    /// <example>
    ///     <para>Remove all servers associalted with site $site with a display name "basic", or server name "basic"</para>
    ///     <code>$site | Remove-CMSServer basic -Exact</code>
    /// </example>
    /// <example>
    ///     <para>Remove all the servers with the specified IDs.</para>
    ///     <code>Remove-CMSServer -ID 5,304,5</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSServer", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [Alias("rserver")]
    public class RemoveCmsServerCmslet : MefCmdlet
    {
        #region Constants

        private const string NONE = "None";
        private const string OBJECTSET = "Object";
        private const string DISPLAYNAME = "Dislpay Name";
        private const string IDSETNAME = "ID";

        #endregion
        #region Properties

        /// <summary>
        /// <para type="description">The server site id for the server to update.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = DISPLAYNAME)]
        public int SiteID { get; set; }

        /// <summary>
        /// <para type="description">The associalted site for the server to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, ParameterSetName = OBJECTSET)]
        public SiteInfo Site { get; set; }

        /// <summary>
        /// <para type="description">The display name of the server to retrive.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = DISPLAYNAME)]
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = OBJECTSET)]
        [Alias("ServerName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// <para type="description">If set, the match is exact,</para>
        /// <para type="description">else the match performs a contains for display name and category name and starts with for path.</para>
        /// </summary>
        [Parameter(ParameterSetName = DISPLAYNAME)]
        [Parameter(ParameterSetName = OBJECTSET)]
        public SwitchParameter Exact { get; set; }

        /// <summary>
        /// <para type="description">The IDs of the server to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = IDSETNAME)]
        public int[] ID { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this server. Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCmsServerBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case DISPLAYNAME:
                    this.BusinessLayer.Remove(this.SiteID, this.DisplayName, this.Exact.ToBool());
                    break;
                case OBJECTSET:
                    this.BusinessLayer.Remove(this.Site.SiteID, this.DisplayName, this.Exact.ToBool());
                    break;
                case IDSETNAME:
                    this.BusinessLayer.Remove(this.ID);
                    break;
            }
        }

        #endregion
    }
}

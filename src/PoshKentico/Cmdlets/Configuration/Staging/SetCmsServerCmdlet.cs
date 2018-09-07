// <copyright file="SetCmsServerCmdlet.cs" company="Chris Crutchfield">
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
using CMS.Synchronization;
using ImpromptuInterface;
using PoshKentico.Business.Configuration.Staging;
using PoshKentico.Core.Services.Configuration.Staging;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.Servers
{
    /// <summary>
    /// <para type="synopsis">Sets a server.</para>
    /// <para type="description">Sets a new server based off of the provided input.</para>
    /// <para type="description">This cmdlet returns the server to update when the -PassThru switch is used.</para>
    /// <example>
    ///     <para>Set server specifying an existing server.</para>
    ///     <code>Set-CMSServer -Server $server</code>
    /// </example>
    /// <example>
    ///     <para>Set server specifying an existing server.</para>
    ///     <code>$server | Set-CMSServer</code>
    /// </example>
    /// <example>
    ///     <para>Get server specifying the ServerName and SiteID, Set its DisplayName, URL, Authentication, Enabled, UserName, Password.</para>
    ///     <code>Set-CMSServer -ServerName "Server Name to find" -SiteID "Site ID to find" -DisplayName "New Display Name" -URL "New URL"
    ///     -Authentication "UserName or X509" -Enabled "True or False" -UserName "New User Name" -Password "New Password"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Set, "CMSServer")]
    [OutputType(typeof(ServerInfo))]
    [Alias("sserver")]
    public class SetCmsServerCmdlet : MefCmdlet
    {
        #region Constants

        private const string OBJECTSET = "Object";
        private const string PROPERTYSET = "Property";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">A reference to the server to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = OBJECTSET)]
        [Alias("Server")]
        public ServerInfo ServerToSet { get; set; }

        /// <summary>
        /// <para type="description">The server name for the server to update.</para>
        /// <para type="description">If null, then the display name is used for the server name.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PROPERTYSET)]
        public string ServerName { get; set; }

        /// <summary>
        /// <para type="description">The server site id for the server to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = PROPERTYSET)]
        public int SiteID { get; set; }

        /// <summary>
        /// <para type="description">The display name for the server to update.</para>
        /// <para type="description">Server display name cannot be blank.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 2, ParameterSetName = PROPERTYSET)]
        public string DisplayName { get; set; }

        /// <summary>
        /// <para type="description">The server url for the server to update.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 3, ParameterSetName = PROPERTYSET)]
        public string URL { get; set; }

        /// <summary>
        /// <para type="description">The authentication for the server to update. </para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 4, ParameterSetName = PROPERTYSET)]
        public ServerAuthenticationEnum Authentication { get; set; }

        /// <summary>
        /// <para type="description">The enabled status for the server to update.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 5, ParameterSetName = PROPERTYSET)]
        public bool? Enabled { get; set; }

        /// <summary>
        /// <para type="description">The user name for the server to update.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 6, ParameterSetName = PROPERTYSET)]
        public string UserName { get; set; }

        /// <summary>
        /// <para type="description">The password for the server to update.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 7, ParameterSetName = PROPERTYSET)]
        public string Password { get; set; }

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the server to update.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = OBJECTSET)]
        [Parameter(Mandatory = false, ParameterSetName = PROPERTYSET)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this server.  Populated by MEF.
        /// </summary>
        [Import]
        public SetCmsServerBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IServer updatedServer = null;
            switch (this.ParameterSetName)
            {
                case OBJECTSET:
                    updatedServer = this.BusinessLayer.Set(this.ServerToSet.ActLike<IServer>());
                    break;
                case PROPERTYSET:
                    updatedServer = this.BusinessLayer.Set(this.ServerName, this.SiteID, this.DisplayName, this.URL, this.Authentication, this.Enabled, this.UserName, this.Password);
                    break;
            }

            if (this.PassThru.ToBool())
            {
                this.WriteObject(updatedServer.UndoActLike());
            }
        }

        #endregion
    }
}

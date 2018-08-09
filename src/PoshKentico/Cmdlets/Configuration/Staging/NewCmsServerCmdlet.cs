// <copyright file="NewCmsServerCmdlet.cs" company="Chris Crutchfield">
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
using CMS.Synchronization;
using ImpromptuInterface;
using PoshKentico.Business.Configuration.Staging;
using PoshKentico.Core.Services.Configuration.Sites;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.Staging
{
    /// <summary>
    /// <para type="synopsis">Creates a new server.</para>
    /// <para type="description">Creates a new server based off of the provided input.</para>
    /// <para type="description">This cmdlet returns the newly created server when the -PassThru switch is used.</para>
    /// <example>
    ///     <para>Create a new server specifying the display name and Url.</para>
    ///     <code>New-CMSServer -DisplayName "My Test Name" -URL "My Server Url"</code>
    /// </example>
    /// <example>
    ///     <para>Create a new server specifying the display name, server name, status, and server name.</para>
    ///     <code>New-CMSServer -DisplayName "My Test Name" -ServerName "My Server Name" -URL "My Server Url"
    ///             -Authentication UserName -Enabled True -UserName "My User Name" -Password "My Password"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.New, "CMSServer")]
    [OutputType(typeof(ServerInfo), ParameterSetName = new string[] { PASSTHRU })]
    [Alias("nserver")]
    public class NewCmsServerCmdlet : MefCmdlet
    {
        #region Constants

        private const string PASSTHRU = "PassThru";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The display name for the newly created server.</para>
        /// <para type="description">Server display name cannot be blank.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string DisplayName { get; set; }

        /// <summary>
        /// <para type="description">The server name for the newly created server.</para>
        /// <para type="description">If null, then the display name is used for the server name.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 1)]
        public string ServerName { get; set; }

        /// <summary>
        /// <para type="description">The server url for the newly created server.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 2)]
        public string URL { get; set; }

        /// <summary>
        /// <para type="description">The authentication for the newly created server. </para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 3)]
        public ServerAuthenticationEnum Authentication { get; set; }

        /// <summary>
        /// <para type="description">The enabled status for the newly created server.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 4)]
        public bool? Enabled { get; set; }

        /// <summary>
        /// <para type="description">The user name for the newly created server.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 5)]
        public string UserName { get; set; }

        /// <summary>
        /// <para type="description">The password for the newly created server.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 6)]
        public string Password { get; set; }

        /// <summary>
        /// <para type="description">The associalted site for the newly created server.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 7)]
        public SiteInfo Site { get; set; }

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the newly created server.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = PASSTHRU)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this server.  Populated by MEF.
        /// </summary>
        [Import]
        public NewCmsServerBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            this.Authentication = this.Authentication == ServerAuthenticationEnum.X509 ? this.Authentication : ServerAuthenticationEnum.UserName;
            this.Enabled = this.Enabled == false ? false : true;
            this.UserName = string.IsNullOrEmpty(this.UserName) ? string.Empty : this.UserName;
            this.Password = string.IsNullOrEmpty(this.Password) ? string.Empty : this.Password;

            var newServer = this.BusinessLayer.CreateServer(
                                        this.DisplayName,
                                        this.ServerName,
                                        this.URL,
                                        this.Authentication,
                                        (bool)this.Enabled,
                                        this.UserName,
                                        this.Password,
                                        this.Site.ActLike<ISite>());

            if (this.PassThru.ToBool())
            {
                this.WriteObject(newServer.UndoActLike());
            }
        }

        #endregion
    }
}

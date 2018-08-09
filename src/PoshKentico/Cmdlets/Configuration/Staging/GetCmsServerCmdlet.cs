// <copyright file="GetCmsServerCmdlet.cs" company="Chris Crutchfield">
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

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using CMS.Synchronization;
using ImpromptuInterface;
using PoshKentico.Business.Configuration.Staging;
using PoshKentico.Core.Services.Configuration.Staging;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.Staging
{
    /// <summary>
    /// <para type="synopsis">Gets the servers selected by the provided input.</para>
    /// <para type="description">Gets the servers selected by the provided input.  This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">Without parameters, this command returns all servers.</para>
    /// <para type="description">With parameters, this command returns the servers that match the criteria.</para>
    /// <example>
    ///     <para>Get all the servers.</para>
    ///     <code>Get-CMSServer</code>
    /// </example>
    /// <example>
    ///     <para>Get all servers with a display name "*bas*", or server name "*bas*".</para>
    ///     <code>Get-CMSServer bas</code>
    /// </example>
    /// <example>
    ///     <para>Get all servers with a display name "basic", or server name "basic"</para>
    ///     <code>Get-CMSServer basic -Exact</code>
    /// </example>
    /// <example>
    ///     <para>Get all the servers with the specified IDs.</para>
    ///     <code>Get-CMSServer -ID 5,304,5</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Get, "CMSServer", DefaultParameterSetName = NONE)]
    [OutputType(typeof(ServerInfo))]
    [Alias("gserver")]
    public class GetCmsServerCmdlet : MefCmdlet
    {
        #region Constants

        private const string NONE = "None";
        private const string DISPLAYNAME = "Dislpay Name";
        private const string IDSETNAME = "ID";

        #endregion
        #region Properties

        /// <summary>
        /// <para type="description">The display name of the server to retrive.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = DISPLAYNAME)]
        [Alias("ServerName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// <para type="description">If set, the match is exact,</para>
        /// <para type="description">else the match performs a contains for display name and category name and starts with for path.</para>
        /// </summary>
        [Parameter(ParameterSetName = DISPLAYNAME)]
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
        public GetCmsServerBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<IServer> servers = null;

            switch (this.ParameterSetName)
            {
                case DISPLAYNAME:
                    servers = this.BusinessLayer.GetServers(this.DisplayName, this.Exact.ToBool());
                    break;
                case IDSETNAME:
                    servers = this.BusinessLayer.GetServers(this.ID);
                    break;
                case NONE:
                    servers = this.BusinessLayer.GetServers();
                    break;
            }

            foreach (var server in servers)
            {
                this.WriteObject(server.UndoActLike());
            }
        }

        #endregion
    }
}

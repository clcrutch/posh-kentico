// <copyright file="SyncCmsStagingTaskCmdlet.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Cmdlets.Configuration.Staging
{
    /// <summary>
    /// <para type="synopsis">Synchronize the staging tasks that target the given server.</para>
    /// <para type="description">Synchronize the staging tasks that target the given server.</para>
    /// <example>
    ///     <para>Given an existing server and synchronize all related staging tasks.</para>
    ///     <code>Sync-CMSStagingTask -Server $server</code>
    /// </example>
    /// <example>
    ///     <para>Given an existing server and synchronize all related staging tasks.</para>
    ///     <code>$server | Sync-CMSStagingTask</code>
    /// </example>
    /// <example>
    ///     <para>Get a server by ServerName and SiteID, and synchronize all related staging tasks.</para>
    ///     <code>Sync-CMSStagingTask -ServerName "Server Name to Find" -SiteID "Site Id to Find"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet("Sync", "CMSStagingTask")]
    [Alias("sstask")]
    public class SyncCmsStagingTaskCmdlet : MefCmdlet
    {
        #region Constants

        private const string NONE = "None";
        private const string OBJECTSET = "Object";
        private const string PROPERTYSET = "Property";

        #endregion
        #region Properties

        /// <summary>
        /// <para type="description">A reference to the server to sync all related staging tasks.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = OBJECTSET)]
        [Alias("Server")]
        public ServerInfo ServerToSync { get; set; }

        /// <summary>
        /// <para type="description">The server name for the server to sync all related staging tasks.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PROPERTYSET)]
        public string ServerName { get; set; }

        /// <summary>
        /// <para type="description">The server site id for the server to sync all related staging tasks.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = PROPERTYSET)]
        public int SiteID { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this server.  Populated by MEF.
        /// </summary>
        [Import]
        public SyncCmsStagingTaskBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case OBJECTSET:
                    this.BusinessLayer.SyncStaging(this.ServerToSync.ActLike<IServer>());
                    break;
                case PROPERTYSET:
                    this.BusinessLayer.SyncStaging(this.ServerName, this.SiteID);
                    break;
            }
        }

        #endregion
    }
}

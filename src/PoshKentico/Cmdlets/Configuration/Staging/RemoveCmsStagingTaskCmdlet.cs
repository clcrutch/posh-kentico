// <copyright file="RemoveCmsStagingTaskCmdlet.cs" company="Chris Crutchfield">
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
    /// <para type="synopsis">Remove the staging tasks that target the given server.</para>
    /// <para type="description">Removes the staging tasks that target the given server.</para>
    /// <example>
    ///     <para>Remove all related staging tasks for a given server.</para>
    ///     <code>Remove-CMSStagingTask -Server $server</code>
    /// </example>
    /// <example>
    ///     <para>Remove all related staging tasks for a given server.</para>
    ///     <code>$server | Remove-CMSStagingTask</code>
    /// </example>
    /// <example>
    ///     <para>Remove all related staging tasks for servers with a display name "basic", or server name "basic".</para>
    ///     <code>Remove-CMSServer basic </code>
    /// </example>
    /// <example>
    ///     <para>Remove all related staging tasks for all servers with a display name "*basic*", or server name "*basic*"</para>
    ///     <code>Remove-CMSServer *basic* -RegularExpression</code>
    /// </example>
    /// <example>
    ///     <para>Remove all related staging tasks for all servers with a site id 5,  and a display name "basic" or server name "basic".</para>
    ///     <code>Remove-CMSServer -SiteID 5 -ServerName "basic"</code>
    /// </example>
    /// <example>
    ///     <para>Remove all related staging tasks for servers associalted with site $site with a display name "basic", or server name "basic"</para>
    ///     <code>$site | Remove-CMSServer basic</code>
    /// </example>
    /// <example>
    ///     <para>Remove all related staging tasks for all servers with a site id 5, and a display name "*basic*" or server name "*basic*"</para>
    ///     <code>Remove-CMSServer 5 *basic* -RegularExpression</code>
    /// </example>
    /// <example>
    ///     <para>Remove all related staging tasks for all servers associalted with site $site with a display name "*basic*", or server name "*basic*"</para>
    ///     <code>$site | Remove-CMSServer *basic* -RegularExpression</code>
    /// </example>
    /// <example>
    ///     <para>Remove all related staging tasks for all the servers with the specified IDs.</para>
    ///     <code>Remove-CMSServer -ID 5,304,5</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet("Remove", "CMSStagingTask")]
    [Alias("rmstask")]
    public class RemoveCmsStagingTaskCmdlet : GetCmsServerCmdlet
    {
        #region Constants

        private const string SERVEROBJECTSET = "Server Object";

        #endregion
        #region Properties

        /// <summary>
        /// <para type="description">A reference to the server to remove all related staging tasks.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = SERVEROBJECTSET)]
        [Alias("Server")]
        public ServerInfo ServerToRemove { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this server.  Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCmsStagingTaskBusiness RemoveBusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            if (this.ParameterSetName == SERVEROBJECTSET)
            {
                this.ActOnObject(this.ServerToRemove.ActLike<IServer>());
            }
            else
            {
                base.ProcessRecord();
            }
        }

        /// <inheritdoc />
        protected override void ActOnObject(IServer server)
        {
            if (server == null)
            {
                return;
            }

            this.RemoveBusinessLayer.RemoveStaging(server);
        }
        #endregion
    }
}

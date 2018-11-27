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
using CMS.Synchronization;
using ImpromptuInterface;
using PoshKentico.Business.Configuration.Staging;
using PoshKentico.Core.Services.Configuration.Staging;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.Staging
{
    /// <summary>
    /// <para type="synopsis">Removes the servers selected by the provided input.</para>
    /// <para type="description">Removes the servers selected by the provided input.  This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <example>
    ///     <para>Remove a server.</para>
    ///     <code>$server | Remove-CMSServer</code>
    /// </example>
    /// <example>
    ///     <para>Remove all servers with a display name "basic", or server name "basic".</para>
    ///     <code>Remove-CMSServer basic </code>
    /// </example>
    /// <example>
    ///     <para>Remove all servers with a site id 5,  and a display name "basic" or server name "basic".</para>
    ///     <code>Remove-CMSServer -SiteID 5 -ServerName "basic"</code>
    /// </example>
    /// <example>
    ///     <para>Remove all servers associalted with site $site with a display name "basic", or server name "basic"</para>
    ///     <code>$site | Remove-CMSServer basic</code>
    /// </example>
    /// <example>
    ///     <para>Remove all servers with a display name "*basic*", or server name "*basic*"</para>
    ///     <code>Remove-CMSServer *basic* -RegularExpression</code>
    /// </example>
    /// <example>
    ///     <para>Remove all servers with a site id 5, and a display name "*basic*" or server name "*basic*"</para>
    ///     <code>Remove-CMSServer 5 *basic* -RegularExpression</code>
    /// </example>
    /// <example>
    ///     <para>Remove all servers associalted with site $site with a display name "*basic*", or server name "*basic*"</para>
    ///     <code>$site | Remove-CMSServer *basic* -RegularExpression</code>
    /// </example>
    /// <example>
    ///     <para>Remove all the servers with the specified IDs.</para>
    ///     <code>Remove-CMSServer -ID 5,304,5</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSServer", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [Alias("rserver")]
    public class RemoveCmsServerCmslet : GetCmsServerCmdlet
    {
        #region Constants

        private const string SERVEROBJECTSET = "Object";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">A reference to the site to remove.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = SERVEROBJECTSET)]
        [Alias("Server")]
        public ServerInfo ServerToRemove { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this server. Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCmsServerBusiness RemoveBusinessLayer { get; set; }

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

            this.RemoveBusinessLayer.Remove(server);
        }
        #endregion
    }
}

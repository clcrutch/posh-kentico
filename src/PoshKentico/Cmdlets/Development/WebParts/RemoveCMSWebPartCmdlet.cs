// <copyright file="RemoveCMSWebPartCmdlet.cs" company="Chris Crutchfield">
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
using CMS.PortalEngine;
using ImpromptuInterface;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.WebParts
{
    /// <summary>
    /// <para type="synopsis">Removes the web parts selected by the provided input.</para>
    /// <para type="description">Removes the web parts selected by the provided input.</para>
    /// <para type="description">This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">Without parameters, this command removes all webparts.</para>
    /// <para type="description">With parameters, this command removes the webparts that match the criteria.</para>
    /// <example>
    ///     <para>Remove all the webparts.</para>
    ///     <code>Remove-CMSWebPart</code>
    /// </example>
    /// <example>
    ///     <para>Remove web parts by category.</para>
    ///     <code>Get-CMSWebPartCategory | Remove-CMSWebPart</code>
    /// </example>
    /// <example>
    ///     <para>Remove web parts by category name.</para>
    ///     <code>Remove-CMSWebPart -Category *test*</code>
    /// </example>
    /// <example>
    ///     <para>Remove web parts by name.</para>
    ///     <code>Remove-CMSWebPart -WebPartName *webpartname*</code>
    /// </example>
    /// <example>
    ///     <para>Remove web parts by path</para>
    ///     <code>Remove-CMSWebPart -Path /path/to/webpart</code>
    /// </example>
    /// <example>
    ///     <code>Remove web parts through the pipeline.</code>
    ///     <code>$webPart | Remove-CMSWebPart</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSWebPart", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = NONE)]
    [Alias("rmwp")]
    public class RemoveCMSWebPartCmdlet : GetCMSWebPartCmdlet
    {
        private const string WEBPART = "WebPart";

        /// <summary>
        /// <para type="description">The web part to remove from the system.</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, Mandatory = true, ParameterSetName = WEBPART)]
        public WebPartInfo WebPart { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this web part. Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCMSWebPartBusiness RemoveBusinessLayer { get; set; }

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            if (this.ParameterSetName == WEBPART)
            {
                this.ActOnObject(this.WebPart.ActLike<IWebPart>());
            }
            else
            {
                base.ProcessRecord();
            }
        }

        /// <inheritdoc />
        protected override void ActOnObject(IWebPart webPart)
        {
            if (webPart == null)
            {
                return;
            }

            this.RemoveBusinessLayer.RemoveWebPart(webPart);
        }
    }
}

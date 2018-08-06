// <copyright file="GetCMSWebPartCmdlet.cs" company="Chris Crutchfield">
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
using ImpromptuInterface;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.WebParts
{
    /// <summary>
    /// <para type="synopsis">Gets the web parts selected by the provided input.</para>
    /// <para type="description">Gets the web parts selected b y the provided input.  This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">Without parameters, this command returns all webparts.</para>
    /// <example>
    ///     <para>Get all the webparts.</para>
    ///     <code>Get-CMSWebPart</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Get, "CMSWebPart", DefaultParameterSetName = NONE)]
    [Alias("gwp")]
    public class GetCMSWebPartCmdlet : MefCmdlet
    {
        #region Constants

        private const string CATEGORY = "Category";
        private const string NONE = "None";

        #endregion

        #region Properties

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = CATEGORY)]
        [Alias("Category")]
        public IWebPartCategory WebPartCategory { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this web part. Populated by MEF.
        /// </summary>
        [Import]
        public GetCMSWebPartBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<IWebPart> webparts = null;

            switch (this.ParameterSetName)
            {
                case CATEGORY:
                    webparts = this.BusinessLayer.GetWebParts(this.WebPartCategory);
                    break;
                case NONE:
                    webparts = this.BusinessLayer.GetWebParts();
                    break;
            }

            foreach (var webpart in webparts)
            {
                this.WriteObject(webpart.UndoActLike());
            }
        }

        #endregion

    }
}

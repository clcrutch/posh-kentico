// <copyright file="RemoveCMSWebPartCategoryCmdlet.cs" company="Chris Crutchfield">
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
    /// <para type="synopsis">Deletes the web part categories selected by the provided input.</para>
    /// <para type="description">Deletes the web part categories selected by the provided input.</para>
    /// <para type="description">This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">This command deletes the webpart categories that match the criteria.</para>
    /// <example>
    ///     <para>Delete all the webpart categories.</para>
    ///     <code>Get-CMSWebPartCategory | Remove-CMSWebPartCategory</code>
    /// </example>
    /// <example>
    ///     <para>Delete all webparts with a category name "*bas*", display name "*bas*", or a path "bas*".</para>
    ///     <code>Remove-CMSWebPartCategory bas</code>
    /// </example>
    /// <example>
    ///     <para>Delete all webparts with a category name "basic", display name "basic", or path "basic"</para>
    ///     <code>Remove-CMSWebPartCategory basic</code>
    /// </example>
    /// <example>
    ///     <para>Delete all the webparts with the specified IDs.</para>
    ///     <code>Remove-CMSWebPartCategory -ID 5,304,55</code>
    /// </example>
    /// </summary>
    /// 

    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSWebPartCategory", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = NONE)]
    [Alias("rmwpc")]
    public class RemoveCMSWebPartCategoryCmdlet : GetCMSWebPartCategoryCmdletBase
    {
        #region Constants

        private const string WEBPARTCATEGORY = "WebPartCategory";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The web part category to remove from the system.</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, Mandatory = true, ParameterSetName = WEBPARTCATEGORY)]
        public WebPartCategoryInfo WebPartCategory { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this web part.  Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCMSWebPartCategoryBusiness RemoveBusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            if (this.ParameterSetName == WEBPARTCATEGORY)
            {
                this.ActOnObject(this.WebPartCategory.ActLike<IWebPartCategory>());
            }
            else
            {
                base.ProcessRecord();
            }
        }

        /// <inheritdoc />
        protected override void ActOnObject(IWebPartCategory webPartCategory)
        {
            if (webPartCategory == null)
            {
                return;
            }

            this.RemoveBusinessLayer.RemoveWebPartCategory(webPartCategory, this.Recurse.ToBool());
        }

        #endregion

    }
}

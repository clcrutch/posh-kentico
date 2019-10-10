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
using PoshKentico.Core.Providers.Development.WebParts;
using PoshKentico.Core.Services.Development;
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
    ///     <para>Remove all the webpart categories.</para>
    ///     <code>Remove-CMSWebPartCategory</code>
    /// </example>
    /// <example>
    ///     <para>Remove all web part categories with a category name "*bas*", display name "*bas*".</para>
    ///     <code>Remove-CMSWebPartCategory *bas*</code>
    /// </example>
    /// <example>
    ///     <para>Remove all web part categories with a category name "basic", display name "basic"</para>
    ///     <code>Remove-CMSWebPartCategory basic</code>
    /// </example>
    /// <example>
    ///     <para>Remove all the web part categories with the specified IDs.</para>
    ///     <code>Remove-CMSWebPartCategory -ID 5,304,5</code>
    /// </example>
    /// <example>
    ///     <para>Remove all the web part categories under the basic category.</para>
    ///     <code>Remove-CMSWebPartCategory basic -Recurse</code>
    /// </example>
    /// <example>
    ///     <para>Remove the web part category associated with the web part.</para>
    ///     <code>$webPart | Remove-WebPartCategory</code>
    /// </example>
    /// <example>
    ///     <para>Remove the web part category from Kentico.</para>
    ///     <code>$webPartCategory | Remove-WebPartCategory</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSWebPartCategory", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = NONE)]
    [Alias("rmwpc")]
    public class RemoveCMSWebPartCategoryCmdlet : GetCMSControlCategoryCmdletBase<GetCMSWebPartCategoryBusiness, IWebPartService, WebPartInfo, WebPartCategoryInfo, WebPart>
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
                this.ActOnObject(new WebPartCategory(this.WebPartCategory));
            }
            else
            {
                base.ProcessRecord();
            }
        }

        /// <inheritdoc />
        protected override void ActOnObject(IControlCategory<WebPartCategoryInfo> webPartCategory)
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

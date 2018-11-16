// <copyright file="RemoveCMSPageTemplateCategoryCmdlet.cs" company="Chris Crutchfield">
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
using PoshKentico.Business.Development.PageTemplates;
using PoshKentico.Core.Services.Development.PageTemplates;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.PageTemplates
{
    /// <summary>
    /// <para type="synopsis">Deletes the page template categories selected by the provided input.</para>
    /// <para type="description">Deletes the page template categories selected by the provided input.</para>
    /// <para type="description">This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">This command deletes the pagetemplate categories that match the criteria.</para>
    /// <example>
    ///     <para>Remove all the pagetemplate categories.</para>
    ///     <code>Remove-CMSPageTemplateCategory</code>
    /// </example>
    /// <example>
    ///     <para>Remove all page template categories with a category name "*bas*", display name "*bas*".</para>
    ///     <code>Remove-CMSPageTemplateCategory *bas*</code>
    /// </example>
    /// <example>
    ///     <para>Remove all page template categories with a category name "basic", display name "basic"</para>
    ///     <code>Remove-CMSPageTemplateCategory basic</code>
    /// </example>
    /// <example>
    ///     <para>Remove all the page template categories with the specified IDs.</para>
    ///     <code>Remove-CMSPageTemplateCategory -ID 5,304,5</code>
    /// </example>
    /// <example>
    ///     <para>Remove all the page template categories under the basic category.</para>
    ///     <code>Remove-CMSPageTemplateCategory basic -Recurse</code>
    /// </example>
    /// <example>
    ///     <para>Remove the page template category associated with the page template.</para>
    ///     <code>$pageTemplate | Remove-PageTemplateCategory</code>
    /// </example>
    /// <example>
    ///     <para>Remove the page template category from Kentico.</para>
    ///     <code>$pageTemplateCategory | Remove-PageTemplateCategory</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSPageTemplateCategory", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = NONE)]
    [Alias("rmptc")]
    public class RemoveCMSPageTemplateCategoryCmdlet : GetCMSPageTemplateCategoryCmdletBase
    {
        #region Constants

        private const string WEBPARTCATEGORY = "PageTemplateCategory";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The page template category to remove from the system.</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, Mandatory = true, ParameterSetName = WEBPARTCATEGORY)]
        public PageTemplateCategoryInfo PageTemplateCategory { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this page template.  Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCMSPageTemplateCategoryBusiness RemoveBusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            if (this.ParameterSetName == WEBPARTCATEGORY)
            {
                this.ActOnObject(this.PageTemplateCategory.ActLike<IPageTemplateCategory>());
            }
            else
            {
                base.ProcessRecord();
            }
        }

        /// <inheritdoc />
        protected override void ActOnObject(IPageTemplateCategory pageTemplateCategory)
        {
            if (pageTemplateCategory == null)
            {
                return;
            }

            this.RemoveBusinessLayer.RemovePageTemplateCategory(pageTemplateCategory, this.Recurse.ToBool());
        }

        #endregion

    }
}

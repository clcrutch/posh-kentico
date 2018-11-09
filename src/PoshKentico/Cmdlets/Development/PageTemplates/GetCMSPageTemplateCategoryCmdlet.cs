// <copyright file="GetCMSPageTemplateCategoryCmdlet.cs" company="Chris Crutchfield">
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
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using CMS.PortalEngine;
using ImpromptuInterface;
using PoshKentico.Core.Services.Development.PageTemplates;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.PageTemplates
{
    /// <summary>
    /// <para type="synopsis">Gets the page template categories selected by the provided input.</para>
    /// <para type="description">Gets the page template categories selected by the provided input.  This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">Without parameters, this command returns all pagetemplate categories.</para>
    /// <para type="description">With parameters, this command returns the pagetemplate categories that match the criteria.</para>
    /// <example>
    ///     <para>Get all the pagetemplate categories.</para>
    ///     <code>Get-CMSPageTemplateCategory</code>
    /// </example>
    /// <example>
    ///     <para>Get all page template categories with a category name "*bas*", display name "*bas*".</para>
    ///     <code>Get-CMSPageTemplateCategory *bas*</code>
    /// </example>
    /// <example>
    ///     <para>Get all page template categories with a category name "basic", display name "basic"</para>
    ///     <code>Get-CMSPageTemplateCategory basic</code>
    /// </example>
    /// <example>
    ///     <para>Get all the page template categories with the specified IDs.</para>
    ///     <code>Get-CMSPageTemplateCategory -ID 5,304,5</code>
    /// </example>
    /// <example>
    ///     <para>Get all the page template categories under the basic category.</para>
    ///     <code>Get-CMSPageTemplateCategory basic -Recurse</code>
    /// </example>
    /// <example>
    ///     <para>Get the page template category associated with the page template.</para>
    ///     <code>$pageTemplate | Get-PageTemplateCategory</code>
    /// </example>
    /// <example>
    ///     <para>Get the page template categories under a parent category.</para>
    ///     <code>$pageTemplateCategory | Get-PageTemplateCategory</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Get, "CMSPageTemplateCategory", DefaultParameterSetName = NONE)]
    [OutputType(typeof(PageTemplateCategoryInfo[]))]
    [Alias("gptc")]
    public class GetCMSPageTemplateCategoryCmdlet : GetCMSPageTemplateCategoryCmdletBase
    {
        #region Constants

        private const string PARENTCATEGORY = "Parent Category";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The pagetemplate category that contains the pagetemplate categories.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = PARENTCATEGORY)]
        [Alias("Parent", "ParentCategory")]
        public PageTemplateCategoryInfo ParentPageTemplateCategory { get; set; }

        /// <inheritdoc />
        [Parameter(ParameterSetName = PARENTCATEGORY)]
        public override SwitchParameter Recurse
        {
            get => base.Recurse;
            set => base.Recurse = value;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<IPageTemplateCategory> categories = null;

            if (this.ParameterSetName == PARENTCATEGORY)
            {
                categories = this.BusinessLayer.GetPageTemplateCategories(this.ParentPageTemplateCategory.ActLike<IPageTemplateCategory>(), this.Recurse.ToBool());

                foreach (var category in categories)
                {
                    this.ActOnObject(category);
                }
            }
            else
            {
                base.ProcessRecord();
            }
        }

        #endregion

    }
}

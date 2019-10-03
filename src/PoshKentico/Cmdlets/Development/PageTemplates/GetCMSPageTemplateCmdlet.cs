// <copyright file="GetCMSPageTemplateCmdlet.cs" company="Chris Crutchfield">
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
using CMS.FormEngine;
using CMS.PortalEngine;
using ImpromptuInterface;
using PoshKentico.Business.Development.PageTemplates;
using PoshKentico.Core.Services.Development.PageTemplates;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.PageTemplates
{
    /// <summary>
    /// <para type="synopsis">Gets the page templates selected by the provided input.</para>
    /// <para type="description">Gets the page templates selected by the provided input.</para>
    /// <para type="description">This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">Without parameters, this command returns all pagetemplates.</para>
    /// <para type="description">With parameters, this command returns the pagetemplates that match the criteria.</para>
    /// <example>
    ///     <para>Get all the pagetemplates.</para>
    ///     <code>Get-CMSPageTemplate</code>
    /// </example>
    /// <example>
    ///     <para>Get page templates by category.</para>
    ///     <code>Get-CMSPageTemplateCategory | Get-CMSPageTemplate</code>
    /// </example>
    /// <example>
    ///     <para>Get page templates by category name.</para>
    ///     <code>Get-CMSPageTemplate -Category *test*</code>
    /// </example>
    /// <example>
    ///     <para>Get page templates by name.</para>
    ///     <code>Get-CMSPageTemplate -PageTemplateName *pagetemplatename*</code>
    /// </example>
    /// <example>
    ///     <para>Get page templates by path</para>
    ///     <code>Get-CMSPageTemplate -Path /path/to/pagetemplate</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Get, "CMSPageTemplate", DefaultParameterSetName = NONE)]
    [OutputType(typeof(PageTemplateInfo[]))]
    [Alias("gpt")]
    public class GetCMSPageTemplateCmdlet : MefCmdlet<GetCMSPageTemplateBusiness>
    {
        #region Constants

        /// <summary>
        /// Represents no parameters.
        /// </summary>
        protected const string NONE = "None";

        private const string CATEGORY = "Category";
        private const string CATEGORYNAME = "Category Name";
        private const string NAME = "Name";
        private const string PATH = "Path";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The category name or display name of the pagetemplate category that contains the pagetemplates.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = CATEGORYNAME)]
        [Alias("Category")]
        public string CategoryName { get; set; }

        /// <summary>
        /// <para type="description">Indicates if the CategoryName or Name supplied is a regular expression.</para>
        /// </summary>
        [Parameter(ParameterSetName = CATEGORYNAME)]
        [Parameter(ParameterSetName = NAME)]
        [Alias("Regex")]
        public SwitchParameter RegularExpression { get; set; }

        /// <summary>
        /// <para type="description">The name or display name of the pagetemplate.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ValueFromPipeline = true, ParameterSetName = NAME)]
        [Alias("Name")]
        public string PageTemplateName { get; set; }

        /// <summary>
        /// <para type="description">The path to the pagetemplate.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PATH)]
        [Alias("Path")]
        public string PageTemplatePath { get; set; }

        /// <summary>
        /// <para type="description">An object that represents the pagetemplate category that contains the pagetemplates.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = CATEGORY)]
        public PageTemplateCategoryInfo PageTemplateCategory { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<IPageTemplate> pageTemplates = null;

            switch (this.ParameterSetName)
            {
                case CATEGORY:
                    pageTemplates = this.BusinessLayer.GetPageTemplatesByCategory(this.PageTemplateCategory.ActLike<IPageTemplateCategory>());
                    break;
                case CATEGORYNAME:
                    pageTemplates = this.BusinessLayer.GetPageTemplatesByCategories(this.CategoryName, this.RegularExpression.ToBool());
                    break;
                case NAME:
                    pageTemplates = this.BusinessLayer.GetPageTemplates(this.PageTemplateName, this.RegularExpression.ToBool());
                    break;
                case PATH:
                    pageTemplates = new IPageTemplate[]
                    {
                        this.BusinessLayer.GetPageTemplate(this.PageTemplatePath),
                    };
                    break;
                case NONE:
                    pageTemplates = this.BusinessLayer.GetPageTemplates();
                    break;
            }

            foreach (var pagetemplate in pageTemplates)
            {
                var item = Newtonsoft.Json.JsonConvert.SerializeObject(pagetemplate?.UndoActLike());

                this.ActOnObject(pagetemplate);
            }
        }

        /// <summary>
        /// When overridden in a child class, operates on the specified page template.
        /// </summary>
        /// <param name="pageTemplate">The page template to operate on.</param>
        protected virtual void ActOnObject(IPageTemplate pageTemplate)
        {
            this.WriteObject(pageTemplate?.UndoActLike());
        }

        #endregion

    }
}

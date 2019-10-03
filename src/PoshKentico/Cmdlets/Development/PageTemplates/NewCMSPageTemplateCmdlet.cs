// <copyright file="NewCMSPageTemplateCmdlet.cs" company="Chris Crutchfield">
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
using CMS.DataEngine;
using CMS.PortalEngine;
using ImpromptuInterface;
using PoshKentico.Business.Development.PageTemplates;
using PoshKentico.Core.Services.Development.PageTemplates;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.PageTemplates
{
    /// <summary>
    /// <para type="synopsis">Creates a new page template.</para>
    /// <para type="description">Creates a new page template category based off of the provided input.</para>
    /// <para type="description">This cmdlet returns the newly created page template when the -PassThru switch is used.</para>
    /// <para type="description">This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <example>
    ///     <para>Create a new page template implying the display name from the path.</para>
    ///     <code>New-CMSPageTemplate -Path /TestCategory/TestPageTemplate -FileName Test.ascx</code>
    /// </example>
    /// <example>
    ///     <para>Create a new page template using the path.</para>
    ///     <code>New-CMSPageTemplate -Path /TestCategory/TestPageTemplate -FileName Test.ascx -DisplayName TestDisplayName</code>
    /// </example>
    /// <example>
    ///     <para>Create a new page template implying the display name from the name.</para>
    ///     <code>$category | New-CMSPageTemplate -Name TestPageTemplate -FileName Test.ascx</code>
    /// </example>
    /// <example>
    ///     <para>Create a new page template using the category and name.</para>
    ///     <code>$category | New-CMSPageTemplate -Name TestPageTemplate -FileName Test.ascx -DisplayName TestDisplayName</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.New, "CMSPageTemplate", DefaultParameterSetName = PATH)]
    [OutputType(typeof(PageTemplateInfo[]))]
    [Alias("nwpt")]
    public class NewCMSPageTemplateCmdlet : MefCmdlet<NewCMSPageTemplateBusiness>
    {
        #region Constants
        private const string PATH = "Path";
        private const string CATEGORY = "Category";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">Show as master template..</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public bool ShowAsMasterTemplate { get; set; }

        /// <summary>
        /// <para type="description">Page template for all pages.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public bool PageTemplateForAllPages { get; set; }

        /// <summary>
        /// <para type="description">Page template icon class defining the page template thumbnail.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string IconClass { get; set; }

        /// <summary>
        /// <para type="description">Page template CSS.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string CSS { get; set; }

        /// <summary>
        /// <para type="description">Gets or sets flag whether page template is reusable.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public bool IsReusable { get; set; }

        /// <summary>
        /// <para type="description">The description for the newly created pagetemplate.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Description { get; set; }

        /// <summary>
        /// <para type="description">The file name for the pagetemplate code behind.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [Alias("File")]
        public string FileName { get; set; }

        /// <summary>
        /// <para type="description">Page template layout.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public string Layout { get; set; }

        /// <summary>
        /// <para type="description">Page template layout type.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 2)]
        public LayoutTypeEnum LayoutType { get; set; }

        /// <summary>
        /// <para type="description">The display name for the newly created pagetemplate.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        public string DisplayName { get; set; }

        /// <summary>
        /// <para type="description">The Code Name for the pagetemplate.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = CATEGORY)]
        [Alias("CodeName")]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">The path to the pagetemplate.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PATH)]
        public string Path { get; set; }

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the newly created page template.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        /// <para type="description">The pagetemplate category to add the pagetemplate under.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = CATEGORY)]
        [Alias("Category", "Parent")]
        public PageTemplateCategoryInfo PageTemplateCategory { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IPageTemplate pageTemplate = null;
            switch (this.ParameterSetName)
            {
                case PATH:
                    pageTemplate = this.BusinessLayer.CreatePageTemplate(this.Path, this.FileName ?? string.Empty, this.DisplayName, this.Description, this.ShowAsMasterTemplate, this.PageTemplateForAllPages, this.LayoutType, this.Layout, this.IconClass, this.CSS, this.IsReusable);
                    break;
                case CATEGORY:
                    pageTemplate = this.BusinessLayer.CreatePageTemplate(this.Name, this.FileName ?? string.Empty, this.DisplayName, this.Description, this.ShowAsMasterTemplate, this.PageTemplateForAllPages, this.LayoutType, this.Layout, this.IconClass, this.CSS, this.IsReusable, this.PageTemplateCategory.ActLike<IPageTemplateCategory>());
                    break;
            }

            if (this.PassThru.ToBool())
            {
                this.WriteObject(pageTemplate.UndoActLike());
            }
        }

        #endregion

    }
}

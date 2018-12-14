// <copyright file="GetCMSPageTemplateCategoryCmdletBase.cs" company="Chris Crutchfield">
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

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Management.Automation;
using CMS.PortalEngine;
using ImpromptuInterface;
using PoshKentico.Business.Development.PageTemplates;
using PoshKentico.Core.Services.Development.PageTemplates;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.PageTemplates
{
    /// <summary>
    /// Base class for cmdlets that need to Get a list of <see cref="PageTemplateCategoryInfo"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GetCMSPageTemplateCategoryCmdletBase : MefCmdlet
    {
        #region Constants

        /// <summary>
        /// Represents no parameters.
        /// </summary>
        protected const string NONE = "None";

        private const string CATEGORYNAME = "Category Name";
        private const string IDSETNAME = "ID";
        private const string PATH = "Path";
        private const string PAGETEMPLATE = "Page Template";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The category name or display name the pagetemplate category.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = CATEGORYNAME)]
        [Alias("DisplayName", "Name")]
        public string CategoryName { get; set; }

        /// <summary>
        /// <para type="description">The path to get the page template category at.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PATH)]
        [Alias("Path")]
        public string CategoryPath { get; set; }

        /// <summary>
        /// <para type="description">The IDs of the page template category to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = IDSETNAME)]
        public int[] ID { get; set; }

        /// <summary>
        /// <para type="description">Indiciates if the cmdlet should look recursively for page template categories.</para>
        /// </summary>
        [Parameter(ParameterSetName = CATEGORYNAME, Position = 2)]
        [Parameter(ParameterSetName = IDSETNAME, Position = 2)]
        [Parameter(ParameterSetName = PATH, Position = 2)]
        public virtual SwitchParameter Recurse { get; set; }

        /// <summary>
        /// <para type="description">Indicates if the CategoryName supplied is a regular expression.</para>
        /// </summary>
        [Parameter(ParameterSetName = CATEGORYNAME, Position = 3)]
        [Alias("Regex")]
        public SwitchParameter RegularExpression { get; set; }

        /// <summary>
        /// <para type="description">The pagetemplate to get the page template category for.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = PAGETEMPLATE)]
        public PageTemplateInfo PageTemplate { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this page template. Populated by MEF.
        /// </summary>
        [Import]
        public GetCMSPageTemplateCategoryBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<IPageTemplateCategory> categories = null;

            switch (this.ParameterSetName)
            {
                case CATEGORYNAME:
                    categories = this.BusinessLayer.GetPageTemplateCategories(this.CategoryName, this.RegularExpression.ToBool(), false);
                    break;
                case IDSETNAME:
                    categories = this.BusinessLayer.GetPageTemplateCategories(this.ID, false);
                    break;
                case PATH:
                    categories = this.BusinessLayer.GetPageTemplateCategories(this.CategoryPath, false);
                    break;
                case PAGETEMPLATE:
                    categories = new IPageTemplateCategory[]
                    {
                        this.BusinessLayer.GetPageTemplateCategory(this.PageTemplate.ActLike<IPageTemplate>()),
                    };
                    break;

                case NONE:
                    categories = this.BusinessLayer.GetPageTemplateCategories();
                    break;
            }

            foreach (var category in categories)
            {
                this.ActOnObject(category);
            }
        }

        /// <summary>
        /// When overridden in a child class, operates on the specified page template category.
        /// </summary>
        /// <param name="pageTemplateCategory">The page template category to operate on.</param>
        protected virtual void ActOnObject(IPageTemplateCategory pageTemplateCategory)
        {
            this.WriteObject(pageTemplateCategory?.UndoActLike());
        }

        #endregion

    }
}

// <copyright file="GetCMSWebPartCategoryCmdletBase.cs" company="Chris Crutchfield">
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
using CMS.PortalEngine;
using ImpromptuInterface;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.WebParts
{
    /// <summary>
    /// Base class for cmdlets that need to Get a list of <see cref="WebPartCategoryInfo"/>.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GetCMSWebPartCategoryCmdletBase : MefCmdlet<GetCMSWebPartCategoryBusiness>
    {
        #region Constants

        /// <summary>
        /// Represents no parameters.
        /// </summary>
        protected const string NONE = "None";

        private const string CATEGORYNAME = "Category Name";
        private const string IDSETNAME = "ID";
        private const string PATH = "Path";
        private const string WEBPART = "Web Part";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The category name or display name the webpart category.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = CATEGORYNAME)]
        [Alias("DisplayName", "Name")]
        public string CategoryName { get; set; }

        /// <summary>
        /// <para type="description">The path to get the web part category at.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PATH)]
        [Alias("Path")]
        public string CategoryPath { get; set; }

        /// <summary>
        /// <para type="description">The IDs of the web part category to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = IDSETNAME)]
        public int[] ID { get; set; }

        /// <summary>
        /// <para type="description">Indiciates if the cmdlet should look recursively for web part categories.</para>
        /// </summary>
        [Parameter(ParameterSetName = CATEGORYNAME)]
        [Parameter(ParameterSetName = IDSETNAME)]
        [Parameter(ParameterSetName = PATH)]
        public virtual SwitchParameter Recurse { get; set; }

        /// <summary>
        /// <para type="description">Indicates if the CategoryName supplied is a regular expression.</para>
        /// </summary>
        [Parameter(ParameterSetName = CATEGORYNAME)]
        [Alias("Regex")]
        public SwitchParameter RegularExpression { get; set; }

        /// <summary>
        /// <para type="description">The webpart to get the web part category for.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = WEBPART)]
        public WebPartInfo WebPart { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<IWebPartCategory> categories = null;

            switch (this.ParameterSetName)
            {
                case CATEGORYNAME:
                    categories = this.BusinessLayer.GetWebPartCategories(this.CategoryName, this.RegularExpression.ToBool(), this.Recurse.ToBool());
                    break;
                case IDSETNAME:
                    categories = this.BusinessLayer.GetWebPartCategories(this.ID, this.Recurse.ToBool());
                    break;
                case PATH:
                    categories = this.BusinessLayer.GetWebPartCategories(this.CategoryPath, this.Recurse.ToBool());
                    break;
                case WEBPART:
                    categories = new IWebPartCategory[]
                    {
                        this.BusinessLayer.GetWebPartCategory(this.WebPart.ActLike<IWebPart>()),
                    };
                    break;

                case NONE:
                    categories = this.BusinessLayer.GetWebPartCategories();
                    break;
            }

            foreach (var category in categories)
            {
                this.ActOnObject(category);
            }
        }

        /// <summary>
        /// When overridden in a child class, operates on the specified web part category.
        /// </summary>
        /// <param name="webPartCategory">The web part category to operate on.</param>
        protected virtual void ActOnObject(IWebPartCategory webPartCategory)
        {
            this.WriteObject(webPartCategory?.UndoActLike());
        }

        #endregion

    }
}

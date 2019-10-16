// <GetCMSControlCategoryCmdletBase file="GetCMSWebPartCategoryCmdletBase.cs" company="Chris Crutchfield">
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
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using PoshKentico.Business.Development;
using PoshKentico.Core.Services.Development;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development
{
    /// <summary>
    /// Base class for cmdlets that need to Get a list of TControlCategory.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class GetCMSControlCategoryCmdletBase<TBusinessLayer, TControlService, TControl, TControlCategory, TControlHolder> : MefCmdlet<TBusinessLayer>
        where TBusinessLayer : GetCMSControlCategoryBusiness<TControlService, TControl, TControlCategory>
        where TControlService : IControlService<TControl, TControlCategory>
        where TControlHolder : IControl<TControl>
    {
        #region Constants

        /// <summary>
        /// Represents no parameters.
        /// </summary>
        protected const string NONE = "None";

        protected const string CATEGORYNAME = "Category Name";
        protected const string IDSETNAME = "ID";
        protected const string PATH = "Path";
        protected const string CONTROL = "Control";

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
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = CONTROL)]
        [Alias("WebPart", "Widget")]
        public TControl Control { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<IControlCategory<TControlCategory>> categories = null;

            switch (this.ParameterSetName)
            {
                case CATEGORYNAME:
                    categories = this.BusinessLayer.GetControlCategories(this.CategoryName, this.RegularExpression.ToBool(), this.Recurse.ToBool());
                    break;
                case IDSETNAME:
                    categories = this.BusinessLayer.GetControlCategories(this.ID, this.Recurse.ToBool());
                    break;
                case PATH:
                    categories = this.BusinessLayer.GetControlCategories(this.CategoryPath, this.Recurse.ToBool());
                    break;
                case CONTROL:
                    categories = new IControlCategory<TControlCategory>[]
                    {
                        this.BusinessLayer.GetControlCategory((TControlHolder)Activator.CreateInstance(typeof(TControlHolder), this.Control)),
                    };
                    break;

                case NONE:
                    categories = this.BusinessLayer.GetControlCategories();
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
        /// <param name="category">The web part category to operate on.</param>
        protected virtual void ActOnObject(IControlCategory<TControlCategory> category)
        {
            this.WriteObject(category.BackingControlCategory);
        }

        #endregion

    }
}

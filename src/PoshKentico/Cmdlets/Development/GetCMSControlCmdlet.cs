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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using CMS.FormEngine;
using ImpromptuInterface;
using PoshKentico.Business.Development;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development
{
    [ExcludeFromCodeCoverage]
    public class GetCMSControlCmdlet<TBusinessLayer, TGetCMSControlCategoryBusiness, TControlService, TControl, TControlCategory, TControlCategoryHolder> : MefCmdlet<TBusinessLayer>
        where TBusinessLayer : GetCMSControlBusiness<TGetCMSControlCategoryBusiness, TControlService, TControl, TControlCategory>
        where TGetCMSControlCategoryBusiness : GetCMSControlCategoryBusiness<TControlService, TControl, TControlCategory>
        where TControlService : IControlService<TControl, TControlCategory>
        where TControlCategoryHolder : IControlCategory<TControlCategory>
        where TControl : class
    {
        #region Constants

        /// <summary>
        /// Represents no parameters.
        /// </summary>
        protected const string NONE = "None";

        private const string CATEGORY = "Category";
        private const string CATEGORYNAME = "Category Name";
        private const string FIELD = "Field";
        private const string NAME = "Name";
        private const string PATH = "Path";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The category name or display name of the webpart category that contains the webparts.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = CATEGORYNAME)]
        [Alias("Category")]
        public string CategoryName { get; set; }

        /// <summary>
        /// <para type="description">The field to get the associated web part for.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = FIELD, ValueFromPipeline = true)]
        [Alias("Property")]
        public FormFieldInfo Field { get; set; }

        /// <summary>
        /// <para type="description">Indicates if the CategoryName or Name supplied is a regular expression.</para>
        /// </summary>
        [Parameter(ParameterSetName = CATEGORYNAME)]
        [Parameter(ParameterSetName = NAME)]
        [Alias("Regex")]
        public SwitchParameter RegularExpression { get; set; }

        /// <summary>
        /// <para type="description">The name or display name of the webpart.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = NAME)]
        [Alias("Name")]
        public string WebPartName { get; set; }

        /// <summary>
        /// <para type="description">The path to the webpart.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PATH)]
        [Alias("Path")]
        public string WebPartPath { get; set; }

        /// <summary>
        /// <para type="description">An object that represents the webpart category that contains the webparts.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = CATEGORY)]
        public TControlCategory WebPartCategory { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<IControl<TControl>> controls = null;

            switch (this.ParameterSetName)
            {
                case CATEGORY:
                    controls = this.BusinessLayer.GetControlsByCategory((TControlCategoryHolder)Activator.CreateInstance(typeof(TControlCategoryHolder), this.WebPartCategory));
                    break;
                case CATEGORYNAME:
                    controls = this.BusinessLayer.GetControlsByCategories(this.CategoryName, this.RegularExpression.ToBool());
                    break;
                case FIELD:
                    controls = new IControl<TControl>[]
                    {
                        this.BusinessLayer.GetControl(this.Field.ActLike<IControlField<TControl>>()),
                    };
                    break;
                case NAME:
                    controls = this.BusinessLayer.GetControls(this.WebPartName, this.RegularExpression.ToBool());
                    break;
                case PATH:
                    controls = new IControl<TControl>[]
                    {
                        this.BusinessLayer.GetControl(this.WebPartPath),
                    };
                    break;

                case NONE:
                    controls = this.BusinessLayer.GetControls();
                    break;
            }

            foreach (var control in controls)
            {
                this.ActOnObject(control);
            }
        }

        /// <summary>
        /// When overridden in a child class, operates on the specified web part.
        /// </summary>
        /// <param name="control">The control to operate on.</param>
        protected virtual void ActOnObject(IControl<TControl> control)
        {
            this.WriteObject(control?.BackingControl);
        }

        #endregion

    }
}

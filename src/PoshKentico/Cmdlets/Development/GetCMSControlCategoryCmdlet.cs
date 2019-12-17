// <copyright file="GetCMSControlCategoryCmdlet.cs" company="Chris Crutchfield">
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
    /// Base class for all Get-CMSControlCategory cmdlets.
    /// </summary>
    /// <typeparam name="TBusinessLayer">The business layer type used by the cmdlet.</typeparam>
    /// <typeparam name="TControlService">The type of the control service used to get the controls and control categories.</typeparam>
    /// <typeparam name="TControl">The type of control return by the control service.</typeparam>
    /// <typeparam name="TControlCategory">The type of control category returned by the control service.</typeparam>
    /// <typeparam name="TControlHolder">The type of control holder used to fields.</typeparam>
    /// <typeparam name="TControlCategoryHolder">The type of control category holder used to fields.</typeparam>
    [ExcludeFromCodeCoverage]
    public class GetCMSControlCategoryCmdlet<TBusinessLayer, TControlService, TControl, TControlCategory, TControlHolder, TControlCategoryHolder> : GetCMSControlCategoryCmdletBase<TBusinessLayer, TControlService, TControl, TControlCategory, TControlHolder>
        where TBusinessLayer : GetCMSControlCategoryBusiness<TControlService, TControl, TControlCategory>
        where TControlService : IControlService<TControl, TControlCategory>
        where TControlHolder : IControl<TControl>
        where TControlCategoryHolder : IControlCategory<TControlCategory>
    {
        #region Constants

        private const string PARENTCATEGORY = "Parent Category";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The webpart category that contains the webpart categories.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = PARENTCATEGORY)]
        [Alias("Parent", "ParentCategory", "ParentWebPartCategory", "ParentWidgetCategory")]
        public TControlCategory ParentControlCategory { get; set; }

        /// <summary>
        /// <para type="description">Indiciates if the cmdlet should look recursively for web part categories.</para>
        /// </summary>
        [Parameter(ParameterSetName = PARENTCATEGORY)]
        [Parameter(ParameterSetName = CATEGORYNAME)]
        [Parameter(ParameterSetName = IDSETNAME)]
        [Parameter(ParameterSetName = PATH)]
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
            if (this.ParameterSetName == PARENTCATEGORY)
            {
                IEnumerable<IControlCategory<TControlCategory>> categories = this.BusinessLayer.GetControlCategories((TControlCategoryHolder)Activator.CreateInstance(typeof(TControlCategoryHolder), this.ParentControlCategory), this.Recurse.ToBool());

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

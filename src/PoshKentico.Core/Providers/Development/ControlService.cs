// <copyright file="ControlService.cs" company="Chris Crutchfield">
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
using System.Linq;
using Castle.DynamicProxy;
using CMS.FormEngine;
using ImpromptuInterface;
using PoshKentico.Core.Services.Development;

namespace PoshKentico.Core.Providers.Development
{
    /// <summary>
    /// A generic service for getting controls from the CMS.
    /// </summary>
    /// <typeparam name="TControl">The type of control to return from the service.</typeparam>
    /// <typeparam name="TControlCategory">The type of control category to return from the service.</typeparam>
    public abstract class ControlService<TControl, TControlCategory> : IControlService<TControl, TControlCategory>
    {
        #region Fields

        private readonly ProxyGenerator proxyGenerator = new ProxyGenerator();

        #endregion

        /// <summary>
        /// Gets a list of controls.
        /// </summary>
        public abstract IEnumerable<IControl<TControl>> Controls { get; }

        /// <summary>
        /// Gets a list of control categories.
        /// </summary>
        public abstract IEnumerable<IControlCategory<TControlCategory>> Categories { get; }

        /// <inheritdoc />
        public IControlField<TControl> AddField(IControlField<TControl> field, IControl<TControl> control)
        {
            var formInfo = new FormInfo(control.Properties);
            var fieldInfo = new FormFieldInfo
            {
                AllowEmpty = field.AllowEmpty,
                Caption = field.Caption,
                DataType = field.DataType,
                DefaultValue = field.DefaultValue,
                Name = field.Name,
                Size = field.Size,
            };
            formInfo.AddFormItem(fieldInfo);

            control.Properties = formInfo.GetXmlDefinition();

            this.SetControlInfo(control.BackingControl);

            return this.AppendWebPart(fieldInfo, control).ActLike<IControlField<TControl>>();
        }

        /// <inheritdoc />
        public IControlCategory<TControlCategory> Create(IControlCategory<TControlCategory> controlCategory)
        {
            this.SetControlCategoryInfo(controlCategory.BackingControlCategory);

            return controlCategory;
        }

        /// <summary>
        /// When implemented by a child class, deletes the specified <see cref="IControlCategory{T}"/>.
        /// </summary>
        /// <param name="controlCategory">The <see cref="IControlCategory{T}"/> to delete.</param>
        public abstract void Delete(IControlCategory<TControlCategory> controlCategory);

        /// <summary>
        /// When implemented by a child class, deletes teh specified <see cref="IControl{T}"/>.
        /// </summary>
        /// <param name="control">The <see cref="IControl{T}"/> to delete.</param>
        public abstract void Delete(IControl<TControl> control);

        /// <inheritdoc />
        public IEnumerable<IControlCategory<TControlCategory>> GetControlCategories(IControlCategory<TControlCategory> parentCategory) =>
            (from c in this.Categories
             where c.ParentID == parentCategory.ID
             select c).ToArray();

        /// <inheritdoc />
        public IControlCategory<TControlCategory> GetControlCategory(int id) =>
            (from c in this.Categories
             where c.ID == id
             select c).SingleOrDefault();

        /// <summary>
        /// Gets a <see cref="IControl{T}"/> for <paramref name="id"/>.
        /// </summary>
        /// <param name="id">The ID to get the <see cref="IControl{T}"/> for.</param>
        /// <returns>The <see cref="IControl{T}"/> which matches <paramref name="id"/>.</returns>
        public IControl<TControl> GetControl(int id) =>
            (from c in this.Controls
             where c.ID == id
             select c).SingleOrDefault();

        /// <inheritdoc />
        public IEnumerable<IControlField<TControl>> GetControlFields(IControl<TControl> control) =>
               (from f in new FormInfo(control.Properties).GetFields<FormFieldInfo>()
                select this.AppendWebPart(f, control).ActLike<IControlField<TControl>>()).ToArray();

        /// <inheritdoc />
        public IEnumerable<IControl<TControl>> GetControls(IControlCategory<TControlCategory> category) =>
            (from c in this.Controls
             where c.CategoryID == category.ID
             select c).ToArray();

        /// <inheritdoc />
        public void Update(IControlCategory<TControlCategory> controlCategory)
        {
            var category = this.GetControlCategory(controlCategory.ID);

            if (category == null)
            {
                return;
            }

            category.DisplayName = controlCategory.DisplayName;
            category.Name = controlCategory.Name;
            category.ImagePath = controlCategory.ImagePath;
            category.ParentID = controlCategory.ParentID;

            this.SetControlCategoryInfo(category.BackingControlCategory);
        }

        /// <summary>
        /// Creates or updates a control category.
        /// </summary>
        /// <param name="controlCategory">The control category to create or update.</param>
        protected abstract void SetControlCategoryInfo(TControlCategory controlCategory);

        /// <summary>
        /// Creates or update a control.
        /// </summary>
        /// <param name="control">The control to create or update.</param>
        protected abstract void SetControlInfo(TControl control);

        private FormFieldInfo AppendWebPart(FormFieldInfo formFieldInfo, IControl<TControl> control)
        {
            var options = new ProxyGenerationOptions();
            options.AddMixinInstance(new ControlHolder<TControl>
            {
                Control = control,
            });

            var result = this.proxyGenerator.CreateClassProxyWithTarget(formFieldInfo, options);

            return result as FormFieldInfo;
        }
    }
}

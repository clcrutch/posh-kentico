using Castle.DynamicProxy;
using CMS.FormEngine;
using ImpromptuInterface;
using PoshKentico.Core.Services.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Providers.Development
{
    public abstract class ControlService<TControl, TControlCategory> : IControlService<TControl, TControlCategory>
    {
        #region Fields

        private readonly ProxyGenerator proxyGenerator = new ProxyGenerator();

        #endregion

        public abstract IEnumerable<IControl<TControl>> Controls { get; }

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

        public abstract void Delete(IControlCategory<TControlCategory> controlCategory);

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

        protected abstract void SetControlCategoryInfo(TControlCategory controlCategory);

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

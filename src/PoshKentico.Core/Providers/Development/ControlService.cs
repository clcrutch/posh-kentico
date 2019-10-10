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
        public abstract IEnumerable<IControl<TControl>> Controls { get; }

        public abstract IEnumerable<IControlCategory<TControlCategory>> Categories { get; }

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
    }
}

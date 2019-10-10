using CMS.PortalEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Providers.Development.Widgets
{
    public class WidgetCategory : ControlCategory<WidgetCategoryInfo>
    {
        public WidgetCategory()
        {
        }

        public WidgetCategory(WidgetCategoryInfo backingControlCategory)
            : base(backingControlCategory)
        {
        }

        public override string DisplayName { get => this.BackingControlCategory.WidgetCategoryDisplayName; set => this.BackingControlCategory.WidgetCategoryDisplayName = value; }

        public override int ID { get => this.BackingControlCategory.WidgetCategoryID; set => this.BackingControlCategory.WidgetCategoryID = value; }

        public override string ImagePath { get => this.BackingControlCategory.WidgetCategoryImagePath; set => this.BackingControlCategory.WidgetCategoryImagePath = value; }

        public override string Name { get => this.BackingControlCategory.WidgetCategoryName; set => this.BackingControlCategory.WidgetCategoryName = value; }

        public override int ParentID { get => this.BackingControlCategory.WidgetCategoryParentID; set => this.BackingControlCategory.WidgetCategoryParentID = value; }

        public override string Path { get => this.BackingControlCategory.WidgetCategoryPath; set => this.BackingControlCategory.WidgetCategoryPath = value; }
    }
}

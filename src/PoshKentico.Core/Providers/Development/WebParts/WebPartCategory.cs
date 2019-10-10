using CMS.PortalEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Providers.Development.WebParts
{
    public class WebPartCategory : ControlCategory<WebPartCategoryInfo>
    {
        public WebPartCategory()
        {
        }

        public WebPartCategory(WebPartCategoryInfo backingControlCategory)
            : base(backingControlCategory)
        {
        }

        public override string DisplayName { get => this.BackingControlCategory.CategoryDisplayName; set => this.BackingControlCategory.CategoryDisplayName = value; }

        public override int ID { get => this.BackingControlCategory.CategoryID; set => this.BackingControlCategory.CategoryID = value; }

        public override string ImagePath { get => this.BackingControlCategory.CategoryImagePath; set => this.BackingControlCategory.CategoryImagePath = value; }

        public override string Name { get => this.BackingControlCategory.CategoryName; set => this.BackingControlCategory.CategoryName = value; }

        public override int ParentID { get => this.BackingControlCategory.CategoryParentID; set => this.BackingControlCategory.CategoryParentID = value; }

        public override string Path { get => this.BackingControlCategory.CategoryPath; set => this.BackingControlCategory.CategoryPath = value; }
    }
}

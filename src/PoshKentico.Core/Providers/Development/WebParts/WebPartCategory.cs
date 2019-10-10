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
        public WebPartCategory(WebPartCategoryInfo backingControlCategory)
            : base(backingControlCategory)
        {
        }

        public override string DisplayName => this.BackingControlCategory.CategoryDisplayName;

        public override int ID => this.BackingControlCategory.CategoryID;

        public override string ImagePath => this.BackingControlCategory.CategoryImagePath;

        public override string Name => this.BackingControlCategory.CategoryName;

        public override int ParentID => this.BackingControlCategory.CategoryParentID;

        public override string Path => this.BackingControlCategory.CategoryPath;
    }
}

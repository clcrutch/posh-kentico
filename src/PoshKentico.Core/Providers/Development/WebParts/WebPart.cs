using CMS.PortalEngine;
using PoshKentico.Core.Services.Development.WebParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Providers.Development.WebParts
{
    public class WebPart : Control<WebPartInfo>, IWebPart
    {
        public WebPart()
        {
        }

        public WebPart(WebPartInfo backingControl)
            : base(backingControl)
        {
        }

        public override int CategoryID { get => this.BackingControl.WebPartCategoryID; set => this.BackingControl.WebPartCategoryID = value; }

        public override string DisplayName { get => this.BackingControl.WebPartDisplayName; set => this.BackingControl.WebPartDisplayName = value; }

        public override int ID { get => this.BackingControl.WebPartID; set => this.BackingControl.WebPartID = value; }

        public override string Name { get => this.BackingControl.WebPartName; set => this.BackingControl.WebPartName = value; }

        public string FileName { get => this.BackingControl.WebPartFileName; set => this.BackingControl.WebPartFileName = value; }

        public string Properties { get => this.BackingControl.WebPartProperties; set => this.BackingControl.WebPartProperties = value; }
    }
}

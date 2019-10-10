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
        public WebPart(WebPartInfo backingControl)
            : base(backingControl)
        {
        }

        public override int CategoryID => this.BackingControl.WebPartCategoryID;

        public override string DisplayName => this.BackingControl.WebPartDisplayName;

        public override int ID => this.BackingControl.WebPartID;

        public override string Name => this.BackingControl.WebPartName;

        public string FileName => this.BackingControl.WebPartFileName;

        public string Properties { get => this.BackingControl.WebPartProperties; set => this.BackingControl.WebPartProperties = value; }
    }
}

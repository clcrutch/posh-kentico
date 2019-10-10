using CMS.PortalEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Providers.Development.Widgets
{
    public class Widget : Control<WidgetInfo>
    {
        public Widget(WidgetInfo backingControl)
            : base(backingControl)
        {
        }

        public override int CategoryID { get => this.BackingControl.WidgetCategoryID; set => this.BackingControl.WidgetCategoryID = value; }

        public override string DisplayName { get => this.BackingControl.WidgetDisplayName; set => this.BackingControl.WidgetDisplayName = value; }

        public override int ID { get => this.BackingControl.WidgetID; set => this.BackingControl.WidgetID = value; }

        public override string Name { get => this.BackingControl.WidgetName; set => this.BackingControl.WidgetName = value; }
    }
}

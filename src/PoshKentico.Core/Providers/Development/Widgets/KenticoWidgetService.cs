using CMS.PortalEngine;
using PoshKentico.Core.Services.Development;
using PoshKentico.Core.Services.Development.Widgets;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Providers.Development.Widgets
{
    [Export(typeof(IWidgetService))]
    public class KenticoWidgetService : ControlService<WidgetInfo, WidgetCategoryInfo>, IWidgetService
    {
        public override IEnumerable<IControl<WidgetInfo>> Controls => (from w in WidgetInfoProvider.GetWidgets()
                                                                       select new Widget(w)).ToArray();

        public override IEnumerable<IControlCategory<WidgetCategoryInfo>> Categories => (from c in WidgetCategoryInfoProvider.GetWidgetCategories()
                                                                                         select new WidgetCategory(c)).ToArray();

        public override void Delete(IControlCategory<WidgetCategoryInfo> controlCategory) =>
            WidgetCategoryInfoProvider.DeleteWidgetCategoryInfo(controlCategory.ID);

        public override void Delete(IControl<WidgetInfo> control) =>
            WidgetInfoProvider.DeleteWidgetInfo(control.ID);

        protected override void SetControlCategoryInfo(WidgetCategoryInfo controlCategory) =>
            WidgetCategoryInfoProvider.SetWidgetCategoryInfo(controlCategory);
    }
}

// <copyright file="KenticoWidgetService.cs" company="Chris Crutchfield">
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
using System.ComponentModel.Composition;
using System.Linq;
using CMS.PortalEngine;
using PoshKentico.Core.Services.Development;
using PoshKentico.Core.Services.Development.Widgets;

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

        protected override void SetControlInfo(WidgetInfo control) =>
            WidgetInfoProvider.SetWidgetInfo(control);
    }
}

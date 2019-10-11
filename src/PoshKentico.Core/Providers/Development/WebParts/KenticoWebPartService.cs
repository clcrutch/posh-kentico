// <copyright file="KenticoWebPartService.cs" company="Chris Crutchfield">
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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Castle.DynamicProxy;
using CMS.FormEngine;
using CMS.PortalEngine;
using ImpromptuInterface;
using PoshKentico.Core.Services.Development;
using PoshKentico.Core.Services.Development.WebParts;

namespace PoshKentico.Core.Providers.Development.WebParts
{
    /// <summary>
    /// Implementation of <see cref="IWebPartService"/> that uses Kentico.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Export(typeof(IWebPartService))]
    public class KenticoWebPartService : ControlService<WebPartInfo, WebPartCategoryInfo>, IWebPartService
    {
        #region Properties

        /// <inheritdoc />
        public override IEnumerable<IControl<WebPartInfo>> Controls => (from wp in WebPartInfoProvider.GetWebParts()
                                                                        select new WebPart(wp)).ToArray();

        /// <inheritdoc />
        public override IEnumerable<IControlCategory<WebPartCategoryInfo>> Categories => (from c in WebPartCategoryInfoProvider.GetCategories()
                                                                                          select new WebPartCategory(c)).ToArray();

        #endregion

        #region Methods

        /// <inheritdoc />
        public IWebPart Create(IWebPart webPart)
        {
            var webPartInfo = new WebPartInfo
            {
                WebPartCategoryID = webPart.CategoryID,
                WebPartFileName = webPart.FileName,
                WebPartDisplayName = webPart.DisplayName,
                WebPartName = webPart.Name,
                WebPartProperties = FormInfo.GetEmptyFormDocument().OuterXml,
            };

            WebPartInfoProvider.SetWebPartInfo(webPartInfo);

            return webPartInfo.ActLike<IWebPart>();
        }

        /// <inheritdoc />
        public override void Delete(IControlCategory<WebPartCategoryInfo> webPartCategory) =>
            WebPartCategoryInfoProvider.DeleteCategoryInfo(webPartCategory.ID);

        /// <inheritdoc />
        public override void Delete(IControl<WebPartInfo> control) =>
            WebPartInfoProvider.DeleteWebPartInfo(control.ID);

        /// <inheritdoc />
        public void RemoveField(IControlField<WebPartInfo> field)
        {
            var formInfo = new FormInfo(field.Control.Properties);
            formInfo.RemoveFields(x => x.Name == field.Name);

            field.Control.Properties = formInfo.GetXmlDefinition();

            this.SetControlInfo(field.Control.BackingControl);
        }

        /// <inheritdoc />
        public void Update(IWebPart webPart)
        {
            var webPartInfo = WebPartInfoProvider.GetWebPartInfo(webPart.ID);

            if (webPartInfo == null)
            {
                return;
            }

            webPartInfo.WebPartCategoryID = webPart.ID;
            webPartInfo.WebPartDisplayName = webPart.DisplayName;
            webPartInfo.WebPartFileName = webPart.FileName;
            webPartInfo.WebPartName = webPart.Name;
            webPartInfo.WebPartProperties = webPart.Properties;

            WebPartInfoProvider.SetWebPartInfo(webPartInfo);
        }

        /// <inheritdoc />
        public void Update(IControlField<WebPartInfo> field)
        {
            var formInfo = new FormInfo(field.Control.Properties);
            var fieldInfo = formInfo.GetFormField(field.Name);

            fieldInfo.AllowEmpty = field.AllowEmpty;
            fieldInfo.Caption = field.Caption;
            fieldInfo.DataType = field.DataType;
            fieldInfo.DefaultValue = field.DefaultValue;
            fieldInfo.Size = field.Size;

            formInfo.UpdateFormField(field.Name, fieldInfo);

            field.Control.Properties = formInfo.GetXmlDefinition();

            this.SetControlInfo(field.Control.BackingControl);
        }

        protected override void SetControlCategoryInfo(WebPartCategoryInfo controlCategory) =>
            WebPartCategoryInfoProvider.SetWebPartCategoryInfo(controlCategory);

        protected override void SetControlInfo(WebPartInfo control) =>
            WebPartInfoProvider.SetWebPartInfo(control);

        #endregion

    }
}

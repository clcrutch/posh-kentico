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
        #region Fields

        private readonly ProxyGenerator proxyGenerator = new ProxyGenerator();

        #endregion

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
        public IWebPartField AddField(IWebPartField field, IWebPart webPart)
        {
            var formInfo = new FormInfo(webPart.Properties);
            var fieldInfo = new FormFieldInfo
            {
                AllowEmpty = field.AllowEmpty,
                Caption = field.Caption,
                DataType = field.DataType,
                DefaultValue = field.DefaultValue,
                Name = field.Name,
                Size = field.Size,
            };
            formInfo.AddFormItem(fieldInfo);

            webPart.Properties = formInfo.GetXmlDefinition();

            this.SaveFormUpdates(webPart);

            return this.AppendWebPart(fieldInfo, webPart).ActLike<IWebPartField>();
        }

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
        public IEnumerable<IWebPartField> GetWebPartFields(IWebPart webPart) =>
               (from f in new FormInfo(webPart.Properties).GetFields<FormFieldInfo>()
                select this.AppendWebPart(f, webPart).ActLike<IWebPartField>()).ToArray();

        /// <inheritdoc />
        public void RemoveField(IWebPartField field)
        {
            var formInfo = new FormInfo(field.WebPart.Properties);
            formInfo.RemoveFields(x => x.Name == field.Name);

            field.WebPart.Properties = formInfo.GetXmlDefinition();

            this.SaveFormUpdates(field.WebPart);
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
        public void Update(IWebPartField field)
        {
            var formInfo = new FormInfo(field.WebPart.Properties);
            var fieldInfo = formInfo.GetFormField(field.Name);

            fieldInfo.AllowEmpty = field.AllowEmpty;
            fieldInfo.Caption = field.Caption;
            fieldInfo.DataType = field.DataType;
            fieldInfo.DefaultValue = field.DefaultValue;
            fieldInfo.Size = field.Size;

            formInfo.UpdateFormField(field.Name, fieldInfo);

            field.WebPart.Properties = formInfo.GetXmlDefinition();

            this.SaveFormUpdates(field.WebPart);
        }

        protected override void SetControlCategoryInfo(WebPartCategoryInfo controlCategory) =>
            WebPartCategoryInfoProvider.SetWebPartCategoryInfo(controlCategory);

        private FormFieldInfo AppendWebPart(FormFieldInfo formFieldInfo, IWebPart webPart)
        {
            var options = new ProxyGenerationOptions();
            options.AddMixinInstance(new WebPartHolder
            {
                WebPart = webPart,
            });

            var result = this.proxyGenerator.CreateClassProxyWithTarget(formFieldInfo, options);

            return result as FormFieldInfo;
        }

        private void SaveFormUpdates(IWebPart webPart)
        {
            var webPartInfo = WebPartInfoProvider.GetWebPartInfo(webPart.ID);

            if (webPartInfo == null)
            {
                return;
            }

            webPartInfo.WebPartProperties = webPart.Properties;

            WebPartInfoProvider.SetWebPartInfo(webPartInfo);
        }

        #endregion

    }
}

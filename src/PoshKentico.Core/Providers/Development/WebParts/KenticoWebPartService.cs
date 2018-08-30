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
using System.Linq;
using Castle.DynamicProxy;
using CMS.FormEngine;
using CMS.PortalEngine;
using ImpromptuInterface;
using PoshKentico.Core.Services.Development.WebParts;

namespace PoshKentico.Core.Providers.Development.WebParts
{
    /// <summary>
    /// Implementation of <see cref="IWebPartService"/> that uses Kentico.
    /// </summary>
    [Export(typeof(IWebPartService))]
    public class KenticoWebPartService : IWebPartService
    {
        #region Fields

        private readonly ProxyGenerator proxyGenerator = new ProxyGenerator();

        #endregion

        #region Properties

        /// <inheritdoc />
        public IEnumerable<IWebPart> WebParts => (from wp in WebPartInfoProvider.GetWebParts()
                                                  select Impromptu.ActLike<IWebPart>(wp as WebPartInfo)).ToArray();

        /// <inheritdoc />
        public IEnumerable<IWebPartCategory> WebPartCategories => (from c in WebPartCategoryInfoProvider.GetCategories()
                                                                   select Impromptu.ActLike<IWebPartCategory>(c as WebPartCategoryInfo)).ToArray();

        #endregion

        #region Methods

        /// <inheritdoc />
        public IWebPartField AddField(IWebPartField field, IWebPart webPart)
        {
            var formInfo = new FormInfo(webPart.WebPartProperties);
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

            webPart.WebPartProperties = formInfo.GetXmlDefinition();

            this.SaveFormUpdates(webPart);

            return this.AppendWebPart(fieldInfo, webPart).ActLike<IWebPartField>();
        }

        /// <inheritdoc />
        public IWebPartCategory Create(IWebPartCategory webPartCategory)
        {
            var category = new WebPartCategoryInfo
            {
                CategoryDisplayName = webPartCategory.CategoryDisplayName,
                CategoryName = webPartCategory.CategoryName,
                CategoryImagePath = webPartCategory.CategoryImagePath,
                CategoryParentID = webPartCategory.CategoryParentID,
            };

            WebPartCategoryInfoProvider.SetWebPartCategoryInfo(category);

            return category.ActLike<IWebPartCategory>();
        }

        /// <inheritdoc />
        public IWebPart Create(IWebPart webPart)
        {
            var webPartInfo = new WebPartInfo
            {
                WebPartCategoryID = webPart.WebPartCategoryID,
                WebPartFileName = webPart.WebPartFileName,
                WebPartDisplayName = webPart.WebPartDisplayName,
                WebPartName = webPart.WebPartName,
                WebPartProperties = FormInfo.GetEmptyFormDocument().OuterXml,
            };

            WebPartInfoProvider.SetWebPartInfo(webPartInfo);

            return webPartInfo.ActLike<IWebPart>();
        }

        /// <inheritdoc />
        public void Delete(IWebPartCategory webPartCategory) =>
            WebPartCategoryInfoProvider.DeleteCategoryInfo(webPartCategory.CategoryID);

        /// <inheritdoc />
        public void Delete(IWebPart webPart) =>
            WebPartInfoProvider.DeleteWebPartInfo(webPart.WebPartID);

        /// <inheritdoc />
        public IEnumerable<IWebPartCategory> GetWebPartCategories(IWebPartCategory parentWebPartCategory) =>
            (from c in this.WebPartCategories
             where c.CategoryParentID == parentWebPartCategory.CategoryID
             select c).ToArray();

        /// <inheritdoc />
        public IWebPartCategory GetWebPartCategory(int id) =>
            (WebPartCategoryInfoProvider.GetWebPartCategoryInfoById(id) as WebPartCategoryInfo)?.ActLike<IWebPartCategory>();

        /// <inheritdoc />
        public IEnumerable<IWebPartField> GetWebPartFields(IWebPart webPart)
            => (from f in new FormInfo(webPart.WebPartProperties).GetFields<FormFieldInfo>()
                select this.AppendWebPart(f, webPart).ActLike<IWebPartField>()).ToArray();

        /// <inheritdoc />
        public IEnumerable<IWebPart> GetWebParts(IWebPartCategory webPartCategory) =>
            (from wp in this.WebParts
             where wp.WebPartCategoryID == webPartCategory.CategoryID
             select wp).ToArray();

        public void RemoveField(IWebPartField field, IWebPart webPart)
        {
            var formInfo = new FormInfo(webPart.WebPartProperties);
            formInfo.RemoveFields(x => x.Name == field.Name);

            webPart.WebPartProperties = formInfo.GetXmlDefinition();

            this.SaveFormUpdates(webPart);
        }

        /// <inheritdoc />
        public void Update(IWebPartCategory webPartCategory)
        {
            var category = WebPartCategoryInfoProvider.GetWebPartCategoryInfoById(webPartCategory.CategoryID);

            if (category == null)
            {
                return;
            }

            category.CategoryDisplayName = webPartCategory.CategoryDisplayName;
            category.CategoryName = webPartCategory.CategoryName;
            category.CategoryImagePath = webPartCategory.CategoryImagePath;
            category.CategoryParentID = webPartCategory.CategoryParentID;

            WebPartCategoryInfoProvider.SetWebPartCategoryInfo(category);
        }

        /// <inheritdoc />
        public void Update(IWebPart webPart)
        {
            var webPartInfo = WebPartInfoProvider.GetWebPartInfo(webPart.WebPartID);

            if (webPartInfo == null)
            {
                return;
            }

            webPartInfo.WebPartCategoryID = webPart.WebPartCategoryID;
            webPartInfo.WebPartDisplayName = webPart.WebPartDisplayName;
            webPartInfo.WebPartFileName = webPart.WebPartFileName;
            webPartInfo.WebPartName = webPart.WebPartName;
            webPartInfo.WebPartProperties = webPart.WebPartProperties;

            WebPartInfoProvider.SetWebPartInfo(webPartInfo);
        }

        private FormFieldInfo AppendWebPart(FormFieldInfo formFieldInfo, IWebPart webPart)
        {
            var options = new ProxyGenerationOptions();

            return this.proxyGenerator.CreateClassProxyWithTarget(formFieldInfo, options);
        }

        private void SaveFormUpdates(IWebPart webPart)
        {
            var webPartInfo = WebPartInfoProvider.GetWebPartInfo(webPart.WebPartID);

            if (webPartInfo == null)
            {
                return;
            }

            webPartInfo.WebPartProperties = webPart.WebPartProperties;

            WebPartInfoProvider.SetWebPartInfo(webPartInfo);
        }

        #endregion

    }
}

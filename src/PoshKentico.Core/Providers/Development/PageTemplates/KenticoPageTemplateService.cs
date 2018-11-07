// <copyright file="KenticoPageTemplateService.cs" company="Chris Crutchfield">
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

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Castle.DynamicProxy;
using CMS.FormEngine;
using CMS.PortalEngine;
using ImpromptuInterface;
using PoshKentico.Core.Services.Development.PageTemplates;

namespace PoshKentico.Core.Providers.Development.PageTemplates
{
    /// <summary>
    /// Implementation of <see cref="IPageTemplateService"/> that uses Kentico.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Export(typeof(IPageTemplateService))]
    public class KenticoPageTemplateService : IPageTemplateService
    {
        #region Fields

        private readonly ProxyGenerator proxyGenerator = new ProxyGenerator();

        #endregion

        #region Properties

        /// <inheritdoc />
        public IEnumerable<IPageTemplate> PageTemplates => Get();

        /// <inheritdoc />
        public IEnumerable<IPageTemplateCategory> PageTemplateCategories => (from c in PageTemplateCategoryInfoProvider.GetPageTemplateCategories()
                                                                   select Impromptu.ActLike<IPageTemplateCategory>(c as PageTemplateCategoryInfo)).ToArray();

        #endregion

        #region Methods

        public IEnumerable<IPageTemplate> Get()
        {
            //PageTemplateInstance newPageTemplate = new PageTemplateInstance();
            //newPageTemplate.PageTemplateType = "javascript";
            //var items = PageTemplateInfoProvider.GetPageTemplates().ToList();

            var item = PageTemplateInfoProvider.GetTemplates().ToList(); //.Where(i => i.CodeName == "Header_Placeholder_Footer");
            var itemsss = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            var wps = PageTemplateInfoProvider.GetTemplates().ToList().Select(i => new
            {
                i.PageTemplateProperties,
                form = new FormInfo(i.PageTemplateProperties),
                pt = i,
            }).ToList();

            return (from pt in PageTemplateInfoProvider.GetTemplates()
                    select Impromptu.ActLike<IPageTemplate>(pt as PageTemplateInfo)).ToArray();
        }

        /// <inheritdoc />
        public IPageTemplateField AddField(IPageTemplateField field, IPageTemplate pageTemplate)
        {
            var formInfo = new FormInfo(pageTemplate.PageTemplateProperties);
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

            pageTemplate.PageTemplateProperties = formInfo.GetXmlDefinition();

            this.SaveFormUpdates(pageTemplate);

            return this.AppendPageTemplate(fieldInfo, pageTemplate).ActLike<IPageTemplateField>();
        }

        /// <inheritdoc />
        public IPageTemplateCategory Create(IPageTemplateCategory pageTemplateCategory)
        {
            var category = new PageTemplateCategoryInfo
            {
                DisplayName = pageTemplateCategory.DisplayName,
                CategoryName = pageTemplateCategory.CategoryName,
                CategoryImagePath = pageTemplateCategory.CategoryImagePath,
                ParentId = pageTemplateCategory.ParentId,
            };

            PageTemplateCategoryInfoProvider.SetPageTemplateCategoryInfo(category);

            return category.ActLike<IPageTemplateCategory>();
        }
        
        /// <inheritdoc />
        public IPageTemplate Create(IPageTemplate pageTemplate)
        {
            var pageTemplateInfo = new PageTemplateInfo
            {
                CategoryID = pageTemplate.CategoryID,
                FileName = pageTemplate.FileName,
                DisplayName = pageTemplate.DisplayName,
                CodeName = pageTemplate.CodeName,
                PageTemplateLayout = pageTemplate.PageTemplateLayout,
                PageTemplateIconClass = pageTemplate.PageTemplateIconClass,
                PageTemplateCSS = pageTemplate.PageTemplateCSS,
                IsReusable = pageTemplate.IsReusable,

                PageTemplateProperties = FormInfo.GetEmptyFormDocument().OuterXml,
            };

            PageTemplateInfoProvider.SetPageTemplateInfo(pageTemplateInfo);

            return pageTemplateInfo.ActLike<IPageTemplate>();
        }

        /// <inheritdoc />
        public void Delete(IPageTemplateCategory pageTemplateCategory) =>
            PageTemplateCategoryInfoProvider.DeletePageTemplateCategory(pageTemplateCategory.CategoryId);

        /// <inheritdoc />
        public void Delete(IPageTemplate pageTemplate) =>
            PageTemplateInfoProvider.DeletePageTemplate(pageTemplate.PageTemplateID);

        /// <inheritdoc />
        public IEnumerable<IPageTemplateCategory> GetPageTemplateCategories(IPageTemplateCategory parentPageTemplateCategory) =>
            (from c in this.PageTemplateCategories
             where c.CategoryId == parentPageTemplateCategory.CategoryId
             select c).ToArray();

        /// <inheritdoc />
        public IPageTemplateCategory GetPageTemplateCategory(int id) =>
            (PageTemplateCategoryInfoProvider.GetPageTemplateCategoryInfo(id) as PageTemplateCategoryInfo)?.ActLike<IPageTemplateCategory>();

        /// <inheritdoc />
        public IEnumerable<IPageTemplateField> GetPageTemplateFields(IPageTemplate pageTemplate)
            => (from f in new FormInfo(pageTemplate.PageTemplateProperties).GetFields<FormFieldInfo>()
                select this.AppendPageTemplate(f, pageTemplate).ActLike<IPageTemplateField>()).ToArray();

        /// <inheritdoc />
        public IEnumerable<IPageTemplate> GetPageTemplates(IPageTemplateCategory pageTemplateCategory) =>
            (from wp in this.PageTemplates
             where wp.CategoryID == pageTemplateCategory.CategoryId
             select wp).ToArray();

        /// <inheritdoc />
        public void RemoveField(IPageTemplateField field)
        {
            var formInfo = new FormInfo(field.PageTemplate.PageTemplateProperties);
            formInfo.RemoveFields(x => x.Name == field.Name);

            field.PageTemplate.PageTemplateProperties = formInfo.GetXmlDefinition();

            this.SaveFormUpdates(field.PageTemplate);
        }

        /// <inheritdoc />
        public void Update(IPageTemplateCategory pageTemplateCategory)
        {
            var category = PageTemplateCategoryInfoProvider.GetPageTemplateCategoryInfoByGUID(pageTemplateCategory.CategoryGUID);

            if (category == null)
            {
                return;
            }

            category.DisplayName = pageTemplateCategory.DisplayName;
            category.CategoryName = pageTemplateCategory.CategoryName;
            category.CategoryImagePath = pageTemplateCategory.CategoryImagePath;
            category.ParentId = pageTemplateCategory.ParentId;

            PageTemplateCategoryInfoProvider.SetPageTemplateCategoryInfo(category);
        }

        /// <inheritdoc />
        public void Update(IPageTemplate pageTemplate)
        {
            var pageTemplateInfo = PageTemplateInfoProvider.GetPageTemplateInfo(pageTemplate.PageTemplateID);

            if (pageTemplateInfo == null)
            {
                return;
            }

            pageTemplateInfo.CategoryID = pageTemplate.CategoryID;
            pageTemplateInfo.DisplayName = pageTemplate.DisplayName;
            pageTemplateInfo.FileName = pageTemplate.FileName;
            pageTemplateInfo.CodeName = pageTemplate.CodeName;
            pageTemplateInfo.PageTemplateProperties = pageTemplate.PageTemplateProperties;

            PageTemplateInfoProvider.SetPageTemplateInfo(pageTemplateInfo);
        }

        /// <inheritdoc />
        public void Update(IPageTemplateField field)
        {
            var formInfo = new FormInfo(field.PageTemplate.PageTemplateProperties);
            var fieldInfo = formInfo.GetFormField(field.Name);

            fieldInfo.AllowEmpty = field.AllowEmpty;
            fieldInfo.Caption = field.Caption;
            fieldInfo.DataType = field.DataType;
            fieldInfo.DefaultValue = field.DefaultValue;
            fieldInfo.Size = field.Size;

            formInfo.UpdateFormField(field.Name, fieldInfo);

            field.PageTemplate.PageTemplateProperties = formInfo.GetXmlDefinition();

            this.SaveFormUpdates(field.PageTemplate);
        }

        private FormFieldInfo AppendPageTemplate(FormFieldInfo formFieldInfo, IPageTemplate pageTemplate)
        {
            var options = new ProxyGenerationOptions();
            options.AddMixinInstance(new PageTemplateHolder
            {
                PageTemplate = pageTemplate,
            });

            var result = this.proxyGenerator.CreateClassProxyWithTarget(formFieldInfo, options);

            ((IPageTemplateHolder)result).PageTemplate = pageTemplate;

            return result as FormFieldInfo;
        }

        private void SaveFormUpdates(IPageTemplate pageTemplate)
        {
            var pageTemplateInfo = PageTemplateInfoProvider.GetPageTemplateInfo(pageTemplate.PageTemplateID);

            if (pageTemplateInfo == null)
            {
                return;
            }

            pageTemplateInfo.PageTemplateProperties = pageTemplate.PageTemplateProperties;

            PageTemplateInfoProvider.SetPageTemplateInfo(pageTemplateInfo);
        }

        #endregion

    }
}

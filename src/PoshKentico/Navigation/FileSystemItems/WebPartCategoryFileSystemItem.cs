// <copyright file="WebPartCategoryFileSystemItem.cs" company="Chris Crutchfield">
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
using System.Linq;
using CMS.PortalEngine;
using PoshKentico.Navigation.DynamicParameters;

namespace PoshKentico.Navigation.FileSystemItems
{
    public class WebPartCategoryFileSystemItem : AbstractFileSystemItem
    {
        #region Fields

        private IEnumerable<IFileSystemItem> children;
        private WebPartCategoryInfo webPartCategoryInfo;

        #endregion

        #region Constructors

        public WebPartCategoryFileSystemItem(WebPartCategoryInfo webPartCategoryInfo, IFileSystemItem parent)
            : base(parent)
        {
            this.webPartCategoryInfo = webPartCategoryInfo;
        }

        #endregion

        #region Properties

        public override IEnumerable<IFileSystemItem> Children
        {
            get
            {
                if (this.children == null)
                {
                    IEnumerable<IFileSystemItem> childCategories = from c in WebPartCategoryInfoProvider.GetCategories()
                                                                   where c.CategoryParentID == this.webPartCategoryInfo.CategoryID
                                                                   select new WebPartCategoryFileSystemItem(c, this);
                    IEnumerable<IFileSystemItem> childWebParts = from w in WebPartInfoProvider.GetAllWebParts(this.webPartCategoryInfo.CategoryID)
                                                                 select new WebPartFileSystemItem(w, this);

                    this.children = childCategories.Concat(childWebParts).ToArray();
                }

                return this.children;
            }
        }

        public override bool IsContainer => true;

        public override object Item => this.webPartCategoryInfo;

        public override string Path => this.webPartCategoryInfo.CategoryPath
            .Replace("/", "Development\\WebParts\\")
            .Replace("/", "\\");

        #endregion

        #region Methods

        public static void Create(string displayName, string name, string imagePath, IFileSystemItem parent)
        {
            var parentCategoryItem = parent as WebPartCategoryFileSystemItem;
            if (parentCategoryItem == null)
            {
                return;
            }

            var newCategory = new WebPartCategoryInfo();
            newCategory.CategoryDisplayName = displayName;
            newCategory.CategoryName = name;
            newCategory.CategoryImagePath = imagePath;
            newCategory.CategoryParentID = parentCategoryItem.webPartCategoryInfo.CategoryParentID;

            WebPartCategoryInfoProvider.SetWebPartCategoryInfo(newCategory);
        }

        public override bool Delete(bool recurse)
        {
            if (recurse && !this.DeleteChildren())
            {
                return false;
            }

            return this.webPartCategoryInfo.Delete();
        }

        public override bool Exists(string path)
        {
            return this.FindPath(path) != null;
        }

        public override IFileSystemItem FindPath(string path)
        {
            var adjustedPath = path.ToLowerInvariant()
                .Replace("development\\webparts", string.Empty)
                .Replace('\\', '/');

            if (string.IsNullOrWhiteSpace(adjustedPath))
            {
                adjustedPath = "/";
            }

            var webPartCategoryInfo = (from c in WebPartCategoryInfoProvider.GetCategories()
                                       where c.CategoryPath.Equals(adjustedPath, StringComparison.InvariantCultureIgnoreCase)
                                       select c).FirstOrDefault();

            if (webPartCategoryInfo != null)
            {
                return new WebPartCategoryFileSystemItem(webPartCategoryInfo, this);
            }
            else
            {
                return null;
            }
        }

        public override void NewItem(string name, string itemTypeName, object newItemValue)
        {
            switch (itemTypeName.ToLowerInvariant())
            {
                case "webpartcategory":
                    if (newItemValue is WebPartCategoryInfo)
                    {
                        var webPartCategoryInfo = newItemValue as WebPartCategoryInfo;
                        webPartCategoryInfo.CategoryParentID = this.webPartCategoryInfo.CategoryID;

                        WebPartCategoryInfoProvider.SetWebPartCategoryInfo(webPartCategoryInfo);
                    }
                    else if (newItemValue is NewWebPartCategoryDynamicParameter)
                    {
                        var dynamicParameter = newItemValue as NewWebPartCategoryDynamicParameter;
                        string displayName = dynamicParameter.DisplayName ?? name;
                        string imagePath = dynamicParameter.ImagePath;

                        WebPartCategoryFileSystemItem.Create(displayName, name, imagePath, this);
                    }
                    else
                    {
                        throw new NotSupportedException($"Type \"{newItemValue.GetType().Name}\" cannot be used to create a new WebPartCategory.");
                    }

                    return;
                case "webpart":
                    if (newItemValue is WebPartInfo)
                    {
                        var webPartInfo = newItemValue as WebPartInfo;
                        webPartInfo.WebPartCategoryID = this.webPartCategoryInfo.CategoryID;

                        WebPartInfoProvider.SetWebPartInfo(webPartInfo);
                    }
                    else if (newItemValue is NewWebPartDynamicParameter)
                    {
                        var dynamicParameter = newItemValue as NewWebPartDynamicParameter;
                        string displayName = dynamicParameter?.DispalyName ?? name;
                        string fileName = dynamicParameter?.FileName;

                        WebPartFileSystemItem.Create(displayName, name, fileName, this);
                    }
                    else
                    {
                        throw new NotSupportedException($"Type \"{newItemValue.GetType().Name}\" cannot be used to create a new WebPart.");
                    }

                    return;
                default:
                    throw new NotSupportedException($"Cannot create ItemType \"{itemTypeName}\" at \"{this.Path}\\{name}\".");
            }
        }

        #endregion

    }
}

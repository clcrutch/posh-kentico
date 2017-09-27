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
using System.Collections.ObjectModel;
using System.Linq;
using CMS.PortalEngine;
using PoshKentico.Navigation.DynamicParameters;

namespace PoshKentico.Navigation.FileSystemItems
{
    /// <summary>
    /// File system item representing a <see cref="WebPartCategoryInfo"/>.
    /// </summary>
    public class WebPartCategoryFileSystemItem : AbstractFileSystemItem
    {
        #region Fields

        private IEnumerable<IFileSystemItem> children;
        private WebPartCategoryInfo webPartCategoryInfo;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebPartCategoryFileSystemItem"/> class.
        /// </summary>
        /// <param name="webPartCategoryInfo">The <see cref="WebPartCategoryInfo"/> for the file system to represent.</param>
        /// <param name="parent">The parent file system item. Null if root.</param>
        public WebPartCategoryFileSystemItem(WebPartCategoryInfo webPartCategoryInfo, IFileSystemItem parent)
            : base(parent)
        {
            this.webPartCategoryInfo = webPartCategoryInfo;
        }

        #endregion

        #region Properties

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public override bool IsContainer => true;

        /// <inheritdoc/>
        public override object Item => this.webPartCategoryInfo;

        /// <inheritdoc/>
        public override string Path => this.webPartCategoryInfo.CategoryPath
            .Replace("/", "Development\\WebParts\\")
            .Replace("/", "\\");

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new <see cref="WebPartCategoryInfo"/> under the specified parent.
        /// </summary>
        /// <param name="displayName">Display name for the new <see cref="WebPartCategoryInfo"/>.</param>
        /// <param name="name">Name for the <see cref="WebPartCategoryInfo"/>.</param>
        /// <param name="imagePath">Image Path for the <see cref="WebPartCategoryInfo"/></param>
        /// <param name="parent">The parent of type <see cref="WebPartCategoryFileSystemItem"/>.</param>
        public static void Create(string displayName, string name, string imagePath, IFileSystemItem parent)
        {
            var parentCategoryItem = parent as WebPartCategoryFileSystemItem;
            if (parentCategoryItem == null)
            {
                return;
            }

            var newCategory = new WebPartCategoryInfo
            {
                CategoryDisplayName = displayName,
                CategoryName = name,
                CategoryImagePath = imagePath,
                CategoryParentID = parentCategoryItem.webPartCategoryInfo.CategoryParentID
            };

            WebPartCategoryInfoProvider.SetWebPartCategoryInfo(newCategory);
        }

        /// <inheritdoc/>
        public override bool Delete(bool recurse)
        {
            if (recurse && !this.DeleteChildren())
            {
                return false;
            }

            return this.webPartCategoryInfo.Delete();
        }

        /// <inheritdoc/>
        public override bool Exists(string path)
        {
            return this.FindPath(path) != null;
        }

        /// <inheritdoc/>
        public override IFileSystemItem FindPath(string path)
        {
            var kenticoPath = this.ConvertToKenticoPath(path);

            var categories = WebPartCategoryInfoProvider.GetCategories();

            var webPartCategoryInfo = (from c in categories
                                       where c.CategoryPath.Equals(kenticoPath, StringComparison.InvariantCultureIgnoreCase)
                                       select c).FirstOrDefault();

            if (webPartCategoryInfo != null)
            {
                return new WebPartCategoryFileSystemItem(webPartCategoryInfo, this);
            }
            else
            {
                var parentDirectory = KenticoNavigationCmdletProvider.GetDirectory(path);
                kenticoPath = this.ConvertToKenticoPath(parentDirectory);

                var parentWebPartCategoryInfo = (from c in categories
                                                 where c.CategoryPath.Equals(kenticoPath, StringComparison.InvariantCultureIgnoreCase)
                                                 select c).FirstOrDefault();

                if (parentWebPartCategoryInfo == null)
                {
                    return null;
                }

                var webPartName = KenticoNavigationCmdletProvider.GetName(path);
                var webPart = (from w in WebPartInfoProvider.GetAllWebParts(parentWebPartCategoryInfo.CategoryID)
                               where w.WebPartName.Equals(webPartName, StringComparison.InvariantCultureIgnoreCase)
                               select w).FirstOrDefault();

                if (webPart == null)
                {
                    return null;
                }

                return new WebPartFileSystemItem(webPart, this);
            }
        }

        /// <inheritdoc/>
        public override Dictionary<string, object> GetProperty(Collection<string> providerSpecificPickList)
        {
            var properties = new Dictionary<string, object>
            {
                { "displayname", this.webPartCategoryInfo.CategoryDisplayName },
                { "imagepath", this.webPartCategoryInfo.CategoryImagePath }
            };

            this.PurgeUnwantedProperties(providerSpecificPickList, properties);

            return properties;
        }

        /// <inheritdoc/>
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
                        string displayName = dynamicParameter?.DisplayName ?? name;
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

        /// <inheritdoc/>
        public override void SetProperty(Dictionary<string, object> propertyValue)
        {
            bool updatedValue = false;
            if (propertyValue.ContainsKey("displayname"))
            {
                this.webPartCategoryInfo.CategoryDisplayName = propertyValue["displayname"] as string;
                updatedValue = true;
            }

            if (propertyValue.ContainsKey("imagepath"))
            {
                this.webPartCategoryInfo.CategoryImagePath = propertyValue["imagepath"] as string;
                updatedValue = true;
            }

            if (updatedValue)
            {
                WebPartCategoryInfoProvider.SetWebPartCategoryInfo(this.webPartCategoryInfo);
            }
        }

        private string ConvertToKenticoPath(string path)
        {
            var kenticoPath = path.ToLowerInvariant()
                .Replace("development\\webparts", string.Empty)
                .Replace('\\', '/');

            if (string.IsNullOrWhiteSpace(kenticoPath))
            {
                kenticoPath = "/";
            }

            return kenticoPath;
        }

        #endregion

    }
}

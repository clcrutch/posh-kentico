// <copyright file="WebPartFileSystemItem.cs" company="Chris Crutchfield">
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
using CMS.PortalEngine;

namespace PoshKentico.Navigation.FileSystemItems
{
    /// <summary>
    /// File system item representing a <see cref="WebPartInfo"/>.
    /// </summary>
    public class WebPartFileSystemItem : AbstractFileSystemItem
    {
        #region Fields

        private string path;
        private WebPartInfo webPartInfo;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebPartFileSystemItem"/> class.
        /// </summary>
        /// <param name="webPartInfo">The <see cref="WebPartInfo"/> for the file system to represent.</param>
        /// <param name="parent">The parent file system item. Null if root.</param>
        public WebPartFileSystemItem(WebPartInfo webPartInfo, IFileSystemItem parent)
            : base(parent)
        {
            this.path = $"{parent.Path}\\{webPartInfo.WebPartName}";
            this.webPartInfo = webPartInfo;
        }

        #endregion

        #region Properties

        /// <inheritdoc/>
        public override IEnumerable<IFileSystemItem> Children => null;

        /// <inheritdoc/>
        public override bool IsContainer => false;

        /// <inheritdoc/>
        public override object Item => this.webPartInfo;

        /// <inheritdoc/>
        public override string Path => this.path;

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new <see cref="WebPartInfo"/> under the specified parent.
        /// </summary>
        /// <param name="displayName">Display name for the new <see cref="WebPartInfo"/>.</param>
        /// <param name="name">Name for the <see cref="WebPartInfo"/>.</param>
        /// <param name="fileName">File name for the new <see cref="WebPartInfo"/>.</param>
        /// <param name="parent">The parent of type <see cref="WebPartFileSystemItem"/>.</param>
        public static void Create(string displayName, string name, string fileName, IFileSystemItem parent)
        {
            var webPartInfo = new WebPartInfo
            {
                WebPartDisplayName = displayName,
                WebPartName = name,
                WebPartFileName = fileName,
                WebPartProperties = "<form></form>",
                WebPartCategoryID = ((parent.Item as WebPartCategoryInfo)?.CategoryID).GetValueOrDefault(0)
            };

            WebPartInfoProvider.SetWebPartInfo(webPartInfo);
        }

        /// <inheritdoc/>
        public override bool Delete(bool recurse)
        {
            return this.webPartInfo.Delete();
        }

        /// <inheritdoc/>
        public override bool Exists(string path)
        {
            return this.path.Equals(path, StringComparison.InvariantCultureIgnoreCase) && this.webPartInfo != null;
        }

        /// <inheritdoc/>
        public override IFileSystemItem FindPath(string path)
        {
            if (this.path.Equals(path, StringComparison.InvariantCultureIgnoreCase))
            {
                return this;
            }

            return null;
        }

        /// <inheritdoc/>
        public override Dictionary<string, object> GetProperty(Collection<string> providerSpecificPickList)
        {
            var properties = new Dictionary<string, object>
            {
                { "displayname", this.webPartInfo.WebPartDisplayName },
                { "filename", this.webPartInfo.WebPartFileName }
            };

            this.PurgeUnwantedProperties(providerSpecificPickList, properties);

            return properties;
        }

        /// <inheritdoc/>
        public override void NewItem(string name, string itemTypeName, object newItemValue)
        {
            throw new NotSupportedException("Cannot create a new item as a child of a web part file system item.");
        }

        /// <inheritdoc/>
        public override void SetProperty(Dictionary<string, object> propertyValue)
        {
            bool updatedValue = false;
            if (propertyValue.ContainsKey("displayname"))
            {
                this.webPartInfo.WebPartDisplayName = propertyValue["displayname"] as string;
                updatedValue = true;
            }

            if (propertyValue.ContainsKey("filename"))
            {
                this.webPartInfo.WebPartFileName = propertyValue["filename"] as string;
                updatedValue = true;
            }

            if (updatedValue)
            {
                WebPartInfoProvider.SetWebPartInfo(this.webPartInfo);
            }
        }

        #endregion

    }
}

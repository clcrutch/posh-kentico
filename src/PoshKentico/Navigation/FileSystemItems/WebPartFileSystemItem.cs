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

        /// <summary>
        /// Gets the Children of the file system item.
        /// </summary>
        public override IEnumerable<IFileSystemItem> Children => null;

        /// <summary>
        /// Gets if the file system item is a container
        /// </summary>
        public override bool IsContainer => false;

        /// <summary>
        /// Gets the item that the file system item represents.
        /// </summary>
        public override object Item => this.webPartInfo;

        /// <summary>
        /// Gets the full path of the file system item.
        /// </summary>
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
            var webPartInfo = new WebPartInfo();

            webPartInfo.WebPartDisplayName = displayName;
            webPartInfo.WebPartName = name;
            webPartInfo.WebPartFileName = fileName;
            webPartInfo.WebPartProperties = "<form></form>";
            webPartInfo.WebPartCategoryID = ((parent.Item as WebPartCategoryInfo)?.CategoryID).GetValueOrDefault(0);

            WebPartInfoProvider.SetWebPartInfo(webPartInfo);
        }

        /// <summary>
        /// Deletes the file system item.
        /// </summary>
        /// <param name="recurse">Indicates if the delete function should delete children.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public override bool Delete(bool recurse)
        {
            return this.webPartInfo.Delete();
        }

        /// <summary>
        /// Checks if the path specified exists.
        /// </summary>
        /// <param name="path">File system path to check.</param>
        /// <returns>True if exists, false otherwise.</returns>
        public override bool Exists(string path)
        {
            return this.path.Equals(path, StringComparison.InvariantCultureIgnoreCase) && this.webPartInfo != null;
        }

        /// <summary>
        /// Finds the file system item representing the path specified.
        /// </summary>
        /// <param name="path">File system path to find.</param>
        /// <returns>The file system item representing the path specified.  Null if not found.</returns>
        public override IFileSystemItem FindPath(string path)
        {
            if (this.path.Equals(path, StringComparison.InvariantCultureIgnoreCase))
            {
                return this;
            }

            return null;
        }

        /// <summary>
        /// This method is not supported.
        /// </summary>
        /// <param name="name">Name of the new item.</param>
        /// <param name="itemTypeName">Type of the new item.  Specified as the -ItemType parameter.</param>
        /// <param name="newItemValue">Either the dynamic parameter or the value specified on the -Value parameter.</param>
        public override void NewItem(string name, string itemTypeName, object newItemValue)
        {
            throw new NotSupportedException("Cannot create a new item as a child of a web part file system item.");
        }

        #endregion

    }
}

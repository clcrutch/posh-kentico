// <copyright file="RootFileSystemItem.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Navigation.FileSystemItems
{
    /// <summary>
    /// The root file system item.
    /// </summary>
    public class RootFileSystemItem : AbstractFileSystemItem
    {
        #region Fields

        private IEnumerable<IFileSystemItem> children;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RootFileSystemItem"/> class.
        /// </summary>
        public RootFileSystemItem()
            : base(null)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Children of the file system item.
        /// </summary>
        public override IEnumerable<IFileSystemItem> Children
        {
            get
            {
                if (this.children == null)
                {
                    this.children = this.CreateChildren();
                }

                return this.children;
            }
        }

        /// <summary>
        /// Gets if the file system item is a container
        /// </summary>
        public override bool IsContainer => true;

        /// <summary>
        /// Gets the parent of the file system item.
        /// </summary>
        public override object Item => this;

        /// <summary>
        /// Gets the full path of the file system item.
        /// </summary>
        public override string Path => string.Empty;

        #endregion

        #region Methods

        /// <summary>
        /// Deletes the file system item.
        /// </summary>
        /// <param name="recurse">Indicates if the delete function should delete children.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public override bool Delete(bool recurse)
        {
            if (recurse)
            {
                return this.DeleteChildren();
            }

            return false;
        }

        /// <summary>
        /// Checks if the path specified exists.
        /// </summary>
        /// <param name="path">File system path to check.</param>
        /// <returns>True if exists, false otherwise.</returns>
        public override bool Exists(string path)
        {
            return path == string.Empty || this.Children.Any(c => c.Exists(path));
        }

        /// <summary>
        /// Finds the file system item representing the path specified.
        /// </summary>
        /// <param name="path">File system path to find.</param>
        /// <returns>The file system item representing the path specified.  Null if not found.</returns>
        public override IFileSystemItem FindPath(string path)
        {
            var itemContainingPath = this.Children.FirstOrDefault(c => c.Exists(path));

            if (path == string.Empty)
            {
                return this;
            }
            else
            {
                return itemContainingPath?.FindPath(path);
            }
        }

        /// <summary>
        /// This method is not supported.
        /// </summary>
        /// <param name="name">Name of the new item.</param>
        /// <param name="itemTypeName">Type of the new item.  Specified as the -ItemType parameter.</param>
        /// <param name="newItemValue">Either the dynamic parameter or the value specified on the -Value parameter.</param>
        public override void NewItem(string name, string itemTypeName, object newItemValue)
        {
            throw new NotSupportedException("Cannot create a new item as a child of the root file system item.");
        }

        private IEnumerable<IFileSystemItem> CreateChildren()
        {
            return new IFileSystemItem[]
            {
#pragma warning disable SA1118 // Parameter must not span multiple lines
                new MetaFileSystemItem("Development", this, new IFileSystemItem[]
                {
                    new WebPartCategoryFileSystemItem(WebPartCategoryInfoProvider.GetWebPartCategoryInfoByCodeName("/"), this)
                })
#pragma warning restore SA1118 // Parameter must not span multiple lines
            };
        }

        #endregion

    }
}

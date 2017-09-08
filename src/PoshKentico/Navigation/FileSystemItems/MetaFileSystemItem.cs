// <copyright file="MetaFileSystemItem.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Navigation.FileSystemItems
{
    /// <summary>
    /// File system item which represents items that are not actually part of Kentico.
    /// </summary>
    public class MetaFileSystemItem : AbstractFileSystemItem
    {
        #region Fields

        private IEnumerable<IFileSystemItem> children;
        private string path;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetaFileSystemItem"/> class.
        /// </summary>
        /// <param name="path">The path for the current <see cref="MetaFileSystemItem"/>.</param>
        /// <param name="parent">The parent <see cref="IFileSystemItem"/>.</param>
        /// <param name="children">The children <see cref="IFileSystemItem"/>s.</param>
        public MetaFileSystemItem(string path, IFileSystemItem parent, IEnumerable<IFileSystemItem> children)
            : base(parent)
        {
            this.path = path;
            this.children = children;
        }

        #endregion

        #region Properties

        /// <inheritdoc/>
        public override IEnumerable<IFileSystemItem> Children => this.children;

        /// <inheritdoc/>
        public override bool IsContainer => true;

        /// <inheritdoc/>
        public override object Item => this;

        /// <inheritdoc/>
        public override string Path => this.path;

        #endregion

        #region Methods

        /// <inheritdoc/>
        public override bool Delete(bool recurse)
        {
            if (recurse)
            {
                return this.DeleteChildren();
            }

            return false;
        }

        /// <inheritdoc/>
        public override bool Exists(string path)
        {
            var pathParts = path.Split('\\');

            return (pathParts.Length > 0 && pathParts[0].Equals(this.Path, StringComparison.InvariantCultureIgnoreCase) && pathParts.Length == 1) ||
                (this.Children?.Any(c => c.Exists(path))).GetValueOrDefault(false);
        }

        /// <inheritdoc/>
        public override IFileSystemItem FindPath(string path)
        {
            if (path.Equals(this.Path, StringComparison.InvariantCultureIgnoreCase))
            {
                return this;
            }
            else
            {
                var itemContainingPath = this.Children?.FirstOrDefault(c => c.Exists(path));

                return itemContainingPath?.FindPath(path);
            }
        }

        /// <inheritdoc/>
        public override void NewItem(string name, string itemTypeName, object newItemValue)
        {
            throw new NotSupportedException("Cannot create a new item as a child of a meta file system item.");
        }

        #endregion

    }
}

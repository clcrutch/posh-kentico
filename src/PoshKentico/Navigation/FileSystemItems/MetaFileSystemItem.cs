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
    public class MetaFileSystemItem : AbstractFileSystemItem
    {
        #region Fields

        private IEnumerable<IFileSystemItem> children;
        private string path;

        #endregion

        #region Constructors

        public MetaFileSystemItem(string path, IFileSystemItem parent, IEnumerable<IFileSystemItem> children)
            : base(parent)
        {
            this.path = path;
            this.children = children;
        }

        #endregion

        #region Properties

        public override IEnumerable<IFileSystemItem> Children => this.children;

        public override bool IsContainer => true;

        public override object Item => this;

        public override string Path => this.path;

        #endregion

        #region Methods

        public override bool Delete(bool recurse)
        {
            if (recurse)
            {
                return this.DeleteChildren();
            }

            return false;
        }

        public override bool Exists(string path)
        {
            var pathParts = path.Split('\\');

            return (pathParts.Length > 0 && pathParts[0].Equals(this.Path, StringComparison.InvariantCultureIgnoreCase)) ||
                (this.Children?.Any(c => c.Exists(path))).GetValueOrDefault(false);
        }

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

        public override void NewItem(string name, string itemTypeName, object newItemValue)
        {
            throw new NotSupportedException("Cannot create a new item as a child of a meta file system item.");
        }

        #endregion

    }
}

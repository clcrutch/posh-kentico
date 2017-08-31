// <copyright file="AbstractFileSystemItem.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Navigation.FileSystemItems
{
    public abstract class AbstractFileSystemItem : IFileSystemItem
    {
        #region Constructors

        public AbstractFileSystemItem(IFileSystemItem parent)
        {
            this.Parent = parent;
        }

        #endregion

        #region Properties

        public abstract IEnumerable<IFileSystemItem> Children { get; }

        public abstract bool IsContainer { get; }

        public abstract object Item { get; }

        public virtual IFileSystemItem Parent { get; protected set; }

        public abstract string Path { get; }

        #endregion

        #region Methods

        public abstract bool Delete(bool recurse);

        public virtual bool DeleteChildren()
        {
            if (this.Children == null)
            {
                return true;
            }

            foreach (var child in this.Children)
            {
                if (!child.Delete(true))
                {
                    return false;
                }
            }

            return true;
        }

        public abstract bool Exists(string path);

        public abstract IFileSystemItem FindPath(string path);

        public abstract void NewItem(string name, string itemTypeName, object newItemValue);

        #endregion

    }
}

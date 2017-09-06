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
using System.Linq;
using System.Text.RegularExpressions;

namespace PoshKentico.Navigation.FileSystemItems
{
    /// <summary>
    /// Base class for FileSystemItems.
    /// </summary>
    public abstract class AbstractFileSystemItem : IFileSystemItem
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractFileSystemItem"/> class.
        /// </summary>
        /// <param name="parent">The parent file system item. Null if root.</param>
        public AbstractFileSystemItem(IFileSystemItem parent)
        {
            this.Parent = parent;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the Children of the file system item.
        /// </summary>
        public abstract IEnumerable<IFileSystemItem> Children { get; }

        /// <summary>
        /// Gets if the file system item is a container
        /// </summary>
        public abstract bool IsContainer { get; }

        /// <summary>
        /// Gets the item that the file system item represents.
        /// </summary>
        public abstract object Item { get; }

        /// <summary>
        /// Gets the name of the file system item.
        /// </summary>
        public virtual string Name
        {
            get
            {
                var modifiedPath = this.Path.TrimEnd('\\');
                var slashIndex = modifiedPath.LastIndexOf('\\');

                return slashIndex > -1 ? modifiedPath.Substring(slashIndex + 1, modifiedPath.Length - slashIndex - 1) : modifiedPath;
            }
        }

        /// <summary>
        /// Gets the parent of the file system item.
        /// </summary>
        public virtual IFileSystemItem Parent { get; protected set; }

        /// <summary>
        /// Gets the full path of the file system item.
        /// </summary>
        public abstract string Path { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes the file system item.
        /// </summary>
        /// <param name="recurse">Indicates if the delete function should delete children.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public abstract bool Delete(bool recurse);

        /// <summary>
        /// Deletes all children.
        /// </summary>
        /// <returns>True if successful, false otherwise.</returns>
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

        /// <summary>
        /// Checks if the path specified exists.
        /// </summary>
        /// <param name="path">File system path to check.</param>
        /// <returns>True if exists, false otherwise.</returns>
        public abstract bool Exists(string path);

        /// <summary>
        /// Finds the file system item representing the path specified.
        /// </summary>
        /// <param name="path">File system path to find.</param>
        /// <returns>The file system item representing the path specified.  Null if not found.</returns>
        public abstract IFileSystemItem FindPath(string path);

        /// <summary>
        /// Finds the file system item representing the path specified.
        /// </summary>
        /// <param name="regex">File system path to find.</param>
        /// <returns>The file system item representing the path specified.  Null if not found.</returns>
        public virtual IFileSystemItem[] GetItemsFromRegex(Regex regex)
        {
            if (this.Children == null)
            {
                return null;
            }

            return (from c in this.Children
                    where regex.IsMatch(c.Name)
                    select c).ToArray();
        }

        /// <summary>
        /// Creates a new item under the current path.
        /// </summary>
        /// <param name="name">Name of the new item.</param>
        /// <param name="itemTypeName">Type of the new item.  Specified as the -ItemType parameter.</param>
        /// <param name="newItemValue">Either the dynamic parameter or the value specified on the -Value parameter.</param>
        public abstract void NewItem(string name, string itemTypeName, object newItemValue);

        #endregion

    }
}

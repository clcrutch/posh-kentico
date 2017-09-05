// <copyright file="IFileSystemItem.cs" company="Chris Crutchfield">
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
    /// <summary>
    /// Interface for FileSystemItems.
    /// </summary>
    public interface IFileSystemItem
    {
        #region Properties

        /// <summary>
        /// Gets the Children of the file system item.
        /// </summary>
        IEnumerable<IFileSystemItem> Children { get; }

        /// <summary>
        /// Gets if the file system item is a container
        /// </summary>
        bool IsContainer { get; }

        /// <summary>
        /// Gets the item that the file system item represents.
        /// </summary>
        object Item { get; }

        /// <summary>
        /// Gets the parent of the file system item.
        /// </summary>
        IFileSystemItem Parent { get; }

        /// <summary>
        /// Gets the full path of the file system item.
        /// </summary>
        string Path { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes the file system item.
        /// </summary>
        /// <param name="recurse">Indicates if the delete function should delete children.</param>
        /// <returns>True if successful, false otherwise.</returns>
        bool Delete(bool recurse);

        /// <summary>
        /// Checks if the path specified exists.
        /// </summary>
        /// <param name="path">File system path to check.</param>
        /// <returns>True if exists, false otherwise.</returns>
        bool Exists(string path);

        /// <summary>
        /// Finds the file system item representing the path specified.
        /// </summary>
        /// <param name="path">File system path to find.</param>
        /// <returns>The file system item representing the path specified.  Null if not found.</returns>
        IFileSystemItem FindPath(string path);

        /// <summary>
        /// Creates a new item under the current path.
        /// </summary>
        /// <param name="name">Name of the new item.</param>
        /// <param name="itemTypeName">Type of the new item.  Specified as the -ItemType parameter.</param>
        /// <param name="newItemValue">Either the dynamic parameter or the value specified on the -Value parameter.</param>
        void NewItem(string name, string itemTypeName, object newItemValue);

        #endregion

    }
}

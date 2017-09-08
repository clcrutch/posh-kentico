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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        /// <inheritdoc/>
        public abstract IEnumerable<IFileSystemItem> Children { get; }

        /// <inheritdoc/>
        public abstract bool IsContainer { get; }

        /// <inheritdoc/>
        public abstract object Item { get; }

        /// <inheritdoc/>
        public virtual string Name => KenticoNavigationCmdletProvider.GetName(this.Path);

        /// <inheritdoc/>
        public virtual IFileSystemItem Parent { get; protected set; }

        /// <inheritdoc/>
        public abstract string Path { get; }

        #endregion

        #region IFileSystemItem Implementation

        /// <inheritdoc/>
        public abstract bool Delete(bool recurse);

        /// <inheritdoc/>
        public abstract bool Exists(string path);

        /// <inheritdoc/>
        public abstract IFileSystemItem FindPath(string path);

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public virtual Dictionary<string, object> GetProperty(Collection<string> providerSpecificPickList)
        {
            return null;
        }

        /// <inheritdoc/>
        public abstract void NewItem(string name, string itemTypeName, object newItemValue);

        /// <inheritdoc/>
        public virtual void SetProperty(Dictionary<string, object> propertyValue)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region Methods

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
        /// Removes properties not specified from the provided dictionary.
        /// </summary>
        /// <param name="providerSpecificPickList">List of properties to keep in the dictionary.</param>
        /// <param name="properties">Dictionary which contains properties and values.</param>
        protected void PurgeUnwantedProperties(Collection<string> providerSpecificPickList, Dictionary<string, object> properties)
        {
            if (providerSpecificPickList.Count > 0)
            {
                var itemsToRemove = properties.Keys.Except(providerSpecificPickList).ToArray();

                foreach (var itemToRemove in itemsToRemove)
                {
                    properties.Remove(itemToRemove.ToLowerInvariant());
                }
            }
        }

        #endregion

    }
}

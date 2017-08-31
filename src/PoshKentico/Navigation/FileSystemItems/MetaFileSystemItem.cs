// <copyright file="MetaFileSystemItem.cs" company="Chris Crutchfield">
// Copyright (c) Chris Crutchfield. All rights reserved.
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

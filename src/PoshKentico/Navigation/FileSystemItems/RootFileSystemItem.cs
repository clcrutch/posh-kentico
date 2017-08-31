// <copyright file="RootFileSystemItem.cs" company="Chris Crutchfield">
// Copyright (c) Chris Crutchfield. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using CMS.PortalEngine;

namespace PoshKentico.Navigation.FileSystemItems
{
    public class RootFileSystemItem : AbstractFileSystemItem
    {
        #region Fields

        private IEnumerable<IFileSystemItem> children;

        #endregion

        #region Constructors

        public RootFileSystemItem()
            : base(null)
        {
        }

        #endregion

        #region Properties

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

        public override bool IsContainer => true;

        public override object Item => this;

        public override string Path => string.Empty;

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
            return path == string.Empty || this.Children.Any(c => c.Exists(path));
        }

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

// <copyright file="WebPartFileSystemItem.cs" company="Chris Crutchfield">
// Copyright (c) Chris Crutchfield. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using CMS.PortalEngine;

namespace PoshKentico.Navigation.FileSystemItems
{
    public class WebPartFileSystemItem : AbstractFileSystemItem
    {
        #region Fields

        private string path;
        private WebPartInfo webPartInfo;

        #endregion

        #region Constructors

        public WebPartFileSystemItem(WebPartInfo webPartInfo, IFileSystemItem parent)
            : base(parent)
        {
            this.path = $"{parent.Path}\\{webPartInfo.WebPartName}";
            this.webPartInfo = webPartInfo;
        }

        #endregion

        #region Properties

        public override IEnumerable<IFileSystemItem> Children => null;

        public override bool IsContainer => false;

        public override object Item => this.webPartInfo;

        public override string Path => this.path;

        #endregion

        #region Methods

        public override bool Delete(bool recurse)
        {
            return this.webPartInfo.Delete();
        }

        public override bool Exists(string path)
        {
            return this.path.Equals(path, StringComparison.InvariantCultureIgnoreCase) && this.webPartInfo != null;
        }

        public override IFileSystemItem FindPath(string path)
        {
            // TODO
            return this;
        }

        public override void NewItem(string name, string itemTypeName, object newItemValue)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}

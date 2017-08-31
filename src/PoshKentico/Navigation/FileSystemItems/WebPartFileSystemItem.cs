// <copyright file="WebPartFileSystemItem.cs" company="Chris Crutchfield">
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

        public static void Create(string displayName, string name, string fileName, IFileSystemItem parent)
        {
            var webPartInfo = new WebPartInfo();

            webPartInfo.WebPartDisplayName = displayName;
            webPartInfo.WebPartName = name;
            webPartInfo.WebPartFileName = fileName;
            webPartInfo.WebPartProperties = "<form></form>";
            webPartInfo.WebPartCategoryID = ((parent.Item as WebPartCategoryInfo)?.CategoryID).GetValueOrDefault(0);

            WebPartInfoProvider.SetWebPartInfo(webPartInfo);
        }

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

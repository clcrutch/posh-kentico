﻿// <copyright file="KenticoNavigationCmdletProvider.cs" company="Chris Crutchfield">
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
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;
using PoshKentico.Helpers;
using PoshKentico.Navigation.DynamicParameters;
using PoshKentico.Navigation.FileSystemItems;

namespace PoshKentico.Navigation
{
    [CmdletProvider("KenticoProvider", ProviderCapabilities.None)]
    public class KenticoNavigationCmdletProvider : NavigationCmdletProvider
    {
        #region Constants

        private const string DRIVENAME = "Kentico";

        #endregion

        #region Fields

        private IFileSystemItem rootItem = new RootFileSystemItem();

        #endregion

        #region Methods

        protected override bool IsValidPath(string path)
        {
            return true;
        }

        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            var drive = new PSDriveInfo(DRIVENAME, this.ProviderInfo, string.Empty, string.Empty, null);
            var drives = new Collection<PSDriveInfo>() { drive };

            return drives;
        }

        protected override bool ItemExists(string path)
        {
            this.InitKentico();

            return this.rootItem.Exists(path);
        }

        protected override bool IsItemContainer(string path)
        {
            return (this.rootItem.FindPath(path)?.IsContainer).GetValueOrDefault(false);
        }

        protected override void GetChildItems(string path, bool recurse)
        {
            this.InitKentico();

            IFileSystemItem fileSystemItem = this.rootItem.FindPath(path);

            if (fileSystemItem == null || fileSystemItem.Children == null)
            {
                return;
            }

            foreach (var child in fileSystemItem.Children)
            {
                this.WriteItemObject(child, recurse);
            }
        }

        protected override void GetItem(string path)
        {
            this.InitKentico();

            this.WriteItemObject(this.rootItem.FindPath(path), false);
        }

        protected override bool HasChildItems(string path)
        {
            this.InitKentico();

            return (this.rootItem.FindPath(path)?.Children.Any()).GetValueOrDefault(false);
        }

        protected override void NewItem(string path, string itemTypeName, object newItemValue)
        {
            this.InitKentico();

            int lastSlash = path.LastIndexOf('\\');
            string directory = path.Substring(0, lastSlash);
            string name = path.Substring(lastSlash + 1);
            var item = this.rootItem.FindPath(directory);

            this.rootItem.FindPath(directory)?.NewItem(name, itemTypeName, newItemValue ?? this.DynamicParameters);
        }

        protected override object NewItemDynamicParameters(string path, string itemTypeName, object newItemValue)
        {
            switch (itemTypeName.ToLowerInvariant())
            {
                case "webpartcategory":
                    return new NewWebPartCategoryDynamicParameter();
                case "webpart":
                    return new NewWebPartDynamicParameter();
                default:
                    return null;
            }
        }

        protected override void RemoveItem(string path, bool recurse)
        {
            this.InitKentico();

            if (!(this.rootItem.FindPath(path)?.Delete(recurse)).GetValueOrDefault(false))
            {
                throw new Exception($"Cannot delete item at \"{path}\".");
            }
        }

        private void InitKentico()
        {
            CmsApplicationHelper.InitializeKentico(this.WriteDebug, this.WriteVerbose);
        }

        private void WriteItemObject(IFileSystemItem item, bool recurse)
        {
            if (item == null)
            {
                return;
            }

            this.WriteItemObject(item.Item, item.Path, item.IsContainer);

            if (recurse && item.Children != null)
            {
                foreach (var child in item.Children)
                {
                    this.WriteItemObject(child, recurse);
                }
            }
        }

        #endregion

    }
}

// <copyright file="KenticoNavigationCmdletProvider.cs" company="Chris Crutchfield">
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
using System.Text.RegularExpressions;
using CMS.PortalEngine;
using PoshKentico.Helpers;
using PoshKentico.Navigation.DynamicParameters;
using PoshKentico.Navigation.FileSystemItems;

namespace PoshKentico.Navigation
{
    /// <summary>
    /// Kentico CMS file system provider for PowerShell.
    /// Creates Kentico: by default.
    /// </summary>
    [OutputType(typeof(WebPartCategoryInfo), typeof(WebPartInfo), ProviderCmdlet = "Get-Item")]
    [CmdletProvider("KenticoProvider", ProviderCapabilities.ExpandWildcards)]
    public class KenticoNavigationCmdletProvider : NavigationCmdletProvider
    {
        #region Constants

        private const string DRIVENAME = "Kentico";

        #endregion

        #region Fields

        private IFileSystemItem rootItem = new RootFileSystemItem();

        #endregion

        #region Methods

        /// <summary>
        /// Expands the path specified.
        /// </summary>
        /// <param name="path">The file system path to exand.</param>
        /// <returns>An array representing the expanded paths.</returns>
        protected override string[] ExpandPath(string path)
        {
            return (from i in this.GetItemsFromPath(path)
                    select i.Name).ToArray();
        }

        /// <summary>
        /// Indicates if the path specified is valid.
        /// </summary>
        /// <param name="path">The file system path to check.</param>
        /// <returns>True if the path is valid, false otherwise.</returns>
        protected override bool IsValidPath(string path)
        {
            return true;
        }

        /// <summary>
        /// Creates default drives.
        /// Creates a Kentico: drive.
        /// </summary>
        /// <returns>A collection of the drives created.</returns>
        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            var drive = new PSDriveInfo(DRIVENAME, this.ProviderInfo, string.Empty, string.Empty, null);
            var drives = new Collection<PSDriveInfo>() { drive };

            return drives;
        }

        /// <summary>
        /// Checks if an item exists at the specified path.
        /// </summary>
        /// <param name="path">The file system path to check.</param>
        /// <returns>True if the item exists at the specified path, false otherwise.</returns>
        protected override bool ItemExists(string path)
        {
            this.InitKentico();

            return this.rootItem.Exists(path);
        }

        /// <summary>
        /// Checks if the item is a container (directory).
        /// </summary>
        /// <param name="path">The file system path to check.</param>
        /// <returns>True if the item at the specified path is a container, false otherwise.</returns>
        protected override bool IsItemContainer(string path)
        {
            this.InitKentico();

            return (this.rootItem.FindPath(path)?.IsContainer).GetValueOrDefault(false);
        }

        /// <summary>
        /// Gets all of the child items at the specified path.
        /// </summary>
        /// <param name="path">The file system path to get the child items of.</param>
        /// <param name="recurse">Indicates if we should iterate through all children recursively.</param>
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

        /// <summary>
        /// Gets the item at the specified path.
        /// </summary>
        /// <param name="path">The file system path to the item to get.</param>
        protected override void GetItem(string path)
        {
            this.InitKentico();

            this.WriteItemObject(this.rootItem.FindPath(path), false);
        }

        /// <summary>
        /// Indicates whether the item at the specified path has any children.
        /// </summary>
        /// <param name="path">The file system path to the item to check for children.</param>
        /// <returns>True if the specified item has any children, false otherwise.</returns>
        protected override bool HasChildItems(string path)
        {
            this.InitKentico();

            return (this.rootItem.FindPath(path)?.Children.Any()).GetValueOrDefault(false);
        }

        /// <summary>
        /// Creates a new item at the specified path.
        /// </summary>
        /// <param name="path">The file system path to create the new item at.</param>
        /// <param name="itemTypeName">The value specified to the -ItemType parameter.</param>
        /// <param name="newItemValue">The value of the item to create at the specified location.</param>
        protected override void NewItem(string path, string itemTypeName, object newItemValue)
        {
            this.InitKentico();

            int lastSlash = path.LastIndexOf('\\');
            string directory = path.Substring(0, lastSlash);
            string name = path.Substring(lastSlash + 1);
            var item = this.rootItem.FindPath(directory);

            this.rootItem.FindPath(directory)?.NewItem(name, itemTypeName, newItemValue ?? this.DynamicParameters);
        }

        /// <summary>
        /// Creates a new dynamic parameter for New-Item.
        /// </summary>
        /// <param name="path">The file system path to create the new item at.</param>
        /// <param name="itemTypeName">The value specified to the -ItemType parameter.</param>
        /// <param name="newItemValue">The value of the item to create at the specified location.</param>
        /// <returns>The new dynamic parameter for the specified item type.</returns>
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

        /// <summary>
        /// Normalizes the specified path to an item, returning a path relative to a specified base path.
        /// </summary>
        /// <param name="path">A fully-qualified provider specific path to an item.</param>
        /// <param name="basePath">The path that the return value is relative to.</param>
        /// <returns>A normalized path that is relative to the specified base path.</returns>
        protected override string NormalizeRelativePath(string path, string basePath)
        {
            return path.Replace('/', '\\');
        }

        /// <summary>
        /// Removes the item at the specified path.
        /// </summary>
        /// <param name="path">The file system path to the item to remove.</param>
        /// <param name="recurse">Indicates if children items should be removed.</param>
        protected override void RemoveItem(string path, bool recurse)
        {
            this.InitKentico();

            if (!(this.rootItem.FindPath(path)?.Delete(recurse)).GetValueOrDefault(false))
            {
                throw new Exception($"Cannot delete item at \"{path}\".");
            }
        }

        private IFileSystemItem[] GetItemsFromPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }

            int lastSlash = path.LastIndexOf('\\');
            string directory = lastSlash > -1 ? path.Substring(0, lastSlash) : string.Empty;
            string name = lastSlash > -1 ? path.Substring(lastSlash + 1) : path;
            var item = this.rootItem.FindPath(directory);

            var regexString = $"^{Regex.Escape(name).Replace("\\*", ".*")}\\\\*$";
            var regex = new Regex(regexString, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            return item.GetItemsFromRegex(regex);
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

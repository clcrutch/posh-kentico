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
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Text.RegularExpressions;
using CMS.PortalEngine;
using PoshKentico.Extensions;
using PoshKentico.Navigation.DynamicParameters;
using PoshKentico.Navigation.FileSystemItems;
using PoshKentico.Services;

namespace PoshKentico.Navigation
{
    /// <summary>
    /// Kentico CMS file system provider for PowerShell.
    /// Creates Kentico: by default.
    /// </summary>
    [OutputType(typeof(WebPartCategoryInfo), typeof(WebPartInfo), ProviderCmdlet = "Get-Item")]
    [CmdletProvider("KenticoProvider", ProviderCapabilities.ExpandWildcards)]
    public class KenticoNavigationCmdletProvider : NavigationCmdletProvider, IPropertyCmdletProvider
    {
        #region Constants

        private const string DRIVENAME = "Kentico";

        #endregion

        #region Fields

        private IFileSystemItem rootItem = new RootFileSystemItem();

        #endregion

        #region Properties

        /// <summary>
        /// CMS Application service used for interacting with a CMS Application.
        /// </summary>
        [Import]
        public ICmsApplicationService CmsApplicationService { get; set; }

        #endregion

        #region Statics

        /// <summary>
        /// Gets the parent directory of the file system path specified.
        /// </summary>
        /// <param name="path">The file system path to get the parent directory of.</param>
        /// <returns>The parent directory file system path.</returns>
        public static string GetDirectory(string path)
        {
            string adjustedPath = path.TrimEnd('\\');
            int lastSlashIndex = adjustedPath.LastIndexOf('\\');

            return lastSlashIndex > -1 ? adjustedPath.Substring(0, lastSlashIndex) : string.Empty;
        }

        /// <summary>
        /// Gets the name of the file system path specified.
        /// </summary>
        /// <param name="path">The file system path to get the name of.</param>
        /// <returns>The name of the file system path specified.</returns>
        public static string GetName(string path)
        {
            string adjustedPath = path.TrimEnd('\\');
            int lastSlashIndex = adjustedPath.LastIndexOf('\\');

            return lastSlashIndex > -1 ? adjustedPath.Substring(lastSlashIndex + 1, adjustedPath.Length - lastSlashIndex - 1) : adjustedPath;
        }

        /// <summary>
        /// Joins two items into a single path using the '\' character.
        /// </summary>
        /// <param name="items">Lists of items to join.</param>
        /// <returns>A single file system path.</returns>
        public static string JoinPath(params string[] items)
        {
#pragma warning disable SA1118 // Parameter must not span multiple lines
            return string.Join("\\", from i in items
                                     select i.TrimEnd('\\'));
#pragma warning restore SA1118 // Parameter must not span multiple lines
        }

        #endregion

        #region IPropertyCmdletProvider Implementation

        /// <inheritdoc/>
        public void GetProperty(string path, Collection<string> providerSpecificPickList)
        {
            this.Initialize();

            var outputObject = this.rootItem.FindPath(path)?.GetProperty(providerSpecificPickList).ToPSObject();

            if (outputObject != null)
            {
                this.WritePropertyObject(outputObject, path);
            }
        }

        /// <inheritdoc/>
        public object GetPropertyDynamicParameters(string path, Collection<string> providerSpecificPickList)
        {
            return null;
        }

        /// <inheritdoc/>
        public void SetProperty(string path, PSObject propertyValue)
        {
            this.rootItem.FindPath(path)?.SetProperty(propertyValue.ToDictionary());
        }

        /// <inheritdoc/>
        public object SetPropertyDynamicParameters(string path, PSObject propertyValue)
        {
            return null;
        }

        /// <inheritdoc/>
        public void ClearProperty(string path, Collection<string> propertyToClear)
        {
            throw new PSNotSupportedException();
        }

        /// <inheritdoc/>
        public object ClearPropertyDynamicParameters(string path, Collection<string> propertyToClear)
        {
            throw new PSNotSupportedException();
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        protected override string[] ExpandPath(string path)
        {
            this.Initialize();

            var regex = new Regex($"^{this.PSDriveInfo.CurrentLocation.Replace("\\", "\\\\")}\\\\", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            return (from i in this.GetItemsFromPath(path)
                    select regex.Replace(i.Path, string.Empty)).ToArray();
        }

        /// <inheritdoc/>
        protected override bool IsValidPath(string path)
        {
            return true;
        }

        /// <inheritdoc/>
        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            var drive = new PSDriveInfo(DRIVENAME, this.ProviderInfo, string.Empty, string.Empty, null);
            var drives = new Collection<PSDriveInfo>() { drive };

            return drives;
        }

        /// <inheritdoc/>
        protected override bool ItemExists(string path)
        {
            this.Initialize();

            return this.rootItem.Exists(path);
        }

        /// <inheritdoc/>
        protected override bool IsItemContainer(string path)
        {
            this.Initialize();

            return (this.rootItem.FindPath(path)?.IsContainer).GetValueOrDefault(false);
        }

        /// <inheritdoc/>
        protected override void GetChildItems(string path, bool recurse)
        {
            this.Initialize();

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

        /// <inheritdoc/>
        protected override void GetItem(string path)
        {
            this.Initialize();

            this.WriteItemObject(this.rootItem.FindPath(path), false);
        }

        /// <inheritdoc/>
        protected override bool HasChildItems(string path)
        {
            this.Initialize();

            return (this.rootItem.FindPath(path)?.Children?.Any()).GetValueOrDefault(false);
        }

        /// <inheritdoc/>
        protected override void NewItem(string path, string itemTypeName, object newItemValue)
        {
            this.Initialize();

            string directory = KenticoNavigationCmdletProvider.GetDirectory(path);
            string name = KenticoNavigationCmdletProvider.GetName(path);
            var item = this.rootItem.FindPath(directory);

            if (item == null)
            {
                throw new IOException($"The path \"{directory}\" was not found.");
            }

            item.NewItem(name, itemTypeName, newItemValue ?? this.DynamicParameters);
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        protected override string NormalizeRelativePath(string path, string basePath)
        {
            return path.Replace('/', '\\');
        }

        /// <inheritdoc/>
        protected override void RemoveItem(string path, bool recurse)
        {
            this.Initialize();

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

            string directory = KenticoNavigationCmdletProvider.GetDirectory(path);
            string name = KenticoNavigationCmdletProvider.GetName(path);
            var item = this.rootItem.FindPath(directory);

            var regexString = $"^{Regex.Escape(name).Replace("\\*", ".*")}$";
            var regex = new Regex(regexString, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            return item.GetItemsFromRegex(regex);
        }

        private void Initialize()
        {
            MefHost.Initialize();
            MefHost.Container.ComposeParts(this);

            this.CmsApplicationService.Initialize(this.WriteDebug, this.WriteVerbose);
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

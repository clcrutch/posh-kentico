// <copyright file="MefCmdletProvider.cs" company="Chris Crutchfield">
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
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;
using PoshKentico.Business;
using PoshKentico.CmdletProviders.Resource;
using PoshKentico.Core.Services.General;
using PoshKentico.Core.Services.Resource;
using PoshKentico.Extensions;

namespace PoshKentico
{
    /// <summary>
    /// Base class for navigation cmdlet providers.
    /// </summary>
    /// <typeparam name="TBusinessProvider">The Business provider to use for accessing Kentico resources. Must inherit from <see cref="CmdletProviderBusinessBase"/></></typeparam>
    public abstract class MefCmdletProvider<TBusinessProvider> : NavigationCmdletProvider, IPropertyCmdletProvider, IContentCmdletProvider, ICmdlet
        where TBusinessProvider : CmdletProviderBusinessBase
    {
        /// <summary>
        /// <see cref=" ICmsApplicationService"/>
        /// </summary>
        [Import]
        public ICmsApplicationService CmsApplicationService { get; set; }

        /// <summary>
        /// The name of the provider
        /// </summary>
        protected abstract string ProviderName { get; }

        /// <summary>
        /// The provider drive name
        /// </summary>
        protected abstract string DriveName { get; }

        /// <summary>
        /// The root path of the drive
        /// </summary>
        protected abstract string DriveRootPath { get; }

        /// <summary>
        /// The 
        /// </summary>
        protected abstract string DriveDescription { get; }

        /// <summary>
        /// Gets or sets the <see cref="TBusinessProvider"/> for this provider.
        /// </summary>
        [Import]
        public TBusinessProvider Business { get; set; }

        #region Statics

        public static string GetDirectory(string path)
        {
            string adjustedPath = path.TrimEnd('\\');
            int lastSlashIndex = adjustedPath.LastIndexOf('\\');

            return lastSlashIndex > -1 ? adjustedPath.Substring(0, lastSlashIndex) : string.Empty;
        }

        public static string GetName(string path)
        {
            string adjustedPath = path.TrimEnd('\\');
            int lastSlashIndex = adjustedPath.LastIndexOf('\\');

            return lastSlashIndex > -1 ? adjustedPath.Substring(lastSlashIndex + 1, adjustedPath.Length - lastSlashIndex - 1) : adjustedPath;
        }

        public static string JoinPath(params string[] items)
        {
#pragma warning disable SA1118 // Parameter must not span multiple lines
            return string.Join("\\", from i in items
                                     select i.TrimEnd('\\'));
#pragma warning restore SA1118 // Parameter must not span multiple lines
        }

        #endregion

        #region IPropertyCmdletProvider Implementation

        /// <inheritdoc />
        public virtual void GetProperty(string path, Collection<string> providerSpecificPickList)
        {
            this.Initialize();

            var outputObject = this.Business.GetProperty(path, providerSpecificPickList)?.ToPSObject();

            if (outputObject != null)
            {
                this.WritePropertyObject(outputObject, path);
            }
        }

        /// <inheritdoc />
        public virtual object GetPropertyDynamicParameters(string path, Collection<string> providerSpecificPickList)
        {
            return null;
        }

        /// <inheritdoc />
        public virtual void SetProperty(string path, PSObject propertyValue)
        {
            this.Business.SetProperty(path, propertyValue.ToDictionary());
        }

        /// <inheritdoc />
        public virtual object SetPropertyDynamicParameters(string path, PSObject propertyValue)
        {
            return null;
        }

        /// <inheritdoc />
        public virtual void ClearProperty(string path, Collection<string> propertyToClear)
        {
            throw new PSNotSupportedException();
        }

        /// <inheritdoc />
        public virtual object ClearPropertyDynamicParameters(string path, Collection<string> propertyToClear)
        {
            throw new PSNotSupportedException();
        }

        #endregion

        #region IContentCmdletProvider Implementation

        /// <inheritdoc />
        public virtual IContentReader GetContentReader(string path)
        {
            this.Initialize();

            return new ResourceContentReaderWriter(Business.GetReaderWriter(path));
        }

        /// <inheritdoc />
        public virtual object GetContentReaderDynamicParameters(string path)
        {
            return null;
        }

        /// <inheritdoc />
        public virtual IContentWriter GetContentWriter(string path)
        {
            this.Initialize();

            return new ResourceContentReaderWriter(Business.GetReaderWriter(path));
        }

        /// <inheritdoc />
        public virtual object GetContentWriterDynamicParameters(string path)
        {
            return null;
        }

        /// <inheritdoc />
        public virtual void ClearContent(string path)
        {
            return;
        }

        /// <inheritdoc />
        public virtual object ClearContentDynamicParameters(string path)
        {
            return null;
        }

        #endregion

        /// <inheritdoc />
        protected override string[] ExpandPath(string path)
        {
            this.Initialize();

            return Business.ExpandPath(path, PSDriveInfo.CurrentLocation);
        }

        /// <inheritdoc />
        protected override bool IsValidPath(string path)
        {
            return true;
        }

        /// <inheritdoc />
        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            this.Initialize();

            var drive = new PSDriveInfo(this.DriveName, this.ProviderInfo, this.DriveRootPath, this.DriveDescription, null);
            var drives = new Collection<PSDriveInfo>() { drive };

            return drives;
        }

        /// <inheritdoc />
        protected override string NormalizeRelativePath(string path, string basePath)
        {
            this.Initialize();

            return Business.NormalizeRelativePath(path, basePath);
        }

        /// <inheritdoc />
        protected override bool ItemExists(string path)
        {
            this.Initialize();

            return this.Business.Exists(path);
        }

        /// <inheritdoc />
        protected override bool IsItemContainer(string path)
        {
            this.Initialize();

            return this.Business.IsContainer(path);
        }

        /// <inheritdoc />
        protected override void GetChildItems(string path, bool recurse)
        {
            this.Initialize();

            var resources = this.Business.GetAllResources(path, recurse);

            foreach (var child in recurse ? resources.Flatten(i => i.Children) : resources)
            {
                this.WriteItemObject(child, recurse);
            }
        }

        /// <inheritdoc />
        protected override void GetItem(string path)
        {
            this.Initialize();

            this.WriteItemObject(this.Business.GetResource(path), false);
        }

        /// <inheritdoc />
        protected override bool HasChildItems(string path)
        {
            this.Initialize();

            var resource = this.Business.GetResource(path, false);
            
            return (resource?.Children?.Any()).GetValueOrDefault(false);
        }

        /// <inheritdoc />
        protected override void NewItem(string name, string itemTypeName, object newItemValue)
        {
            this.Initialize();

            this.Business.CreateResource(name, itemTypeName, newItemValue);
        }

        /// <inheritdoc />
        protected override void RemoveItem(string path, bool recurse)
        {
            this.Initialize();

            var isDeleted = this.Business.Delete(path, recurse);

            if (!isDeleted)
            {
                throw new Exception($"Cannot delete item at \"{path}\".");
            }
        }

        /// <inheritdoc />
        protected override void CopyItem(string path, string copyPath, bool recurse)
        {
            this.Initialize();

            Business.CopyItem(path, copyPath, recurse);
        }

        /// <inheritdoc />
        protected override object CopyItemDynamicParameters(string path, string destination, bool recurse)
        {
            return new PSNotSupportedException();
        }

        /// <summary>
        /// Writes the <see cref="IResourceInfo"/> to the output
        /// </summary>
        /// <param name="resource">The <see cref="IResourceInfo"/> to be written to the output</param>
        /// <param name="recurse">When set to true, will recurse through all child containers of <paramref name="resource"/></param> and write them to the output.
        protected virtual void WriteItemObject(IResourceInfo resource, bool recurse)
        {
            if (resource == null)
                return;

            base.WriteItemObject(resource, resource.Path, resource.IsContainer);

            if (recurse && (resource.Children?.Any()).GetValueOrDefault(false))
            {
                foreach (var childResource in resource.Children.Flatten(i => i.Children))
                {
                    base.WriteItemObject(new IResourceInfo[] { resource }, resource.Path, resource.IsContainer);
                }
            }
        }

        /// <summary>
        /// Performs the necessary initialization for this provider.
        /// </summary>
        protected virtual void Initialize()
        {
            Bootstrapper.Instance.Initialize(this);

            this.CmsApplicationService.Initialize(true, this.WriteVerbose, this.WriteDebug);
            this.Business.Initialize();
        }
    }
}

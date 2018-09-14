using PoshKentico.Business;
using PoshKentico.Core.Services.General;
using PoshKentico.Core.Services.Resource;
using PoshKentico.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Text.RegularExpressions;

namespace PoshKentico.CmdletProviders
{
    public abstract class CmdletProvider<TBusinessProvider> : NavigationCmdletProvider, IPropertyCmdletProvider, IContentCmdletProvider
        where TBusinessProvider : CmdletProviderBusinessBase
    {
        protected abstract string ProviderName { get; }
        protected abstract string DriveName { get; }
        protected abstract string DriveRootPath { get; }
        protected abstract string DriveDescription { get; }

        [Import]
        public ICmsApplicationService CmsApplicationService { get; set; }

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

        public virtual void GetProperty(string path, Collection<string> providerSpecificPickList)
        {
            this.Initialize();

            var outputObject = this.Business.GetProperty(path, providerSpecificPickList)?.ToPSObject();

            if (outputObject != null)
            {
                this.WritePropertyObject(outputObject, path);
            }
        }

        public virtual object GetPropertyDynamicParameters(string path, Collection<string> providerSpecificPickList)
        {
            return null;
        }

        public virtual void SetProperty(string path, PSObject propertyValue)
        {
            this.Business.SetProperty(path, propertyValue.ToDictionary());
        }

        public virtual object SetPropertyDynamicParameters(string path, PSObject propertyValue)
        {
            return null;
        }

        public virtual void ClearProperty(string path, Collection<string> propertyToClear)
        {
            throw new PSNotSupportedException();
        }

        public virtual object ClearPropertyDynamicParameters(string path, Collection<string> propertyToClear)
        {
            throw new PSNotSupportedException();
        }

        #endregion

        #region IContentCmdletProvider Implementation

        public virtual IContentReader GetContentReader(string path)
        {
            this.Initialize();

            return new ResourceContentReaderWriter(Business.GetReaderWriter(path));
        }

        public virtual object GetContentReaderDynamicParameters(string path)
        {
            return null;
        }

        public virtual IContentWriter GetContentWriter(string path)
        {
            this.Initialize();

            return new ResourceContentReaderWriter(Business.GetReaderWriter(path));
        }

        public virtual object GetContentWriterDynamicParameters(string path)
        {
            throw new PSNotSupportedException();
        }

        public virtual void ClearContent(string path)
        {
            throw new PSNotSupportedException();
        }

        public virtual object ClearContentDynamicParameters(string path)
        {
            throw new PSNotSupportedException();
        }

        #endregion

        protected override string[] ExpandPath(string path)
        {
            this.Initialize();

            return Business.ExpandPath(path, PSDriveInfo.CurrentLocation);
        }

        protected override bool IsValidPath(string path)
        {
            return true;
        }

        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            this.Initialize();

            var drive = new PSDriveInfo(this.DriveName, this.ProviderInfo, this.DriveRootPath, this.DriveDescription, null);
            var drives = new Collection<PSDriveInfo>() { drive };

            return drives;
        }

        protected override string NormalizeRelativePath(string path, string basePath)
        {
            this.Initialize();

            return Business.NormalizeRelativePath(path, basePath);
        }

        protected override bool ItemExists(string path)
        {
            this.Initialize();

            return this.Business.Exists(path);
        }

        protected override bool IsItemContainer(string path)
        {
            this.Initialize();

            return this.Business.IsContainer(path);
        }

        protected override void GetChildItems(string path, bool recurse)
        {
            this.Initialize();

            var resources = this.Business.GetAllResources(path, recurse);

            foreach (var child in recurse ? resources.Flatten(i => i.Children) : resources)
            {
                this.WriteItemObject(child, recurse);
            }
        }

        protected override void GetItem(string path)
        {
            this.Initialize();

            this.WriteItemObject(this.Business.GetResource(path), false);
        }

        protected override bool HasChildItems(string path)
        {
            this.Initialize();

            var resource = this.Business.GetResource(path, false);
            
            return (resource?.Children?.Any()).GetValueOrDefault(false);
        }

        protected override void NewItem(string name, string itemTypeName, object newItemValue)
        {
            this.Initialize();

            this.Business.CreateResource(name, itemTypeName, newItemValue);
        }

        protected override void RemoveItem(string path, bool recurse)
        {
            this.Initialize();

            var isDeleted = this.Business.Delete(path, recurse);

            if (!isDeleted)
            {
                throw new Exception($"Cannot delete item at \"{path}\".");
            }
        }

        protected override void CopyItem(string path, string copyPath, bool recurse)
        {
            this.Initialize();

            Business.CopyItem(path, copyPath, recurse);
        }

        protected override object CopyItemDynamicParameters(string path, string destination, bool recurse)
        {
            return new PSNotSupportedException();
        }

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

        protected virtual void Initialize()
        {
            MefHost.Initialize();
            MefHost.Container.ComposeParts(this);

            this.CmsApplicationService.Initialize(true, this.WriteVerbose, this.WriteDebug);
            this.Business.Initialize();
        }
    }
}

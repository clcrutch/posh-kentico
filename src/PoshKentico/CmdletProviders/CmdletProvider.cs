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
    public abstract class CmdletProvider<T> : NavigationCmdletProvider, IPropertyCmdletProvider
        where T : CmdletProviderBusinessBase
    {
        protected abstract string ProviderName { get; }
        protected abstract string DriveName { get; }

        [Import]
        public ICmsApplicationService CmsApplicationService { get; set; }

        [Import]
        public T ResourceBusiness { get; set; }

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

        /// <inheritdoc/>
        public virtual void GetProperty(string path, Collection<string> providerSpecificPickList)
        {
            this.Initialize();

            var resource = this.ResourceBusiness.FindPath(path);
            var outputObject = this.ResourceBusiness.GetProperty(resource, providerSpecificPickList)?.ToPSObject();

            if (outputObject != null)
            {
                this.WritePropertyObject(outputObject, path);
            }
        }

        /// <inheritdoc/>
        public virtual object GetPropertyDynamicParameters(string path, Collection<string> providerSpecificPickList)
        {
            return null;
        }

        /// <inheritdoc/>
        public virtual void SetProperty(string path, PSObject propertyValue)
        {
            var resource = this.ResourceBusiness.FindPath(path);
            this.ResourceBusiness.SetProperty(resource, propertyValue.ToDictionary());
        }

        /// <inheritdoc/>
        public virtual object SetPropertyDynamicParameters(string path, PSObject propertyValue)
        {
            return null;
        }

        /// <inheritdoc/>
        public virtual void ClearProperty(string path, Collection<string> propertyToClear)
        {
            throw new PSNotSupportedException();
        }

        /// <inheritdoc/>
        public virtual object ClearPropertyDynamicParameters(string path, Collection<string> propertyToClear)
        {
            throw new PSNotSupportedException();
        }

        #endregion

        protected override string[] ExpandPath(string path)
        {
            this.Initialize();

            var regex = new Regex($"^{this.PSDriveInfo.CurrentLocation.Replace("\\", "\\\\")}\\\\", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            return (from i in this.ResourceBusiness.GetResources(path)
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
            this.Initialize();

            var drive = new PSDriveInfo(this.DriveName, this.ProviderInfo, string.Empty, string.Empty, null);
            var drives = new Collection<PSDriveInfo>() { drive };

            return drives;
        }

        protected override string NormalizeRelativePath(string path, string basePath)
        {
            return path.Replace('/', '\\');
        }

        protected override void RemoveItem(string path, bool recurse)
        {
            this.Initialize();
            var resource = this.ResourceBusiness.FindPath(path);
            var isDeleted = this.ResourceBusiness.Delete(resource, recurse);

            if (!isDeleted)
            {
                throw new Exception($"Cannot delete item at \"{path}\".");
            }
        }


        protected override bool ItemExists(string path)
        {
            return this.ResourceBusiness.Exists(path);
        }

        protected override bool IsItemContainer(string path)
        {
            this.Initialize();

            var resource = this.ResourceBusiness.FindPath(path);
            return (resource?.IsContainer).GetValueOrDefault(false);
        }

        protected override void GetChildItems(string path, bool recurse)
        {
            this.Initialize();

            var fileSresourystemItem = this.ResourceBusiness.GetResources(path, recurse);
        }

        protected override void GetItem(string path)
        {
            this.Initialize();

            this.WriteItemObject(this.ResourceBusiness.FindPath(path), false);
        }

        protected override bool HasChildItems(string path)
        {
            this.Initialize();
            var resource = this.ResourceBusiness.FindPath(path);
            
            return (resource?.Children?.Any()).GetValueOrDefault(false);
        }

        protected virtual void WriteItemObject(IResource resource, bool recurse)
        {
            this.WriteItemObject(new IResource[] { resource }, resource.Path, resource.IsContainer);
        }

        protected virtual void WriteItemObject(IEnumerable<IResource> resources, bool recurse)
        {
            foreach (var resource in resources)
            {
                base.WriteItemObject(resource, resource.Path, resource.IsContainer);
            }
        }

        protected override void NewItem(string name, string itemTypeName, object newItemValue)
        {
            this.ResourceBusiness.NewItem(name, itemTypeName, newItemValue);
        }

        protected virtual void Initialize()
        {
            MefHost.Initialize();
            MefHost.Container.ComposeParts(this);

            this.CmsApplicationService.Initialize(true, this.WriteVerbose, this.WriteDebug);
            this.ResourceBusiness.Initialize();
        }
    }
}

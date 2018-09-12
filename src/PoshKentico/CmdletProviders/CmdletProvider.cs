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
    public abstract class CmdletProvider<T> : NavigationCmdletProvider, IPropertyCmdletProvider, IContentCmdletProvider
        where T : CmdletProviderBusinessBase
    {
        protected abstract string ProviderName { get; }
        protected abstract string DriveName { get; }
        protected abstract string DriveRootPath { get; }
        protected abstract string DriveDescription { get; }

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

        public virtual void GetProperty(string path, Collection<string> providerSpecificPickList)
        {
            this.Initialize();

            var outputObject = this.ResourceBusiness.GetProperty(path, providerSpecificPickList)?.ToPSObject();

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
            this.ResourceBusiness.SetProperty(path, propertyValue.ToDictionary());
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
            throw new NotImplementedException();
        }

        public virtual object GetContentReaderDynamicParameters(string path)
        {
            throw new NotImplementedException();
        }

        public virtual IContentWriter GetContentWriter(string path)
        {
            throw new NotImplementedException();
        }

        public virtual object GetContentWriterDynamicParameters(string path)
        {
            throw new NotImplementedException();
        }

        public virtual void ClearContent(string path)
        {
            throw new NotImplementedException();
        }

        public virtual object ClearContentDynamicParameters(string path)
        {
            throw new NotImplementedException();
        } 

        #endregion

        protected override string[] ExpandPath(string path)
        {
            this.Initialize();

            var regex = new Regex($"^{this.PSDriveInfo.CurrentLocation.Replace("\\", "\\\\")}\\\\", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            return (from i in this.ResourceBusiness.GetAll(this.PSDriveInfo.CurrentLocation, true).Flatten(i => i.Children)
                    select regex.Replace(i.Path, string.Empty)).ToArray();
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
            return path.Replace('/', '\\');
        }

        protected override bool ItemExists(string path)
        {
            this.Initialize();

            return this.ResourceBusiness.Exists(path);
        }

        protected override bool IsItemContainer(string path)
        {
            this.Initialize();

            return this.ResourceBusiness.IsContainer(path);
        }

        protected override void GetChildItems(string path, bool recurse)
        {
            this.Initialize();

            var resources = this.ResourceBusiness.GetAll(path, recurse);

            foreach (var child in recurse ? resources.Flatten(i => i.Children) : resources)
            {
                this.WriteItemObject(child, recurse);
            }
        }

        protected override void GetItem(string path)
        {
            this.Initialize();

            this.WriteItemObject(this.ResourceBusiness.Get(path), false);
        }

        protected override bool HasChildItems(string path)
        {
            this.Initialize();

            var resource = this.ResourceBusiness.Get(path, true);
            
            return (resource?.Children?.Any()).GetValueOrDefault(false);
        }

        protected override void NewItem(string name, string itemTypeName, object newItemValue)
        {
            this.Initialize();

            this.ResourceBusiness.Create(name, itemTypeName, newItemValue);
        }

        protected override void RemoveItem(string path, bool recurse)
        {
            this.Initialize();

            var isDeleted = this.ResourceBusiness.Delete(path, recurse);

            if (!isDeleted)
            {
                throw new Exception($"Cannot delete item at \"{path}\".");
            }
        }

        protected virtual void WriteItemObject(IResource resource, bool recurse)
        {
            if (resource == null)
                return;

            base.WriteItemObject(resource, resource.Path, resource.IsContainer);

            if (recurse && (resource.Children?.Any()).GetValueOrDefault(false))
            {
                foreach (var childResource in resource.Children.Flatten(i => i.Children))
                {
                    base.WriteItemObject(new IResource[] { resource }, resource.Path, resource.IsContainer);
                }
            }
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

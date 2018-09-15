using ImpromptuInterface;
using PoshKentico.Core.Extensions;
using PoshKentico.Core.Services.Resource;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PoshKentico.Business
{
    public abstract class CmdletProviderBusinessBase : CmdletBusinessBase
    {
        public virtual IResourceService ResourceService { get; set; }
        public virtual IResourceReaderWriterService ReaderWriterService { get; set; }

        public virtual bool Exists(string path)
        {
            return this.ResourceService.Exists(path);
        }

        public virtual void CreateResource(string name, string itemTypeName, object newItemValue)
        {
            if (this.ResourceService.IsContainer(name))
            {
                ResourceService.CreateContainer(name);
            }
            else
            {
                ResourceService.CreateItem(name, newItemValue as string);
            }
        }

        public virtual bool Delete(string path, bool recurse = false)
        {
            try
            {
                if (this.ResourceService.Exists(path))
                {
                    if (this.ResourceService.IsContainer(path))
                        this.ResourceService.DeleteContainer(path, recurse);
                    else
                        this.ResourceService.DeleteItem(path);
                }
            }
            catch (Exception ex)
            {
                // log exception?
            }

            return this.ResourceService.Exists(path);
        }

        public virtual IResourceInfo GetResource(string path, bool recurse = false)
        {
            if (!this.ResourceService.IsContainer(path))
                return this.ResourceService.GetItem(path);

            if (recurse)
            {
                return this.ResourceService.GetContainer(path, recurse);
            }

            return this.ResourceService.GetContainer(path, false); ;
        }

        public virtual IEnumerable<IResourceInfo> GetAllResources(string path, bool recurse = false)
        {
            return this.ResourceService.GetAll(path, recurse);
        }

        public virtual Dictionary<string, object> GetProperty(string path, Collection<string> providerSpecificPickList)
        {
            this.Initialize();

            var resource = this.ResourceService.IsContainer(path) ? this.ResourceService.GetContainer(path, false) : this.ResourceService.GetItem(path);
            var properties = new Dictionary<string, object>
            {
                { "IsContainer", resource.IsContainer },
                { "ContainerPath", resource.ContainerPath },
                { "Name", resource.Name },
                { "Path", resource.Path },
                { "CreationTime", resource.CreationTime },
                { "LastWriteTime", resource.LastWriteTime },
                { "ResourceType", resource.ResourceType },
                { "TotalContainers", resource.Children.Where(i => i.IsContainer).Count() },
                { "TotalItems", resource.Children.Where(i => !i.IsContainer).Count() },
                { "TotalResources", resource.Children.Count() },
            };

            this.PurgeUnwantedProperties(providerSpecificPickList, properties);

            return properties;
        }

        public virtual void PurgeUnwantedProperties(Collection<string> providerSpecificPickList, Dictionary<string, object> properties)
        {
            if (providerSpecificPickList.Count > 0)
            {
                var itemsToRemove = properties.Keys.Except(providerSpecificPickList).ToArray();

                foreach (var itemToRemove in itemsToRemove)
                {
                    properties.Remove(itemToRemove.ToLowerInvariant());
                }
            }
        }

        public IResourceReaderWriterService GetReaderWriter(string path)
        {
            var instance = (IResourceReaderWriterService)Activator.CreateInstance(ReaderWriterService.GetType());
            instance.Initialize(this.ResourceService, path);
            return instance;
        }

        public virtual void SetProperty(string path, Dictionary<string, object> propertyValue)
        {
            if (this.ResourceService.IsContainer(path))
                return;

            if (propertyValue.ContainsKey("content"))
            {
                this.ResourceService.CreateItem(path, propertyValue["content"] as string);
            }
        }

        public virtual string[] ExpandPath(string path, string currentLocation)
        {
            var resource = GetResource(currentLocation, false);

            if (resource.Children == null)
            {
                return null;
            }

            var normalizedPath = NormalizeRelativePath(path, null);
            var resourceName = ResourceService.GetName(normalizedPath);
            var regexString = Regex.Escape(resourceName).Replace("\\*", ".*");
            var regex = new Regex("^" + regexString + "$", RegexOptions.IgnoreCase);

            var matchingItems = (from item in resource.Children
                                 where regex.IsMatch(item.Name)
                                 select currentLocation + "/" + item.Name).ToArray();

            return matchingItems.Any() ? matchingItems : null;
        }

        public virtual string NormalizeRelativePath(string path, string basePath)
        {
            return path.Replace('/', '\\');
        }

        public virtual bool IsContainer(string path)
        {
            return this.ResourceService.IsContainer(path);
        }

        public virtual void CopyItem(string sourcePath, string destinationPath, bool recurse)
        {
            var sourceResource = GetResource(sourcePath, recurse);
            var normalizedDestinationPath = NormalizeDestinationPath(sourcePath, destinationPath);

            if (sourceResource.IsContainer)
            {
                CopyContainer(sourceResource, normalizedDestinationPath, recurse);
                return;
            }

            CopyItem(sourceResource, normalizedDestinationPath);
        }

        private void CopyItem(IResourceInfo sourceResource, string destinationPath)
        {
            var sourceReader = GetReaderWriter(sourceResource.Path);
            var destinationWriter = GetReaderWriter(destinationPath);

            destinationWriter.Write(sourceReader.Read());
        }

        private void CopyContainer(IResourceInfo sourceResource, string destinationBasePath, bool recurse)
        {
            var children = sourceResource.Children.Flatten(i => i.Children)
                            .Select(i => new
                            {
                                Resource = i,
                                NewPath = ResourceService.JoinPath(destinationBasePath, i.Path.Replace(sourceResource.Path, string.Empty))
                            })
                            .GroupBy(i => i.Resource.ResourceType)
                            .ToDictionary(i => i.Key, i => i.AsEnumerable());

            foreach (var item in children[ResourceType.Container].OrderBy(i => i.NewPath))
            {
                ResourceService.CreateContainer(item.NewPath);
            }

            foreach (var item in children[ResourceType.Item].OrderBy(i => i.NewPath))
            {
                ResourceService.CopyResourceItem(item.Resource.Path, item.NewPath);
            }
        }

        private string NormalizeDestinationPath(string sourcePath, string destinationPath)
        {
            string normalizedDestinationPath = string.Empty;

            var isRelativePath = ResourceService.IsAbsolutePath(destinationPath);

            if (!isRelativePath)
            {
                normalizedDestinationPath = NormalizeRelativePath(destinationPath, null);
                return ResourceService.JoinPath(sourcePath, normalizedDestinationPath);
            }

            return destinationPath;
        }
    }
}

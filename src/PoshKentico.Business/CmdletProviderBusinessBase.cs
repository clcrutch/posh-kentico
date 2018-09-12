using ImpromptuInterface;
using PoshKentico.Core.Services.Resource;
using System;
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
    public class CmdletProviderBusinessBase : CmdletBusinessBase
    {
        public virtual IResourceService ResourceService { get; set; }

        public virtual bool Exists(string path)
        {
            return this.ResourceService.Exists(path);
        }

        public virtual void Create(string name, string itemTypeName, object newItemValue)
        {
            IResource resource = resource = new
            {
                Children = new IResource[] { },
                ResourceType = ResourceType.File,
                IsContainer = false,
                Path = name,
                Content = newItemValue as string,
                CreationTime = DateTime.Now,
            }.ActLike<IResource>();

            if (this.ResourceService.IsContainer(name))
            {
                resource.IsContainer = true;
                resource.ResourceType = ResourceType.Directory;
                this.ResourceService.CreateContainer(resource);
            }
            else
            {
                this.ResourceService.CreateItem(resource);
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

        public virtual IResource Get(string path, bool recurse = false)
        {
            if (this.ResourceService.IsContainer(path))
                return this.ResourceService.GetContainer(path, recurse);
            else
                return this.ResourceService.GetItem(path);
        }

        public virtual IEnumerable<IResource> GetAll(string path, bool recurse = false)
        {
            return this.ResourceService.GetAll(path, recurse);
        }

        public virtual Dictionary<string, object> GetProperty(string path, Collection<string> providerSpecificPickList)
        {
            this.Initialize();

            var resource = this.ResourceService.IsContainer(path) ? this.ResourceService.GetContainer(path, false) : this.ResourceService.GetItem(path);
            var properties = new Dictionary<string, object>
            {
                { "resourcename", resource.Name },
                { "resourcepath", resource.Path },
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

        public virtual void SetProperty(string path, Dictionary<string, object> propertyValue)
        {
            if (this.ResourceService.IsContainer(path))
                return;

            var resource = this.ResourceService.GetItems(path)?.FirstOrDefault();

            if (propertyValue.ContainsKey("content"))
            {
                resource.Content = propertyValue["content"] as string;
                this.ResourceService.CreateItem(resource);
            }
        }

        public bool IsContainer(string path)
        {
            return this.ResourceService.IsContainer(path);
        }
    }
}

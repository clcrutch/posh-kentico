using PoshKentico.Core.Services.Resource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PoshKentico.Business
{
    public class CmdletProviderBusinessBase : CmdletBusinessBase
    {
        public virtual IResourceService ResourceService { get; set; }

        public virtual Dictionary<string, object> GetProperty(IResource resource, Collection<string> providerSpecificPickList)
        {
            var properties = new Dictionary<string, object>
            {
                { "resourcename", resource.Name },
                { "resourcepath", resource.Path },
            };

            this.PurgeUnwantedProperties(providerSpecificPickList, properties);

            return properties;
        }

        public virtual IResource FindPath(string path)
        {
            return this.ResourceService.Get(path);
        }

        public virtual IEnumerable<IResource> GetResources(string path, bool recurse = false)
        {
            return this.ResourceService.Get(path, recurse);
        }

        public virtual bool Exists(string path)
        {
            return this.ResourceService.Exists(path);
        }
        public virtual IResource[] GetResourcesFromRegEx(string path, Regex regex)
        {
            var children = this.ResourceService.Get(path, false);

            if (children == null)
            {
                return null;
            }

            return (from c in children
                    where regex.IsMatch(c.Name)
                    select c).ToArray();
        }

        public virtual void NewItem(string name, string itemTypeName, object newItemValue)
        {
            this.ResourceService.Create(null, (ResourceType)Enum.Parse(typeof(ResourceType), itemTypeName, true), name, newItemValue as string);
        }

        public virtual void SetProperty(IResource resource, Dictionary<string, object> propertyValue)
        {
            bool updatedValue = false;
            if (propertyValue.ContainsKey("content"))
            {
                resource.Content = propertyValue["content"] as string;
                updatedValue = true;
            }

            if (updatedValue)
            {
                this.ResourceService.Create(resource);
            }
        }
        public virtual bool Delete(IResource resource, bool recurse = false)
        {
            return this.ResourceService.Delete(resource, recurse);
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
        public virtual void Initialize()
        {
            base.Initialize();
        }
    }
}

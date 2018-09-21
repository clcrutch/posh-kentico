// <copyright file="CmdletProviderBusinessBase.cs" company="Chris Crutchfield">
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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ImpromptuInterface;
using PoshKentico.Core.Extensions;
using PoshKentico.Core.Services.Resource;

namespace PoshKentico.Business
{
    /// <summary>
    /// Base class for cmdlet business providers
    /// </summary>
    public abstract class CmdletProviderBusinessBase : CmdletBusinessBase
    {
        /// <summary>
        /// Gets or sets the resource service
        /// </summary>
        public virtual IResourceService ResourceService { get; set; }

        /// <summary>
        /// Gets or sets the resource resource reader/writer
        /// </summary>
        public virtual IResourceReaderWriterService ReaderWriterService { get; set; }

        /// <summary>
        /// Determines if the resource exists
        /// </summary>
        /// <param name="path">The absolute path of the resource</param>
        /// <returns>If the resource exists</returns>
        public virtual bool Exists(string path)
        {
            return this.ResourceService.Exists(path);
        }

        /// <summary>
        /// Creates the resource
        /// </summary>
        /// <param name="path">The resource path</param>
        /// <param name="itemTypeName">The type of resource</param>
        /// <param name="newItemValue">New resource property values</param>
        public virtual void CreateResource(string path, string itemTypeName, object newItemValue)
        {
            if (this.ResourceService.IsContainer(path))
            {
                this.ResourceService.CreateContainer(path);
            }
            else
            {
                this.ResourceService.CreateItem(path, newItemValue as string);
            }
        }

        /// <summary>
        /// Deletes a resource
        /// </summary>
        /// <param name="path">The full path of the resource to be deleted</param>
        /// <param name="recurse">If true, will delete all resources in a container</param>
        /// <returns>If resource was deleted</returns>
        public virtual bool Delete(string path, bool recurse = false)
        {
            try
            {
                if (this.ResourceService.Exists(path))
                {
                    if (this.ResourceService.IsContainer(path))
                    {
                        this.ResourceService.DeleteContainer(path, recurse);
                    }
                    else
                    {
                        this.ResourceService.DeleteItem(path);
                    }
                }
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                // log exception?
            }

            return this.ResourceService.Exists(path);
        }

        /// <summary>
        /// Gets the resource
        /// </summary>
        /// <param name="path">The full path of the resource</param>
        /// <param name="recurse">If true, retrieves all containers. If false, will only retrieve immediate containers</param>
        /// <returns>Returns the resource item <see cref="IResourceInfo"/> </returns>
        public virtual IResourceInfo GetResource(string path, bool recurse = false)
        {
            if (!this.ResourceService.IsContainer(path))
            {
                return this.ResourceService.GetItem(path);
            }

            if (recurse)
            {
                return this.ResourceService.GetContainer(path, recurse);
            }

            return this.ResourceService.GetContainer(path, false);
        }

        /// <summary>
        /// Retrieves resource items and containers
        /// </summary>
        /// <param name="path">The path to the resource</param>
        /// <param name="recurse">If true and resource is a container, will retrieve all of its children</param>
        /// <returns>Returns a list of <see cref="IResourceInfo"/></returns>
        public virtual IEnumerable<IResourceInfo> GetAllResources(string path, bool recurse = false)
        {
            return this.ResourceService.GetAll(path, recurse);
        }

        /// <summary>
        /// Gets the specified properties from the resource.
        /// </summary>
        /// <param name="path">The path to the resource</param>
        /// <param name="providerSpecificPickList">List of properties to retrieve.</param>
        /// <returns>A dictionary containing the requested properties.</returns>
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

        /// <summary>
        /// Removes properties not specified from the provided dictionary.
        /// </summary>
        /// <param name = "providerSpecificPickList" > List of properties to keep in the dictionary.</param>
        /// <param name = "properties" > Dictionary which contains properties and values.</param>
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

        /// <summary>
        /// Retrieves the <see cref="IResourceReaderWriterService"/>
        /// </summary>
        /// <param name="path">The path to the resource</param>
        /// <returns>The service used to read and write to a resource</returns>
        public IResourceReaderWriterService GetReaderWriter(string path)
        {
            var instance = (IResourceReaderWriterService)Activator.CreateInstance(this.ReaderWriterService.GetType());
            instance.Initialize(this.ResourceService, path);
            return instance;
        }

        /// <summary>
        /// Sets the specified properties on the resource
        /// </summary>
        /// <param name="path">The path to the resource</param>
        /// <param name="propertyValue">A dictionary containing the properties and their respective values.</param>
        public virtual void SetProperty(string path, Dictionary<string, object> propertyValue)
        {
            if (this.ResourceService.IsContainer(path))
            {
                return;
            }

            if (propertyValue.ContainsKey("content"))
            {
                this.ResourceService.CreateItem(path, propertyValue["content"] as string);
            }
        }

        /// <summary>
        /// Resolves the wildcard characters in a path, and displays the path contents.
        /// </summary>
        /// <param name="path">The path with wildcard characters</param>
        /// <param name="currentLocation">The current directory</param>
        /// <returns>List of resource paths</returns>
        public virtual string[] ExpandPath(string path, string currentLocation)
        {
            var resource = this.GetResource(currentLocation, false);

            if (resource.Children == null)
            {
                return null;
            }

            var normalizedPath = this.NormalizeRelativePath(path, null);
            var resourceName = this.ResourceService.GetName(normalizedPath);
            var regexString = Regex.Escape(resourceName).Replace("\\*", ".*");
            var regex = new Regex("^" + regexString + "$", RegexOptions.IgnoreCase);

            var matchingItems = (from item in resource.Children
                                 where regex.IsMatch(item.Name)
                                 select currentLocation + "/" + item.Name).ToArray();

            return matchingItems.Any() ? matchingItems : null;
        }

        /// <summary>
        /// Normalizes the path that was passed in and returns the normalized path as a relative path to the basePath that was passed.
        /// </summary>
        /// <param name="path">The full path of the resource</param>
        /// <param name="basePath">The path that the return value should be relative to</param>
        /// <returns>A normalized path that is relative to the basePath that was passed</returns>
        public virtual string NormalizeRelativePath(string path, string basePath)
        {
            return path.Replace('/', '\\');
        }

        /// <summary>
        /// Is the resource a container
        /// </summary>
        /// <param name="path">Full path of the rosource</param>
        /// <returns>If the requested path is a container</returns>
        public virtual bool IsContainer(string path)
        {
            return this.ResourceService.IsContainer(path);
        }

        /// <summary>
        /// Copies either a resource or a resource item
        /// </summary>
        /// <param name="sourcePath">The full path to the resource being copied</param>
        /// <param name="destinationPath">The destination where the resource will be copied to</param>
        /// <param name="recurse">If true, will copy all of a containers child resources</param>
        public virtual void CopyItem(string sourcePath, string destinationPath, bool recurse)
        {
            var sourceResource = this.GetResource(sourcePath, recurse);
            var normalizedDestinationPath = this.NormalizeDestinationPath(sourcePath, destinationPath);

            if (sourceResource.IsContainer)
            {
                this.CopyContainer(sourceResource, normalizedDestinationPath, recurse);
                return;
            }

            this.CopyItem(sourceResource, normalizedDestinationPath);
        }

        /// <summary>
        /// Copies a resource item
        /// </summary>
        /// <param name="sourceResource">The full path to the resource being copied</param>
        /// <param name="destinationPath">The destination where the resource item will be copied to</param>
        private void CopyItem(IResourceInfo sourceResource, string destinationPath)
        {
            var sourceReader = this.GetReaderWriter(sourceResource.Path);
            var destinationWriter = this.GetReaderWriter(destinationPath);

            destinationWriter.Write(sourceReader.Read());
        }

        /// <summary>
        /// Copies a resource container
        /// </summary>
        /// <param name="sourceResource">The full path to the resource being copied</param>
        /// <param name="destinationBasePath">The destination where the resource item will be copied to</param>
        /// <param name="recurse">If true, will copy all of a containers child resources</param>
        private void CopyContainer(IResourceInfo sourceResource, string destinationBasePath, bool recurse)
        {
            var children = sourceResource.Children.Flatten(i => i.Children)
                            .Select(i => new
                            {
                                Resource = i,
                                NewPath = this.ResourceService.JoinPath(destinationBasePath, i.Path.Replace(sourceResource.Path, string.Empty)),
                            })
                            .GroupBy(i => i.Resource.ResourceType)
                            .ToDictionary(i => i.Key, i => i.AsEnumerable());

            foreach (var item in children[ResourceType.Container].OrderBy(i => i.NewPath))
            {
                this.ResourceService.CreateContainer(item.NewPath);
            }

            foreach (var item in children[ResourceType.Item].OrderBy(i => i.NewPath))
            {
                this.ResourceService.CopyResourceItem(item.Resource.Path, item.NewPath);
            }
        }

        /// <summary>
        /// Normalizes the path that was passed in and returns the normalized path as a relative path to the basePath that was passed.
        /// </summary>
        /// <param name="sourcePath">The full path of the resource</param>
        /// <param name="destinationPath">The path that the return value should be relative to</param>
        /// <returns>A normalized path that is relative to the basePath that was passed</returns>
        private string NormalizeDestinationPath(string sourcePath, string destinationPath)
        {
            string normalizedDestinationPath = string.Empty;

            var isRelativePath = this.ResourceService.IsAbsolutePath(destinationPath);

            if (!isRelativePath)
            {
                normalizedDestinationPath = this.NormalizeRelativePath(destinationPath, null);
                return this.ResourceService.JoinPath(sourcePath, normalizedDestinationPath);
            }

            return destinationPath;
        }
    }
}

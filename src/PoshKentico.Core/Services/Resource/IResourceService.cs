// <copyright file="IResourceService.cs" company="Chris Crutchfield">
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
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.Resource
{
    /// <summary>
    /// Service used to access CMS resources.
    /// </summary>
    public interface IResourceService
    {
        /// <summary>
        /// Determines if this resource is a container.
        /// </summary>
        /// <param name="path">The absolute path of the resource.</param>
        /// <returns>If resource is a container.</returns>
        bool IsContainer(string path);

        /// <summary>
        /// Determines if the resource exists.
        /// </summary>
        /// <param name="path">The absolute path of the resource.</param>
        /// <returns>If the resource exists.</returns>
        bool Exists(string path);

        /// <summary>
        /// Determines if the path is absolute.
        /// </summary>
        /// <param name="path">The relative or absolulte path of the resource.</param>
        /// <returns>If path is absolute.</returns>
        bool IsAbsolutePath(string path);

        /// <summary>
        /// Gets the name of the resource.
        /// </summary>
        /// <param name="path">The full path of the resource.</param>
        /// <returns>Resource name.</returns>
        string GetName(string path);

        /// <summary>
        /// Joins two paths together.
        /// </summary>
        /// <param name="path">The former path of a resource.</param>
        /// <param name="partialPath">The latter path of a resource.</param>
        /// <returns>Returns the joint path.</returns>
        string JoinPath(string path, string partialPath);

        /// <summary>
        /// Clears attributes assigned to a resource. If given resource were a file, it would try to remove attributes like Hidden and ReadOnly..
        /// </summary>
        /// <param name="path">The full path of the resource.</param>
        void ClearAttributes(string path);

        /// <summary>
        /// Deletes the container.
        /// </summary>
        /// <param name="path">The full path of the container.</param>
        /// <param name="recurse">If true, all children are deleted.</param>
        void DeleteContainer(string path, bool recurse);

        /// <summary>
        /// Deletes the resource item.
        /// </summary>
        /// <param name="path">The full path of the resource item.</param>
        void DeleteItem(string path);

        /// <summary>
        /// Gets the <see cref="IResourceInfo"/> items.
        /// </summary>
        /// <param name="path">The full path of the container.</param>
        /// <returns>Retrieves a list of <see cref="IResourceInfo"/> items.</returns>
        IEnumerable<IResourceInfo> GetItems(string path);

        /// <summary>
        /// Gets a single <see cref="IResourceInfo"/>.
        /// </summary>
        /// <param name="path">The full path of the resource item.</param>
        /// <returns>Returns the resource item <see cref="IResourceInfo"/>. </returns>
        IResourceInfo GetItem(string path);

        /// <summary>
        /// Gets a list of containers.
        /// </summary>
        /// <param name="path">The full path of the container.</param>
        /// <param name="recurse">If true, retrieves all containers. If false, will only retrieve immediate containers.</param>
        /// <returns>Returns a list of <see cref="IResourceInfo"/>.</returns>
        IEnumerable<IResourceInfo> GetContainers(string path, bool recurse);

        /// <summary>
        /// Gets the <see cref="IResourceInfo"/>.
        /// </summary>
        /// <param name="path">The full path of the container.</param>
        /// <param name="recurse">If true, retrieves all child resources. If false, will only retrieve immediate children.</param>
        /// <returns>Returns the container <see cref="IResourceInfo"/>. </returns>
        IResourceInfo GetContainer(string path, bool recurse);

        /// <summary>
        /// Returns a list of <see cref="IResourceInfo"/>.
        /// </summary>
        /// <param name="path">The full path of the resource.</param>
        /// <param name="recurse">If true, retrieves all child resources. If false, will only retrieve immediate children.</param>
        /// <returns>Returns the a list of <see cref="IResourceInfo"/>. </returns>
        IEnumerable<IResourceInfo> GetAll(string path, bool recurse);

        /// <summary>
        /// Writes content to a resource item.
        /// </summary>
        /// <param name="path">The full path of the resource item.</param>
        /// <param name="content"><see cref="byte"/>Data that will be written to a resource.</param>
        /// <param name="isWriting">Is currently writting?.</param>
        /// <returns>What was written of the resource.</returns>
        IList Write(string path, IList content, ref bool isWriting);

        /// <summary>
        /// Reads the content from a resource.
        /// </summary>
        /// <param name="path">The full path of the resource item.</param>
        /// <param name="finishedReading">Has it been read?.</param>
        /// <returns>Returns what was read from the resource.</returns>
        IList Read(string path, ref bool finishedReading);

        /// <summary>
        /// Creates the resource item.
        /// </summary>
        /// <param name="path">The full path of the resource item.</param>
        /// <param name="content">The content to be written.</param>
        void CreateItem(string path, string content);

        /// <summary>
        /// Creates a container.
        /// </summary>
        /// <param name="path">The full path of the container.</param>
        void CreateContainer(string path);

        /// <summary>
        /// Copies the resource from one path to another.
        /// </summary>
        /// <param name="sourcePath">The source path.</param>
        /// <param name="destinationPath">The destination path.</param>
        void CopyResourceItem(string sourcePath, string destinationPath);
    }
}

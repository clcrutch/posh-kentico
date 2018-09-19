// <copyright file="KenticoFileSystemResourceService.cs" company="Chris Crutchfield">
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

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using CMS.IO;
using ImpromptuInterface;
using PoshKentico.Core.Services.Resource;

namespace PoshKentico.Core.Providers.Resource
{
    /// <summary>
    /// Implementation of <see cref="IFileSystemResourceService"/> that uses Kentico
    /// </summary>
    [Export(typeof(IFileSystemResourceService))]
    public class KenticoFileSystemResourceService : IFileSystemResourceService
    {
        /// <inheritdoc />
        public bool IsContainer(string path)
        {
            return FileInfo.New(path).Attributes == FileAttributes.Directory;
        }

        /// <inheritdoc />
        public bool Exists(string path)
        {
            return Directory.Exists(path) || File.Exists(path);
        }

        /// <inheritdoc />
        public bool IsAbsolutePath(string path)
        {
            return Path.IsPathRooted(path);
        }

        /// <inheritdoc />
        public string GetName(string name)
        {
            return name.Split('\\').Last();
        }

        /// <inheritdoc />
        public IEnumerable<IResourceInfo> GetAll(string path, bool recurse)
        {
            return this.GetItems(path).Concat(this.GetContainers(path, recurse));
        }

        /// <inheritdoc />
        public IEnumerable<IResourceInfo> GetItems(string path)
        {
            List<IResourceInfo> items = new List<IResourceInfo>();

            foreach (var item in Directory.EnumerateFiles(path))
            {

                items.Add(this.GetItem(item));
            }

            return items.AsEnumerable();
        }

        /// <inheritdoc />
        public IResourceInfo GetItem(string path)
        {
            FileInfo itemInfo = FileInfo.New(path);

            return new
            {
                Name = itemInfo.Name,
                Path = itemInfo.FullName,
                ContainerPath = itemInfo.Directory.FullName,
                CreationTime = itemInfo.CreationTime,
                LastWriteTime = itemInfo.LastWriteTime,
                ResourceType = ResourceType.Item,
                IsContainer = false,
                Children = new IResourceInfo[] { },
            }.ActLike<IResourceInfo>();
        }

        /// <inheritdoc />
        public IEnumerable<IResourceInfo> GetContainers(string path, bool recurse)
        {
            List<IResourceInfo> items = new List<IResourceInfo>();

            foreach (var item in Directory.EnumerateDirectories(path))
            {
                items.Add(this.GetContainer(item, recurse));
            }

            return items.AsEnumerable();
        }

        /// <inheritdoc />
        public IResourceInfo GetContainer(string path, bool recurse)
        {
            DirectoryInfo itemInfo = DirectoryInfo.New(path);

            return new
            {
                Name = itemInfo.Name,
                Path = itemInfo.FullName,
                ContainerPath = itemInfo.Parent.FullName,
                CreationTime = itemInfo.CreationTime,
                LastWriteTime = itemInfo.LastWriteTime,
                ResourceType = ResourceType.Container,
                IsContainer = true,
                Children = recurse ? this.GetAll(itemInfo.FullName, true) : this.GetAll(itemInfo.FullName, false),
            }.ActLike<IResourceInfo>();
        }

        /// <inheritdoc />
        public void CreateContainer(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        /// <inheritdoc />
        public void CreateItem(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        /// <inheritdoc />
        public void DeleteContainer(string path, bool recurse)
        {
            Directory.Delete(path, recurse);
        }

        /// <inheritdoc />
        public void DeleteItem(string path)
        {
            File.Delete(path);
        }

        /// <inheritdoc />
        public string JoinPath(string sourcePath, string partialPath)
        {
            return Path.Combine(sourcePath, partialPath.TrimStart('\\'));
        }

        /// <inheritdoc />
        public void CopyResourceItem(string source, string destination)
        {
            File.Copy(source, destination);
        }

        /// <inheritdoc />
        public byte[] Write(string path, byte[] content, ref bool isWriting)
        {
            if (!isWriting)
                File.WriteAllBytes(path, content);
            else
            {
                using (var stream = FileStream.New(path, FileMode.Append))
                {
                    stream.Write(content, 0, content.Length);
                }
            }

            isWriting = true;
            return content;
        }

        /// <inheritdoc />
        public byte[] Read(string path, ref bool finishedReading)
        {
            if (finishedReading)
                return null;

            finishedReading = true;

            var lines = new ArrayList();

            var content = File.ReadAllBytes(path);

            if (content == null || content.Length == 0)
                return null;

            return content;
        }

        /// <inheritdoc />
        public void ClearAttributes(string path)
        {
            if (File.Exists(path))
            {
                var attributesToClear = FileAttributes.Hidden;
                attributesToClear |= FileAttributes.ReadOnly;

                File.SetAttributes(path, (FileInfo.New(path).Attributes & ~attributesToClear));
            }
        }
    }
}

using PoshKentico.Core.Services.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.IO;
using ImpromptuInterface;

namespace PoshKentico.Core.Providers.Resource
{
    [Export(typeof(IFileSystemResourceService))]
    public class KenticoFileSystemResourceService : IFileSystemResourceService
    {
        public bool IsContainer(string path)
        {
            return FileInfo.New(path).Attributes == FileAttributes.Directory;
        }

        public bool Exists(string path)
        {
            return (Directory.Exists(path) || File.Exists(path));
        }

        public IEnumerable<IResource> GetAll(string path, bool recurse)
        {
            return this.GetItems(path).Concat(this.GetContainers(path, recurse));
        }

        public IEnumerable<IResource> GetItems(string path)
        {
            List<IResource> items = new List<IResource>();

            foreach (var item in Directory.EnumerateFiles(path))
            {

                items.Add(this.GetItem(path));
            }

            return items.AsEnumerable();
        }

        public IResource GetItem(string path)
        {
            FileInfo itemInfo = FileInfo.New(path);

            return new
            {
                Name = itemInfo.Name,
                Path = itemInfo.FullName,
                CreationTime = itemInfo.CreationTime,
                LastWriteTime = itemInfo.LastWriteTime,
                ResourceType = ResourceType.File,
                IsContainer = false,
                Children = new IResource[] { },
            }.ActLike<IResource>();
        }

        public IEnumerable<IResource> GetContainers(string path, bool recurse)
        {
            List<IResource> items = new List<IResource>();

            foreach (var item in Directory.EnumerateDirectories(path))
            {
                items.Add(this.GetContainer(path, recurse));
            }

            return items.AsEnumerable();
        }

        public IResource GetContainer(string path, bool recurse)
        {
            DirectoryInfo itemInfo = DirectoryInfo.New(path);

            return new
            {
                Name = itemInfo.Name,
                Path = itemInfo.FullName,
                CreationTime = itemInfo.CreationTime,
                LastWriteTime = itemInfo.LastWriteTime,
                ResourceType = ResourceType.Directory,
                IsContainer = true,
                Children = recurse ? this.GetAll(itemInfo.FullName, recurse) : new IResource[] { },
            }.ActLike<IResource>();
        }

        public void CreateContainer(IResource resource)
        {
            var directory = Directory.CreateDirectory(resource.Path);
            resource.LastWriteTime = directory.LastWriteTime;
        }

        public void CreateItem(IResource resource)
        {
            File.WriteAllText(resource.Path, resource.Content);

            var fileInfo = FileInfo.New(resource.Path);
            resource.LastWriteTime = fileInfo.LastWriteTime;
        }

        public void DeleteContainer(string path, bool recurse)
        {
            Directory.Delete(path, recurse);
        }

        public void DeleteItem(string path)
        {
            File.Delete(path);
        }
    }
}
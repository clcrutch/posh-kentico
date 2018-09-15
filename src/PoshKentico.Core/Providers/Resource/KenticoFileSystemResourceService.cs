using CMS.IO;
using ImpromptuInterface;
using PoshKentico.Core.Services.Resource;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

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

        public bool IsAbsolutePath(string path)
        {
            return Path.IsPathRooted(path);
        }

        public string GetName(string name)
        {
            return name.Split('\\').Last();
        }

        public IEnumerable<IResourceInfo> GetAll(string path, bool recurse)
        {
            return this.GetItems(path).Concat(this.GetContainers(path, recurse));
        }

        public IEnumerable<IResourceInfo> GetItems(string path)
        {
            List<IResourceInfo> items = new List<IResourceInfo>();

            foreach (var item in Directory.EnumerateFiles(path))
            {

                items.Add(this.GetItem(item));
            }

            return items.AsEnumerable();
        }

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

        public IEnumerable<IResourceInfo> GetContainers(string path, bool recurse)
        {
            List<IResourceInfo> items = new List<IResourceInfo>();

            foreach (var item in Directory.EnumerateDirectories(path))
            {
                items.Add(this.GetContainer(item, recurse));
            }

            return items.AsEnumerable();
        }

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

        public void CreateContainer(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public void CreateItem(string path, string content)
        {
            File.WriteAllText(path, content);
        }

        public void DeleteContainer(string path, bool recurse)
        {
            Directory.Delete(path, recurse);
        }

        public void DeleteItem(string path)
        {
            File.Delete(path);
        }

        public string JoinPath(string sourcePath, string partialPath)
        {
            return Path.Combine(sourcePath, partialPath.TrimStart('\\'));
        }

        public void CopyResourceItem(string source, string destination)
        {
            File.Copy(source, destination);
        }

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
using PoshKentico.Core.Services.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.IO;
using ImpromptuInterface;
using System.Collections;

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
                ResourceType = ResourceType.File,
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
                CreationTime = itemInfo.CreationTime,
                LastWriteTime = itemInfo.LastWriteTime,
                ResourceType = ResourceType.Directory,
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

        public IList Write(string path, IList content, ref bool isWriting)
        {
            var bytes = content.Cast<byte[]>().ToArray();

            if (!isWriting)
                File.WriteAllBytes(path, bytes[0]);
            else
            {
                using (var stream = FileStream.New(path, FileMode.Append))
                {
                    stream.Write(bytes[0], 0, bytes[0].Length);
                }
            }

            isWriting = true;
            return content;
        }

        public IList Read(string path, ref bool finishedReading)
        {
            if (finishedReading)
                return null;

            finishedReading = true;

            var lines = new ArrayList();

            var content = File.ReadAllBytes(path);

            if (content == null || content.Length == 0)
                return null;

            lines.Add(content);

            return lines;
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

        public void Init()
        {

        }
    }
}
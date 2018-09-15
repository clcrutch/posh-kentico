using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.Resource
{
    public interface IResourceService
    {
        bool IsContainer(string path);
        bool Exists(string path);
        bool IsAbsolutePath(string copyPath);
        string GetName(string name);
        string JoinPath(string path, string copyPath);
        void ClearAttributes(string path);
        void DeleteContainer(string path, bool recurse);
        void DeleteItem(string path);
        IEnumerable<IResourceInfo> GetItems(string path);
        IResourceInfo GetItem(string path);
        IEnumerable<IResourceInfo> GetContainers(string path, bool recurse);
        IResourceInfo GetContainer(string path, bool recurse);
        IEnumerable<IResourceInfo> GetAll(string path, bool recurse);
        byte[] Write(string path, byte[] content, ref bool isWriting);
        byte[] Read(string path, ref bool finishedReading);
        void CreateItem(string path, string content);
        void CreateContainer(string path);
        void CopyResourceItem(string path, string newPath);
    }
}

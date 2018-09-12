using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.Resource
{
    public interface IResourceService
    {
        void CreateContainer(IResource resource);
        void CreateItem(IResource resource);
        IEnumerable<IResource> GetItems(string path);
        IResource GetItem(string path);
        IEnumerable<IResource> GetContainers(string path, bool recurse);
        IResource GetContainer(string path, bool recurse);
        IEnumerable<IResource> GetAll(string path, bool recurse);
        void DeleteContainer(string path, bool recurse);
        void DeleteItem(string path);
        bool IsContainer(string path);
        bool Exists(string path);
    }
}

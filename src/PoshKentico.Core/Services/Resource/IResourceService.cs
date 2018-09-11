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
        IResource Get(string path);
        IEnumerable<IResource> Get(string path, bool recurse);
        bool Exists(string path);
        IResource Create(IResource parent, ResourceType resourceType, string path, string content = "");
        void Create(IResource resource);
        bool Delete(IResource resource, bool recurse);
    }
}

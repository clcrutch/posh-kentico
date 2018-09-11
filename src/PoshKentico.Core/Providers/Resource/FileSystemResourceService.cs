using PoshKentico.Core.Services.Resource;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Providers.Resource
{
    [Export(typeof(IFileSytemService))]
    public class FileSystemResourceService : IFileSytemService
    {
        public IResource Create(IResource parent, ResourceType resourceType, string path, string content = "")
        {
            throw new NotImplementedException();
        }

        public void Create(IResource resource)
        {
            throw new NotImplementedException();
        }

        public bool Delete(IResource resource, bool recurse)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string path)
        {
            throw new NotImplementedException();
        }

        public IResource Get(string path)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IResource> Get(string path, bool recurse)
        {
            throw new NotImplementedException();
        }
    }
}

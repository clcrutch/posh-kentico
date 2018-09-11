using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.Resource
{
    public class ResourceItem : IResource
    {
        private IEnumerable<IResource> children;

        public ResourceItem() { }

        public IEnumerable<IResource> Children { get; set; }

        public bool IsContainer => true;
        public ResourceType ResourceType { get; set; }

        public bool IsRootItem { get; set; }

        public object Item => this;

        public string Name { get; set; }
        public string Path { get; set; }
        public string Content { get; set; }

        public IResource Parent { get; set; }
        public DateTime CreationTime { get; set; }

        public DateTime LastWriteTime { get; }

    }
}

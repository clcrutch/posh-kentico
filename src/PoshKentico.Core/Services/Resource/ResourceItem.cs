using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.Resource
{
    public class ResourceItem : IResourceInfo
    {
        public bool IsContainer { get; set; }
        public string ContainerPath { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public string Content { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public ResourceType ResourceType { get; set; }
        public IEnumerable<IResourceInfo> Children { get; set; }
    }
}

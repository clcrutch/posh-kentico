using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.Resource
{
    public interface IResourceInfo
    {
        #region Properties
        bool IsContainer { get; set; }
        string ContainerPath { get; set; }
        string Name { get; set; }
        string Path { get; set; }        DateTime CreationTime { get; set; }
        DateTime LastWriteTime { get; set; }
        ResourceType ResourceType { get; set; }
        IEnumerable<IResourceInfo> Children { get; set; }
        #endregion
    }
}

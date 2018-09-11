using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.Resource
{
    [InheritedExport]
    public interface IResource
    {
        #region Properties

        IEnumerable<IResource> Children { get; }

        bool IsContainer { get; }
        ResourceType ResourceType { get; }

        bool IsRootItem { get; }

        object Item { get; }

        string Name { get; }

        string Path { get; }

        string Content { get; set; }

        IResource Parent { get; }

        DateTime CreationTime { get; }

        DateTime LastWriteTime { get; }

        #endregion
    }
}

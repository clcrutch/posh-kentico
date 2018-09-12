using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.Resource
{
    public interface IResource
    {
        #region Properties

        IEnumerable<IResource> Children { get; set; }

        bool IsContainer { get; set; }

        ResourceType ResourceType { get; set; }

        object Item { get; set; }

        string Name { get; set; }

        string Path { get; set; }

        string Content { get; set; }

        DateTime CreationTime { get; set; }

        DateTime LastWriteTime { get; set; }

        #endregion
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.Resource
{
    public interface IResourceReaderWriterService
    {
        string Path { get; }
        void Close();
        void Dispose();
        void Initialize(IResourceService resourceService, string path);
        IList Read(long readCount);
        IList Write(IList content);
    }
}

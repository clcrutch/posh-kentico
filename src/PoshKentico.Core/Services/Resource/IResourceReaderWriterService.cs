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
        byte[] Read();
        byte[] Write(byte[] content);
    }
}

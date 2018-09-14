using CMS.IO;
using PoshKentico.Core.Services.Resource;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace PoshKentico.Core.Providers.Resource
{
    [Export(typeof(IFileSystemReaderWriterService))]
    public class KenticoResourceReaderWriterService : IResourceReaderWriterService
    {
        private bool _isWriting;
        private bool _finishedReading;
        public string Path { get; private set; }

        [Import]
        IResourceService ResourceService { get; set; }

        public void Close()
        {
        }

        public void Dispose()
        {
        }

        public IList Read(long readCount)
        {
            ResourceService.ClearAttributes(Path);

            return ResourceService.Read(Path, ref _finishedReading);
        }

        public IList Write(IList content)
        {
            ResourceService.ClearAttributes(Path);

            return ResourceService.Write(Path, content, ref _isWriting);
        }

        public void Initialize(IResourceService resourceService, string path)
        {
            ResourceService = resourceService;
            Path = path;
        }
    }
}

using PoshKentico.Core.Services.Resource;
using System.ComponentModel.Composition;

namespace PoshKentico.Core.Providers.Resource
{
    [Export(typeof(IFileSystemReaderWriterService))]
    public class KenticoResourceReaderWriterService : IResourceReaderWriterService
    {
        private bool _isWriting;
        private bool _finishedReading;
        public string Path { get; private set; }

        IResourceService ResourceService { get; set; }

        public void Close()
        {
        }

        public void Dispose()
        {
        }

        public byte[] Read()
        {
            ResourceService.ClearAttributes(Path);

            return ResourceService.Read(Path, ref _finishedReading);
        }

        public byte[] Write(byte[] content)
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

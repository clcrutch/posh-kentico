using PoshKentico.Core.Services.Resource;
using System.Collections;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;

namespace PoshKentico.CmdletProviders.Resource
{
    public class ResourceContentReaderWriter : IContentWriter, IContentReader
    {
        private IResourceReaderWriterService ReadWriteService { get; set; }

        public ResourceContentReaderWriter(IResourceReaderWriterService readWriteService)
        {
            this.ReadWriteService = readWriteService;
        }

        public void Close()
        {
            ReadWriteService.Close();
        }

        public void Dispose()
        {
            ReadWriteService.Dispose();
        }

        public IList Read(long readCount)
        {
            ArrayList list = new ArrayList();

            var content = ReadWriteService.Read();

            if (content != null)
                list.Add(content);

            return list;
        }

        public void Seek(long offset, SeekOrigin origin)
        {
            throw new PSNotSupportedException();
        }

        public IList Write(IList content)
        {
            return ReadWriteService.Write(content.Cast<byte>().ToArray());
        }
    }
}

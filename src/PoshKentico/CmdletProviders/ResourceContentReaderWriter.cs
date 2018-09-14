using PoshKentico.Core.Services.Resource;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.CmdletProviders
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
            return ReadWriteService.Read(readCount);
        }

        public void Seek(long offset, SeekOrigin origin)
        {
            throw new PSNotSupportedException();
        }

        public IList Write(IList content)
        {
            return ReadWriteService.Write(content);
        }
    }
}

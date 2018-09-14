using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PoshKentico.Core.Services.Resource;

namespace PoshKentico.Business.Resource
{
    [Export(typeof(KenticoFileSystemBusines))]
    public class KenticoFileSystemBusines : CmdletProviderBusinessBase
    {
        [Import(typeof(IFileSystemResourceService))]
        public override IResourceService ResourceService { get; set; }

        [Import(typeof(IFileSystemReaderWriterService))]
        public override IResourceReaderWriterService ReaderWriterService { get; set; }
    }
}

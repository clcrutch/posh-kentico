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
    [Export(typeof(KenticoFileSystemBusiness))]
    public class KenticoFileSystemBusiness : CmdletProviderBusinessBase
    {
        [Import(typeof(IFileSystemResourceService))]
        public override IResourceService ResourceService { get; set; }
    }
}

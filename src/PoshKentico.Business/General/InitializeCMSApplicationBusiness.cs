using PoshKentico.Core.Services.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Business.General
{
    [Export(typeof(InitializeCMSApplicationBusiness))]
    public class InitializeCMSApplicationBusiness : CmdletBusinessBase
    {
        [Import]
        public ICmsApplicationService CmsApplicationService { get; set; }
    }
}

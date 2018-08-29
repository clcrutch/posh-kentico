using PoshKentico.Core.Services.Development.WebParts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Business.Development.WebParts
{
    [Export(typeof(SetCMSWebPartBusiness))]
    public class SetCMSWebPartBusiness : WebPartBusinessBase
    {
        public void Set(IWebPart webPart)
        {
            this.WebPartService.Update(webPart);
        }
    }
}

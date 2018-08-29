using PoshKentico.Core.Services.Development.WebParts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Business.Development.WebParts
{
    [Export(typeof(RemoveCMSWebPartBusiness))]
    public class RemoveCMSWebPartBusiness : WebPartBusinessBase
    {
        public void RemoveWebPart(IWebPart webPart)
        {
            this.WebPartService.Delete(webPart);
        }
    }
}

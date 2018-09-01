using PoshKentico.Core.Services.Development.WebParts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Business.Development.WebParts
{
    [Export(typeof(RemoveCMSWebPartFieldBusiness))]
    public class RemoveCMSWebPartFieldBusiness : WebPartBusinessBase
    {
        public void RemoveField(IWebPartField field)
        {
            if (this.ShouldProcess(field.Name, $"Remove the field from web part named '{field.WebPart.WebPartName}'."))
            {
                this.WebPartService.RemoveField(field);
            }
        }
    }
}

using PoshKentico.Core.Services.Development.WebParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Providers.Development.WebParts
{
    internal interface IWebPartHolder 
    {
        IWebPart WebPart { get; set; }
    }

    internal class WebPartHolder : IWebPartHolder
    {
        public IWebPart WebPart { get; set; }
    }
}

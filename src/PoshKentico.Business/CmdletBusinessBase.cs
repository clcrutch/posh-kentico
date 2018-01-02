using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Business
{
    public abstract class CmdletBusinessBase
    {
        public Action<string> WriteDebug { get; set; }
        public Action<string> WriteVerbose { get; set; }
    }
}

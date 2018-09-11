using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.CmdletProviders.DynamicParameters
{
    public class FileSystemResourceDynamicParameters
    {
        [Parameter]
        [ValidateSet("Directory", "File")]
        public string ResourceType { get; set; }
        [Parameter]
        public string Content { get; set; }
    }
}

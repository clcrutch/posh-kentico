using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Business.Resource
{
    public class ResourceDynamicParameters
    {
        [Parameter]
        [ValidateSet("Directory", "File")]
        public string ResourceType { get; set; }
        [Parameter]
        public string Content { get; set; }
    }
}

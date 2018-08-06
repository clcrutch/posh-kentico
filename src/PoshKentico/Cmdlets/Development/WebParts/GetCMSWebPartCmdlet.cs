using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Cmdlets.Development.WebParts
{
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Get, "CMSWebPart")]
    [Alias("gwp")]
    public class GetCMSWebPartCmdlet : MefCmdlet
    {
    }
}

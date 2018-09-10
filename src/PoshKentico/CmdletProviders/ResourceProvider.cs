using PoshKentico.Core.Services.Resource;
using System.Management.Automation;
using System.Management.Automation.Provider;

namespace PoshKentico.CmdletProviders
{
    [OutputType(typeof(ResourceItem), ProviderCmdlet = "Get-Item")]
    [CmdletProvider("KenticoResourceProvider", ProviderCapabilities.ExpandWildcards)]
    public class ResourceProvider : CmdletProvider
    {
        protected override string ProviderName => "Kenti";
    }
}

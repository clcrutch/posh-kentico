using CMS.Base;
using PoshKentico.Business;
using PoshKentico.Business.Resource;
using PoshKentico.CmdletProviders.DynamicParameters;
using PoshKentico.Core.Services.Resource;
using System.ComponentModel.Composition;
using System.Management.Automation;
using System.Management.Automation.Provider;

namespace PoshKentico.CmdletProviders
{
    [OutputType(typeof(ResourceItem), ProviderCmdlet = ProviderCmdlet.GetItem)]
    [CmdletProvider("KenticoResourceProvider", ProviderCapabilities.ExpandWildcards)]
    public class ResourceProvider : CmdletProvider<KenticoFileSystemBusines>
    {
        protected sealed override string ProviderName { get => "KenticoResourceProvider"; }
        protected sealed override string DriveName { get => "Kenti"; }
        protected sealed override string DriveRootPath { get => SystemContext.WebApplicationPhysicalPath; }
        protected sealed override string DriveDescription { get => "Kentico resource provider."; }
    }
}

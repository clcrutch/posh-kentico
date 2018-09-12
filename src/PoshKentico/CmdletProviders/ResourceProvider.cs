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
    [OutputType(typeof(ResourceItem), ProviderCmdlet = "Get-Item")]
    [CmdletProvider("KenticoResourceProvider", ProviderCapabilities.ExpandWildcards)]
    public class ResourceProvider : CmdletProvider<KenticoFileSystemBusiness>
    {
        protected sealed override string ProviderName { get => "KenticoResourceProvider"; }
        protected sealed override string DriveName { get => "Kenti"; }
        protected sealed override string DriveRootPath { get => SystemContext.WebApplicationPhysicalPath; }
        protected sealed override string DriveDescription { get => "Kentico resource provider."; }

        protected override object NewItemDynamicParameters(string path, string itemTypeName, object newItemValue)
        {
            switch (itemTypeName.ToLowerInvariant())
            {
                case "filesystem":
                    return new FileSystemResourceDynamicParameters();
                default:
                    return null;
            }
        }
    }
}

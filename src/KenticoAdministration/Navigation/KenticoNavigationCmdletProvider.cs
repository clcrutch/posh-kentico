using CMS.PortalEngine;
using KenticoAdministration.Navigation.FileSystemItems;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Text;
using System.Threading.Tasks;

namespace KenticoAdministration.Navigation
{
    [CmdletProvider("KenticoProvider", ProviderCapabilities.None)]
    public class KenticoNavigationCmdletProvider : NavigationCmdletProvider
    {
        private IFileSystemItem _rootItem = new RootFileSystemItem();

        protected override bool IsValidPath(string path)
        {
            return true;
        }

        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            var drive = new PSDriveInfo("Kentico", ProviderInfo, string.Empty, string.Empty, null);
            var drives = new Collection<PSDriveInfo>() { drive };

            return drives;
        }

        protected override bool ItemExists(string path)
        {
            return _rootItem.Exists(path);
        }

        protected override bool IsItemContainer(string path)
        {
            return (_rootItem.FindPath(path)?.IsContainer).GetValueOrDefault(false);
        }

        protected override void GetChildItems(string path, bool recurse)
        {
            IFileSystemItem fileSystemItem = _rootItem.FindPath(path);

            if (fileSystemItem == null || fileSystemItem.Children == null) return;

            foreach (var child in fileSystemItem.Children)
            {
                WriteItemObject(child, recurse);
            }
        }

        private void WriteItemObject(IFileSystemItem item, bool recurse)
        {
            WriteItemObject(item.Item, item.Path, item.IsContainer);

            if (recurse && item.Children != null)
            {
                foreach (var child in item.Children)
                {
                    WriteItemObject(child, recurse);
                }
            }
        }
    }
}

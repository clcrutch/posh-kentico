using PoshKentico.Helpers;
using PoshKentico.Navigation.FileSystemItems;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Provider;

namespace PoshKentico.Navigation
{
    [CmdletProvider("KenticoProvider", ProviderCapabilities.None)]
    public class KenticoNavigationCmdletProvider : NavigationCmdletProvider
    {

        #region Fields

        private IFileSystemItem _rootItem = new RootFileSystemItem();

        #endregion


        #region Methods

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
            CmsApplicationHelper.InitializeKentico(WriteDebug, WriteVerbose);

            return _rootItem.Exists(path);
        }

        protected override bool IsItemContainer(string path)
        {
            return (_rootItem.FindPath(path)?.IsContainer).GetValueOrDefault(false);
        }

        protected override void GetChildItems(string path, bool recurse)
        {
            CmsApplicationHelper.InitializeKentico(WriteDebug, WriteVerbose);

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

        #endregion

    }
}

using PoshKentico.Helpers;
using PoshKentico.Navigation.DynamicParameters;
using PoshKentico.Navigation.FileSystemItems;
using System;
using System.Collections.ObjectModel;
using System.Linq;
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

        private void InitKentico()
        {
            CmsApplicationHelper.InitializeKentico(WriteDebug, WriteVerbose);
        }

        protected override Collection<PSDriveInfo> InitializeDefaultDrives()
        {
            var drive = new PSDriveInfo("Kentico", ProviderInfo, string.Empty, string.Empty, null);
            var drives = new Collection<PSDriveInfo>() { drive };

            return drives;
        }

        protected override bool ItemExists(string path)
        {
            InitKentico();

            return _rootItem.Exists(path);
        }

        protected override bool IsItemContainer(string path)
        {
            return (_rootItem.FindPath(path)?.IsContainer).GetValueOrDefault(false);
        }

        protected override void GetChildItems(string path, bool recurse)
        {
            InitKentico();

            IFileSystemItem fileSystemItem = _rootItem.FindPath(path);

            if (fileSystemItem == null || fileSystemItem.Children == null) return;

            foreach (var child in fileSystemItem.Children)
            {
                WriteItemObject(child, recurse);
            }
        }

        protected override void GetItem(string path)
        {
            InitKentico();

            WriteItemObject(_rootItem.FindPath(path), false);
        }

        protected override bool HasChildItems(string path)
        {
            InitKentico();

            return (_rootItem.FindPath(path)?.Children.Any()).GetValueOrDefault(false);
        }

        protected override void NewItem(string path, string itemTypeName, object newItemValue)
        {
            InitKentico();

            int lastSlash = path.LastIndexOf('\\');
            string directory = path.Substring(0, lastSlash);
            string name = path.Substring(lastSlash + 1);
            var item = _rootItem.FindPath(directory);

            _rootItem.FindPath(directory)?.NewItem(name, itemTypeName, newItemValue);
        }

        protected override object NewItemDynamicParameters(string path, string itemTypeName, object newItemValue)
        {
            switch (itemTypeName.ToLowerInvariant())
            {
                case "webpartcategory":
                    return new NewWebPartCategoryDynamicParameter();
                default:
                    return null;
            }
        }

        protected override void RemoveItem(string path, bool recurse)
        {
            InitKentico();

            if (!(_rootItem.FindPath(path)?.Delete(recurse)).GetValueOrDefault(false))
                throw new Exception($"Cannot delete item at \"{path}\".");
        }

        private void WriteItemObject(IFileSystemItem item, bool recurse)
        {
            if (item == null) return;

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

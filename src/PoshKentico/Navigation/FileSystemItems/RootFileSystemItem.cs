using CMS.PortalEngine;
using System.Collections.Generic;
using System.Linq;

namespace PoshKentico.Navigation.FileSystemItems
{
    public class RootFileSystemItem : IFileSystemItem
    {

        #region Fields

        private IEnumerable<IFileSystemItem> _children;

        #endregion


        #region Properties

        public IEnumerable<IFileSystemItem> Children
        {
            get
            {
                if (_children == null)
                    _children = CreateChildren();

                return _children;
            }
        }

        public bool IsContainer => true;
        public object Item => this;
        public string Path => string.Empty;

        #endregion


        #region Methods

        public bool Exists(string path)
        {
            return path == string.Empty || Children.Any(c => c.Exists(path));
        }

        public IFileSystemItem FindPath(string path)
        {
            var itemContainingPath = Children.FirstOrDefault(c => c.Exists(path));

            if (path == string.Empty)
                return this;
            else
                return itemContainingPath?.FindPath(path);
        }

        private IEnumerable<IFileSystemItem> CreateChildren()
        {
            return new IFileSystemItem[]
            {
                new SecondLevelFileSystemItem("Development", new IFileSystemItem[]
                {
                    new WebPartCategoryFileSystemItem(WebPartCategoryInfoProvider.GetWebPartCategoryInfoByCodeName("/"))
                })
            };
        }

        #endregion

    }
}

using CMS.PortalEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoshKentico.Navigation.FileSystemItems
{
    public class RootFileSystemItem : AbstractFileSystemItem
    {

        #region Fields

        private IEnumerable<IFileSystemItem> _children;

        #endregion


        #region Properties

        public override IEnumerable<IFileSystemItem> Children
        {
            get
            {
                if (_children == null)
                    _children = CreateChildren();

                return _children;
            }
        }

        public override bool IsContainer => true;
        public override object Item => this;
        public override string Path => string.Empty;

        #endregion


        #region Constructors

        public RootFileSystemItem()
            : base(null)
        {

        }

        #endregion


        #region Methods

        public override bool Delete(bool recurse)
        {
            if (recurse) return DeleteChildren();

            return false;
        }

        public override bool Exists(string path)
        {
            return path == string.Empty || Children.Any(c => c.Exists(path));
        }

        public override IFileSystemItem FindPath(string path)
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
                new MetaFileSystemItem("Development", this, new IFileSystemItem[]
                {
                    new WebPartCategoryFileSystemItem(WebPartCategoryInfoProvider.GetWebPartCategoryInfoByCodeName("/"), this)
                })
            };
        }

        public override void NewItem(string name, string itemTypeName, object newItemValue)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace PoshKentico.Navigation.FileSystemItems
{
    public class MetaFileSystemItem : AbstractFileSystemItem
    {

        #region Fields

        private IEnumerable<IFileSystemItem> _children;
        private string _path;

        #endregion


        #region Properties

        public override IEnumerable<IFileSystemItem> Children => _children;
        public override bool IsContainer => true;
        public override object Item => this;
        public override string Path => _path;

        #endregion


        #region Constructors

        public MetaFileSystemItem(string path, IFileSystemItem parent, IEnumerable<IFileSystemItem> children)
            : base(parent)
        {
            _path = path;
            _children = children;
        }

        #endregion


        #region Methods

        public override bool Delete(bool recursive)
        {
            if (recursive) return DeleteChildren();

            return false;                
        }

        public override bool Exists(string path)
        {
            var pathParts = path.Split('\\');

            return (pathParts.Length > 0 && pathParts[0].Equals(Path, StringComparison.InvariantCultureIgnoreCase)) ||
                (Children?.Any(c => c.Exists(path))).GetValueOrDefault(false);
        }

        public override IFileSystemItem FindPath(string path)
        {
            if (path.Equals(Path, StringComparison.InvariantCultureIgnoreCase))
                return this;
            else
            {
                var itemContainingPath = Children?.FirstOrDefault(c => c.Exists(path));

                return itemContainingPath?.FindPath(path);
            }
        }

        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace PoshKentico.Navigation.FileSystemItems
{
    public class SecondLevelFileSystemItem : IFileSystemItem
    {

        #region Properties

        public IEnumerable<IFileSystemItem> Children { get; private set; }
        public bool IsContainer => true;
        public object Item => this;
        public string Path { get; private set; }

        #endregion


        #region Constructors

        public SecondLevelFileSystemItem(string path, IEnumerable<IFileSystemItem> children)
        {
            Path = path;
            Children = children;
        }

        #endregion


        #region Methods

        public bool Exists(string path)
        {
            var pathParts = path.Split('\\');

            return (pathParts.Length > 0 && pathParts[0].Equals(Path, StringComparison.InvariantCultureIgnoreCase)) ||
                (Children?.Any(c => c.Exists(path))).GetValueOrDefault(false);
        }

        public IFileSystemItem FindPath(string path)
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

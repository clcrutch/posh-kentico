using System;
using System.Collections.Generic;

namespace PoshKentico.Navigation.FileSystemItems
{
    public abstract class AbstractFileSystemItem : IFileSystemItem
    {

        #region Properties

        public abstract IEnumerable<IFileSystemItem> Children { get; }
        public abstract bool IsContainer { get; }
        public abstract object Item { get; }
        public virtual IFileSystemItem Parent { get; protected set; }
        public abstract string Path { get; }

        #endregion


        #region Constructors

        public AbstractFileSystemItem(IFileSystemItem parent)
        {
            Parent = parent;
        }

        #endregion


        #region Methods

        public abstract bool Delete(bool recurse);
        
        public virtual bool DeleteChildren()
        {
            if (Children == null) return true;

            foreach (var child in Children)
            {
                if (!child.Delete(true))
                    return false;
            }

            return true;
        }

        public abstract bool Exists(string path);
        public abstract IFileSystemItem FindPath(string path);
        public abstract void NewItem(string name, string itemTypeName, object newItemValue);

        #endregion

    }
}

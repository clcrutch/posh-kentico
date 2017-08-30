using System;
using System.Collections.Generic;

namespace PoshKentico.Navigation.FileSystemItems
{
    public interface IFileSystemItem
    {

        #region Properties

        IEnumerable<IFileSystemItem> Children { get; }
        bool IsContainer { get; }
        object Item { get; }
        string Path { get; }

        #endregion


        #region Methods

        bool Exists(string path);
        IFileSystemItem FindPath(string path);

        #endregion

    }
}

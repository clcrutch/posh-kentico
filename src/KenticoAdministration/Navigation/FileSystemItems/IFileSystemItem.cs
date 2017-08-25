using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenticoAdministration.Navigation.FileSystemItems
{
    public interface IFileSystemItem
    {
        IEnumerable<IFileSystemItem> Children { get; }
        bool IsContainer { get; }
        object Item { get; }
        string Path { get; }

        bool Exists(string path);
        IFileSystemItem FindPath(string path);
    }
}

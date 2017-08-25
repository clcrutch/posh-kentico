using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenticoAdministration.Navigation.FileSystemItems
{
    public class SecondLevelFileSystemItem : IFileSystemItem
    {
        public IEnumerable<IFileSystemItem> Children { get; private set; }

        public bool IsContainer => true;
        public object Item => this;
        public string Path { get; private set; }

        public SecondLevelFileSystemItem(string path, IEnumerable<IFileSystemItem> children)
        {
            Path = path;
            Children = children;
        }

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
    }
}

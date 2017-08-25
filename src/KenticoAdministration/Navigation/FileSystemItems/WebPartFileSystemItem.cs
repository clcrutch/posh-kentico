using CMS.PortalEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenticoAdministration.Navigation.FileSystemItems
{
    public class WebPartFileSystemItem : IFileSystemItem
    {
        private WebPartInfo _webPartInfo;

        public IEnumerable<IFileSystemItem> Children => null;
        public bool IsContainer => false;
        public object Item => _webPartInfo;
        public string Path => _webPartInfo.WebPartName;

        public WebPartFileSystemItem(WebPartInfo webPartInfo)
        {
            _webPartInfo = webPartInfo;
        }

        public bool Exists(string path)
        {
            // TODO
            return false;
        }

        public IFileSystemItem FindPath(string path)
        {
            // TODO
            return this;
        }
    }
}

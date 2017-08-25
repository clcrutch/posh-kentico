using CMS.PortalEngine;
using System.Collections.Generic;

namespace KenticoAdministration.Navigation.FileSystemItems
{
    public class WebPartFileSystemItem : IFileSystemItem
    {

        #region Fields

        private WebPartInfo _webPartInfo;

        #endregion


        #region Properties

        public IEnumerable<IFileSystemItem> Children => null;
        public bool IsContainer => false;
        public object Item => _webPartInfo;
        public string Path => _webPartInfo.WebPartName;

        #endregion


        #region Constructors

        public WebPartFileSystemItem(WebPartInfo webPartInfo)
        {
            _webPartInfo = webPartInfo;
        }

        #endregion


        #region Methods

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

        #endregion

    }
}

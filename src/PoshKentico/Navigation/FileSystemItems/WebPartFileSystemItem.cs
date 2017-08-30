using CMS.PortalEngine;
using System;
using System.Collections.Generic;

namespace PoshKentico.Navigation.FileSystemItems
{
    public class WebPartFileSystemItem : AbstractFileSystemItem
    {

        #region Fields

        private string _path;
        private WebPartInfo _webPartInfo;

        #endregion


        #region Properties

        public override IEnumerable<IFileSystemItem> Children => null;
        public override bool IsContainer => false;
        public override object Item => _webPartInfo;
        public override string Path => _path;

        #endregion


        #region Constructors

        public WebPartFileSystemItem(WebPartInfo webPartInfo, IFileSystemItem parent)
            : base(parent)
        {
            if (webPartInfo == null)
                throw new ArgumentNullException(nameof(webPartInfo));

            _path = $"{parent.Path}\\{webPartInfo.WebPartName}";
            _webPartInfo = webPartInfo;
        }

        #endregion


        #region Methods

        public override bool Delete(bool recurse)
        {
            return _webPartInfo.Delete();
        }

        public override bool Exists(string path)
        {
            return _path.Equals(path, StringComparison.InvariantCultureIgnoreCase) && _webPartInfo != null;
        }

        public override IFileSystemItem FindPath(string path)
        {
            // TODO
            return this;
        }

        public override void NewItem(string name, string itemTypeName, object newItemValue)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}

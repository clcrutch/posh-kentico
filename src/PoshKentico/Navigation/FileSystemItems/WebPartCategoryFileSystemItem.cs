using CMS.PortalEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoshKentico.Navigation.FileSystemItems
{
    public class WebPartCategoryFileSystemItem : AbstractFileSystemItem
    {

        #region Fields

        private IEnumerable<IFileSystemItem> _children;
        private WebPartCategoryInfo _webPartCategoryInfo;

        #endregion


        #region Properties

        public override IEnumerable<IFileSystemItem> Children
        {
            get
            {
                if (_children == null)
                {
                    IEnumerable<IFileSystemItem> childCategories = (from c in WebPartCategoryInfoProvider.GetCategories()
                                                                    where c.CategoryParentID == _webPartCategoryInfo.CategoryID
                                                                    select new WebPartCategoryFileSystemItem(c, this));
                    IEnumerable<IFileSystemItem> childWebParts = (from w in WebPartInfoProvider.GetAllWebParts(_webPartCategoryInfo.CategoryID)
                                                                  select new WebPartFileSystemItem(w, this));

                    _children = childCategories.Concat(childWebParts).ToArray();
                }

                return _children;
            }
        }     
                                                
        public override bool IsContainer => true;
        public override object Item => _webPartCategoryInfo;
        public override string Path => _webPartCategoryInfo.CategoryPath
            .Replace("/", "Development\\WebParts\\")
            .Replace("/", "\\");

        #endregion


        #region Constructors

        public WebPartCategoryFileSystemItem(WebPartCategoryInfo webPartCategoryInfo, IFileSystemItem parent)
            : base(parent)
        {
            _webPartCategoryInfo = webPartCategoryInfo;
        }

        #endregion


        #region Methods

        public override bool Delete(bool recursive)
        {
            if (recursive && !DeleteChildren()) return false;

            return _webPartCategoryInfo.Delete();
        }

        public override bool Exists(string path)
        {
            return FindPath(path) != null;
        }

        public override IFileSystemItem FindPath(string path)
        {
            var adjustedPath = path.ToLowerInvariant()
                .Replace("development\\webparts", string.Empty)
                .Replace('\\', '/');

            if (string.IsNullOrWhiteSpace(adjustedPath)) adjustedPath = "/";

            var webPartCategoryInfo = (from c in WebPartCategoryInfoProvider.GetCategories()
                                       where c.CategoryPath.Equals(adjustedPath, StringComparison.InvariantCultureIgnoreCase)
                                       select c).FirstOrDefault();

            if (webPartCategoryInfo != null)
                return new WebPartCategoryFileSystemItem(webPartCategoryInfo, this);
            else
                return null;
        }

        #endregion

    }
}

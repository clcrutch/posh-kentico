using CMS.PortalEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KenticoAdministration.Navigation.FileSystemItems
{
    public class WebPartCategoryFileSystemItem : IFileSystemItem
    {

        #region Fields

        private IEnumerable<IFileSystemItem> _children;
        private WebPartCategoryInfo _webPartCategoryInfo;

        #endregion


        #region Properties

        public IEnumerable<IFileSystemItem> Children
        {
            get
            {
                if (_children == null)
                {
                    IEnumerable<IFileSystemItem> childCategories = (from c in WebPartCategoryInfoProvider.GetCategories()
                                                                    where c.CategoryParentID == _webPartCategoryInfo.CategoryID
                                                                    select new WebPartCategoryFileSystemItem(c));
                    IEnumerable<IFileSystemItem> childWebParts = (from w in WebPartInfoProvider.GetAllWebParts(_webPartCategoryInfo.CategoryID)
                                                                  select new WebPartFileSystemItem(w));

                    _children = childCategories.Concat(childWebParts).ToArray();
                }

                return _children;
            }
        }     
                                                
        public bool IsContainer => true;
        public object Item => _webPartCategoryInfo;
        public string Path => _webPartCategoryInfo.CategoryPath
            .Replace("/", "Development\\WebParts\\")
            .Replace("/", "\\");

        #endregion


        #region Constructors

        public WebPartCategoryFileSystemItem(WebPartCategoryInfo webPartCategoryInfo)
        {
            _webPartCategoryInfo = webPartCategoryInfo;
        }

        #endregion


        #region Methods

        public bool Exists(string path)
        {
            return FindPath(path) != null;
        }

        public IFileSystemItem FindPath(string path)
        {
            var adjustedPath = path.ToLowerInvariant()
                .Replace("development\\webparts", string.Empty)
                .Replace('\\', '/');

            if (string.IsNullOrWhiteSpace(adjustedPath)) adjustedPath = "/";

            var webPartCategoryInfo = (from c in WebPartCategoryInfoProvider.GetCategories()
                                       where c.CategoryPath.Equals(adjustedPath, StringComparison.InvariantCultureIgnoreCase)
                                       select c).FirstOrDefault();

            if (webPartCategoryInfo != null)
                return new WebPartCategoryFileSystemItem(webPartCategoryInfo);
            else
                return null;
        }

        #endregion

    }
}

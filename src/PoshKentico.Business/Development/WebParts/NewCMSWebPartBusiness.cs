// <copyright file="NewCMSWebPartBusiness.cs" company="Chris Crutchfield">
// Copyright (C) 2017  Chris Crutchfield
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see &lt;http://www.gnu.org/licenses/&gt;.
// </copyright>

using System.ComponentModel.Composition;
using PoshKentico.Core.Services.Development.WebParts;

namespace PoshKentico.Business.Development.WebParts
{
    [Export(typeof(NewCMSWebPartBusiness))]
    public class NewCMSWebPartBusiness : WebPartBusinessBase
    {
        #region Methods

        public IWebPart CreateWebPart(string path, string fileName, string displayName)
        {
            var name = path.Substring(path.LastIndexOf('/') + 1);
            var basePath = path.Substring(0, path.LastIndexOf('/'));

            if (string.IsNullOrWhiteSpace(basePath))
            {
                basePath = "/";
            }

            var parent = this.GetCategoryFromPath(basePath);

            return this.CreateWebPart(name, fileName, displayName, parent);
        }

        public IWebPart CreateWebPart(string name, string fileName, string displayName, IWebPartCategory webPartCategory)
        {
            if (string.IsNullOrEmpty(displayName))
            {
                displayName = name;
            }

            var data = new WebPart
            {
                WebPartDisplayName = displayName,
                WebPartFileName = fileName,
                WebPartName = name,

                WebPartCategoryID = webPartCategory.CategoryID,
            };

            return this.WebPartService.Create(data);
        }

        #endregion

        #region Classes

        private class WebPart : IWebPart
        {
            public int WebPartCategoryID { get; set; }

            public string WebPartDisplayName { get; set; }

            public string WebPartFileName { get; set; }

            public int WebPartID { get; set; }

            public string WebPartName { get; set; }

            public string WebPartProperties { get; set; }
        }

        #endregion

    }
}

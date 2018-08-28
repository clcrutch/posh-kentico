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
    /// <summary>
    /// Business layer fo the New-CMSWebPart cmdlet.
    /// </summary>
    [Export(typeof(NewCMSWebPartBusiness))]
    public class NewCMSWebPartBusiness : WebPartBusinessBase
    {
        #region Methods

        /// <summary>
        /// Creates a <see cref="IWebPart"/> at the specified path.
        /// </summary>
        /// <param name="path">The path to create the <see cref="IWebPart"/> at.</param>
        /// <param name="fileName">The file name for the underlying class file.</param>
        /// <param name="displayName">The display name for the <see cref="IWebPart"/>.</param>
        /// <returns>The newly created <see cref="IWebPart"/>.</returns>
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

        /// <summary>
        /// Creates a <see cref="IWebPart"/> with the specified name under the specified <see cref="IWebPartCategory"/>.
        /// </summary>
        /// <param name="name">The name for the <see cref="IWebPart"/>.</param>
        /// <param name="fileName">The file name for the underlying class file.</param>
        /// <param name="displayName">The display name for the <see cref="IWebPart"/>.</param>
        /// <param name="webPartCategory">The <see cref="IWebPartCategory"/> to create the <see cref="IWebPart"/> under.</param>
        /// <returns>The newly created <see cref="IWebPart"/>.</returns>
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

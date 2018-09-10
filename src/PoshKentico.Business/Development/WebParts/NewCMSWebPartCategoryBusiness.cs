// <copyright file="NewCMSWebPartCategoryBusiness.cs" company="Chris Crutchfield">
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
using System.Diagnostics.CodeAnalysis;
using PoshKentico.Core.Services.Development.WebParts;

namespace PoshKentico.Business.Development.WebParts
{
    /// <summary>
    /// Business layer for the New-CMSWebPartCategory cmdlet.
    /// </summary>
    [Export(typeof(NewCMSWebPartCategoryBusiness))]
    public class NewCMSWebPartCategoryBusiness : WebPartBusinessBase
    {
        #region Methods

        /// <summary>
        /// Creates a new WebPartCategory in the CMS System.
        /// </summary>
        /// <param name="path">The path for the new WebPartCategory.</param>
        /// <param name="displayName">The display name for the WebPartCategory.</param>
        /// <param name="imagePath">The image path for the new WebPartCategory.</param>
        /// <returns>The newly created WebPartCategory.</returns>
        public IWebPartCategory CreateWebPartCategory(string path, string displayName, string imagePath)
        {
            var name = path.Substring(path.LastIndexOf('/') + 1);
            var basePath = path.Substring(0, path.LastIndexOf('/'));

            if (string.IsNullOrWhiteSpace(basePath))
            {
                basePath = "/";
            }

            var parent = this.GetCategoryFromPath(basePath);

            if (string.IsNullOrEmpty(displayName))
            {
                displayName = name;
            }

            var data = new WebPartCategory
            {
                CategoryName = name,
                CategoryPath = path,
                CategoryDisplayName = displayName,
                CategoryImagePath = imagePath,
                CategoryParentID = parent.CategoryID,

                CategoryID = -1,
            };

            return this.WebPartService.Create(data);
        }

        #endregion

        #region Classes

        [ExcludeFromCodeCoverage]
        private class WebPartCategory : IWebPartCategory
        {
            public string CategoryDisplayName { get; set; }

            public int CategoryID { get; set; }

            public string CategoryImagePath { get; set; }

            public string CategoryName { get; set; }

            public int CategoryParentID { get; set; }

            public string CategoryPath { get; set; }
        }

        #endregion

    }
}

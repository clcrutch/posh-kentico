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

using System;
using System.ComponentModel.Composition;
using System.Linq;
using ImpromptuInterface;
using PoshKentico.Core.Services.Development;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Business.Development
{
    /// <summary>
    /// Business layer for the New-CMSWebPartCategory cmdlet.
    /// </summary>
    [Export(typeof(NewCMSWebPartCategoryBusiness))]
    public class NewCMSWebPartCategoryBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the CMS Application Service.  Populated by MEF.
        /// </summary>
        [Import]
        public ICmsApplicationService CmsApplicationService { get; set; }

        /// <summary>
        /// Gets or sets a reference to the WebPart Service.  Populated by MEF.
        /// </summary>
        [Import]
        public IWebPartService WebPartService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new WebPartCategory in the CMS System.
        /// </summary>
        /// <param name="path">The path for the new WebPartCategory.</param>
        /// <param name="displayName">The display name for the WebPartCategory.</param>
        /// <param name="imagePath">The image path for the new WebPartCategory.</param>
        /// <returns>The newly created WebPartCategory.</returns>
        public IWebPartCategory CreateWebPart(string path, string displayName, string imagePath)
        {
            this.CmsApplicationService.Initialize(true, this.WriteDebug, this.WriteVerbose);

            var name = path.Substring(path.LastIndexOf('/') + 1);
            var basePath = path.Substring(0, path.LastIndexOf('/'));

            if (string.IsNullOrWhiteSpace(basePath))
            {
                basePath = "/";
            }

            var parent = (from c in this.WebPartService.WebPartCategories
                          where c.CategoryPath.Equals(basePath, StringComparison.InvariantCultureIgnoreCase)
                          select c).SingleOrDefault();

            if (parent == null)
            {
                throw new Exception("Cannot find parent WebPartCategory.");
            }

            if (string.IsNullOrEmpty(displayName))
            {
                displayName = name;
            }

            var data = new
            {
                CategoryName = name,
                CategoryPath = path,
                CategoryDisplayName = displayName,
                CategoryImagePath = imagePath,
                CategoryParentID = parent.CategoryID,

                CategoryID = -1,
            };

            return this.WebPartService.Create(data.ActLike<IWebPartCategory>());
        }

        #endregion

    }
}

// <copyright file="RemoveCMSWebPartCategoryBusiness.cs" company="Chris Crutchfield">
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
    /// Business layer for the Remove-CMSWebPartCategory cmdlet.
    /// </summary>
    [Export(typeof(RemoveCMSWebPartCategoryBusiness))]
    public class RemoveCMSWebPartCategoryBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the WebPart Service.  Populated by MEF.
        /// </summary>
        [Import]
        public IWebPartService WebPartService { get; set; }

        /// <summary>
        /// Gets or sets a reference to the <see cref="GetCMSWebPartCategoryBusiness"/> used to get the WebPartCategories to delete.  Populated by MEF.
        /// </summary>
        [Import]
        public GetCMSWebPartCategoryBusiness GetCMSWebPartCategoryBusiness { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes all of the <see cref="IWebPartCategory"/> which match the specified criteria.
        /// </summary>
        /// <param name="matchString">The string which to match the webparts to.</param>
        /// <param name="exact">A boolean which indicates if the match should be exact.</param>
        public void RemoveWebPartCategories(string matchString, bool exact)
        {
            foreach (var cat in this.GetCMSWebPartCategoryBusiness.GetWebPartCategories(matchString, exact))
            {
                this.RemoveWebPartCategory(cat);
            }
        }

        /// <summary>
        /// Deletes all of the <see cref="IWebPartCategory"/> which match the supplied IDs.
        /// </summary>
        /// <param name="ids">The IDs of the <see cref="IWebPartCategory"/> to delete.</param>
        public void RemoveWebPartCategories(params int[] ids)
        {
            foreach (var cat in this.GetCMSWebPartCategoryBusiness.GetWebPartCategories(ids))
            {
                this.RemoveWebPartCategory(cat);
            }
        }

        /// <summary>
        /// Deletes the specified <see cref="IWebPartCategory"/>.
        /// </summary>
        /// <param name="webPartCategory">The webpart category to delete.</param>
        public void RemoveWebPartCategory(IWebPartCategory webPartCategory)
        {
            // Prompt for confirmation.
            if (this.ShouldProcess(webPartCategory.CategoryDisplayName, "delete"))
            {
                this.WebPartService.Delete(webPartCategory);
            }
        }

        #endregion

    }
}

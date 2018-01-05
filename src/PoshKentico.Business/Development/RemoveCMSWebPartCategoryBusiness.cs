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
using PoshKentico.Core.Services.Development;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Business.Development
{
    [Export(typeof(RemoveCMSWebPartCategoryBusiness))]
    public class RemoveCMSWebPartCategoryBusiness : CmdletBusinessBase
    {
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

        [Import]
        public GetCMSWebPartCategoryBusiness GetCMSWebPartCategoryBusiness { get; set; }

        public void DeleteWebPartCategory(string matchString, bool exact)
        {
            this.CmsApplicationService.Initialize(true, this.WriteDebug, this.WriteVerbose);

            foreach (var cat in this.GetCMSWebPartCategoryBusiness.GetWebPartCategories(matchString, exact))
            {
                this.DeleteWebPartCategory(cat);
            }
        }

        public void DeleteWebPartCategory(int[] ids)
        {
            this.CmsApplicationService.Initialize(true, this.WriteDebug, this.WriteVerbose);

            foreach (var cat in this.GetCMSWebPartCategoryBusiness.GetWebPartCategories(ids))
            {
                this.DeleteWebPartCategory(cat);
            }
        }

        public void DeleteWebPartCategory(IWebPartCategory webPartCategory)
        {
            if (this.ShouldProcess(webPartCategory.CategoryDisplayName, "delete"))
            {
                this.CmsApplicationService.Initialize(true, this.WriteDebug, this.WriteVerbose);

                this.WebPartService.Delete(webPartCategory);
            }
        }
    }
}

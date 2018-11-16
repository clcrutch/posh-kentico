// <copyright file="SetCMSPageTemplateCategoryBusiness.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Development.PageTemplates;

namespace PoshKentico.Business.Development.PageTemplates
{
    /// <summary>
    /// Business layer for the Set-CMSPageTemplateCategory cmdlet.
    /// </summary>
    [Export(typeof(SetCMSPageTemplateCategoryBusiness))]
    public class SetCMSPageTemplateCategoryBusiness : PageTemplateBusinessBase
    {
        #region Methods

        /// <summary>
        /// Sets the <see cref="IPageTemplateCategory"/> within Kentico.
        /// </summary>
        /// <param name="pageTemplateCategory">The <see cref="IPageTemplateCategory"/> to set.</param>
        public void Set(IPageTemplateCategory pageTemplateCategory)
        {
            this.PageTemplateService.Update(pageTemplateCategory);
        }

        #endregion

    }
}

// <copyright file="RemoveCMSPageTemplateBusiness.cs" company="Chris Crutchfield">
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
    /// Business layer fo the Remove-CMSPageTemplate cmdlet.
    /// </summary>
    [Export(typeof(RemoveCMSPageTemplateBusiness))]
    public class RemoveCMSPageTemplateBusiness : PageTemplateBusinessBase
    {
        #region Methods

        /// <summary>
        /// Removes the supplied <see cref="IPageTemplate"/> from the system.
        /// </summary>
        /// <param name="pageTemplate">The <see cref="IPageTemplate"/> to remove.</param>
        public void RemovePageTemplate(IPageTemplate pageTemplate)
        {
            if (this.ShouldProcess(pageTemplate.CodeName, "Remove the page template from Kentico."))
            {
                this.PageTemplateService.Delete(pageTemplate);
            }
        }

        #endregion

    }
}

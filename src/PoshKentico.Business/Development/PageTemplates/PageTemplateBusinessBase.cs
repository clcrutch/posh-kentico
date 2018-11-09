// <copyright file="PageTemplateBusinessBase.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Development.PageTemplates;

namespace PoshKentico.Business.Development.PageTemplates
{
    /// <summary>
    /// Base class for all Cmdlet Business objects which depend on the <see cref="IPageTemplateService"/>.
    /// </summary>
    public class PageTemplateBusinessBase : CmdletBusinessBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PageTemplateBusinessBase"/> class.
        /// </summary>
        /// <param name="initCmsApplication">Indicates if the CMS Application should be initialized.</param>
        public PageTemplateBusinessBase(bool initCmsApplication = true)
            : base(initCmsApplication)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a reference to the <see cref="IPageTemplateService"/>.  Populated by MEF.
        /// </summary>
        [Import]
        public virtual IPageTemplateService PageTemplateService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the <see cref="IPageTemplateCategory"/> which represents the current path.
        /// </summary>
        /// <param name="path">The path to look for the <see cref="IPageTemplateCategory"/>.</param>
        /// <returns>Returns the <see cref="IPageTemplateCategory"/> that reprsents the supplied path.</returns>
        protected IPageTemplateCategory GetCategoryFromPath(string path) =>
            (from c in this.PageTemplateService.PageTemplateCategories
             where c.CategoryPath.Equals(path, StringComparison.InvariantCultureIgnoreCase)
             select c).Single();

        #endregion

    }
}

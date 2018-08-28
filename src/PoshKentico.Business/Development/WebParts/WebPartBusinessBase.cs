// <copyright file="WebPartBusinessBase.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Development.WebParts;

namespace PoshKentico.Business.Development.WebParts
{
    /// <summary>
    /// Base class for all Cmdlet Business objects which depend on the <see cref="IWebPartService"/>.
    /// </summary>
    public abstract class WebPartBusinessBase : CmdletBusinessBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WebPartBusinessBase"/> class.
        /// </summary>
        /// <param name="initCmsApplication">Indicates if the CMS Application should be initialized.</param>
        public WebPartBusinessBase(bool initCmsApplication = true)
            : base(initCmsApplication)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a reference to the <see cref="IWebPartService"/>.  Populated by MEF.
        /// </summary>
        [Import]
        public IWebPartService WebPartService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the <see cref="IWebPartCategory"/> which represents the current path.
        /// </summary>
        /// <param name="path">The path to look for the <see cref="IWebPartCategory"/>.</param>
        /// <returns>Returns the <see cref="IWebPartCategory"/> that reprsents the supplied path.</returns>
        protected IWebPartCategory GetCategoryFromPath(string path) =>
            (from c in this.WebPartService.WebPartCategories
             where c.CategoryPath.Equals(path, StringComparison.InvariantCultureIgnoreCase)
             select c).Single();

        #endregion
    }
}

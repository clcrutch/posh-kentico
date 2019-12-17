// <copyright file="ControlBusinessBase.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Development;

namespace PoshKentico.Business.Development
{
    /// <summary>
    /// Base class for all Cmdlet Business objects which depend on the <see cref="IControlService{TControl, TControlCategory}"/>.
    /// </summary>
    /// <typeparam name="TControlService">The type for the control service used to return controls and control categories.</typeparam>
    /// <typeparam name="TControl">The type of control returned by the control service.</typeparam>
    /// <typeparam name="TControlCategory">The type of control category returned by the control service.</typeparam>
    public abstract class ControlBusinessBase<TControlService, TControl, TControlCategory> : CmdletBusinessBase
        where TControlService : IControlService<TControl, TControlCategory>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlBusinessBase{TControlService, TControl, TControlCategory}"/> class.
        /// </summary>
        /// <param name="initCmsApplication">Indicates if the CMS Application should be initialized.</param>
        public ControlBusinessBase(bool initCmsApplication = true)
            : base(initCmsApplication)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a reference to the <see cref="IControlService{TControl, TControlCategory}"/>.  Populated by MEF.
        /// </summary>
        [Import]
        public virtual TControlService ControlService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the <see cref="IControlCategory{T}"/> which represents the current path.
        /// </summary>
        /// <param name="path">The path to look for the <see cref="IControlCategory{T}"/>.</param>
        /// <returns>Returns the <see cref="IControlCategory{T}"/> that reprsents the supplied path.</returns>
        protected IControlCategory<TControlCategory> GetCategoryFromPath(string path) =>
            (from c in this.ControlService.Categories
             where c.Path.Equals(path, StringComparison.InvariantCultureIgnoreCase)
             select c).Single();

        #endregion

    }
}

// <copyright file="IControlService.cs" company="Chris Crutchfield">
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

using System.Collections.Generic;

namespace PoshKentico.Core.Services.Development
{
    /// <summary>
    /// Represents a service for interacting with controls in the CMS.
    /// </summary>
    /// <typeparam name="TControl">The type of the control to return.</typeparam>
    /// <typeparam name="TControlCategory">The type of the control category to return.</typeparam>
    public interface IControlService<TControl, TControlCategory>
    {
        #region Properties

        /// <summary>
        /// Gets a list of all of the <see cref="IControl{T}"/> provided by the CMS System.
        /// </summary>
        IEnumerable<IControl<TControl>> Controls { get; }

        /// <summary>
        /// Gets a list of all of the <see cref="IControlCategory{T}"/> provided by the CMS System.
        /// </summary>
        IEnumerable<IControlCategory<TControlCategory>> Categories { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a field to a <see cref="IControl{T}"/>.
        /// </summary>
        /// <param name="field">The <see cref="IControlField{T}"/> to add.</param>
        /// <param name="control">The <see cref="IControl{T}"/> to attach the field to.</param>
        /// <returns>The newly created <see cref="IControlField{T}"/>.</returns>
        IControlField<TControl> AddField(IControlField<TControl> field, IControl<TControl> control);

        /// <summary>
        /// Creates the <see cref="IControlCategory{T}"/>.
        /// </summary>
        /// <param name="controlCategory">The <see cref="IControlCategory{T}"/> to create.</param>
        /// <returns>The newly created <see cref="IControlCategory{T}"/>.</returns>
        IControlCategory<TControlCategory> Create(IControlCategory<TControlCategory> controlCategory);

        /// <summary>
        /// Deletes the specified <see cref="IControlCategory{T}"/>.
        /// </summary>
        /// <param name="controlCategory">The <see cref="IControlCategory{T}"/> to delete.</param>
        void Delete(IControlCategory<TControlCategory> controlCategory);

        /// <summary>
        /// Deletes the specified <see cref="IControl{T}"/>.
        /// </summary>
        /// <param name="control">The <see cref="IControl{T}"/> to delete.</param>
        void Delete(IControl<TControl> control);

        /// <summary>
        /// Gets a list of <see cref="IControlCategory{T}"/> that have <paramref name="parentControlCategory"/> as the parent.
        /// </summary>
        /// <param name="parentControlCategory">The parent <see cref="IControlCategory{T}"/> to the desired web part categories.</param>
        /// <returns>A list of <see cref="IControlCategory{T}"/> that have <paramref name="parentControlCategory"/> as the parent.</returns>
        IEnumerable<IControlCategory<TControlCategory>> GetControlCategories(IControlCategory<TControlCategory> parentControlCategory);

        /// <summary>
        /// Gets the <see cref="IControlCategory{T}"/> which matches the supplied ID.
        /// </summary>
        /// <param name="id">The ID of the <see cref="IControlCategory{T}"/> to return.</param>
        /// <returns>The <see cref="IControlCategory{T}"/> which matches the ID, else null.</returns>
        IControlCategory<TControlCategory> GetControlCategory(int id);

        /// <summary>
        /// Gets a control based off of the ID.
        /// </summary>
        /// <param name="id">The ID of the control to return.</param>
        /// <returns>The control that matches <paramref name="id"/>.</returns>
        IControl<TControl> GetControl(int id);

        /// <summary>
        /// Gets the <see cref="IControlField{T}"/> associated with the supplied <see cref="IControl{T}"/>.
        /// </summary>
        /// <param name="control">The <see cref="IControl{T}"/> to get the list of fields for.</param>
        /// <returns>A list of the <see cref="IControlField{T}"/> that is associated with the supplied <see cref="IControl{T}"/>.</returns>
        IEnumerable<IControlField<TControl>> GetControlFields(IControl<TControl> control);

        /// <summary>
        /// Gets a list of <see cref="IControl{T}"/> which have <paramref name="controlCategory"/> as their parent category.
        /// </summary>
        /// <param name="controlCategory">The <see cref="IControlCategory{T}"/> which is the category for the desired list of <see cref="IControlCategory{T}"/>.</param>
        /// <returns>A list of <see cref="IControl{T}"/> which are related to <paramref name="controlCategory"/>.</returns>
        IEnumerable<IControl<TControl>> GetControls(IControlCategory<TControlCategory> controlCategory);

        /// <summary>
        /// Updates the <see cref="IControlCategory{T}"/>.
        /// </summary>
        /// <param name="controlCategory">The <see cref="IControlCategory{T}"/> to update.</param>
        void Update(IControlCategory<TControlCategory> controlCategory);

        #endregion

    }
}

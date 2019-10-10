// <copyright file="IWebPartService.cs" company="Chris Crutchfield">
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
using CMS.PortalEngine;

namespace PoshKentico.Core.Services.Development.WebParts
{
    /// <summary>
    /// Service for providing access to a CMS webparts.
    /// </summary>
    public interface IWebPartService : IControlService<WebPartInfo, WebPartCategoryInfo>
    {
        #region Methods

        /// <summary>
        /// Adds a field to a <see cref="IWebPart"/>.
        /// </summary>
        /// <param name="field">The <see cref="IWebPartField"/> to add.</param>
        /// <param name="webPart">The <see cref="IWebPart"/> to attach the field to.</param>
        /// <returns>The newly created <see cref="IWebPartField"/>.</returns>
        IWebPartField AddField(IWebPartField field, IWebPart webPart);

        /// <summary>
        /// Creates the <see cref="IWebPart"/>.
        /// </summary>
        /// <param name="webPart">The <see cref="IWebPart"/> to create.</param>
        /// <returns>The newly created <see cref="IWebPart"/>.</returns>
        IWebPart Create(IWebPart webPart);

        /// <summary>
        /// Gets the <see cref="IWebPartField"/> associated with the supplied <see cref="IWebPart"/>.
        /// </summary>
        /// <param name="webPart">The <see cref="IWebPart"/> to get the list of fields for.</param>
        /// <returns>A list of the <see cref="IWebPartField"/> that is associated with the supplied <see cref="IWebPart"/>.</returns>
        IEnumerable<IWebPartField> GetWebPartFields(IWebPart webPart);

        /// <summary>
        /// Removes a <see cref="IWebPartField"/> from a <see cref="IWebPart"/>.
        /// </summary>
        /// <param name="field">The <see cref="IWebPartField"/> to remove.</param>
        void RemoveField(IWebPartField field);

        /// <summary>
        /// Updates the <see cref="IWebPart"/>.
        /// </summary>
        /// <param name="webPart">The <see cref="IWebPart"/> to update.</param>
        void Update(IWebPart webPart);

        /// <summary>
        /// Updates the <see cref="IWebPartField"/>.
        /// </summary>
        /// <param name="field">The <see cref="IWebPartField"/> to update.</param>
        void Update(IWebPartField field);

        #endregion

    }
}

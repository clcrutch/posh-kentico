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
        /// Creates the <see cref="IWebPart"/>.
        /// </summary>
        /// <param name="webPart">The <see cref="IWebPart"/> to create.</param>
        /// <returns>The newly created <see cref="IWebPart"/>.</returns>
        IWebPart Create(IWebPart webPart);

        /// <summary>
        /// Removes a <see cref="IControlField{T}"/> from a <see cref="IWebPart"/>.
        /// </summary>
        /// <param name="field">The <see cref="IControlField{T}"/> to remove.</param>
        void RemoveField(IControlField<WebPartInfo> field);

        /// <summary>
        /// Updates the <see cref="IWebPart"/>.
        /// </summary>
        /// <param name="webPart">The <see cref="IWebPart"/> to update.</param>
        void Update(IWebPart webPart);

        /// <summary>
        /// Updates the <see cref="IControlField{T}"/>.
        /// </summary>
        /// <param name="field">The <see cref="IControlField{T}"/> to update.</param>
        void Update(IControlField<WebPartInfo> field);

        #endregion

    }
}

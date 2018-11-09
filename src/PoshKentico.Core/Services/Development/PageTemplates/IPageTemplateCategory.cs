// <copyright file="IPageTemplateCategory.cs" company="Chris Crutchfield">
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.Development.PageTemplates
{
    /// <summary>
    /// Represents a Page Template Category.
    /// </summary>
    public interface IPageTemplateCategory
    {
        #region Properties

        /// <summary>
        /// Gets the display name for the PageTemplate category.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Gets the ID for the PageTemplate category.
        /// </summary>
        int CategoryId { get; }

        /// <summary>
        /// Gets the GUID for the PageTemplate category.
        /// </summary>
        Guid CategoryGUID { get; }

        /// <summary>
        /// Gets the image path for the PageTemplate category.
        /// </summary>
        string CategoryImagePath { get; }

        /// <summary>
        /// Gets the name for the PageTemplate category.
        /// </summary>
        string CategoryName { get; }

        /// <summary>
        /// Gets the parent ID for the PageTemplate category.
        /// </summary>
        int ParentId { get; }

        /// <summary>
        /// Gets the path for the PageTemplate category.
        /// </summary>
        string CategoryPath { get; }

        #endregion
    }
}

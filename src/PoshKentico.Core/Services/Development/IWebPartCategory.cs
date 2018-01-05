// <copyright file="IWebPartCategory.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Core.Services.Development
{
    /// <summary>
    /// Represents a WebPart Category.
    /// </summary>
    public interface IWebPartCategory
    {
        #region Properties

        /// <summary>
        /// Gets the display name for the WebPart category.
        /// </summary>
        string CategoryDisplayName { get; }

        /// <summary>
        /// Gets the ID for the WebPart category.
        /// </summary>
        int CategoryID { get; }

        /// <summary>
        /// Gets the image path for the WebPart category.
        /// </summary>
        string CategoryImagePath { get; }

        /// <summary>
        /// Gets the name for the WebPart category.
        /// </summary>
        string CategoryName { get; }

        /// <summary>
        /// Gets the parent ID for the WebPart category.
        /// </summary>
        int CategoryParentID { get; }

        /// <summary>
        /// Gets the path for the WebPart category.
        /// </summary>
        string CategoryPath { get; }

        #endregion

    }
}

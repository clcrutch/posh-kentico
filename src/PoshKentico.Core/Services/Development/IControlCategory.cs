// <copyright file="IControlCategory.cs" company="Chris Crutchfield">
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
    /// Represents a Control Category.
    /// </summary>
    /// <typeparam name="T">The type of the underlying control category.</typeparam>
    public interface IControlCategory<T>
    {
        #region Properties

        /// <summary>
        /// Gets the backing control category.
        /// </summary>
        T BackingControlCategory { get; }

        /// <summary>
        /// Gets or sets the display name for the category.
        /// </summary>
        string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the ID for the category.
        /// </summary>
        int ID { get; set; }

        /// <summary>
        /// Gets or sets the image path for the category.
        /// </summary>
        string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the name for the category.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent ID for the category.
        /// </summary>
        int ParentID { get; set; }

        /// <summary>
        /// Gets or sets the path for the category.
        /// </summary>
        string Path { get; set; }

        #endregion

    }
}

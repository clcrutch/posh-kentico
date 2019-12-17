// <copyright file="IControl.cs" company="Chris Crutchfield">
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
    /// Represents a control wrapper.
    /// </summary>
    /// <typeparam name="T">The control type to wrap.</typeparam>
    public interface IControl<T>
    {
        /// <summary>
        /// Gets the underlying control.
        /// </summary>
        T BackingControl { get; }

        /// <summary>
        /// Gets or sets the category ID of the underlying control.
        /// </summary>
        int CategoryID { get; set; }

        /// <summary>
        /// Gets or sets the display name of the underlying control.
        /// </summary>
        string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the underlying control.
        /// </summary>
        int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the underlying control.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the properties of the underlying control.
        /// </summary>
        string Properties { get; set; }
    }
}

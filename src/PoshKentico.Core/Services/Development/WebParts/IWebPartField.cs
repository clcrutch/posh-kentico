// <copyright file="IWebPartField.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Core.Services.Development.WebParts
{
    /// <summary>
    /// Represents a Web Part field.
    /// </summary>
    public interface IWebPartField
    {
        /// <summary>
        /// Gets a value indicating whether the current field is allowed to be empty.
        /// </summary>
        bool AllowEmpty { get; }

        /// <summary>
        /// Gets the caption for the current field.
        /// </summary>
        string Caption { get; }

        /// <summary>
        /// Gets the data type for the current field.
        /// </summary>
        string DataType { get; }

        /// <summary>
        /// Gets the default value for the current field.
        /// </summary>
        string DefaultValue { get; }

        /// <summary>
        /// Gets the name for the current field.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the size for the current field.
        /// </summary>
        int Size { get; }
    }
}

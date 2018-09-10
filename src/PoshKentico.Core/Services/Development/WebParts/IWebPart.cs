// <copyright file="IWebPart.cs" company="Chris Crutchfield">
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
    /// Represents a Web Part.
    /// </summary>
    public interface IWebPart
    {
        #region Properties

        /// <summary>
        /// Gets the ID for the web part category this web part belongs to.
        /// </summary>
        int WebPartCategoryID { get; }

        /// <summary>
        /// Gets the display name for the current web part.
        /// </summary>
        string WebPartDisplayName { get; }

        /// <summary>
        /// Gets the file name for the current web part.
        /// </summary>
        string WebPartFileName { get; }

        /// <summary>
        /// Gets the ID for the current web part.
        /// </summary>
        int WebPartID { get; }

        /// <summary>
        /// Gets the code name for the current web part.
        /// </summary>
        string WebPartName { get; }

        /// <summary>
        /// Gets or sets the properties for the current web part.
        /// </summary>
        string WebPartProperties { get; set; }

        #endregion

    }
}

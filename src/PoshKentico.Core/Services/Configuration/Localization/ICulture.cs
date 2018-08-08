// <copyright file="ICulture.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Core.Services.Configuration.Localization
{
    /// <summary>
    /// Represents a Culture Object.
    /// </summary>
    public interface ICulture
    {
        /// <summary>
        /// Gets culture ID.
        /// </summary>
        int CultureID { get; }

        /// <summary>
        /// Gets culture name.
        /// </summary>
        string CultureName { get; }

        /// <summary>
        /// Gets culture code.
        /// </summary>
        string CultureCode { get; }
    }
}

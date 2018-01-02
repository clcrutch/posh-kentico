// <copyright file="TypeExtensions.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Extensions
{
    /// <summary>
    /// Extension Methods for the <see cref="Type"/> class.
    /// </summary>
    public static class TypeExtensions
    {
        #region Methods

        /// <summary>
        /// Gets if the Type inherits from the specified base type somewhere in its inheritance chain.
        /// </summary>
        /// <param name="type">The child type.</param>
        /// <param name="baseType">The super or base type.</param>
        /// <returns>Returns true if type inherits from baseType, otherwise false.</returns>
        public static bool InheritsFrom(this Type type, Type baseType)
        {
            if (type.BaseType == null)
            {
                return false;
            }

            return type.BaseType == baseType || type.BaseType.InheritsFrom(baseType);
        }

        #endregion

    }
}

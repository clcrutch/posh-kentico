// <copyright file="PSObjectExtensions.cs" company="Chris Crutchfield">
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
using System.Linq;
using System.Management.Automation;

namespace PoshKentico.Extensions
{
    /// <summary>
    /// Extension method for <see cref="PSObject"/> objects.
    /// </summary>
    public static class PSObjectExtensions
    {
        #region Methods

        /// <summary>
        /// Converts a <see cref="PSObject"/> into a <see cref="Dictionary{TKey, TValue}"/>.
        /// </summary>
        /// <param name="psObject"><see cref="PSObject"/> to convert to a <see cref="Dictionary{TKey, TValue}"/>.</param>
        /// <returns>A <see cref="Dictionary{TKey, TValue}"/> containing the same properties as the <see cref="PSObject"/>.</returns>
        public static Dictionary<string, object> ToDictionary(this PSObject psObject)
        {
            return psObject.Properties.ToDictionary(p => p.Name.ToLowerInvariant(), p => p.Value);
        }

        #endregion

    }
}

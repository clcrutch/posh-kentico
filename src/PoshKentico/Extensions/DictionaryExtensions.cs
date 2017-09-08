// <copyright file="DictionaryExtensions.cs" company="Chris Crutchfield">
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
    /// Extension methods for <see cref="Dictionary{TKey, TValue}"/> objects.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Converts a <see cref="Dictionary{TKey, TValue}"/> into a <see cref="PSObject"/>.
        /// </summary>
        /// <param name="dictionary"><see cref="Dictionary{TKey, TValue}"/> to convert to a <see cref="PSObject"/>.</param>
        /// <returns>A <see cref="PSObject"/> containing the same properties as the <see cref="Dictionary{TKey, TValue}"/>.</returns>
        public static PSObject ToPSObject(this Dictionary<string, object> dictionary)
        {
            if (dictionary == null)
            {
                return null;
            }

            var outputObject = new PSObject();
            var psNoteProperties = from p in dictionary
                                   select new PSNoteProperty(p.Key, p.Value);

            foreach (var noteProperty in psNoteProperties)
            {
                outputObject.Properties.Add(noteProperty);
            }

            return outputObject;
        }
    }
}

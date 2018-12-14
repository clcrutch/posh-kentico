// <copyright file="EnumerableExtensions.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Resource;

namespace PoshKentico.Core.Extensions
{
    /// <summary>
    /// Resource extensions.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Flattens a collection of collections into a single collection.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="items">The collection the extension is applied on.</param>
        /// <param name="flattenItems">The collection to be flattened.</param>
        /// <returns>A flattened collection. </returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> items, Func<T, IEnumerable<T>> flattenItems)
        {
            return items.SelectMany(c => flattenItems(c).Flatten(flattenItems)).Concat(items);
        }
    }
}

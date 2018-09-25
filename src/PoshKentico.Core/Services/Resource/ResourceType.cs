// <copyright file="ResourceType.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Core.Services.Resource
{
    /// <summary>
    /// Identified <see cref="IResourceInfo"/> as a Container or Item.
    /// </summary>
    public enum ResourceType
    {
        /// <summary>
        /// Items are considered the smallest unit in your drive. Think of an item like a file in a file system.
        /// </summary>
        Item,

        /// <summary>
        /// A container can store a collection of items and containers. Think of containers like a folder in a file sytstem.
        /// </summary>
        Container,
    }
}

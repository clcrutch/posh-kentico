// <copyright file="ResourceItem.cs" company="Chris Crutchfield">
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
    /// The concreate implementation of <see cref="IResourceInfo"/>
    /// </summary>
    public class Resource : IResourceInfo
    {
        /// <inheritdoc />
        public bool IsContainer { get; set; }

        /// <inheritdoc />
        public string ContainerPath { get; set; }

        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string Path { get; set; }

        /// <inheritdoc />
        public DateTime CreationTime { get; set; }

        /// <inheritdoc />
        public DateTime LastWriteTime { get; set; }

        /// <inheritdoc />
        public ResourceType ResourceType { get; set; }

        /// <inheritdoc />
        public IEnumerable<IResourceInfo> Children { get; set; }
    }
}

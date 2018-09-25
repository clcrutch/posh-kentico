// <copyright file="IResourceInfo.cs" company="Chris Crutchfield">
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
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.Resource
{
    /// <summary>
    /// Represents a <see cref="Resource"/>.
    /// </summary>
    public interface IResourceInfo
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the resource is a container.
        /// </summary>
        bool IsContainer { get; set; }

        /// <summary>
        /// Gets or sets the full path to the container this resource resides in.
        /// </summary>
        string ContainerPath { get; set; }

        /// <summary>
        /// Gets or sets the name of the container.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the full path to this resource.
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// Gets or sets the date this resource was creatd.
        /// </summary>
        DateTime CreationTime { get; set; }

        /// <summary>
        /// Gets or sets the last time this resource was written to.
        /// </summary>
        DateTime LastWriteTime { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ResourceType"/>.
        /// </summary>
        ResourceType ResourceType { get; set; }

        /// <summary>
        /// Gets or sets the child resources. Only applicable if the resource is a container.
        /// </summary>
        IEnumerable<IResourceInfo> Children { get; set; }
        #endregion
    }
}

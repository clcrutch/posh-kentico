// <copyright file="IResourceReaderWriterService.cs" company="Chris Crutchfield">
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.Resource
{
    /// <summary>
    /// Service used to read from or write to a resource item
    /// </summary>
    public interface IResourceReaderWriterService
    {
        /// <summary>
        /// Full path to a resource item
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Closes the reader/writer
        /// </summary>
        void Close();

        /// <summary>
        /// Disposes the reader/writer
        /// </summary>
        void Dispose();

        /// <summary>
        /// Initializes the reader/wrtier
        /// </summary>
        /// <param name="resourceService">Service used to do the actual reading and writting</param>
        /// <param name="path">Full path to the resource item</param>
        void Initialize(IResourceService resourceService, string path);

        /// <summary>
        /// Reads content from resource item
        /// </summary>
        /// <returns>Items read from the resource item</returns>
        byte[] Read();

        /// <summary>
        /// Writes content to a resource item
        /// </summary>
        /// <param name="content"></param>
        /// <returns>Items written to the resource item</returns>
        byte[] Write(byte[] content);
    }
}

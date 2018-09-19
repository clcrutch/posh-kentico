// <copyright file="KenticoResourceReaderWriterService.cs" company="Chris Crutchfield">
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

using System.ComponentModel.Composition;
using PoshKentico.Core.Services.Resource;

namespace PoshKentico.Core.Providers.Resource
{
    /// <summary>
    /// Implementation of <see cref="IFileSystemReaderWriterService"/> that uses Kentico
    /// </summary>
    [Export(typeof(IFileSystemReaderWriterService))]
    public class KenticoResourceReaderWriterService : IResourceReaderWriterService
    {
        private bool isWriting;

        private bool finishedReading;

        private IResourceService ResourceService { get; set; }

#pragma warning disable SA1202 // Elements should be ordered by access
                              /// <inheritdoc />
        public string Path { get; private set; }
#pragma warning restore SA1202 // Elements should be ordered by access

        /// <inheritdoc />
        public void Close()
        {
        }

        /// <inheritdoc />
        public void Dispose()
        {
        }

        /// <inheritdoc />
        public byte[] Read()
        {
            this.ResourceService.ClearAttributes(this.Path);

            return this.ResourceService.Read(this.Path, ref this.finishedReading);
        }

        /// <inheritdoc />
        public byte[] Write(byte[] content)
        {
            this.ResourceService.ClearAttributes(this.Path);

            return this.ResourceService.Write(this.Path, content, ref this.isWriting);
        }

        /// <inheritdoc />
        public void Initialize(IResourceService resourceService, string path)
        {
            this.ResourceService = resourceService;
            this.Path = path;
        }
    }
}

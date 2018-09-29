// <copyright file="ResourceContentReaderWriter.cs" company="Chris Crutchfield">
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

using System.Collections;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;
using PoshKentico.Core.Services.Resource;

namespace PoshKentico.CmdletProviders.Resource
{
    /// <summary>
    /// Used by providers for reading and writing content.
    /// </summary>
    public class ResourceContentReaderWriter : IContentWriter, IContentReader
    {
        /// <summary>
        /// <see cref="IResourceReaderWriter"/>.
        /// </summary>
        private IResourceReaderWriter ReadWriteService { get; set; }

#pragma warning disable SA1201 // Elements should appear in the correct order
                              /// <summary>
                              /// Initializes a new instance of the <see cref="ResourceContentReaderWriter"/> class.
                              /// </summary>
                              /// <param name="readWriteService">The <see cref="IResourceReaderWriter"/>.</param>
        public ResourceContentReaderWriter(IResourceReaderWriter readWriteService)
#pragma warning restore SA1201 // Elements should appear in the correct order
        {
            this.ReadWriteService = readWriteService;
        }

        /// <inheritdoc/>
        public void Close()
        {
            this.ReadWriteService.Close();
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.ReadWriteService.Dispose();
        }

        /// <inheritdoc/>
        public IList Read(long readCount)
        {
            ArrayList list = new ArrayList();

            var content = this.ReadWriteService.Read();

            if (content != null)
            {
                list.Add(content);
            }

            return list;
        }

        /// <inheritdoc/>
        public void Seek(long offset, SeekOrigin origin)
        {
            throw new PSNotSupportedException();
        }

        /// <inheritdoc/>
        public IList Write(IList content)
        {
            return this.ReadWriteService.Write(content.Cast<byte>().ToArray());
        }
    }
}

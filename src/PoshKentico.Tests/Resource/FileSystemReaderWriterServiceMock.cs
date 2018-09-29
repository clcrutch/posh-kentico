// <copyright file="FileSystemReaderWriterServiceMock.cs" company="Chris Crutchfield">
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
using FluentAssertions;
using Moq;
using PoshKentico.Core.Services.Resource;

namespace PoshKentico.Tests.Resource
{
    public class FileSystemReaderWriterServiceMock : IFileSystemReaderWriterService
    {
        private IList content;
        private IResourceService resourceService;

        public string Path { get; set; }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Initialize(IResourceService resourceService, string path)
        {
            this.resourceService = resourceService;
            this.Path = path;

            this.Path.Should().NotBeNullOrEmpty();
            this.Path.Should().NotBeNullOrWhiteSpace();
            this.resourceService.Should().NotBeNull();
        }

        public IList Read()
        {
            return new ArrayList();
        }

        public IList Write(IList expectedContent)
        {
            this.content = expectedContent;

            this.content.Should().BeOfType(typeof(byte));
            this.content.Should().NotBeNull();
            this.content.Count.Should().Be(expectedContent.Count);

            return this.content;
        }
    }
}

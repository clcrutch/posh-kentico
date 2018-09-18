﻿// <copyright file="KenticoResourceReaderWriterService.cs" company="Chris Crutchfield">
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
    [Export(typeof(IFileSystemReaderWriterService))]
    public class KenticoResourceReaderWriterService : IResourceReaderWriterService
    {
        private bool _isWriting;
        private bool _finishedReading;
        public string Path { get; private set; }

        IResourceService ResourceService { get; set; }

        public void Close()
        {
        }

        public void Dispose()
        {
        }

        public byte[] Read()
        {
            ResourceService.ClearAttributes(Path);

            return ResourceService.Read(Path, ref _finishedReading);
        }

        public byte[] Write(byte[] content)
        {
            ResourceService.ClearAttributes(Path);

            return ResourceService.Write(Path, content, ref _isWriting);
        }

        public void Initialize(IResourceService resourceService, string path)
        {
            ResourceService = resourceService;
            Path = path;
        }
    }
}
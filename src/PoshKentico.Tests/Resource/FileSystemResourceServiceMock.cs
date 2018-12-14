// <copyright file="FileSystemResourceServiceMock.cs" company="Chris Crutchfield">
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
using PoshKentico.Business;
using PoshKentico.Core.Services.Resource;

namespace PoshKentico.Tests.Resource
{
    public class FileSystemResourceServiceMock : CmdletProviderBusinessBase
    {
        public IResourceInfo Create(IResourceInfo resource)
        {
            var testResource = new Mock<IResourceInfo>();
            testResource.SetupGet(i => i.IsContainer).Returns(resource.IsContainer);
            testResource.SetupGet(i => i.ContainerPath).Returns(resource.ContainerPath);
            testResource.SetupGet(i => i.Name).Returns(resource.Name);
            testResource.SetupGet(i => i.Path).Returns(resource.Path);
            testResource.SetupGet(i => i.CreationTime).Returns(resource.CreationTime);
            testResource.SetupGet(i => i.LastWriteTime).Returns(resource.LastWriteTime);
            testResource.SetupGet(i => i.ResourceType).Returns(resource.ResourceType);
            testResource.SetupGet(i => i.Children).Returns(resource.Children);

            return testResource.Object;
        }
    }
}

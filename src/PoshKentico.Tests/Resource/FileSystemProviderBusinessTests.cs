// <copyright file="FileSystemProviderBusinessTests.cs" company="Chris Crutchfield">
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
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Resource;
using PoshKentico.Core.Services.Resource;
using PoshKentico.Tests.Helpers;

namespace PoshKentico.Tests.Resource
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class FileSystemProviderBusinessTests
    {
        private KenticoFileSystemBusiness businessLayer;

        private Mock<IFileSystemResourceService> resourceService;
        private Mock<IFileSystemReaderWriterService> fileSystemReaderWriterMock;
        private List<Mock<IResourceInfo>> resourceMocks;

        [TestCase(@"C:\container1")]
        [TestCase(@"C:\container1\File1.txt")]
        public void FileSystemProvider_Exists(string path)
        {
            var service = new Mock<IFileSystemResourceService>();

            service.Setup(i => i.Exists(path)).Returns(true);

            var businessLayer = new KenticoFileSystemBusiness()
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
                ResourceService = service.Object,
            };

            businessLayer.Exists(path);

            service.Verify(i => i.Exists(path));
        }

        public void FileSystemProvider_CreateFile()
        {
            var path = @"C:\container1\file1.txt";
            var resourceType = ResourceType.Item.ToString();
            var content = "some content";

            this.resourceService.Setup(i => i.CreateItem(path, content));

            this.businessLayer.CreateResource(path, resourceType, content);

            this.resourceService.Verify(i => i.CreateItem(path, content));
        }

        public void FileSystemProvider_CreateDirectory()
        {
            var path = @"C:\container1";
            var resourceType = ResourceType.Container.ToString();

            this.resourceService.Setup(i => i.CreateContainer(path));

            this.businessLayer.CreateResource(path, resourceType, null);

            this.resourceService.Verify(i => i.CreateContainer(path));
        }

        public void FileSystemProvider_DeleteFile()
        {
            var fileMock = this.resourceMocks.First(i => i.Object.Path.Equals(@"C:\container1\file1.txt", StringComparison.OrdinalIgnoreCase));
            var path = fileMock.Object.Path;

            this.resourceService.Setup(i => i.Exists(path)).Returns(true);
            this.resourceService.Setup(i => i.IsContainer(path)).Returns(false);
            this.resourceService.Setup(i => i.DeleteItem(path));

            var result = this.businessLayer.Delete(path);

            this.resourceService.Verify(i => i.Exists(path));
            this.resourceService.Verify(i => i.IsContainer(path));
            this.resourceService.Verify(i => i.DeleteItem(path));

            result.Should().BeTrue();
        }

        [TestCase(true)]
        [TestCase(false)]
        public void FileSystemProvider_DeleteDirectory(bool recurse)
        {
            var directoryMock = this.resourceMocks.First(i => i.Object.Path.Equals(@"C:\container1\container2", StringComparison.OrdinalIgnoreCase));
            var path = directoryMock.Object.Path;

            if (recurse)
            {
                directoryMock.Object.Children = null;
            }

            this.resourceService.Setup(i => i.Exists(path)).Returns(true);
            this.resourceService.Setup(i => i.IsContainer(path)).Returns(true);
            this.resourceService.Setup(i => i.DeleteContainer(path, recurse));

            var result = this.businessLayer.Delete(path, recurse);

            this.resourceService.Verify(i => i.Exists(path));
            this.resourceService.Verify(i => i.IsContainer(path));
            this.resourceService.Verify(i => i.DeleteContainer(path, recurse));

            result.Should().BeTrue();
        }

        public void FileSystemProvider_GetFileResource()
        {
            var fileMock = this.resourceMocks.First(i => i.Object.Path.Equals(@"C:\container1\file1.txt", StringComparison.OrdinalIgnoreCase));
            var path = fileMock.Object.Path;

            this.resourceService.Setup(i => i.IsContainer(path)).Returns(false);
            this.resourceService.Setup(i => i.GetItem(path)).Returns(fileMock.Object);

            this.businessLayer.GetResource(path, false);

            this.resourceService.Verify(i => i.IsContainer(path));
            this.resourceService.Verify(i => i.GetItem(path));
        }

        [TestCase]
        public void FileSystemProvider_GetDirectory()
        {
            var directoryMock = this.resourceMocks.First(i => i.Object.Path.Equals(@"C:\container1", StringComparison.OrdinalIgnoreCase));

            var service = new Mock<IFileSystemResourceService>();

            this.resourceService.Setup(i => i.IsContainer(directoryMock.Object.Path)).Returns(true);
            this.resourceService.Setup(i => i.GetContainer(directoryMock.Object.Path, true)).Returns(directoryMock.Object);

            var result = this.businessLayer.GetResource(directoryMock.Object.Path, true);

            this.resourceService.Verify(i => i.IsContainer(directoryMock.Object.Path));
            this.resourceService.Verify(i => i.GetContainer(directoryMock.Object.Path, true));

            result.Children.Should().NotBeNullOrEmpty();
        }

        [TestCase]
        public void FileSystemProvider_GetChildren()
        {
            var directoryMock = this.resourceMocks.First(i => i.Object.Path.Equals(@"C:\container1", StringComparison.OrdinalIgnoreCase));
            var path = directoryMock.Object.Path;

            this.resourceService.Setup(i => i.GetChildren(path, true)).Returns(directoryMock.Object.Children);

            var result = this.businessLayer.GetChildren(path, true);

            result.Should().NotBeNullOrEmpty();
            this.resourceService.Verify(i => i.GetChildren(path, true));
        }

        [TestCase]
        public void FileSystemProvider_GetFileProperties()
        {
            var fileMock = this.resourceMocks.First(i => i.Object.Path.Equals(@"C:\container1\file1.txt", StringComparison.OrdinalIgnoreCase));
            var path = fileMock.Object.Path;
            var properties = new Collection<string>()
            {
                "IsContainer",
                "ContainerPath",
                "Name",
                "Path",
                "CreationTime",
                "LastWriteTime",
            };

            this.resourceService.Setup(i => i.IsContainer(path)).Returns(false);
            this.resourceService.Setup(i => i.GetItem(path)).Returns(fileMock.Object);

            var result = this.businessLayer.GetProperty(path, properties);

            this.resourceService.Verify(i => i.IsContainer(path));
            this.resourceService.Verify(i => i.GetItem(path));

            result["IsContainer"].As<bool>().Should().BeFalse();
            result["ContainerPath"].As<string>().Should().BeEquivalentTo(fileMock.Object.ContainerPath);
            result["Name"].As<string>().Should().BeEquivalentTo(fileMock.Object.Name);
            result["Path"].As<string>().Should().BeEquivalentTo(fileMock.Object.Path);
            result["CreationTime"].As<DateTime>().Should().Be(fileMock.Object.CreationTime);
            result["LastWriteTime"].As<DateTime>().Should().Be(fileMock.Object.LastWriteTime);
            result.Should().NotContainKey("TotalContainers");
            result.Should().NotContainKey("TotalItems");
            result.Should().NotContainKey("TotalResources");
        }

        [TestCase]
        public void FileSystemProvider_GetDirectoryProperties()
        {
            var folderMock = this.resourceMocks.First(i => i.Object.Path.Equals(@"C:\container1", StringComparison.OrdinalIgnoreCase));
            var path = folderMock.Object.Path;
            var properties = new Collection<string>()
            {
                "IsContainer",
                "ContainerPath",
                "Name",
                "Path",
                "CreationTime",
                "LastWriteTime",
                "TotalContainers",
                "TotalItems",
                "TotalResources",
            };

            this.resourceService.Setup(i => i.IsContainer(path)).Returns(false);
            this.resourceService.Setup(i => i.GetItem(path)).Returns(folderMock.Object);

            var result = this.businessLayer.GetProperty(path, properties);

            this.resourceService.Verify(i => i.IsContainer(path));
            this.resourceService.Verify(i => i.GetItem(path));

            result["IsContainer"].As<bool>().Should().BeTrue();
            result["ContainerPath"].As<string>().Should().BeEquivalentTo(folderMock.Object.ContainerPath);
            result["Name"].As<string>().Should().BeEquivalentTo(folderMock.Object.Name);
            result["Path"].As<string>().Should().BeEquivalentTo(folderMock.Object.Path);
            result["CreationTime"].As<DateTime>().Should().Be(folderMock.Object.CreationTime);
            result["LastWriteTime"].As<DateTime>().Should().Be(folderMock.Object.LastWriteTime);
            result["TotalContainers"].As<int>().Should().Be(folderMock.Object.Children.Where(i => i.IsContainer).Count());
            result["TotalItems"].As<int>().Should().Be(folderMock.Object.Children.Where(i => !i.IsContainer).Count());
            result["TotalResources"].As<int>().Should().Be(folderMock.Object.Children.Count());
        }

        [TestCase]
        public void FileSystemProvider_PurgeUnwantedProperties()
        {
            var directoryMock = this.resourceMocks.First(i => i.Object.Path.Equals(@"C:\container1", StringComparison.OrdinalIgnoreCase));

            Collection<string> propertyPickList = new Collection<string>()
            {
                "IsContainer",
                "ContainerPath",
                "Name",
                "Path",
                "CreationTime",
                "LastWriteTime",
                "ResourceType",
            };

            var properties = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                { "IsContainer", directoryMock.Object.IsContainer },
                { "ContainerPath", directoryMock.Object.ContainerPath },
                { "Name", directoryMock.Object.Name },
                { "Path", directoryMock.Object.Path },
                { "CreationTime", directoryMock.Object.CreationTime },
                { "LastWriteTime", directoryMock.Object.LastWriteTime },
                { "ResourceType", directoryMock.Object.ResourceType },
                { "TotalContainers", directoryMock.Object.Children.Where(i => i.IsContainer).Count() },
                { "TotalItems", directoryMock.Object.Children.Where(i => !i.IsContainer).Count() },
                { "TotalResources", directoryMock.Object.Children.Count() },
            };

            this.businessLayer.PurgeUnwantedProperties(propertyPickList, properties);

            properties.Should().HaveCount(7);

            foreach (var removedProperty in propertyPickList)
            {
                properties.ContainsKey(removedProperty).Should().BeTrue();
            }
        }

        [TestCase]
        public void FileSystemProvider_SetProperty()
        {
            var fileMock = this.resourceMocks.First(i => i.Object.Path.Equals(@"C:\container1\file1.txt", StringComparison.OrdinalIgnoreCase));
            var path = fileMock.Object.Path;
            Dictionary<string, object> propertyValueMock = new Dictionary<string, object>()
            {
                { "content", "some updated content" },
            };

            this.resourceService.Setup(i => i.IsContainer(path)).Returns(false);
            this.resourceService.Setup(i => i.CreateItem(path, propertyValueMock["content"] as string));

            this.businessLayer.SetProperty(path, propertyValueMock);

            this.resourceService.Verify(i => i.IsContainer(path));
            this.resourceService.Verify(i => i.CreateItem(path, propertyValueMock["content"] as string));
        }

        [TestCase]
        public void FileSystemProvider_ExpandPath()
        {
            var directoryMock = this.resourceMocks.First(i => i.Object.Path.Equals(@"C:\container1", StringComparison.OrdinalIgnoreCase));
            var path = directoryMock.Object.Path;

            this.resourceService.Setup(i => i.IsContainer(path)).Returns(true);
            this.resourceService.Setup(i => i.GetContainer(path, false)).Returns(directoryMock.Object);
            this.resourceService.Setup(i => i.GetName(It.IsAny<string>())).Returns("*file*");

            var results = this.businessLayer.ExpandPath(@"C:\container1\*file*", path);

            this.resourceService.Verify(i => i.IsContainer(path));
            this.resourceService.Verify(i => i.GetContainer(path, false));
            this.resourceService.Verify(i => i.GetName(It.IsAny<string>()));

            results.Should().NotBeNullOrEmpty();
            results.Should().HaveCount(2);

            foreach (var item in results)
            {
                item.Should().Contain("file");
            }
        }

        [TestCase]
        public void FileSystemProvider_NormalizePath()
        {
            var path1 = @"C:\container1\file1.txt";
            var path2 = "C:/container1/file1.txt";
            var actualPath = @"C:\container1\file1.txt";

            var path1Result = this.businessLayer.NormalizeRelativePath(path1, null);
            var path2Result = this.businessLayer.NormalizeRelativePath(path2, null);

            path1Result.Should().Be(actualPath);
            path2Result.Should().Be(actualPath);
        }

        [TestCase]
        public void FileSystemProvider_IsContainer()
        {
            var directoryMock = this.resourceMocks.First(i => i.Object.Path.Equals(@"C:\container1", StringComparison.OrdinalIgnoreCase));
            var path = directoryMock.Object.Path;

            this.resourceService.Setup(i => i.IsContainer(path)).Returns(true);
            var result = this.businessLayer.IsContainer(path);

            this.resourceService.Verify(i => i.IsContainer(path));
            result.Should().BeTrue();
        }

        [TestCase]
        public void FileSystemProvider_CopyFile()
        {
            var fileMock = this.resourceMocks.First(i => i.Object.Path.Equals(@"C:\container1\file1.txt", StringComparison.OrdinalIgnoreCase));
            var sourcePath = fileMock.Object.Path;
            var destinationPath = @"C:\container1\container2\file999.txt";

            this.resourceService.Setup(i => i.IsContainer(sourcePath)).Returns(false);
            this.resourceService.Setup(i => i.GetItem(sourcePath)).Returns(fileMock.Object);
            this.resourceService.Setup(i => i.IsAbsolutePath(destinationPath)).Returns(true);
            this.resourceService.Setup(i => i.CopyResourceItem(sourcePath, destinationPath));

            this.businessLayer.CopyItem(sourcePath, destinationPath, false);

            this.resourceService.Verify(i => i.IsContainer(sourcePath));
            this.resourceService.Verify(i => i.GetItem(sourcePath));
            this.resourceService.Verify(i => i.IsAbsolutePath(destinationPath));
            this.resourceService.Verify(i => i.CopyResourceItem(sourcePath, destinationPath));
        }

        [TestCase]
        public void FileSystemProvider_CopyDirectory()
        {
            var directoryMock = this.resourceMocks.First(i => i.Object.Path.Equals(@"C:\container1", StringComparison.OrdinalIgnoreCase));
            var sourcePath = directoryMock.Object.Path;
            var destinationPath = @"C:\container1\container2\container3";

            this.resourceService.Setup(i => i.IsContainer(sourcePath)).Returns(true);
            this.resourceService.Setup(i => i.GetContainer(It.IsAny<string>(), It.IsAny<bool>())).Returns(directoryMock.Object);
            this.resourceService.Setup(i => i.JoinPath(sourcePath, destinationPath)).Returns(destinationPath);
            this.resourceService.Setup(i => i.CreateContainer(It.IsAny<string>()));
            this.resourceService.Setup(i => i.CopyResourceItem(It.IsAny<string>(), It.IsAny<string>()));

            this.businessLayer.CopyItem(sourcePath, destinationPath, false);

            this.resourceService.Verify(i => i.IsContainer(sourcePath));
            this.resourceService.Verify(i => i.GetContainer(It.IsAny<string>(), It.IsAny<bool>()));
            this.resourceService.Verify(i => i.JoinPath(sourcePath, destinationPath));
            this.resourceService.Verify(i => i.CreateContainer(It.IsAny<string>()));
            this.resourceService.Verify(i => i.CopyResourceItem(It.IsAny<string>(), It.IsAny<string>()));
        }

        [SetUp]
        public void Setup()
        {
            this.ServiceSetup();
            this.SetupBusinessLayer();
            this.SetupMockResources();
        }

        private void ServiceSetup()
        {
            this.resourceService = new Mock<IFileSystemResourceService>();
            this.fileSystemReaderWriterMock = new Mock<IFileSystemReaderWriterService>();
        }

        private void SetupBusinessLayer()
        {
            this.businessLayer = new KenticoFileSystemBusiness()
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
                ResourceService = this.resourceService.Object,
            };
        }

        private void SetupMockResources()
        {
            var mockRootContainer = new Mock<IResourceInfo>();

            mockRootContainer.SetupGet(i => i.IsContainer).Returns(true);
            mockRootContainer.SetupGet(i => i.ContainerPath).Returns(@"C:\");
            mockRootContainer.SetupGet(i => i.Name).Returns("container1");
            mockRootContainer.SetupGet(i => i.Path).Returns(@"C:\container1");
            mockRootContainer.SetupGet(i => i.CreationTime).Returns(DateTime.Now);
            mockRootContainer.SetupGet(i => i.LastWriteTime).Returns(DateTime.Now);
            mockRootContainer.SetupGet(i => i.ResourceType).Returns(ResourceType.Container);

            var mockResourceItem1 = new Mock<IResourceInfo>();

            mockResourceItem1.SetupGet(i => i.IsContainer).Returns(false);
            mockResourceItem1.SetupGet(i => i.ContainerPath).Returns(mockRootContainer.Object.Path);
            mockResourceItem1.SetupGet(i => i.Name).Returns(@"file1.txt");
            mockResourceItem1.SetupGet(i => i.Path).Returns($@"{mockRootContainer.Object.Path}\file1.txt");
            mockResourceItem1.SetupGet(i => i.CreationTime).Returns(DateTime.Now);
            mockResourceItem1.SetupGet(i => i.LastWriteTime).Returns(DateTime.Now);
            mockResourceItem1.SetupGet(i => i.ResourceType).Returns(ResourceType.Item);

            var mockResourceItem2 = new Mock<IResourceInfo>();

            mockResourceItem2.SetupGet(i => i.IsContainer).Returns(false);
            mockResourceItem2.SetupGet(i => i.ContainerPath).Returns(mockRootContainer.Object.Path);
            mockResourceItem2.SetupGet(i => i.Name).Returns(@"file2.txt");
            mockResourceItem2.SetupGet(i => i.Path).Returns($@"{mockRootContainer.Object.Path}\file2.txt");
            mockResourceItem2.SetupGet(i => i.CreationTime).Returns(DateTime.Now);
            mockResourceItem2.SetupGet(i => i.LastWriteTime).Returns(DateTime.Now);
            mockResourceItem2.SetupGet(i => i.ResourceType).Returns(ResourceType.Item);

            var mockResourceContainer1 = new Mock<IResourceInfo>();

            mockResourceContainer1.SetupGet(i => i.IsContainer).Returns(true);
            mockResourceContainer1.SetupGet(i => i.ContainerPath).Returns(mockRootContainer.Object.Path);
            mockResourceContainer1.SetupGet(i => i.Name).Returns("container2");
            mockResourceContainer1.SetupGet(i => i.Path).Returns($@"{mockRootContainer.Object.Path}\container2");
            mockResourceContainer1.SetupGet(i => i.CreationTime).Returns(DateTime.Now);
            mockResourceContainer1.SetupGet(i => i.LastWriteTime).Returns(DateTime.Now);
            mockResourceContainer1.SetupGet(i => i.ResourceType).Returns(ResourceType.Container);

            var mockResourceItem3 = new Mock<IResourceInfo>();

            mockResourceItem3.SetupGet(i => i.IsContainer).Returns(false);
            mockResourceItem3.SetupGet(i => i.ContainerPath).Returns(mockResourceContainer1.Object.Path);
            mockResourceItem3.SetupGet(i => i.Name).Returns("file1.txt");
            mockResourceItem3.SetupGet(i => i.Path).Returns($@"{mockResourceContainer1.Object.Path}\file1.txt");
            mockResourceItem3.SetupGet(i => i.CreationTime).Returns(DateTime.Now);
            mockResourceItem3.SetupGet(i => i.LastWriteTime).Returns(DateTime.Now);
            mockResourceItem3.SetupGet(i => i.ResourceType).Returns(ResourceType.Item);

            var mockResourceItem4 = new Mock<IResourceInfo>();

            mockResourceItem4.SetupGet(i => i.IsContainer).Returns(false);
            mockResourceItem4.SetupGet(i => i.ContainerPath).Returns(mockResourceContainer1.Object.Path);
            mockResourceItem4.SetupGet(i => i.Name).Returns("file2.txt");
            mockResourceItem4.SetupGet(i => i.Path).Returns($@"{mockResourceContainer1.Object.Path}\file2.txt");
            mockResourceItem4.SetupGet(i => i.CreationTime).Returns(DateTime.Now);
            mockResourceItem4.SetupGet(i => i.LastWriteTime).Returns(DateTime.Now);
            mockResourceItem4.SetupGet(i => i.ResourceType).Returns(ResourceType.Item);

            mockResourceContainer1.SetupGet(i => i.Children).Returns(new IResourceInfo[]
            {
                mockResourceItem3.Object,
                mockResourceItem4.Object,
            });

            mockRootContainer.SetupGet(i => i.Children).Returns(new IResourceInfo[]
            {
                mockResourceItem1.Object,
                mockResourceItem2.Object,
                mockResourceContainer1.Object,
            });

            this.resourceMocks = new List<Mock<IResourceInfo>>();

            this.resourceMocks.Add(mockRootContainer);
            this.resourceMocks.Add(mockResourceItem1);
            this.resourceMocks.Add(mockResourceItem2);
            this.resourceMocks.Add(mockResourceContainer1);
            this.resourceMocks.Add(mockResourceItem3);
            this.resourceMocks.Add(mockResourceItem4);
        }
    }
}

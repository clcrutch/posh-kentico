// <copyright file="GetCmsMediaLibraryFileTests.cs" company="Chris Crutchfield">
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

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.ContentManagement.MediaLibraries;
using PoshKentico.Core.Services.ContentManagement.MediaLibraries;

namespace PoshKentico.Tests.ContentManagement.MediaLibraries
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class GetCmsMediaLibraryFileTests
    {
        [Test]
        public void GetCmsMediaLibraryFileTest_NoParameters_None()
        {
            var libraryServiceMock = new Mock<IMediaLibraryService>();
            var businessLayer = new GetCmsMediaLibraryFileBusiness
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                MediaLibraryService = libraryServiceMock.Object,
            };

            businessLayer.GetMediaFiles().Should().BeEmpty();

            libraryServiceMock.Verify(x => x.MediaFiles);
        }

        [Test]
        public void GetCmsMediaLibraryFileTest_NoParameters_All()
        {
            var libraryServiceMock = new Mock<IMediaLibraryService>();

            var libraries = new List<IMediaLibrary>();
            var libraryMock1 = new Mock<IMediaLibrary>();
            libraryMock1.SetupGet(x => x.LibraryName).Returns("MyLibrary1");
            libraryMock1.SetupGet(x => x.LibraryDisplayName).Returns("My Library1");
            libraryMock1.SetupGet(x => x.LibraryFolder).Returns("images1");
            libraryMock1.SetupGet(x => x.LibrarySiteID).Returns(1);
            libraryMock1.SetupGet(x => x.LibraryID).Returns(2);
            libraries.Add(libraryMock1.Object);

            var fileMock1 = new Mock<IMediaFile>();
            fileMock1.SetupGet(x => x.FileName).Returns("Image1");
            fileMock1.SetupGet(x => x.FileTitle).Returns("File title 1");
            fileMock1.SetupGet(x => x.FileDescription).Returns("This file was added through the API.");
            fileMock1.SetupGet(x => x.FilePath).Returns("NewFolder/Image1");
            fileMock1.SetupGet(x => x.FileLibraryID).Returns(2);

            var fileMock2 = new Mock<IMediaFile>();
            fileMock2.SetupGet(x => x.FileName).Returns("Image2");
            fileMock2.SetupGet(x => x.FileTitle).Returns("File title 2");
            fileMock2.SetupGet(x => x.FileDescription).Returns("This file was added through the API.");
            fileMock2.SetupGet(x => x.FilePath).Returns("NewFolder/Image2");
            fileMock2.SetupGet(x => x.FileLibraryID).Returns(2);

            libraryServiceMock.SetupGet(x => x.MediaLibraries).Returns(libraries);
            libraryServiceMock.SetupGet(x => x.MediaFiles).Returns(new IMediaFile[] { fileMock1.Object, fileMock2.Object });

            var businessLayer = new GetCmsMediaLibraryFileBusiness
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                MediaLibraryService = libraryServiceMock.Object,
            };

            businessLayer.GetMediaFiles().Should().NotBeNullOrEmpty().And.HaveCount(2);
            libraryServiceMock.Verify(x => x.MediaFiles);
        }

        [Test]
        public void GetCmsMediaLibraryFileTest_MatchString_ExactFalse()
        {
            var libraryServiceMock = new Mock<IMediaLibraryService>();

            var libraries = new List<IMediaLibrary>();
            var libraryMock1 = new Mock<IMediaLibrary>();
            libraryMock1.SetupGet(x => x.LibraryName).Returns("MyLibrary1");
            libraryMock1.SetupGet(x => x.LibraryDisplayName).Returns("My Library1");
            libraryMock1.SetupGet(x => x.LibraryFolder).Returns("images1");
            libraryMock1.SetupGet(x => x.LibrarySiteID).Returns(1);
            libraryMock1.SetupGet(x => x.LibraryID).Returns(2);
            libraries.Add(libraryMock1.Object);

            var fileMock1 = new Mock<IMediaFile>();
            fileMock1.SetupGet(x => x.FileName).Returns("Image1");
            fileMock1.SetupGet(x => x.FileExtension).Returns(".png");
            fileMock1.SetupGet(x => x.FileTitle).Returns("File title 1");
            fileMock1.SetupGet(x => x.FileDescription).Returns("This file was added through the API.");
            fileMock1.SetupGet(x => x.FilePath).Returns("NewFolder/Image1");
            fileMock1.SetupGet(x => x.FileLibraryID).Returns(2);

            var fileMock2 = new Mock<IMediaFile>();
            fileMock2.SetupGet(x => x.FileName).Returns("Image2");
            fileMock2.SetupGet(x => x.FileExtension).Returns(".png");
            fileMock2.SetupGet(x => x.FileTitle).Returns("File title 2");
            fileMock2.SetupGet(x => x.FileDescription).Returns("This file was added through the API.");
            fileMock2.SetupGet(x => x.FilePath).Returns("NewFolder/Image2");
            fileMock2.SetupGet(x => x.FileLibraryID).Returns(2);

            libraryServiceMock.SetupGet(x => x.MediaLibraries).Returns(libraries);
            libraryServiceMock.SetupGet(x => x.MediaFiles).Returns(new IMediaFile[] { fileMock1.Object, fileMock2.Object });

            var businessLayer = new GetCmsMediaLibraryFileBusiness
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                MediaLibraryService = libraryServiceMock.Object,
            };

            businessLayer.GetMediaFiles(2, ".png", "NewFolder", false).Should().NotBeNullOrEmpty().And.HaveCount(2);
            businessLayer.GetMediaFiles(2, ".png", "NewFolder/Image2", false).Should().NotBeNullOrEmpty().And.HaveCount(1);
            libraryServiceMock.Verify(x => x.MediaFiles);
        }

        [Test]
        public void GetCmsMediaLibraryFileTest__MatchString_ExactTrue()
        {
            var libraryServiceMock = new Mock<IMediaLibraryService>();

            var libraries = new List<IMediaLibrary>();
            var libraryMock1 = new Mock<IMediaLibrary>();
            libraryMock1.SetupGet(x => x.LibraryName).Returns("MyLibrary1");
            libraryMock1.SetupGet(x => x.LibraryDisplayName).Returns("My Library1");
            libraryMock1.SetupGet(x => x.LibraryFolder).Returns("images1");
            libraryMock1.SetupGet(x => x.LibrarySiteID).Returns(1);
            libraryMock1.SetupGet(x => x.LibraryID).Returns(2);
            libraries.Add(libraryMock1.Object);

            var fileMock1 = new Mock<IMediaFile>();
            fileMock1.SetupGet(x => x.FileName).Returns("Image1");
            fileMock1.SetupGet(x => x.FileExtension).Returns(".png");
            fileMock1.SetupGet(x => x.FileTitle).Returns("File title 1");
            fileMock1.SetupGet(x => x.FileDescription).Returns("This file was added through the API.");
            fileMock1.SetupGet(x => x.FilePath).Returns("NewFolder/Image1");
            fileMock1.SetupGet(x => x.FileLibraryID).Returns(2);

            var fileMock2 = new Mock<IMediaFile>();
            fileMock2.SetupGet(x => x.FileName).Returns("Image2");
            fileMock2.SetupGet(x => x.FileExtension).Returns(".png");
            fileMock2.SetupGet(x => x.FileTitle).Returns("File title 2");
            fileMock2.SetupGet(x => x.FileDescription).Returns("This file was added through the API.");
            fileMock2.SetupGet(x => x.FilePath).Returns("NewFolder/Image2");
            fileMock2.SetupGet(x => x.FileLibraryID).Returns(2);

            libraryServiceMock.SetupGet(x => x.MediaLibraries).Returns(libraries);
            libraryServiceMock.SetupGet(x => x.MediaFiles).Returns(new IMediaFile[] { fileMock1.Object, fileMock2.Object });

            var businessLayer = new GetCmsMediaLibraryFileBusiness
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                MediaLibraryService = libraryServiceMock.Object,
            };

            businessLayer.GetMediaFiles(2, ".png", "NewFolder", true).Should().BeNullOrEmpty();
            businessLayer.GetMediaFiles(2, ".png", "NewFolder/Image2", true).Should().NotBeNullOrEmpty().And.HaveCount(1);
            libraryServiceMock.Verify(x => x.MediaFiles);
        }

        [Test]
        public void GetCmsMediaLibraryFile_IDs()
        {
            var libraryServiceMock = new Mock<IMediaLibraryService>();

            var fileMock1 = new Mock<IMediaFile>();
            fileMock1.SetupGet(x => x.FileName).Returns("Image1");
            fileMock1.SetupGet(x => x.FileExtension).Returns(".png");
            fileMock1.SetupGet(x => x.FileTitle).Returns("File title 1");
            fileMock1.SetupGet(x => x.FileDescription).Returns("This file was added through the API.");
            fileMock1.SetupGet(x => x.FilePath).Returns("NewFolder/Image1");
            fileMock1.SetupGet(x => x.FileLibraryID).Returns(2);
            fileMock1.SetupGet(x => x.FileID).Returns(1);

            var fileMock2 = new Mock<IMediaFile>();
            fileMock2.SetupGet(x => x.FileName).Returns("Image2");
            fileMock2.SetupGet(x => x.FileExtension).Returns(".png");
            fileMock2.SetupGet(x => x.FileTitle).Returns("File title 2");
            fileMock2.SetupGet(x => x.FileDescription).Returns("This file was added through the API.");
            fileMock2.SetupGet(x => x.FilePath).Returns("NewFolder/Image2");
            fileMock2.SetupGet(x => x.FileLibraryID).Returns(2);
            fileMock2.SetupGet(x => x.FileID).Returns(2);

            libraryServiceMock.Setup(x => x.GetMediaFile(1)).Returns(fileMock1.Object);
            libraryServiceMock.Setup(x => x.GetMediaFile(2)).Returns(fileMock2.Object);

            var businessLayer = new GetCmsMediaLibraryFileBusiness
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                MediaLibraryService = libraryServiceMock.Object,
            };

            businessLayer.GetMediaFiles(1).Should().NotBeNullOrEmpty().And.HaveCount(1);
            businessLayer.GetMediaFiles(2).Should().NotBeNullOrEmpty().And.HaveCount(1);
            businessLayer.GetMediaFiles(3).Should().BeEmpty();
            businessLayer.GetMediaFiles(1, 2, 3).Should().NotBeNullOrEmpty().And.HaveCount(2);

            libraryServiceMock.Verify(x => x.GetMediaFile(1));
            libraryServiceMock.Verify(x => x.GetMediaFile(2));
            libraryServiceMock.Verify(x => x.GetMediaFile(3));
        }
    }
}

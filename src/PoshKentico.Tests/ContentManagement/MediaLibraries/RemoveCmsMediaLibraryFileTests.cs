// <copyright file="RemoveCmsMediaLibraryFileTests.cs" company="Chris Crutchfield">
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
using Moq;
using NUnit.Framework;
using PoshKentico.Business.ContentManagement.MediaLibraries;
using PoshKentico.Core.Providers.General;
using PoshKentico.Core.Services.ContentManagement.MediaLibraries;
using PoshKentico.Tests.Helpers;

namespace PoshKentico.Tests.ContentManagement.MediaLibraries
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class RemoveCmsMediaLibraryFileTests
    {
        [Test]
        public void RemoveCmsMediaLibraryTest_Object()
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

            var outputService = OutputServiceHelper.GetPassThruOutputService();
            PassThruOutputService.ShouldProcessFunction = (x, y) => true;

            var businessLayer = new RemoveCmsMediaLibraryFileBusiness
            {
                OutputService = outputService,

                MediaLibraryService = libraryServiceMock.Object,
            };

            businessLayer.RemoveMediaFile(fileMock1.Object);
            libraryServiceMock.Verify(x => x.DeleteMediaFile(fileMock1.Object));

            businessLayer.RemoveMediaFile(fileMock2.Object);
            libraryServiceMock.Verify(x => x.DeleteMediaFile(fileMock2.Object));
        }
    }
}

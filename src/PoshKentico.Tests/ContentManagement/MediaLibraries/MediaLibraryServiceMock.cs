// <copyright file="MediaLibraryServiceMock.cs" company="Chris Crutchfield">
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
using FluentAssertions;
using Moq;
using PoshKentico.Core.Services.ContentManagement.MediaLibraries;

namespace PoshKentico.Tests.ContentManagement.MediaLibraries
{
    public class MediaLibraryServiceMock : IMediaLibraryService
    {
        private IMediaLibrary libraryMock;

        public IEnumerable<IMediaLibrary> MediaLibraries => throw new System.NotImplementedException();

        public IMediaLibrary Create(IMediaLibrary library)
        {
            this.libraryMock = library;

            var testServer = new Mock<IMediaLibrary>();
            testServer.SetupGet(x => x.LibraryDisplayName).Returns(library.LibraryDisplayName);
            testServer.SetupGet(x => x.LibraryName).Returns(library.LibraryName);
            testServer.SetupGet(x => x.LibrarySiteID).Returns(library.LibrarySiteID);
            testServer.SetupGet(x => x.LibraryDescription).Returns(library.LibraryDescription);
            testServer.SetupGet(x => x.LibraryFolder).Returns(library.LibraryFolder);

            return testServer.Object;
        }

        public void VerifyCreate(IMediaLibrary expectedLibrary)
        {
            this.libraryMock.LibraryDisplayName.Should().Be(expectedLibrary.LibraryDisplayName);
            this.libraryMock.LibraryName.Should().Be(expectedLibrary.LibraryName);
            this.libraryMock.LibrarySiteID.Should().Be(expectedLibrary.LibrarySiteID);
            this.libraryMock.LibraryDescription.Should().Be(expectedLibrary.LibraryDescription);
            this.libraryMock.LibraryFolder.Should().Be(expectedLibrary.LibraryFolder);
        }

        public IMediaFile CreateMediaLibraryFile(int librarySiteID, string libraryName, string localFilePath, string fileName, string fileTitle, string fileDesc, string filePath)
        {
            throw new System.NotImplementedException();
        }

        public void CreateMediaLibraryFolder(int librarySiteID, string libraryName, string folderName)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(IMediaLibrary library)
        {
            throw new System.NotImplementedException();
        }

        public IMediaLibrary GetMediaLibrary(int id)
        {
            throw new System.NotImplementedException();
        }

        public IMediaLibrary GetMediaLibrary(int librarySiteID, string libraryName)
        {
            throw new System.NotImplementedException();
        }

        public IMediaLibrary Update(IMediaLibrary library, bool isReplace = true)
        {
            throw new System.NotImplementedException();
        }
    }
}
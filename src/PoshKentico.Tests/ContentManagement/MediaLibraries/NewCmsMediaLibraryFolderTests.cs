// <copyright file="NewCmsMediaLibraryFolderTests.cs" company="Chris Crutchfield">
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
using Moq;
using NUnit.Framework;
using PoshKentico.Business.ContentManagement.MediaLibraries;
using PoshKentico.Core.Services.ContentManagement.MediaLibraries;

namespace PoshKentico.Tests.ContentManagement.MediaLibraries
{
    [TestFixture]
    public class NewCmsMediaLibraryFolderTests
    {
        [Test]
        public void CreateCmsMediaLibraryFolderTest()
        {
            var libraryServiceMock = new Mock<IMediaLibraryService>();

            var libraries = new List<IMediaLibrary>();
            var libraryMock1 = new Mock<IMediaLibrary>();
            libraryMock1.SetupGet(x => x.LibraryName).Returns("MyLibrary1");
            libraryMock1.SetupGet(x => x.LibraryDisplayName).Returns("My Library1");
            libraryMock1.SetupGet(x => x.LibraryFolder).Returns("images1");
            libraryMock1.SetupGet(x => x.LibrarySiteID).Returns(1);
            libraries.Add(libraryMock1.Object);

            var libraryMock2 = new Mock<IMediaLibrary>();
            libraryMock2.SetupGet(x => x.LibraryName).Returns("MyLibrary2");
            libraryMock2.SetupGet(x => x.LibraryDisplayName).Returns("My Library2");
            libraryMock2.SetupGet(x => x.LibraryFolder).Returns("images2");
            libraryMock2.SetupGet(x => x.LibrarySiteID).Returns(1);
            libraries.Add(libraryMock2.Object);

            var businessLayer = new NewCmsMediaLibraryFolderBusiness
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                MediaLibraryService = libraryServiceMock.Object,
            };

            string folder = "NewFolder";
            businessLayer.CreateMediaLibraryFolder(1, "MyLibrary1", folder);
            libraryServiceMock.Verify(x => x.CreateMediaFolder(libraryMock1.Object.LibrarySiteID, libraryMock1.Object.LibraryName, folder));

            businessLayer.CreateMediaLibraryFolder(1, "MyLibrary2", folder);
            libraryServiceMock.Verify(x => x.CreateMediaFolder(libraryMock2.Object.LibrarySiteID, libraryMock2.Object.LibraryName, folder));
        }
    }
}

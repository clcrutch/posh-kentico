// <copyright file="RemoveCmsMediaLibraryFolderTests.cs" company="Chris Crutchfield">
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
    public class RemoveCmsMediaLibraryFolderTests
    {
        [Test]
        public void RemoveCmsMediaLibraryFolderTest()
        {
            var libraryServiceMock = new Mock<IMediaLibraryService>();

            var libraryMock1 = new Mock<IMediaLibrary>();
            libraryMock1.SetupGet(x => x.LibraryName).Returns("MyLibrary1");
            libraryMock1.SetupGet(x => x.LibraryDisplayName).Returns("My Library1");
            libraryMock1.SetupGet(x => x.LibraryFolder).Returns("images1");
            libraryMock1.SetupGet(x => x.LibrarySiteID).Returns(1);

            var businessLayer = new RemoveCmsMediaLibraryFolderBusiness
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                MediaLibraryService = libraryServiceMock.Object,
            };

            string folder = "NewFolder";
            businessLayer.RemoveMediaLibraryFolder(1, "MyLibrary1", folder);
            libraryServiceMock.Verify(x => x.DeleteMediaFolder(libraryMock1.Object.LibrarySiteID, libraryMock1.Object.LibraryName, folder));
        }
    }
}

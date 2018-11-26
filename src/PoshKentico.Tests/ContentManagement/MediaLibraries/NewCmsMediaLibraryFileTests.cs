// <copyright file="NewCmsMediaLibraryFileTests.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.ContentManagement.MediaLibraries;
using PoshKentico.Tests.Helpers;

namespace PoshKentico.Tests.ContentManagement.MediaLibraries
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class NewCmsMediaLibraryFileTests
    {
        [Test]
        public void CreateMediaLibraryFileTest()
        {
            var libraryServiceMock = new Mock<IMediaLibraryService>();

            var businessLayer = new NewCmsMediaLibraryFileBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                MediaLibraryService = libraryServiceMock.Object,
            };

            var libraries = new List<IMediaLibrary>();
            var libraryMock1 = new Mock<IMediaLibrary>();
            libraryMock1.SetupGet(x => x.LibraryName).Returns("MyLibrary1");
            libraryMock1.SetupGet(x => x.LibraryDisplayName).Returns("My Library1");
            libraryMock1.SetupGet(x => x.LibraryFolder).Returns("images1");
            libraryMock1.SetupGet(x => x.LibrarySiteID).Returns(1);
            libraries.Add(libraryMock1.Object);

            string localPath = "c:/Test";
            string fileName = "New File";
            string fileTitle = "New File Title";
            string fileDescription = "New File Description";
            string folder = "NewFolder";
            businessLayer.CreateMediaLibraryFile(1, "MyLibrary1", localPath, fileName, fileTitle, fileDescription, folder);
            libraryServiceMock.Verify(x => x.CreateMediaFile(libraryMock1.Object.LibrarySiteID, libraryMock1.Object.LibraryName, localPath, fileName, fileTitle, fileDescription, folder));
        }
    }
}

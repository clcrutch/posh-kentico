// <copyright file="GetCmsMediaLibraryTests.cs" company="Chris Crutchfield">
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
using PoshKentico.Tests.Helpers;

namespace PoshKentico.Tests.ContentManagement.MediaLibraries
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class GetCmsMediaLibraryTests
    {
        [Test]
        public void GetCmsMediaLibraryTest_NoParameters_None()
        {
            var libraryServiceMock = new Mock<IMediaLibraryService>();

            var businessLayer = new GetCmsMediaLibraryBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                MediaLibraryService = libraryServiceMock.Object,
            };

            businessLayer.GetMediaLibraries().Should().BeEmpty();

            libraryServiceMock.Verify(x => x.MediaLibraries);
        }

        [Test]
        public void GetCmsMediaLibraryTest_NoParameters_All()
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

            libraryServiceMock.SetupGet(x => x.MediaLibraries).Returns(libraries);

            var businessLayer = new GetCmsMediaLibraryBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                MediaLibraryService = libraryServiceMock.Object,
            };

            businessLayer.GetMediaLibraries().Should().NotBeNullOrEmpty().And.HaveCount(2);
            libraryServiceMock.Verify(x => x.MediaLibraries);
        }

        [Test]
        public void GetCmsMediaLibraryTest__MatchString_ExactFalse()
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

            libraryServiceMock.SetupGet(x => x.MediaLibraries).Returns(libraries);

            var businessLayer = new GetCmsMediaLibraryBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                MediaLibraryService = libraryServiceMock.Object,
            };

            businessLayer.GetMediaLibraries(1, "MyLibrary", false).Should().NotBeNullOrEmpty().And.HaveCount(2);
            businessLayer.GetMediaLibraries(1, "1", false).Should().NotBeNullOrEmpty().And.HaveCount(1);
            libraryServiceMock.Verify(x => x.MediaLibraries);
        }

        [Test]
        public void GetCmsMediaLibraryTest__MatchString_ExactTrue()
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

            libraryServiceMock.SetupGet(x => x.MediaLibraries).Returns(libraries);

            var businessLayer = new GetCmsMediaLibraryBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                MediaLibraryService = libraryServiceMock.Object,
            };

            businessLayer.GetMediaLibraries(1, "MyLibrary", true).Should().BeEmpty();
            businessLayer.GetMediaLibraries(1, "MyLibrary1", true).Should().NotBeNullOrEmpty().And.HaveCount(1);
            libraryServiceMock.Verify(x => x.MediaLibraries);
        }

        [Test]
        public void GetCmsMediaLibrary_IDs()
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

            libraryServiceMock.Setup(x => x.GetMediaLibrary(1)).Returns(libraryMock1.Object);
            libraryServiceMock.Setup(x => x.GetMediaLibrary(2)).Returns(libraryMock2.Object);

            var businessLayer = new GetCmsMediaLibraryBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                MediaLibraryService = libraryServiceMock.Object,
            };

            businessLayer.GetMediaLibraries(1).Should().NotBeNullOrEmpty().And.HaveCount(1);
            businessLayer.GetMediaLibraries(2).Should().NotBeNullOrEmpty().And.HaveCount(1);
            businessLayer.GetMediaLibraries(3).Should().BeEmpty();

            businessLayer.GetMediaLibraries(1, 2, 3).Should().NotBeNullOrEmpty().And.HaveCount(2);

            libraryServiceMock.Verify(x => x.GetMediaLibrary(1));
            libraryServiceMock.Verify(x => x.GetMediaLibrary(2));
            libraryServiceMock.Verify(x => x.GetMediaLibrary(3));
        }
    }
}

// <copyright file="NewCmsMediaLibraryTests.cs" company="Chris Crutchfield">
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

using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.ContentManagement.MediaLibraries;

namespace PoshKentico.Tests.ContentManagement.MediaLibraries
{
    [TestFixture]
    public class NewCmsMediaLibraryTests
    {
        [Test]
        public void NewCmsMediaLibraryTest_1()
        {
            var libraryServiceMock = new Mock<MediaLibraryServiceMock>();

            var businessLayer = new NewCmsMediaLibraryBusiness
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                MediaLibraryService = libraryServiceMock.Object,
            };

            var result = businessLayer.CreateMediaLibrary("My Media Library 1", "MyLibrary1", "This is My Library 1", "image", 12);

            libraryServiceMock.Object.VerifyCreate(result);

            result.LibraryName.Should().Be("MyLibrary1");
        }
    }
}

// <copyright file="SetCmsMediaLibrarySecurityOptionTests.cs" company="Chris Crutchfield">
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

using System.Diagnostics.CodeAnalysis;
using CMS.Helpers;
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
    public class SetCmsMediaLibrarySecurityOptionTests
    {
        [Test]
        public void SetCmsMediaLibrarySecurityOptionTest_BusinessCallService()
        {
            var libraryServiceMock = new Mock<IMediaLibraryService>();
            var libraryMock1 = new Mock<IMediaLibrary>();
            libraryMock1.SetupGet(x => x.LibraryName).Returns("MyLibrary1");

            var businessLayer = new SetCmsMediaLibrarySecurityOptionBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                MediaLibraryService = libraryServiceMock.Object,
            };

            businessLayer.SetMediaLibrarySecurityOption(libraryMock1.Object, SecurityPropertyEnum.Access, CMS.Helpers.SecurityAccessEnum.AuthorizedRoles);

            libraryServiceMock.Verify(x => x.SetMediaLibrarySecurityOption(libraryMock1.Object, SecurityPropertyEnum.Access, SecurityAccessEnum.AuthorizedRoles));
        }

        [Test]
        public void SetCmsMediaLibrarySecurityOptionTest_ServiceExecution()
        {
            var libraryServiceMock = new MediaLibraryServiceMock();

            var libraryMock1 = new MediaLibraryMock
            {
                Access = SecurityAccessEnum.AllUsers,
            };

            var businessLayer = new SetCmsMediaLibrarySecurityOptionBusiness
            {
                OutputService = OutputServiceHelper.GetPassThruOutputService(),

                MediaLibraryService = libraryServiceMock,
            };

            businessLayer.SetMediaLibrarySecurityOption(libraryMock1, SecurityPropertyEnum.Access, SecurityAccessEnum.AuthorizedRoles);

            libraryMock1.Access.Should().Be(SecurityAccessEnum.AuthorizedRoles);
        }
    }
}

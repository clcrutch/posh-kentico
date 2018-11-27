// <copyright file="RemoveCMSWebPartTests.cs" company="Chris Crutchfield">
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
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Providers.General;
using PoshKentico.Core.Services.Development.WebParts;
using PoshKentico.Tests.Helpers;

namespace PoshKentico.Tests.Development.WebParts
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class RemoveCMSWebPartTests
    {
        [TestCase]
        public void ShouldRemoveWebPart()
        {
            bool shouldProcessCalled = false;

            var webPartMock = new Mock<IWebPart>();

            var webPartServiceMock = new Mock<IWebPartService>();

            var outputService = OutputServiceHelper.GetPassThruOutputService();
            PassThruOutputService.ShouldProcessFunction = (x, y) =>
            {
                shouldProcessCalled = true;

                return true;
            };

            var businessLayer = new RemoveCMSWebPartBusiness
            {
                OutputService = outputService,
                WebPartService = webPartServiceMock.Object,
            };

            businessLayer.RemoveWebPart(webPartMock.Object);

            webPartServiceMock
                .Verify(x => x.Delete(webPartMock.Object));

            shouldProcessCalled.Should().BeTrue();
        }
    }
}

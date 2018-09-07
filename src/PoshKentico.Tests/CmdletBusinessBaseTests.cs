// <copyright file="CmdletBusinessBaseTests.cs" company="Chris Crutchfield">
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

using Moq;
using NUnit.Framework;
using PoshKentico.Business;
using PoshKentico.Core.Services.General;
using System.Diagnostics.CodeAnalysis;

namespace PoshKentico.Tests
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class CmdletBusinessBaseTests
    {
        [TestCase(true)]
        [TestCase(false)]
        public void ShouldInitApplication(bool shouldInitApplication)
        {
            // Setup the ICMSApplicationService mock
            var cmsApplicationServiceMock = new Mock<ICmsApplicationService>();

            // Setup the Business Layer mock
            var mockBusiness = new Mock<CmdletBusinessBase>(shouldInitApplication)
            {
                CallBase = true,
            };
            mockBusiness
                .Setup(x => x.WriteDebug)
                .Returns(Assert.NotNull);
            mockBusiness
                .Setup(x => x.WriteVerbose)
                .Returns(Assert.NotNull);
            var businessObj = mockBusiness.Object;

            businessObj.CmsApplicationService = cmsApplicationServiceMock.Object;
            businessObj.Initialize();

            // Verify the necessary methods were called.
            if (shouldInitApplication)
            {
                cmsApplicationServiceMock.Verify(x => x.Initialize(true, businessObj.WriteDebug, businessObj.WriteVerbose));
            }
        }
    }
}

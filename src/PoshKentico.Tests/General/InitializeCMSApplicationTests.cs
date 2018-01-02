// <copyright file="InitializeCMSApplicationTests.cs" company="Chris Crutchfield">
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
using PoshKentico.Business.General;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Tests.General
{
    [TestFixture]
    public class InitializeCMSApplicationTests
    {
        [TestCase]
        public void InitializeNoParameters()
        {
            var applicationServiceMock = new Mock<ICmsApplicationService>();

            var businessLayer = new InitializeCMSApplicationBusiness
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                CmsApplicationService = applicationServiceMock.Object,
            };

            businessLayer.Initialize();

            applicationServiceMock.Verify(x => x.Initialize(Assert.NotNull, Assert.NotNull));
        }
    }
}

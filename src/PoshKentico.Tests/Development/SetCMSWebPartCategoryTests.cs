// <copyright file="SetCMSWebPartCategoryTests.cs" company="Chris Crutchfield">
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
using PoshKentico.Business.Development;
using PoshKentico.Core.Services.Development;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Tests.Development
{
    [TestFixture]
    public class SetCMSWebPartCategoryTests
    {
        [TestCase]
        public void Set()
        {
            var applicationServiceMock = new Mock<ICmsApplicationService>();
            var webPartServiceMock = new Mock<IWebPartService>();

            var businessLayer = new SetCMSWebPartCategoryBusiness()
            {
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,

                CmsApplicationService = applicationServiceMock.Object,
                WebPartService = webPartServiceMock.Object,
            };

            var catMock1 = new Mock<IWebPartCategory>();
            catMock1.SetupGet(x => x.CategoryDisplayName).Returns("My Category");
            catMock1.SetupGet(x => x.CategoryName).Returns("MyCategory");
            catMock1.SetupGet(x => x.CategoryPath).Returns("/my/category");

            businessLayer.Set(catMock1.Object);

            applicationServiceMock.Verify(x => x.Initialize(true, Assert.NotNull, Assert.NotNull));
            webPartServiceMock.Verify(x => x.Update(catMock1.Object));
        }
    }
}

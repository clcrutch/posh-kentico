// <copyright file="WebPartBusinessBaseTests.cs" company="Chris Crutchfield">
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
using System.Reflection;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;
using PoshKentico.Tests.Helpers;

namespace PoshKentico.Tests.Development.WebParts
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class WebPartBusinessBaseTests
    {
        [TestCase]
        public void ShouldGetCategoryFromPath()
        {
            // Setup the web part category
            var webPartCategoryMock = new Mock<IWebPartCategory>();
            webPartCategoryMock
                .Setup(x => x.CategoryPath)
                .Returns("/Category");

            // Setup the web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.WebPartCategories)
                .Returns(new IWebPartCategory[]
                {
                    webPartCategoryMock.Object,
                });

            // Setup the business layer.
            var businessMock = new Mock<WebPartBusinessBase>(true)
            {
                CallBase = true,
            };
            businessMock
                .Setup(x => x.WebPartService)
                .Returns(webPartServiceMock.Object);

            businessMock.Object.OutputService = OutputServiceHelper.GetPassThruOutputService();

            // We need to use reflection b/c the method is protected.
            var getCategoryFromPathMethod = typeof(WebPartBusinessBase).GetMethod("GetCategoryFromPath", BindingFlags.NonPublic | BindingFlags.Instance);
            var result = getCategoryFromPathMethod.Invoke(businessMock.Object, new object[] { "/Category" }) as IWebPartCategory;

            result
                .Should()
                .NotBeNull();

            result.Should().BeEquivalentTo(webPartCategoryMock.Object);
        }
    }
}

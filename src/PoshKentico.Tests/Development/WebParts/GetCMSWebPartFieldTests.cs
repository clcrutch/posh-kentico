// <copyright file="GetCMSWebPartFieldTests.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Development.WebParts;
using PoshKentico.Tests.Helpers;

namespace PoshKentico.Tests.Development.WebParts
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class GetCMSWebPartFieldTests
    {
        [TestCase]
        public void ShouldGetWebPartFields()
        {
            // Setup mocks
            var webPartMock = new Mock<IWebPart>();
            var webPartServiceMock = new Mock<IWebPartService>();

            // Setup business layer
            var businessLayer = new GetCMSWebPartFieldBusiness
            {
                WebPartService = webPartServiceMock.Object,
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
            };

            businessLayer.GetWebPartFields(webPartMock.Object);

            // Verify the execution
            webPartServiceMock.Verify(x => x.GetWebPartFields(webPartMock.Object));
        }

        [TestCase]
        public void ShouldGetWebPartFieldsByMatchString()
        {
            // Setup the web part fields
            var webPartFieldMock1 = new Mock<IWebPartField>();
            webPartFieldMock1
                .Setup(x => x.Name)
                .Returns("Field1");
            var webPartFieldMock2 = new Mock<IWebPartField>();
            webPartFieldMock2
                .Setup(x => x.Name)
                .Returns("Other");

            // Setup the web part
            var webPartMock = new Mock<IWebPart>();

            // Setup the web part service
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetWebPartFields(webPartMock.Object))
                .Returns(new IWebPartField[]
                {
                    webPartFieldMock1.Object,
                    webPartFieldMock2.Object,
                });

            // Setup the business layer
            var businessLayer = new GetCMSWebPartFieldBusiness
            {
                WebPartService = webPartServiceMock.Object,
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
            };

            // Get the results
            // Should return results
            var results = businessLayer.GetWebPartFields("*field*", false, webPartMock.Object);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Should()
                .Contain(webPartFieldMock1.Object);
            results
                .Should()
                .NotContain(webPartFieldMock2.Object);
            results
                .Should()
                .NotContainNulls();

            // Get the results
            // Should not return results
            results = businessLayer.GetWebPartFields("notfields", false, webPartMock.Object);
            results
                .Should()
                .BeEmpty();
        }

        [TestCase]
        public void ShouldGetWebPartFieldsByRegex()
        {
            // Setup the web part fields
            var webPartFieldMock1 = new Mock<IWebPartField>();
            webPartFieldMock1
                .Setup(x => x.Name)
                .Returns("Field1");
            var webPartFieldMock2 = new Mock<IWebPartField>();
            webPartFieldMock2
                .Setup(x => x.Name)
                .Returns("Other");

            // Setup the web part mock
            var webPartMock = new Mock<IWebPart>();

            // Setup the service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetWebPartFields(webPartMock.Object))
                .Returns(new IWebPartField[]
                {
                    webPartFieldMock1.Object,
                    webPartFieldMock2.Object,
                });

            // Setup the business layer
            var businessLayer = new GetCMSWebPartFieldBusiness
            {
                WebPartService = webPartServiceMock.Object,
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
            };

            // Get the results
            // There should be results
            var results = businessLayer.GetWebPartFields("1", true, webPartMock.Object);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Should()
                .Contain(webPartFieldMock1.Object);
            results
                .Should()
                .NotContain(webPartFieldMock2.Object);
            results
                .Should()
                .NotContainNulls();

            // Get the results
            // There should not be any results
            results = businessLayer.GetWebPartFields("notfields", true, webPartMock.Object);
            results
                .Should()
                .BeEmpty();
        }
    }
}

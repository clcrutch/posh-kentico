// <copyright file="AddCMSWebPartFieldBusinessTests.cs" company="Chris Crutchfield">
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
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;

namespace PoshKentico.Tests.Development.WebParts
{
    [TestFixture]
    public class AddCMSWebPartFieldBusinessTests
    {
        [TestCase(true)]
        [TestCase(false)]
        public void ShouldAddFieldWithDefaultValue(bool required)
        {
            IWebPartField webPartField = null;

            // Create a web part.
            var webPartMock = new Mock<IWebPart>();
            var webPartObj = webPartMock.Object;

            // Setup the web part service.
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.AddField(It.IsAny<IWebPartField>(), webPartObj))
                .Callback<IWebPartField, IWebPart>((x, y) => webPartField = x);

            var addFieldParameter = new AddCMSWebPartFieldBusiness.AddFieldParameter
            {
                Required = required,
                Caption = "TestCaption",
                ColumnType = FieldDataType.Text,
                DefaultValue = "TestDefaultValue",
                Name = "TestName",
                Size = 6789,
            };

            // Create the business layer.
            var businessLayer = new AddCMSWebPartFieldBusiness
            {
                WebPartService = webPartServiceMock.Object,
            };

            businessLayer.AddField(addFieldParameter, webPartObj);

            // Test the web part field to ensure it is accurate.
            webPartField.Should().NotBeNull();

            webPartField.AllowEmpty.Should().Be(!required);
            webPartField.Caption.Should().Be("TestCaption");
            webPartField.DataType.Should().Be("text");
            webPartField.DefaultValue.Should().Be("TestDefaultValue");
            webPartField.Name.Should().Be("TestName");
            webPartField.Size.Should().Be(6789);
            webPartField.WebPart.Should().BeNull();

            // Test that the the necessary methods were called.
            webPartServiceMock.Verify(x => x.AddField(It.IsAny<IWebPartField>(), webPartObj));

            webPartField.WebPart = webPartObj;
            webPartField.WebPart.Should().Be(webPartObj);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ShouldAddFieldWithoutDefaultValue(bool required)
        {
            IWebPartField webPartField = null;

            // Setup the web part.
            var webPartMock = new Mock<IWebPart>();
            var webPartObj = webPartMock.Object;

            // Setup the web part service mock.
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.AddField(It.IsAny<IWebPartField>(), webPartObj))
                .Callback<IWebPartField, IWebPart>((x, y) => webPartField = x);

            var addFieldParameter = new AddCMSWebPartFieldBusiness.AddFieldParameter
            {
                Required = required,
                Caption = "TestCaption",
                ColumnType = FieldDataType.Text,
                DefaultValue = null,
                Name = "TestName",
                Size = 6789,
            };

            // Setup the business layer.
            var businessLayer = new AddCMSWebPartFieldBusiness
            {
                WebPartService = webPartServiceMock.Object,
            };

            businessLayer.AddField(addFieldParameter, webPartObj);

            // Test the field.
            webPartField.Should().NotBeNull();

            webPartField.AllowEmpty.Should().Be(!required);
            webPartField.Caption.Should().Be("TestCaption");
            webPartField.DataType.Should().Be("text");
            webPartField.DefaultValue.Should().BeNull();
            webPartField.Name.Should().Be("TestName");
            webPartField.Size.Should().Be(6789);
            webPartField.WebPart.Should().BeNull();

            // Ensure the service methods were called.
            webPartServiceMock.Verify(x => x.AddField(It.IsAny<IWebPartField>(), webPartObj));

            webPartField.WebPart = webPartObj;
            webPartField.WebPart.Should().Be(webPartObj);
        }
    }
}

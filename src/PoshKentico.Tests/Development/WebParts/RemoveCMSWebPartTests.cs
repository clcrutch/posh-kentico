using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            var businessLayer = new RemoveCMSWebPartBusiness
            {
                WebPartService = webPartServiceMock.Object,
                WriteDebug = Assert.NotNull,
                WriteVerbose = Assert.NotNull,
                ShouldProcess = (x, y) =>
                {
                    shouldProcessCalled = true;

                    return true;
                },
            };

            businessLayer.RemoveWebPart(webPartMock.Object);

            webPartServiceMock
                .Verify(x => x.Delete(webPartMock.Object));

            shouldProcessCalled.Should().BeTrue();
        }
    }
}

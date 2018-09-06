using Moq;
using NUnit.Framework;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace PoshKentico.Tests.Development.WebParts
{
    [TestFixture]
    public class GetCMSWebPartBusinessTests
    {
        [TestCase]
        public void ShouldGetWebPartFromPath()
        {
            // Setup web part category
            var webPartCategoryMock = new Mock<IWebPartCategory>();
            webPartCategoryMock
                .Setup(x => x.CategoryID)
                .Returns(15);
            webPartCategoryMock
                .Setup(x => x.CategoryPath)
                .Returns("/Category");
            var webPartCategoryObj = webPartCategoryMock.Object;

            // Setup web part
            var webPartMock = new Mock<IWebPart>();
            webPartMock
                .Setup(x => x.WebPartCategoryID)
                .Returns(15);
            webPartMock
                .Setup(x => x.WebPartName)
                .Returns("WebPart");
            var webPartObj = webPartMock.Object;

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.WebPartCategories)
                .Returns(new IWebPartCategory[]
                {
                    webPartCategoryObj,
                });
            webPartServiceMock
                .Setup(x => x.GetWebParts(webPartCategoryObj))
                .Returns(new IWebPart[] { webPartObj });

            // Setup business layer
            var businessLayer = new GetCMSWebPartBusiness
            {
                WebPartService = webPartServiceMock.Object,
            };

            var result = businessLayer.GetWebPart("/Category/WebPart");

            result
                .Should()
                .NotBeNull();

            result
                .ShouldBeEquivalentTo(webPartObj);
        }

        [TestCase]
        public void ShouldGetWebPartFromNoPath()
        {
            // Setup web part category
            var webPartCategoryMock = new Mock<IWebPartCategory>();
            webPartCategoryMock
                .Setup(x => x.CategoryID)
                .Returns(15);
            webPartCategoryMock
                .Setup(x => x.CategoryPath)
                .Returns("/");
            var webPartCategoryObj = webPartCategoryMock.Object;

            // Setup web part
            var webPartMock = new Mock<IWebPart>();
            webPartMock
                .Setup(x => x.WebPartCategoryID)
                .Returns(15);
            webPartMock
                .Setup(x => x.WebPartName)
                .Returns("WebPart");
            var webPartObj = webPartMock.Object;

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.WebPartCategories)
                .Returns(new IWebPartCategory[]
                {
                    webPartCategoryObj,
                });
            webPartServiceMock
                .Setup(x => x.GetWebParts(webPartCategoryObj))
                .Returns(new IWebPart[] { webPartObj });

            // Setup business layer
            var businessLayer = new GetCMSWebPartBusiness
            {
                WebPartService = webPartServiceMock.Object,
            };

            var result = businessLayer.GetWebPart("/WebPart");

            result
                .Should()
                .NotBeNull();

            result
                .ShouldBeEquivalentTo(webPartObj);
        }

        [TestCase]
        public void ShouldGetWebPartByField()
        {
            // Setup web part
            var webPartMock = new Mock<IWebPart>();
            var webPartObj = webPartMock.Object;

            // Setup web part field
            var webPartFieldMock = new Mock<IWebPartField>();
            webPartFieldMock
                .Setup(x => x.WebPart)
                .Returns(webPartObj);

            // Setup business layer
            var businessLayer = new GetCMSWebPartBusiness();
            var result = businessLayer.GetWebPart(webPartFieldMock.Object);

            result
                .Should()
                .NotBeNull();

            result
                .ShouldBeEquivalentTo(webPartObj);
        }

        [TestCase]
        public void ShouldGetAllWebParts()
        {
            // Setup web part
            var webPartMock = new Mock<IWebPart>();
            var webPartObj = webPartMock.Object;

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.WebParts)
                .Returns(new IWebPart[] { webPartObj });

            // Setup business layer
            var businessLayer = new GetCMSWebPartBusiness
            {
                WebPartService = webPartServiceMock.Object,
            };
            var result = businessLayer.GetWebParts();

            result
                .Should()
                .NotBeNullOrEmpty();

            result
                .Single()
                .Should()
                .NotBeNull();

            result
                .Single()
                .ShouldBeEquivalentTo(webPartObj);
        }

        [TestCase]
        public void ShouldGetWebPartsByMatchStringWithoutRegex()
        {
            // Setup web part
            var webPartMock = new Mock<IWebPart>();
            webPartMock
                .Setup(x => x.WebPartDisplayName)
                .Returns("aDisplaya");
            webPartMock
                .Setup(x => x.WebPartName)
                .Returns("dTests");
            var webPartObj = webPartMock.Object;

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.WebParts)
                .Returns(new IWebPart[] { webPartObj });

            // Setup business layer
            var businessLayer = new GetCMSWebPartBusiness
            {
                WebPartService = webPartServiceMock.Object,
            };

            // Test Display Name
            var results = businessLayer.GetWebParts("*Display*", false);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Single()
                .ShouldBeEquivalentTo(webPartObj);

            results = businessLayer.GetWebParts("Display*", false);
            results
                .Should()
                .BeEmpty();

            // Test Name
            results = businessLayer.GetWebParts("*test*", false);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Single()
                .ShouldBeEquivalentTo(webPartObj);

            results = businessLayer.GetWebParts("test*", false);
            results
                .Should()
                .BeEmpty();
        }

        [TestCase]
        public void ShouldGetWebPartsByMatchStringWithRegex()
        {
            // Setup web part
            var webPartMock = new Mock<IWebPart>();
            webPartMock
                .Setup(x => x.WebPartDisplayName)
                .Returns("aDisplaya");
            webPartMock
                .Setup(x => x.WebPartName)
                .Returns("dTests");
            var webPartObj = webPartMock.Object;

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.WebParts)
                .Returns(new IWebPart[] { webPartObj });

            // Setup business layer
            var businessLayer = new GetCMSWebPartBusiness
            {
                WebPartService = webPartServiceMock.Object,
            };

            // Test Display name
            var results = businessLayer.GetWebParts("[a-z]Display(a)+", true);
            results
                .Should()
                .NotBeNullOrEmpty();
            results
                .Single()
                .ShouldBeEquivalentTo(webPartObj);

            results = businessLayer.GetWebParts("[a-z]NotDisplay(a)+", true);
            results
                .Should()
                .BeEmpty();

            // Test Name
            results = businessLayer.GetWebParts("[a-z]Test(s)+", true);
            results
                .Should()
                .NotBeNullOrEmpty();
            results.Single()
                .ShouldBeEquivalentTo(webPartObj);

            results = businessLayer.GetWebParts("[a-z]nottest(s)+", true);
            results
                .Should()
                .BeEmpty();
        }

        [TestCase]
        public void ShouldGetWebPartsByCategoryFromCategory()
        {
            // Setup web part category
            var webPartCategoryMock = new Mock<IWebPartCategory>();
            webPartCategoryMock
                .Setup(x => x.CategoryID)
                .Returns(15);
            var webPartCategoryObj = webPartCategoryMock.Object;

            // Setup web part
            var webPartMock = new Mock<IWebPart>();
            webPartMock
                .Setup(x => x.WebPartCategoryID)
                .Returns(15);
            var webPartObj = webPartMock.Object;

            // Setup web part service mock
            var webPartServiceMock = new Mock<IWebPartService>();
            webPartServiceMock
                .Setup(x => x.GetWebParts(webPartCategoryObj))
                .Returns(new IWebPart[] { webPartObj });

            // Setup business layer
            var businessLayer = new GetCMSWebPartBusiness
            {
                WebPartService = webPartServiceMock.Object,
            };

            var result = businessLayer.GetWebPartsByCategory(webPartCategoryObj);

            result
                .Should()
                .NotBeNullOrEmpty();

            result
                .Single()
                .ShouldBeEquivalentTo(webPartObj);
        }
    }
}

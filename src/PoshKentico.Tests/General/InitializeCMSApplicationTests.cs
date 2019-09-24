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

using System.Diagnostics.CodeAnalysis;
using System.IO;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using PoshKentico.Business.General;
using PoshKentico.Core.Providers.General;
using PoshKentico.Core.Services.General;
using PoshKentico.Tests.Helpers;

namespace PoshKentico.Tests.General
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    public class InitializeCMSApplicationTests
    {
        [TestCase]
        public void Initialize_ConnectionString_WebRoot()
        {
            var applicationServiceMock = new Mock<ICmsApplicationService>();

            var businessLayer = new InitializeCMSApplicationBusiness
            {
                CmsApplicationService = applicationServiceMock.Object,
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
            };

            var webRoot = new DirectoryInfo("C:\\Kentico\\WebRoot");

            businessLayer.Initialize("myConnectionString", webRoot);

            applicationServiceMock.Verify(x => x.Initialize(webRoot, "myConnectionString", true));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void Initialize_Cached(bool cacheUsed)
        {
            var applicationServiceMock = new Mock<ICmsApplicationService>();

            var businessLayer = new InitializeCMSApplicationBusiness
            {
                CmsApplicationService = applicationServiceMock.Object,
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
            };

            businessLayer.Initialize(cacheUsed);

            applicationServiceMock.Verify(x => x.Initialize(cacheUsed));
        }

        [TestCase]
        public void Initialize_DatabaseServer_Database_Timeout_WebRoot()
        {
            var applicationServiceMock = new Mock<ICmsApplicationService>();

            var businessLayer = new InitializeCMSApplicationBusiness
            {
                CmsApplicationService = applicationServiceMock.Object,
                OutputService = OutputServiceHelper.GetPassThruOutputService(),
            };

            var webRoot = new DirectoryInfo("C:\\Kentico\\WebRoot");

            businessLayer.Initialize("databaseServer", "database", 103, webRoot);

            applicationServiceMock.Verify(x => x.Initialize(webRoot, $"Data Source=databaseServer;Initial Catalog=database;Integrated Security=True;Persist Security Info=False;Connect Timeout=103;Encrypt=False;Current Language=English", true));
        }

        [TestCase]
        public void Initialize_ConnectionString_WebRoot_AlreadyInitialized()
        {
            var applicationServiceMock = new Mock<ICmsApplicationService>();
            applicationServiceMock
                .Setup(x => x.InitializationState)
                .Returns(InitializationState.Initialized);

            var outputService = OutputServiceHelper.GetPassThruOutputService();
            PassThruOutputService.WriteVerboseAction = x => x.Should().Be("Kentico is already initialized.  Skipping...");

            var businessLayer = new InitializeCMSApplicationBusiness
            {
                CmsApplicationService = applicationServiceMock.Object,
                OutputService = outputService,
            };

            businessLayer.Initialize(null, null);
        }

        [TestCase]
        public void Initialize_Cached_AlreadyInitialized()
        {
            var applicationServiceMock = new Mock<ICmsApplicationService>();
            applicationServiceMock
                .Setup(x => x.InitializationState)
                .Returns(InitializationState.Initialized);

            var outputService = OutputServiceHelper.GetPassThruOutputService();
            PassThruOutputService.WriteVerboseAction = x => x.Should().Be("Kentico is already initialized.  Skipping...");

            var businessLayer = new InitializeCMSApplicationBusiness
            {
                CmsApplicationService = applicationServiceMock.Object,
                OutputService = outputService,
            };

            businessLayer.Initialize(true);
        }
    }
}

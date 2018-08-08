// <copyright file="SiteServiceMock.cs" company="Chris Crutchfield">
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

using System.Collections.Generic;
using FluentAssertions;
using Moq;
using PoshKentico.Core.Services.Configuration.Localization;
using PoshKentico.Core.Services.Configuration.Sites;

namespace PoshKentico.Business.Configuration.Sites.Tests
{
    public class SiteServiceMock : ISiteService
    {
        private ISite siteMock;

        /// <inheritdoc/>
        IEnumerable<ISite> ISiteService.Sites => throw new System.NotImplementedException();

        /// <inheritdoc/>
        ISite ISiteService.Create(ISite newSite)
        {
            this.siteMock = newSite;

            var testSite = new Mock<ISite>();
            testSite.SetupGet(x => x.DisplayName).Returns(newSite.DisplayName);
            testSite.SetupGet(x => x.DomainName).Returns(newSite.DomainName);
            testSite.SetupGet(x => x.SiteName).Returns(newSite.SiteName);
            testSite.SetupGet(x => x.Status).Returns(newSite.Status);

            return testSite.Object;
        }

        public void VerifyCreate(ISite expectedSite)
        {
            this.siteMock.DisplayName.Should().Be(expectedSite.DisplayName);
            this.siteMock.DomainName.Should().Be(expectedSite.DomainName);
            this.siteMock.SiteName.Should().Be(expectedSite.SiteName);
            this.siteMock.Status.Should().Be(expectedSite.Status);
        }

        void ISiteService.Delete(ISite site)
        {
            throw new System.NotImplementedException();
        }

        ISite ISiteService.GetSite(int id)
        {
            throw new System.NotImplementedException();
        }

        void ISiteService.Update(ISite site)
        {
            throw new System.NotImplementedException();
        }

        public void Start(ISite site)
        {
            throw new System.NotImplementedException();
        }

        public void Stop(ISite site)
        {
            throw new System.NotImplementedException();
        }

        public ISite GetSite(string siteName)
        {
            throw new System.NotImplementedException();
        }

        public void AddSiteCulture(ISite site, string cultureCode)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveSiteCulture(ISite site, string cultureCode)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ICulture> GetSiteCultures(ISite site)
        {
            throw new System.NotImplementedException();
        }

        public void AddSiteDomainAlias(ISite site, string aliasName)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveSiteDomainAlias(ISite site, string aliasName)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ISiteDomainAlias> GetDomainAliases(ISite site)
        {
            throw new System.NotImplementedException();
        }
    }
}
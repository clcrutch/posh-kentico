// <copyright file="NewCmsSiteBusiness.cs" company="Chris Crutchfield">
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

using System;
using System.ComponentModel.Composition;
using CMS.SiteProvider;
using ImpromptuInterface;
using PoshKentico.Core.Services.Configuration;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Business.Configuration.Sites
{
    /// <summary>
    /// Business layer for the New-CMSSites cmdlet.
    /// </summary>
    [Export(typeof(NewCmsSiteBusiness))]
    public class NewCmsSiteBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the CMS Application Service.  Populated by MEF.
        /// </summary>
        [Import]
        public ICmsApplicationService CmsApplicationService { get; set; }

        /// <summary>
        /// Gets or sets a reference to the WebPart Service.  Populated by MEF.
        /// </summary>
        [Import]
        public ISiteService SiteService { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Creates a new <see cref="ISite"/> in the CMS System.
        /// </summary>
        /// <param name="displayName">The Display Name for the new Site</param>
        /// <param name="siteName">The Site Name for the new Site</param>
        /// <param name="status">The Status for the new Site</param>
        /// <param name="domainName">The Domain Name for the new Site</param>
        /// <returns>A list of all of the <see cref="ISite"/>.</returns>
        public ISite CreateSite(string displayName, string siteName, string status, string domainName)
        {
            this.CmsApplicationService.Initialize(true, this.WriteVerbose, this.WriteDebug);

            var data = new
            {
                DisplayName = displayName,
                SiteName = siteName,
                Status = (SiteStatusEnum)Enum.Parse(typeof(SiteStatusEnum), status),
                DomainName = domainName,
            };

            return this.SiteService.Create(data.ActLike<ISite>());
        }

        #endregion
    }
}

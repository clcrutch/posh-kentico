﻿// <copyright file="StartCMSSiteBusiness.cs" company="Chris Crutchfield">
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

using System.ComponentModel.Composition;
using PoshKentico.Core.Services.Configuration.Sites;

namespace PoshKentico.Business.Configuration.Sites
{
    /// <summary>
    /// Business Layer for the Start-CMSSite cmdlet.
    /// </summary>
    [Export(typeof(StartCmsSiteBusiness))]
    public class StartCmsSiteBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the Site Service.  Populated by MEF.
        /// </summary>
        [Import]
        public ISiteService SiteService { get; set; }

        /// <summary>
        /// Gets or sets a reference to the <see cref="GetCmsSiteBusiness"/> used to get the site to start.  Populated by MEF.
        /// </summary>
        [Import]
        public GetCmsSiteBusiness GetCmsSiteBusiness { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Starts the <see cref="ISite"/> in the CMS System.
        /// </summary>
        /// <param name="site">The <see cref="ISite"/> to set.</param>
        public void Start(ISite site)
        {
            this.StartSite(site);
        }

        /// <summary>
        /// Starts the <see cref="ISite"/> in the CMS System.
        /// </summary>
        /// <param name="ids">The IDs of the <see cref="ISite"/> to delete.</param>
        public void Start(params int[] ids)
        {
            foreach (var site in this.GetCmsSiteBusiness.GetSites(ids))
            {
                this.StartSite(site);
            }
        }

        /// <summary>
        /// Starts the <see cref="ISite"/> in the CMS System.
        /// </summary>
        /// <param name="matchString">the string which to match the site to.</param>
        /// <param name="exact">A boolean which indicates if the match should be exact.</param>
        public void Start(string matchString, bool exact)
        {
            foreach (var site in this.GetCmsSiteBusiness.GetSites(matchString, exact))
            {
                this.StartSite(site);
            }
        }

        /// <summary>
        /// Starts the <see cref="ISite"/> in the CMS System.
        /// </summary>
        /// <param name="site">The <see cref="ISite"/> to set.</param>
        public void StartSite(ISite site)
        {
            this.SiteService.Start(site);
        }

        #endregion
    }
}

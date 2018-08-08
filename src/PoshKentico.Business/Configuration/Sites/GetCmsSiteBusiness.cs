// <copyright file="GetCmsSiteBusiness.cs" company="Chris Crutchfield">
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
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using PoshKentico.Core.Services.Configuration.Sites;

namespace PoshKentico.Business.Configuration.Sites
{
    /// <summary>
    /// Business layer for the Get-CMSSite cmdlet.
    /// </summary>
    [Export(typeof(GetCmsSiteBusiness))]
    public class GetCmsSiteBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the Site Service.  Populated by MEF.
        /// </summary>
        [Import]
        public ISiteService SiteService { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// Gets a list of all of the <see cref="ISite"/> in the CMS System.
        /// </summary>
        /// <returns>A list of all of the <see cref="ISite"/>.</returns>
        public IEnumerable<ISite> GetSites()
        {
            return this.SiteService.Sites;
        }

        /// <summary>
        /// Gets a list of all of the <see cref="ISite"/> which match the specified criteria.
        /// </summary>
        /// <param name="matchString">The string which to match the webparts to.</param>
        /// <param name="exact">A boolean which indicates if the match should be exact.</param>
        /// <returns>A list of all of the <see cref="ISite"/> which match the specified criteria.</returns>
        public IEnumerable<ISite> GetSites(string matchString, bool exact)
        {
            if (exact)
            {
                return (from c in this.SiteService.Sites
                        where c.DisplayName.ToLowerInvariant().Equals(matchString, StringComparison.InvariantCultureIgnoreCase) ||
                            c.SiteName.ToLowerInvariant().Equals(matchString, StringComparison.InvariantCultureIgnoreCase) ||
                            c.DomainName.ToLowerInvariant().Equals(matchString, StringComparison.InvariantCultureIgnoreCase)
                        select c).ToArray();
            }
            else
            {
                var lowerMatchString = matchString.ToLowerInvariant();

                return (from c in this.SiteService.Sites
                        where c.DisplayName.ToLowerInvariant().Contains(lowerMatchString) ||
                           c.SiteName.ToLowerInvariant().Contains(lowerMatchString) ||
                           c.DomainName.ToLowerInvariant().StartsWith(lowerMatchString)
                        select c).ToArray();
            }
        }

        /// <summary>
        /// Gets a list of the <see cref="ISite"/> which match the supplied IDs.
        /// </summary>
        /// <param name="ids">The IDs of the <see cref="ISite"/> to return.</param>
        /// <returns>A list of the <see cref="ISite"/> which match the supplied IDs.</returns>
        public IEnumerable<ISite> GetSites(params int[] ids)
        {
            var sites = from id in ids select this.SiteService.GetSite(id);

            return (from site in sites
                    where site != null
                    select site).ToArray();
        }

        #endregion
    }
}

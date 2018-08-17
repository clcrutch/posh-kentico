// <copyright file="GetCmsServerBusiness.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Configuration.Staging;

namespace PoshKentico.Business.Configuration.Staging
{
    /// <summary>
    /// Business layer for the Get-CMSServer cmdlet.
    /// </summary>
    [Export(typeof(GetCmsServerBusiness))]
    public class GetCmsServerBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the Staging Service.  Populated by MEF.
        /// </summary>
        [Import]
        public IStagingService StagingService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a list of all of the <see cref="IServer"/> in the CMS System.
        /// </summary>
        /// <returns>A list of all of the <see cref="IServer"/>.</returns>
        public IEnumerable<IServer> GetServers()
        {
            return this.StagingService.Servers;
        }

        /// <summary>
        /// Gets a list of the <see cref="IServer"/> which match the supplied IDs.
        /// </summary>
        /// <param name="ids">The IDs of the <see cref="IServer"/> to return.</param>
        /// <returns>A list of the <see cref="IServer"/> which match the supplied IDs.</returns>
        public IEnumerable<IServer> GetServers(params int[] ids)
        {
            var services = from id in ids select this.StagingService.GetServer(id);

            return (from service in services
                    where service != null
                    select service).ToArray();
        }

        /// <summary>
        /// Gets a list of all of the <see cref="IServer"/> which match the specified criteria.
        /// </summary>
        /// <param name="site">the associated site of the servers</param>
        /// /// <param name="matchString">The string which to match the servers to.</param>
        /// <param name="exact">A boolean which indicates if the match should be exact.</param>
        /// <returns>A list of all of the <see cref="IServer"/> which match the specified criteria.</returns>
        public IEnumerable<IServer> GetServers(ISite site, string matchString, bool exact)
        {
            if (site == null)
            {
                if (exact)
                {
                    return this.GetServerExact(matchString);
                }
                else
                {
                    return this.GetServerContains(matchString);
                }
            }
            else
            {
                if (exact)
                {
                    return this.GetServerExact(site.SiteID, matchString);
                }
                else
                {
                    return this.GetServerContains(site.SiteID, matchString);
                }
            }
        }

        /// <summary>
        /// Gets a list of all of the <see cref="IServer"/> which match the specified criteria.
        /// </summary>
        /// <param name="siteId">the associated site id of the servers</param>
        /// /// <param name="matchString">The string which to match the servers to.</param>
        /// <param name="exact">A boolean which indicates if the match should be exact.</param>
        /// <returns>A list of all of the <see cref="IServer"/> which match the specified criteria.</returns>
        public IEnumerable<IServer> GetServers(int siteId, string matchString, bool exact)
        {
            if (siteId == -1)
            {
                if (exact)
                {
                    return this.GetServerExact(matchString);
                }
                else
                {
                    return this.GetServerContains(matchString);
                }
            }
            else
            {
                if (exact)
                {
                    return this.GetServerExact(siteId, matchString);
                }
                else
                {
                    return this.GetServerContains(siteId, matchString);
                }
            }
        }

        private IEnumerable<IServer> GetServerContains(int siteID, string matchString)
        {
            return (from c in this.StagingService.Servers
                    where c.ServerSiteID.Equals(siteID) &&
                    (c.ServerDisplayName.ToLowerInvariant().Contains(matchString.ToLowerInvariant()) ||
                        c.ServerName.ToLowerInvariant().Contains(matchString.ToLowerInvariant()))
                    select c).ToArray();
        }

        private IEnumerable<IServer> GetServerExact(int siteID, string matchString)
        {
            return (from c in this.StagingService.Servers
                    where c.ServerSiteID.Equals(siteID) &&
                    (c.ServerDisplayName.ToLowerInvariant().Equals(matchString, StringComparison.InvariantCultureIgnoreCase) ||
                        c.ServerName.ToLowerInvariant().Equals(matchString, StringComparison.InvariantCultureIgnoreCase))
                    select c).ToArray();
        }

        private IEnumerable<IServer> GetServerContains(string matchString)
        {
            return (from c in this.StagingService.Servers
                    where c.ServerDisplayName.ToLowerInvariant().Contains(matchString.ToLowerInvariant()) ||
                       c.ServerName.ToLowerInvariant().Contains(matchString.ToLowerInvariant())
                    select c).ToArray();
        }

        private IEnumerable<IServer> GetServerExact(string matchString)
        {
            return (from c in this.StagingService.Servers
                    where c.ServerDisplayName.ToLowerInvariant().Equals(matchString, StringComparison.InvariantCultureIgnoreCase) ||
                        c.ServerName.ToLowerInvariant().Equals(matchString, StringComparison.InvariantCultureIgnoreCase)
                    select c).ToArray();
        }

        #endregion
    }
}

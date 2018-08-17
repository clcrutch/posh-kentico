// <copyright file="RemoveCmsServerBusiness.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Configuration.Staging;

namespace PoshKentico.Business.Configuration.Staging
{
    /// <summary>
    /// Business Layer for the Remove-CMSServer cmdlet.
    /// </summary>
    [Export(typeof(RemoveCmsServerBusiness))]
    public class RemoveCmsServerBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the Staging Service.  Populated by MEF.
        /// </summary>
        [Import]
        public IStagingService StagingService { get; set; }

        /// <summary>
        /// Gets or sets a reference to the <see cref="GetCmsServerBusiness"/> used to get the server to delete.  Populated by MEF.
        /// </summary>
        [Import]
        public GetCmsServerBusiness GetCmsServerBusiness { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Deletes the <see cref="IServer"/> in the CMS System.
        /// </summary>
        /// <param name="server">The <see cref="IServer"/> to delete.</param>
        public void Remove(IServer server)
        {
            this.RemoveServer(server);
        }

        /// <summary>
        /// Deletes the <see cref="IServer"/> in the CMS System.
        /// </summary>
        /// <param name="ids">The IDs of the <see cref="IServer"/> to delete.</param>
        public void Remove(params int[] ids)
        {
            foreach (var server in this.GetCmsServerBusiness.GetServers(ids))
            {
                this.RemoveServer(server);
            }
        }

        /// <summary>
        /// Deletes the <see cref="IServer"/> in the CMS System.
        /// </summary>
        /// <param name="siteID">the site id which to match the server to. </param>
        /// <param name="matchString">the string which to match the server to.</param>
        /// <param name="exact">A boolean which indicates if the match should be exact.</param>
        public void Remove(int siteID, string matchString, bool exact)
        {
            foreach (var server in this.GetCmsServerBusiness.GetServers(siteID, matchString, exact))
            {
                this.RemoveServer(server);
            }
        }

        /// <summary>
        /// Deletes the specified <see cref="IServer"/> in the CMS System.
        /// </summary>
        /// <param name="server">The <see cref="IServer"/> to delete.</param>
        private void RemoveServer(IServer server)
        {
            if (this.ShouldProcess(server.ServerName, "delete"))
            {
                this.StagingService.Delete(server);
            }
        }

        #endregion
    }
}

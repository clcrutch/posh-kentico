// <copyright file="RemoveCmsStagingTaskBusiness.cs" company="Chris Crutchfield">
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
using ImpromptuInterface;
using PoshKentico.Core.Services.Configuration.Staging;

namespace PoshKentico.Business.Configuration.Staging
{
    /// <summary>
    /// Business Layer for the Remove-CMSStagingTask cmdlet.
    /// </summary>
    [Export(typeof(RemoveCmsStagingTaskBusiness))]
    public class RemoveCmsStagingTaskBusiness : CmdletBusinessBase
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
        /// Removes the <see cref="IServer"/> related staging tasks in the CMS System.
        /// </summary>
        /// <param name="server">The <see cref="IServer"/> to remove.</param>
        public void RemoveStaging(IServer server)
        {
            this.StagingService.DeleteStagingTask(server);
        }

        /// <summary>
        /// Removes the <see cref="IServer"/> related staging tasks in the CMS System.
        /// </summary>
        /// <param name="serverName">The server name to look for the server.</param>
        /// <param name="serverSiteId">The server site id to look for the server.</param>
        public void RemoveStaging(string serverName, int serverSiteId)
        {
            var data = new
            {
                ServerName = serverName,
                ServerSiteID = serverSiteId,
            };

            this.StagingService.DeleteStagingTask(data.ActLike<IServer>());
        }

        #endregion

    }
}

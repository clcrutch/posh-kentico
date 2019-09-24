// <copyright file="InitializeCMSApplicationBusiness.cs" company="Chris Crutchfield">
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
using System.IO;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Business.General
{
    /// <summary>
    /// Business layer for the Initialize-CMSApplication cmdlet.
    /// </summary>
    [Export(typeof(InitializeCMSApplicationBusiness))]
    public class InitializeCMSApplicationBusiness : CmdletBusinessBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InitializeCMSApplicationBusiness"/> class.
        /// </summary>
        public InitializeCMSApplicationBusiness()
            : base(false)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes Kentico with the specified connection string and web root.
        /// Matches with the "ConnectionString" parameter set name.
        /// </summary>
        /// <param name="connectionString">The connection string for the database connection.</param>
        /// <param name="webRoot">The root directory for the Kentico site.</param>
        public void Initialize(string connectionString, DirectoryInfo webRoot)
        {
            if (this.CmsApplicationService.InitializationState == InitializationState.Initialized)
            {
                this.OutputService.WriteVerbose("Kentico is already initialized.  Skipping...");

                return;
            }

            this.CmsApplicationService.Initialize(webRoot, connectionString, true);
        }

        /// <summary>
        /// Initializes Kentico by searching for a Kentico site.
        /// </summary>
        /// <param name="useCached">A boolean which indicates if initialization should use a previously cahced value if available.</param>
        public void Initialize(bool useCached)
        {
            if (this.CmsApplicationService.InitializationState == InitializationState.Initialized)
            {
                this.OutputService.WriteVerbose("Kentico is already initialized.  Skipping...");

                return;
            }

            this.CmsApplicationService.Initialize(useCached);
        }

        /// <summary>
        /// Initializes Kentico by using the provided information to generate a connection string.
        /// </summary>
        /// <param name="databaseServer">The server the Kentico database is located on.</param>
        /// <param name="database">The name of the Kentico database.</param>
        /// <param name="timeout">The timeout for connecting to the Kentico database.</param>
        /// <param name="webRoot">The root directory for the Kentico site.</param>
        public void Initialize(string databaseServer, string database, int timeout, DirectoryInfo webRoot)
        {
            var connectionString = $"Data Source={databaseServer};Initial Catalog={database};Integrated Security=True;Persist Security Info=False;Connect Timeout={timeout};Encrypt=False;Current Language=English";
            this.OutputService.WriteDebug("Setting connection string to \"{connectionString}\".");

            this.Initialize(connectionString, webRoot);
        }

        #endregion

    }
}

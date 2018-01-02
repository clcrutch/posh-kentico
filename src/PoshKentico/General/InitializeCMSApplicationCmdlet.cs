// <copyright file="InitializeCMSApplicationCmdlet.cs" company="Chris Crutchfield">
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
using System.Management.Automation;
using PoshKentico.Business.General;

namespace PoshKentico.General
{
    /// <summary>
    /// <para type="synopsis">Initializes a connection to the Kentico CMS Server.</para>
    /// <para type="description">The Initialize-CMSApplication cmdlet initializes a connection to the Kentico CMS server.</para>
    /// <para type="description"></para>
    /// <para type="description">If this cmdlet is run without parameters, then it requires administrator permissions to find the Kentico site.</para>
    /// <para type="description">It does so by performing the following steps:</para>
    /// <para type="description">1. Get a list of all the sites from IIS</para>
    /// <para type="description">2. Get a list of all applications from the sites</para>
    /// <para type="description">3. Get a list of all the virtual directories from the applications</para>
    /// <para type="description">4. Continue processing virtual directory if a web.config file exits</para>
    /// <para type="description">5. Parse the document and find an "add" node with name="CMSConnectionString"</para>
    /// <para type="description">6. If the connection string is valid, then stop processing</para>
    /// <example>
    ///     <para>Initialize the Kentico CMS Application by searching for the Kentico site.</para>
    ///     <para>This option requires administrator rights.</para>
    ///     <code>Initialize-CMSApplication</code>
    /// </example>
    /// <example>
    ///     <para>Initialize the Kentico CMS Application by using the specified connection string.</para>
    ///     <para>This option does not require administrator rights.</para>
    ///     <code>Initialize-CMSApplication -DatabaseServer KenticoServer -Database Kentico -WebRoot C:\kentico</code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsData.Initialize, "CMSApplication", DefaultParameterSetName = NONE)]
    public class InitializeCMSApplicationCmdlet : MefCmdlet
    {
        #region Constants

        private const string CONNECTIONSTRING = "ConnectionString";
        private const string NONE = "None";
        private const string SERVERANDDATABASE = "ServerAndDatabase";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The connection string for the database connection.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = CONNECTIONSTRING)]
        public string ConnectionString { get; set; }

        /// <summary>
        /// <para type="description">The database server to use for generating the connection string.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = SERVERANDDATABASE)]
        [Alias("SQLServer")]
        public string DatabaseServer { get; set; }

        /// <summary>
        /// <para type="description">The database to use for generating the connection string.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = SERVERANDDATABASE)]
        public string Database { get; set; }

        /// <summary>
        /// <para type="description">The timeout to use for generating the connection string.</para>
        /// </summary>
        [Parameter(ParameterSetName = SERVERANDDATABASE)]
        public int Timeout { get; set; } = 60;

        /// <summary>
        /// <para type="description">The root directory for the Kentico site.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = SERVERANDDATABASE)]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = CONNECTIONSTRING)]
        [Alias("KenticoRoot")]
        public string WebRoot { get; set; }

        /// <summary>
        /// Gets or sets a reference to the business layer for this cmdlet.  Sett by MEF.
        /// </summary>
        [Import]
        public InitializeCMSApplicationBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case CONNECTIONSTRING:
                    this.BusinessLayer.Initialize(this.ConnectionString, this.WebRoot);
                    return;
                case NONE:
                    this.BusinessLayer.Initialize();
                    return;
                case SERVERANDDATABASE:
                    this.BusinessLayer.Initialize(this.DatabaseServer, this.Database, this.Timeout, this.WebRoot);
                    return;
            }
        }

        #endregion

    }
}
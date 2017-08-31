// <copyright file="OpenCMSApplication.cs" company="Chris Crutchfield">
// Copyright (c) Chris Crutchfield. All rights reserved.
// </copyright>

using System.IO;
using System.Management.Automation;
using CMS.DataEngine;
using PoshKentico.Helpers;

namespace PoshKentico.General
{
    [Cmdlet(VerbsCommon.Open, "CMSApplication", DefaultParameterSetName = NONE)]
    public class OpenCMSApplication : PSCmdlet
    {
        #region Constants

        private const string CONNECTIONSTRING = "ConnectionString";
        private const string NONE = "None";
        private const string SERVERANDDATABASE = "ServerAndDatabase";

        #endregion

        #region Properties

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = CONNECTIONSTRING)]
        public string ConnectionString { get; set; }

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = SERVERANDDATABASE)]
        public string DatabaseServer { get; set; }

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = SERVERANDDATABASE)]
        public string Database { get; set; }

        [Parameter(ParameterSetName = SERVERANDDATABASE)]
        public int Timeout { get; set; } = 60;

        [Parameter(Mandatory = true, Position = 2, ParameterSetName = SERVERANDDATABASE)]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = CONNECTIONSTRING)]
        public string WebRoot { get; set; }

        #endregion

        #region Methods

        protected override void BeginProcessing()
        {
            if (CMSApplication.ApplicationInitialized.GetValueOrDefault(false))
            {
                return;
            }

            string connectionString = null;
            switch (this.ParameterSetName)
            {
                case CONNECTIONSTRING:
                    connectionString = this.ConnectionString;
                    break;
                case NONE:
                    CmsApplicationHelper.InitializeKentico(this.WriteDebug, this.WriteVerbose);

                    return;
                case SERVERANDDATABASE:
                    connectionString = $"Data Source={this.DatabaseServer};Initial Catalog={this.Database};Integrated Security=True;Persist Security Info=False;Connect Timeout={this.Timeout};Encrypt=False;Current Language=English";
                    this.WriteDebug("Setting connection string to \"{connectionString}\".");

                    DataConnectionFactory.ConnectionString = connectionString;
                    break;
            }

            CmsApplicationHelper.InitializeKentico(connectionString, new DirectoryInfo(this.WebRoot), this.WriteDebug, this.WriteVerbose);
        }

        #endregion

    }
}
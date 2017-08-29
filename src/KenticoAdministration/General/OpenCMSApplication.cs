using CMS.Base;
using CMS.DataEngine;
using KenticoAdministration.Helpers;
using Microsoft.Web.Administration;
using System;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Xml.Linq;

namespace KenticoAdministration.General
{
    [Cmdlet(VerbsCommon.Open, "CMSApplication", DefaultParameterSetName = NONE)]
    public class OpenCMSApplication : PSCmdlet
    {

        #region Constants

        private const string CONNECTION_STRING = "ConnectionString";
        private const string NONE = "None";
        private const string SERVER_AND_DATABASE = "ServerAndDatabase";

        #endregion


        #region Properties

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = CONNECTION_STRING)]
        public string ConnectionString { get; set; }

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = SERVER_AND_DATABASE)]
        public string DatabaseServer { get; set; }

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = SERVER_AND_DATABASE)]
        public string Database { get; set; }

        [Parameter(ParameterSetName = SERVER_AND_DATABASE)]
        public int Timeout { get; set; } = 60;

        [Parameter(Mandatory = true, Position = 2, ParameterSetName = SERVER_AND_DATABASE)]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = CONNECTION_STRING)]
        public string WebRoot { get; set; }

        #endregion


        #region Methods

        protected override void BeginProcessing()
        {
            if (CMSApplication.ApplicationInitialized.GetValueOrDefault(false)) return;

            string connectionString = null;
            switch (ParameterSetName)
            {
                case (CONNECTION_STRING):
                    connectionString = ConnectionString;
                    break;
                case (NONE):
                    CmsApplicationHelper.InitializeKentico(WriteDebug, WriteVerbose);

                    return;
                case (SERVER_AND_DATABASE):
                    connectionString = $"Data Source={DatabaseServer};Initial Catalog={Database};Integrated Security=True;Persist Security Info=False;Connect Timeout={Timeout};Encrypt=False;Current Language=English";
                    WriteDebug("Setting connection string to \"{connectionString}\".");

                    DataConnectionFactory.ConnectionString = connectionString;
                    break;
            }

            CmsApplicationHelper.InitializeKentico(connectionString, new DirectoryInfo(WebRoot), WriteDebug, WriteVerbose);
        }

        #endregion

    }
}
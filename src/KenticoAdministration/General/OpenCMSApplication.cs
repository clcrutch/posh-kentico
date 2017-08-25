using CMS.Base;
using CMS.DataEngine;
using System;
using System.IO;
using System.Management.Automation;

namespace KenticoAdministration.General
{
    [Cmdlet(VerbsCommon.Open, "CMSApplication", DefaultParameterSetName = SERVER_AND_DATABASE)]
    public class OpenCMSApplication : PSCmdlet
    {

        #region Constants

        private const string SERVER_AND_DATABASE = "ServerAndDatabase";
        private const string CONNECTION_STRING = "ConnectionString";

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
            if (ParameterSetName == SERVER_AND_DATABASE)
            {
                var connectionString = $"Data Source={DatabaseServer};Initial Catalog={Database};Integrated Security=True;Persist Security Info=False;Connect Timeout={Timeout};Encrypt=False;Current Language=English";
                WriteDebug("Setting connection string to \"{connectionString}\".");

                DataConnectionFactory.ConnectionString = connectionString;
            }
            else if (ParameterSetName == CONNECTION_STRING)
                DataConnectionFactory.ConnectionString = ConnectionString;

            WriteDebug("Using deprecated .Net functionality.  Need to find a replacement.");
            AppDomain.CurrentDomain.AppendPrivatePath(Path.GetDirectoryName(typeof(OpenCMSApplication).Assembly.Location));

            SystemContext.WebApplicationPhysicalPath = WebRoot;

            if (!CMSApplication.Init())
                throw new Exception("CMS Application initialization failed.");
        }

        #endregion

    }
}

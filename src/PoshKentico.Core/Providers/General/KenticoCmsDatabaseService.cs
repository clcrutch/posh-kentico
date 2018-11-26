using CMS.CMSImportExport;
using CMS.DataEngine;
using CMS.Membership;
using PoshKentico.Core.Services.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Providers.General
{
    [Export(typeof(ICmsDatabaseService))]
    public class KenticoCmsDatabaseService : ICmsDatabaseService
    {
        private const string ACTIVITY = "Creating Database";
        private const int ACTIVITYCOUNT = 533;

        private int currentActivityCount = 0;

        [Import]
        public ICmsApplicationService CmsApplicationService { get; set; }

        [Import]
        public IOutputService OutputService { get; set; }

        public Version Version
        {
            get
            {
                var versionString = DatabaseHelper.DatabaseVersion;

                if (Version.TryParse(versionString, out Version result))
                {
                    return result;
                }

                return null;
            }
        }

        public string ConnectionString
        {
            get
            {
                return ConnectionHelper.ConnectionString;
            }

            set
            {
                ConnectionHelper.ConnectionString = value;
                DataConnectionFactory.ConnectionString = value;
            }
        }

        public bool Exists => DatabaseHelper.DatabaseExists(this.ConnectionString);

        public void ExecuteQuery(string queryText, QueryDataParameters parameters) =>
            ConnectionHelper.ExecuteNonQuery(queryText, parameters, QueryTypeEnum.SQLQuery);

        public void InstallSqlDatabase()
        {
            this.currentActivityCount = 0;

            var progressRecord = new ProgressRecord(Math.Abs(ACTIVITY.GetHashCode()), ACTIVITY, "Starting")
            {
                PercentComplete = 0,
                RecordType = ProgressRecordType.Processing,
            };
            this.OutputService.WriteProgress(progressRecord);

            bool success = SqlInstallationHelper.InstallDatabase(this.ConnectionString, SqlInstallationHelper.GetSQLInstallPath(), "", "", this.Log);

            progressRecord = new ProgressRecord(Math.Abs(ACTIVITY.GetHashCode()), ACTIVITY, "Finished")
            {
                PercentComplete = 100,
                RecordType = ProgressRecordType.Processing,
            };
            this.OutputService.WriteProgress(progressRecord);
        }

        public bool IsDatabaseInstalled() =>
            this.Version != null;

        private void Log(string message, MessageTypeEnum type)
        {
            switch (type)
            {
                case MessageTypeEnum.Info:
                    this.OutputService.WriteVerbose(message);

                    this.currentActivityCount++;

                    var progressRecord = new ProgressRecord(Math.Abs(ACTIVITY.GetHashCode()), ACTIVITY, message)
                    {
                        PercentComplete = (int)Math.Round(((float)this.currentActivityCount / (float)ACTIVITYCOUNT) * 100),
                        RecordType = ProgressRecordType.Processing,
                    };
                    this.OutputService.WriteProgress(progressRecord);
                    break;
                case MessageTypeEnum.Warning:
                    this.OutputService.WriteWarning(message);
                    break;
                case MessageTypeEnum.Error:
                    throw new Exception(message);
            }
        }
    }
}

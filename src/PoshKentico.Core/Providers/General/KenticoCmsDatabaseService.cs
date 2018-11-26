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
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Providers.General
{
    [Export(typeof(ICmsDatabaseService))]
    public class KenticoCmsDatabaseService : ICmsDatabaseService
    {
        [Import]
        public ICmsApplicationService CmsApplicationService { get; set; }

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
            bool success = SqlInstallationHelper.InstallDatabase(this.ConnectionString, SqlInstallationHelper.GetSQLInstallPath(), "", "", this.Log);
        }

        public bool IsDatabaseInstalled() =>
            false;

        private void Log(string message, MessageTypeEnum type)
        {
            Console.WriteLine(message);
        }
    }
}

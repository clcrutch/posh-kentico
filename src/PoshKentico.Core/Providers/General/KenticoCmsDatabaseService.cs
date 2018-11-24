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
            //var zipFile = this.GetSqlZipFileInfo();
            //var temporaryDirectory = this.GetTemporaryDirectory();

            //ZipFile.ExtractToDirectory(zipFile.FullName, temporaryDirectory.FullName);

            //foreach (var script in this.GetObjectScripts(temporaryDirectory.FullName))
            //{
            //    var pieces = script.Split(new string[] { "GO" }, StringSplitOptions.RemoveEmptyEntries);

            //    foreach (var piece in pieces)
            //    {
            //        this.ExecuteQuery(piece, null);
            //    }
            //}

            //this.CmsApplicationService.Initialize(true);

            //var importSettings = new SiteImportSettings(UserInfoProvider.AdministratorUser)
            //{
            //    SourceFilePath = zipFile.FullName,
            //    ImportType = ImportTypeEnum.AllNonConflicting,
            //    ImportOnlyNewObjects = true,
            //    AllowBulkInsert = true,
            //    CreateVersion = false,
            //};
            //importSettings.LoadDefaultSelection();

            //ImportProvider.ImportObjectsData(importSettings);
            //ImportProvider.DeleteTemporaryFiles(importSettings, false);

            //temporaryDirectory.Delete(true);

            bool success = SqlInstallationHelper.InstallDatabase(this.ConnectionString, SqlInstallationHelper.GetSQLInstallPath(), "", "", this.Log);
        }

        private void Log(string message, MessageTypeEnum type)
        {
            Console.WriteLine(message);
        }

        public bool IsDatabaseInstalled() =>
            false;

        private IEnumerable<string> GetObjectScriptPaths(string extractedZipPath)
        {
            var objectFile = new FileInfo(Path.Combine(extractedZipPath, "defaultDBObjects.txt"));

            using (var reader = objectFile.OpenText())
            {
                return
                    from l in reader.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                    where !l.Trim().StartsWith("//")
                    select l.Trim();
            }
        }

        private IEnumerable<string> GetObjectScripts(string extractedZipPath)
        {
            foreach (var path in this.GetObjectScriptPaths(extractedZipPath))
            {
                var combinedPath = Path.Combine(extractedZipPath, path.Replace("/", "\\"));

                using (var reader = File.OpenText(combinedPath))
                {
                    yield return reader.ReadToEnd();
                }
            }
        }

        private FileInfo GetSqlZipFileInfo()
        {
            var zipLocation = SqlInstallationHelper.GetSQLInstallPath();
            zipLocation = zipLocation.Replace("[SQL.zip]", "SQL.zip");

            if (!File.Exists(zipLocation))
            {
                throw new Exception($"Cannot find SQL.zip, expecting it at \"{zipLocation}\"");
            }

            return new FileInfo(zipLocation);
        }

        private DirectoryInfo GetTemporaryDirectory()
        {
            var temporaryPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            var temporaryDirectory = new DirectoryInfo(temporaryPath);

            temporaryDirectory.Create();

            return temporaryDirectory;
        }
    }
}

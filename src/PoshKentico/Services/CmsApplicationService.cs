using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using CMS.Base;
using CMS.DataEngine;
using Microsoft.Web.Administration;

namespace PoshKentico.Services
{
    [Export(typeof(ICmsApplicationService))]
    public class CmsApplicationService : ICmsApplicationService
    {
        public (DirectoryInfo siteLocation, string connectionString) FindSite(Action<string> writeDebug = null, Action<string> writeVerbose = null)
        {
            var serverManager = new ServerManager();

            // All websites that have a web.config.
            var directoryInfos = serverManager.Sites
                .SelectMany(s => s.Applications)
                .SelectMany(a => a.VirtualDirectories)
                .Select(v => new DirectoryInfo(Environment.ExpandEnvironmentVariables(v.PhysicalPath)));

            string connectionString = null;
            foreach (var directoryInfo in directoryInfos)
            {
                writeDebug?.Invoke($"Searching for \"web.config\" in {directoryInfo.FullName}.");

                // The web.config in the folder
                var webConfigDirectoryInfo = directoryInfo.GetFiles("web.config").SingleOrDefault();

                // If we somehow can't find it, continue.
                if (webConfigDirectoryInfo == null)
                {
                    writeDebug?.Invoke("No \"web.config\" found. Skipping.");
                    continue;
                }

                // Parse the document
                var webConfigXDocument = XDocument.Load(webConfigDirectoryInfo.FullName);

                writeDebug?.Invoke("Searching for \"CMSConnectionString\" in \"web.config\".");

                // Find the connection string.
                connectionString = (from d in webConfigXDocument.Descendants("add")
                                    where d.Attribute("name")?.Value == "CMSConnectionString"
                                    select d.Attribute("connectionString")?.Value).SingleOrDefault();

                // If we found one, we can exit.
                if (!string.IsNullOrEmpty(connectionString))
                {
                    writeVerbose?.Invoke($"Selecting \"{directoryInfo.FullName}\" as Kentico site.");

                    return (directoryInfo, connectionString);
                }

                writeDebug?.Invoke("No connection string found.  Skipping.");
            }

            return (null, null);
        }

        public void Initialize(Action<string> writeDebug = null, Action<string> writeVerbose = null)
        {
            // We don't need to do anything if the application is already initialized.
            if (CMSApplication.ApplicationInitialized.GetValueOrDefault(false))
            {
                return;
            }

            // Search for the Kentico site in IIS.
            var kenticoSite = this.FindSite(writeDebug, writeVerbose);

            if (string.IsNullOrWhiteSpace(kenticoSite.connectionString) || kenticoSite.siteLocation == null)
            {
                throw new Exception("Could not find Kentico site.");
            }

            this.Initialize(kenticoSite.connectionString, kenticoSite.siteLocation);
        }

        public void Initialize(string connectionString, DirectoryInfo directoryInfo, Action<string> writeDebug = null, Action<string> writeVerbose = null)
        {
            DataConnectionFactory.ConnectionString = connectionString;

            // This is how Kentico recommends working with their API.
#pragma warning disable CS0618 // Type or member is obsolete
            AppDomain.CurrentDomain.AppendPrivatePath(Path.GetDirectoryName(typeof(CmsApplicationService).Assembly.Location));
#pragma warning restore CS0618 // Type or member is obsolete

            SystemContext.WebApplicationPhysicalPath = directoryInfo.FullName;

            if (!CMSApplication.Init())
            {
                throw new Exception("CMS Application initialization failed.");
            }
        }
    }
}

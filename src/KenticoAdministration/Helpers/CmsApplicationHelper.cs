using CMS.Base;
using CMS.DataEngine;
using Microsoft.Web.Administration;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace KenticoAdministration.Helpers
{
    internal class CmsApplicationHelper
    {
        public static DirectoryInfo FindKenticoSite(out string connectionString, Action<string> writeDebug = null, Action<string> writeVerbose = null)
        {
            // To find a Kentico site, we perform the following steps:
            // 1. Get a list of all the sites from IIS
            // 2. Get a list of all applications from the sites
            // 3. Get a list of all the virtual directories from the applications
            // 4. Continue processing virtual directory if a web.config file exits
            // 5. Parse the document and find an "add" node with name="CMSConnectionString"
            // 6. If the connection string is valid, then stop processing.  This is a Kentico site.

            var serverManager = new ServerManager();
            // All websites that have a web.config.
            var directoryInfos = serverManager.Sites
                .SelectMany(s => s.Applications)
                .SelectMany(a => a.VirtualDirectories)
                .Select(v => new DirectoryInfo(v.PhysicalPath));

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

                    return directoryInfo;
                }

                writeDebug?.Invoke("No connection string found.  Skipping.");
            }

            connectionString = null;
            return null;
        }

        public static void InitializeKentico(Action<string> writeDebug = null, Action<string> writeVerbose = null)
        {
            // We don't need to do anything if the application is already initialized.
            if (CMSApplication.ApplicationInitialized.GetValueOrDefault(false)) return;

            string connectionString = null;
            // Search for the Kentico site in IIS.
            DirectoryInfo kenticoSite = FindKenticoSite(out connectionString, writeDebug, writeVerbose);

            InitializeKentico(connectionString, kenticoSite);
        }

        public static void InitializeKentico(string connectionString, DirectoryInfo directoryInfo, Action<string> writeDebug = null, Action<string> writeVerbose = null)
        {
            DataConnectionFactory.ConnectionString = connectionString;

            writeDebug?.Invoke("Using deprecated .Net functionality.  Need to find a replacement.");
            AppDomain.CurrentDomain.AppendPrivatePath(Path.GetDirectoryName(typeof(CmsApplicationHelper).Assembly.Location));

            SystemContext.WebApplicationPhysicalPath = directoryInfo.FullName;

            if (!CMSApplication.Init())
                throw new Exception("CMS Application initialization failed.");
        }
    }
}

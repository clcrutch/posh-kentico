// <copyright file="KenticoCmsApplicationService.cs" company="Chris Crutchfield">
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

using System;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using CMS.Base;
using CMS.DataEngine;
using Microsoft.Web.Administration;
using Newtonsoft.Json;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Core.Providers.General
{
    /// <summary>
    /// Implementation of <see cref="ICmsApplicationService"/> that uses Kentico.
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Export(typeof(ICmsApplicationService))]
    public class KenticoCmsApplicationService : ICmsApplicationService
    {

        #region Properties

        /// <summary>
        /// Gets the initialization state of the CMS Application.
        /// </summary>
        public InitializationState InitializationState
        {
            get
            {
                if (CMSApplication.ApplicationInitialized.HasValue)
                {
                    return CMSApplication.ApplicationInitialized.Value ? InitializationState.Initialized : InitializationState.Error;
                }

                return InitializationState.Uninitialized;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// This method requires administrator access.
        ///
        /// Finds a Kentico website by performing the following steps:
        /// 1. Get a list of all the sites from IIS
        /// 2. Get a list of all applications from the sites
        /// 3. Get a list of all the virtual directories from the applications
        /// 4. Continue processing virtual directory if a web.config file exits
        /// 5. Parse the document and find an "add" node with name="CMSConnectionString"
        /// 6. If the connection string is valid, then stop processing.  This is a Kentico site.
        /// </summary>
        /// <param name="writeDebug">A delegate for writing to the debug stream.</param>
        /// <param name="writeVerbose">A delegate for writing to the verbose stream.</param>
        /// <returns>The directory and the connection string for the Kentico site.</returns>
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

        /// <summary>
        /// Initialize Kentico CMS Application using FindKenticoSite or a cached version to locate the site.
        /// </summary>
        /// <param name="useCached">Use the cached location for the Kentico Site.  When true and have already found Kentico in a previous run, this method does not require admin.</param>
        /// <param name="writeDebug">A delegate for writing to the debug stream.</param>
        /// <param name="writeVerbose">A delegate for writing to the verbose stream.</param>
        public void Initialize(bool useCached, Action<string> writeDebug = null, Action<string> writeVerbose = null)
        {
            // We don't need to do anything if the application is already initialized.
            if (this.InitializationState != InitializationState.Uninitialized)
            {
                return;
            }

            if (useCached && this.HasCachedSiteLocation())
            {
                var cache = this.GetCache();

                this.Initialize(new DirectoryInfo(cache.SiteLocation), cache.ConnectionString, writeDebug, writeVerbose);
            }
            else
            {
                // Search for the Kentico site in IIS.
                var (siteLocation, connectionString) = this.FindSite(writeDebug, writeVerbose);

                if (string.IsNullOrWhiteSpace(connectionString) || siteLocation == null)
                {
                    throw new Exception("Could not find Kentico site.");
                }

                var cache = this.GetCache();
                this.CacheSiteLocation(siteLocation, connectionString);

                this.Initialize(siteLocation, connectionString, writeDebug, writeVerbose);
            }
        }

        /// <summary>
        /// Initialize Kentico CMS Application using the supplied parameters.
        /// </summary>
        /// <param name="siteLocation">The directory where the Kentico site resides.</param>
        /// <param name="connectionString">The connection string to use for initializing the CMS Application.</param>
        /// <param name="writeDebug">A delegate for writing to the debug stream.</param>
        /// <param name="writeVerbose">A delegate for writing to the verbose stream.</param>
        public void Initialize(DirectoryInfo siteLocation, string connectionString, Action<string> writeDebug = null, Action<string> writeVerbose = null)
        {
            // We don't need to do anything if the application is already initialized.
            if (this.InitializationState != InitializationState.Uninitialized)
            {
                return;
            }

            DataConnectionFactory.ConnectionString = connectionString;

            // This is how Kentico recommends working with their API.
#pragma warning disable CS0618 // Type or member is obsolete
            AppDomain.CurrentDomain.AppendPrivatePath(Path.GetDirectoryName(typeof(KenticoCmsApplicationService).Assembly.Location));
#pragma warning restore CS0618 // Type or member is obsolete

            SystemContext.WebApplicationPhysicalPath = siteLocation.FullName;

            if (!CMSApplication.Init())
            {
                throw new Exception("CMS Application initialization failed.");
            }
        }

        private void CacheSiteLocation(DirectoryInfo siteLocation, string connectionString)
        {
            var cachePath = this.GetCachePath();
            var cacheFileInfo = new FileInfo(cachePath);

            KenticoSiteLocationCache cache = this.GetCache();
            cache.SiteLocation = siteLocation.FullName;
            cache.ConnectionString = connectionString;

            if (this.HasCachedSiteLocation())
            {
                File.Delete(cachePath);
            }

            if (!cacheFileInfo.Directory.Exists)
            {
                cacheFileInfo.Directory.Create();
            }

            using (var writer = File.CreateText(cachePath))
            {
                writer.Write(JsonConvert.SerializeObject(cache));
            }
        }

        private KenticoSiteLocationCache GetCache()
        {
            var cachePath = this.GetCachePath();

            if (this.HasCachedSiteLocation())
            {
                using (var reader = File.OpenText(cachePath))
                {
                    return JsonConvert.DeserializeObject<KenticoSiteLocationCache>(reader.ReadToEnd());
                }
            }
            else
            {
                return new KenticoSiteLocationCache();
            }
        }

        private string GetCachePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "PoshKentico", "cache.json");
        }

        private bool HasCachedSiteLocation()
        {
            return File.Exists(this.GetCachePath());
        }

        #endregion

    }
}

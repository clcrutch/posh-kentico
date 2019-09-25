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
using System.Reflection;
using System.Web.Configuration;
using System.Xml.Linq;
using CMS.Base;
using CMS.DataEngine;
using CMS.Scheduler;
using Microsoft.Web.Administration;
using Mono.Cecil;
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
        #region Variables

        private static bool locallyInitialized = false;

        private DirectoryInfo siteLocation;
        private Version version;
        private System.Configuration.Configuration webConfig;

        #endregion

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

        /// <inheritdoc />
        public string SiteLocation { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ICmsDatabaseService"/>.  Set by MEF.
        /// </summary>
        [Import]
        public ICmsDatabaseService CmsDatabaseService { get; set; }

        /// <summary>
        /// Gets or Sets the <see cref="IOutputService"/>.  Set by MEF.
        /// </summary>
        [Import]
        public IOutputService OutputService { get; set; }

        /// <summary>
        /// Gets the version of the current Kentico application.
        /// </summary>
        public Version Version
        {
            get
            {
                if (this.version == null)
                {
                    var dllLocation = Path.Combine(this.siteLocation.FullName, "bin", "CMS.Base.dll");

                    if (File.Exists(dllLocation))
                    {
                        var assembly = AssemblyDefinition.ReadAssembly(dllLocation);

                        var versionAttribute = assembly.CustomAttributes.First(x => x.AttributeType.Name == nameof(AssemblyInformationalVersionAttribute));
                        var versionString = (string)versionAttribute.ConstructorArguments.First().Value;

                        Version.TryParse(versionString, out this.version);
                    }
                }

                return this.version;
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
        /// <returns>The directory and the connection string for the Kentico site.</returns>
        public (DirectoryInfo siteLocation, string connectionString) FindSite()
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
                this.OutputService.WriteDebug($"Searching for \"web.config\" in {directoryInfo.FullName}.");

                if (!directoryInfo.Exists)
                {
                    this.OutputService.WriteDebug($"Directory {directoryInfo.FullName} does not exist. Skipping.");

                    continue;
                }

                // The web.config in the folder
                var webConfigDirectoryInfo = directoryInfo.GetFiles("web.config").SingleOrDefault();

                // If we somehow can't find it, continue.
                if (webConfigDirectoryInfo == null)
                {
                    this.OutputService.WriteDebug("No \"web.config\" found. Skipping.");
                    continue;
                }

                // Parse the document
                var webConfigXDocument = XDocument.Load(webConfigDirectoryInfo.FullName);

                this.OutputService.WriteDebug("Searching for \"CMSConnectionString\" in \"web.config\".");

                // Find the connection string.
                connectionString = (from d in webConfigXDocument.Descendants("add")
                                    where d.Attribute("name")?.Value == "CMSConnectionString"
                                    select d.Attribute("connectionString")?.Value).SingleOrDefault();

                // If we found one, we can exit.
                if (!string.IsNullOrEmpty(connectionString))
                {
                    this.OutputService.WriteVerbose($"Selecting \"{directoryInfo.FullName}\" as Kentico site.");

                    return (directoryInfo, connectionString);
                }

                this.OutputService.WriteDebug("No connection string found.  Skipping.");
            }

            return (null, null);
        }

        /// <summary>
        /// Initialize Kentico CMS Application using FindKenticoSite or a cached version to locate the site.
        /// </summary>
        /// <param name="useCached">Use the cached location for the Kentico Site.  When true and have already found Kentico in a previous run, this method does not require admin.</param>
        public void Initialize(bool useCached)
        {
            // We don't need to do anything if the application is already initialized.
            if (this.InitializationState != InitializationState.Uninitialized)
            {
                this.OutputService.WriteDebug("Kentico is already initialized, skipping.");

                return;
            }

            if (useCached && this.HasCachedSiteLocation())
            {
                this.OutputService.WriteDebug("Using cached version.");

                var cache = this.GetCache();

                this.Initialize(
                    new DirectoryInfo(cache.SiteLocation),
                    cache.ConnectionString);
            }
            else
            {
                this.OutputService.WriteDebug("Looking for Kentico using IIS.");

                // Search for the Kentico site in IIS.
                var (siteLocation, connectionString) = this.FindSite();

                if (string.IsNullOrWhiteSpace(connectionString) || siteLocation == null)
                {
                    throw new Exception("Could not find Kentico site.");
                }

                this.CacheSiteLocation(siteLocation, connectionString);

                this.Initialize(
                    siteLocation,
                    connectionString);
            }
        }

        /// <summary>
        /// Initialize Kentico CMS Application using the supplied parameters.
        /// </summary>
        /// <param name="siteLocation">The directory where the Kentico site resides.</param>
        /// <param name="connectionString">The connection string to use for initializing the CMS Application.</param>
        public void Initialize(DirectoryInfo siteLocation, string connectionString)
        {
            // We don't need to do anything if the application is already initialized.
            if (this.InitializationState != InitializationState.Uninitialized)
            {
                this.OutputService.WriteDebug("Kentico is already initialized, skipping.");

                return;
            }

            this.OutputService.WriteVerbose($"Login user = {Environment.UserName}");

            if (!locallyInitialized)
            {
                this.CmsDatabaseService.ConnectionString = connectionString;

                // This is how Kentico recommends working with their API.
#pragma warning disable CS0618 // Type or member is obsolete
                AppDomain.CurrentDomain.AppendPrivatePath(Path.GetDirectoryName(typeof(KenticoCmsApplicationService).Assembly.Location));
#pragma warning restore CS0618 // Type or member is obsolete

                SystemContext.WebApplicationPhysicalPath = siteLocation.FullName;
                this.SiteLocation = siteLocation.FullName;

                // SettingsHelper will fire this event and return its value if it returns something.
                CMSAppSettings.GetApplicationSettings += this.CMSAppSettings_GetApplicationSettings;

                SystemContext.WebApplicationPhysicalPath = siteLocation.FullName;
                this.siteLocation = siteLocation;

                locallyInitialized = true;
            }

            // We cannot setup the application unless the database is setup.
            if (!this.CmsDatabaseService.IsDatabaseInstalled())
            {
                this.OutputService.WriteDebug("CMS database is not insalled, exiting...");

                return;
            }

            if (!CMSApplication.Init())
            {
                throw new Exception("CMS Application initialization failed.");
            }
        }

        private void CacheSiteLocation(DirectoryInfo siteLocation, string connectionString)
        {
            var cachePath = this.GetCachePath();
            var cacheFileInfo = new FileInfo(cachePath);

            this.OutputService.WriteDebug($"Cache Path = {cachePath}");

            KenticoSiteLocationCache cache = this.GetCache();
            cache.SiteLocation = siteLocation.FullName;
            cache.ConnectionString = connectionString;

            this.OutputService.WriteDebug($"Site Location = {siteLocation}");
            this.OutputService.WriteDebug($"Connection String = {connectionString}");

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

        private string CMSAppSettings_GetApplicationSettings(string key)
        {
            return this.GetWebConfig().AppSettings.Settings[key]?.Value;
        }

        private KenticoSiteLocationCache GetCache()
        {
            var cachePath = this.GetCachePath();

            this.OutputService.WriteDebug($"Cache Path = {cachePath}");

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

        private System.Configuration.Configuration GetWebConfig()
        {
            if (this.webConfig != null)
            {
                return this.webConfig;
            }

            var configFile = new FileInfo(Path.Combine(this.SiteLocation, "Web.config"));
            var vdm = new VirtualDirectoryMapping(configFile.DirectoryName, true, configFile.Name);
            var wcfm = new WebConfigurationFileMap();
            wcfm.VirtualDirectories.Add("/", vdm);
            this.webConfig = System.Web.Configuration.WebConfigurationManager.OpenMappedWebConfiguration(wcfm, "/");

            return this.webConfig;
        }

        private bool HasCachedSiteLocation()
        {
            return File.Exists(this.GetCachePath());
        }

        #endregion

    }
}

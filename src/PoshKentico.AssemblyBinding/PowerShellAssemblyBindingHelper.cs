// <copyright file="PowerShellAssemblyBindingHelper.cs" company="Chris Crutchfield">
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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace PoshKentico.AssemblyBinding
{
    /// <summary>
    /// Reimplementation of assembly binding redirects for PowerShell modules.
    /// </summary>
    public static class PowerShellAssemblyBindingHelper
    {
        #region Variables

        private static Configuration configuration;

        #endregion

        #region Methods

        /// <summary>
        /// Initializes PowerShell using the [module-name].config file in the module directory.
        /// </summary>
        public static void Initialize()
        {
            configuration = GetConfiguration();

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private static Configuration GetConfiguration()
        {
            var serializer = new XmlSerializer(typeof(Configuration));
            var configurationPath = GetConfigurationPath();

            using (var stream = File.Open(configurationPath, FileMode.Open))
            {
                return serializer.Deserialize(stream) as Configuration;
            }
        }

        private static string GetConfigurationPath()
        {
            var assembly = new FileInfo(typeof(PowerShellAssemblyBindingHelper).Assembly.Location);
            var directory = assembly.Directory;

            var moduleName = GetModuleName();

            return Path.Combine(directory.FullName, $"{moduleName}.config");
        }

        private static string GetModuleName()
        {
            var assembly = new FileInfo(typeof(PowerShellAssemblyBindingHelper).Assembly.Location);
            var moduleDefinition = Directory.GetFiles(assembly.Directory.FullName, "*.psd1").Single();

            return moduleDefinition.Replace(".psd1", string.Empty);
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var name = configuration?.Runtime?.AssemblyBinding?.UpdateAssemblyName(args.Name);

            if (name == null)
            {
                return null;
            }

            var assembly = (from a in AppDomain.CurrentDomain.GetAssemblies()
                            where a.FullName == name
                            select a).FirstOrDefault();

            if (assembly != null)
            {
                return assembly;
            }

            return Assembly.Load(name);
        }

        #endregion

    }
}

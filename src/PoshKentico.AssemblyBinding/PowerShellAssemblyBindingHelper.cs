using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace PoshKentico.AssemblyBinding
{
    public static class PowerShellAssemblyBindingHelper
    {
        private static Configuration configuration;

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

            return assembly.Directory.Name;
        }

        private static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
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
    }
}

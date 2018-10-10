using Autofac;
using Mono.Cecil;
using PoshKentico.CodeWeaving.Weavers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PoshKentico.CodeWeaving
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = GetContainer();

            var buildPath = new DirectoryInfo(args[0]);
            var assemblies = GetAssemblies(buildPath);

            foreach (var assembly in assemblies)
            {
                foreach (var weaver in container.Resolve<IEnumerable<IWeaver>>())
                {
                    weaver.Execute(assembly);
                }

                Console.WriteLine($"Writing \"{assembly.Name.Name}\"...");
                assembly.Write();
            }
        }

        private static IEnumerable<AssemblyDefinition> GetAssemblies(DirectoryInfo buildDirectory)
        {
            foreach (var dll in buildDirectory.GetFiles("CMS.*.dll", SearchOption.TopDirectoryOnly))
            {
                using (FileStream fs = File.Open(dll.FullName, FileMode.Open, FileAccess.ReadWrite))
                {
                    using (var resolver = new AssemblyResolver())
                    {
                        resolver.AddSearchDirectory(buildDirectory.FullName);

                        var parameters = new ReaderParameters
                        {
                            AssemblyResolver = resolver,
                        };

                        using (var assembly = AssemblyDefinition.ReadAssembly(fs, parameters))
                        {
                            yield return assembly;
                        }
                    }
                }
            }
        }

        private static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            var weavers = from t in Assembly.GetEntryAssembly().GetTypes()
                          where t.GetInterfaces().Any(x => x == typeof(IWeaver))
                          select t;

            foreach (var weaver in weavers)
            {
                builder.RegisterType(weaver).As<IWeaver>();
            }

            return builder.Build();
        }
    }
}

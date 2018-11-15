// <copyright file="Program.cs" company="Chris Crutchfield">
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using Mono.Cecil;
using PoshKentico.CodeWeaving.Weavers;

namespace PoshKentico.CodeWeaving
{
    /// <summary>
    /// Entry point for the application.
    /// </summary>
    internal class Program
    {
        private static int Main(string[] args)
        {
            try
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

                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());

                return 1;
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

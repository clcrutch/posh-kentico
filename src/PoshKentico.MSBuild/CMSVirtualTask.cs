// <copyright file="CMSVirtualTask.cs" company="Chris Crutchfield">
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
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Mono.Cecil;

namespace PoshKentico.MSBuild
{
    /// <summary>
    /// MSBuild Task for making properties of CMS.*.dll to virtual.
    /// </summary>
    public class CMSVirtualTask : Task
    {
        #region Properties

        /// <summary>
        /// Gets or sets the target dir for project.
        /// </summary>
        [Required]
        public string TargetDir { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override bool Execute()
        {
            var tempFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            var tempFolderInfo = Directory.CreateDirectory(tempFolder);

            this.CopyFiles(new DirectoryInfo(this.TargetDir), tempFolderInfo);

            try
            {
                var resolver = new DefaultAssemblyResolver();
                resolver.AddSearchDirectory(tempFolder);

                var parameters = new ReaderParameters
                {
                    AssemblyResolver = resolver,
                };

                foreach (var dll in tempFolderInfo.GetFiles("CMS.*.dll", SearchOption.TopDirectoryOnly))
                {
                    using (var assembly = AssemblyDefinition.ReadAssembly(dll.FullName, parameters))
                    {
                        this.MakePropertiesVirtual(assembly);
                        assembly.Write(Path.Combine(this.TargetDir, dll.Name));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                foreach (var file in tempFolderInfo.GetFiles())
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            return true;
        }

        private void CopyFiles(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (var file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name));
            }
        }

        private void MakePropertiesVirtual(AssemblyDefinition assemblyDefinition)
        {
            foreach (var type in assemblyDefinition.MainModule.Types)
            {
                this.MakePropertiesVirtual(type);
            }
        }

        private void MakePropertiesVirtual(TypeDefinition typeDefinition)
        {
            foreach (var prop in typeDefinition.Properties)
            {
                this.MakePropertyMethodVirtual(prop.GetMethod);
                this.MakePropertyMethodVirtual(prop.SetMethod);
            }
        }

        private void MakePropertyMethodVirtual(MethodDefinition methodDefinition)
        {
            if (methodDefinition == null ||
                methodDefinition.IsStatic ||
                !methodDefinition.IsPublic ||
                methodDefinition.DeclaringType.IsInterface ||
                methodDefinition.IsAbstract ||
                (methodDefinition.IsVirtual & !methodDefinition.IsNewSlot))
            {
                return;
            }

            methodDefinition.Attributes =
                MethodAttributes.Public |
                MethodAttributes.HideBySig |
                MethodAttributes.SpecialName |
                MethodAttributes.NewSlot |
                MethodAttributes.Virtual;
        }

        #endregion

    }
}

using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Mono.Cecil;
using System;
using System.IO;
using System.Linq;

namespace PoshKentico.MSBuild
{
    public class CMSVirtualTask : Task
    {
        [Required]
        public string TargetDir { get; set; }

        public override bool Execute()
        {
            var tempFolder = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            var tempFolderInfo = Directory.CreateDirectory(tempFolder);

            CopyFiles(new DirectoryInfo(TargetDir), tempFolderInfo);

            try
            {
                var resolver = new DefaultAssemblyResolver();
                resolver.AddSearchDirectory(tempFolder);

                var parameters = new ReaderParameters
                {
                    AssemblyResolver = resolver
                };

                foreach (var dll in tempFolderInfo.GetFiles("CMS.*.dll", SearchOption.TopDirectoryOnly))
                {
                    using (var assembly = AssemblyDefinition.ReadAssembly(dll.FullName, parameters))
                    {
                        MakePropertiesVirtual(assembly);
                        assembly.Write(Path.Combine(TargetDir, dll.Name));
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
                MakePropertyMethodVirtual(prop.GetMethod);
                MakePropertyMethodVirtual(prop.SetMethod);
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
    }
}

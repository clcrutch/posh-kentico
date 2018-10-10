using System;
using System.Collections.Generic;
using System.Text;
using Mono.Cecil;

namespace PoshKentico.CodeWeaving.Weavers
{
    internal class PublicPropertiesVirtualWeaver : IWeaver
    {
        public void Execute(AssemblyDefinition assemblyDefinition)
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
    }
}

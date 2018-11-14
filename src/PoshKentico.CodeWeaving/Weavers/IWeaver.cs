using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoshKentico.CodeWeaving.Weavers
{
    internal interface IWeaver
    {
        void Execute(AssemblyDefinition assemblyDefinition);
    }
}

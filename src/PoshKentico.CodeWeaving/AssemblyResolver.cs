using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Text;

namespace PoshKentico.CodeWeaving
{
    internal class AssemblyResolver : BaseAssemblyResolver
    {
        private readonly IDictionary<string, AssemblyDefinition> cache;

        public AssemblyResolver()
        {
            cache = new Dictionary<string, AssemblyDefinition>();
        }

        protected override void Dispose(bool disposing)
        {
            foreach (var item in cache.Values)
            {
                item.Dispose();
            }

            base.Dispose(disposing);
        }

        public override AssemblyDefinition Resolve(AssemblyNameReference name)
        {
            var fullName = name.FullName;

            if (cache.TryGetValue(fullName, out AssemblyDefinition @return))
            {
                return @return;
            }

            @return = base.Resolve(name);

            cache.Add(fullName, @return);

            return @return;
        }
    }
}

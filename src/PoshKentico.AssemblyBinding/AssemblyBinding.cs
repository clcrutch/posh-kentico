using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PoshKentico.AssemblyBinding
{
    public class AssemblyBinding
    {
        [XmlElement("dependentAssembly")]
        public List<DependentAssembly> DependentAssemblies { get; set; }

        public string UpdateAssemblyName(string assemblyFullName)
        {
            if (DependentAssemblies == null)
            {
                return null;
            }

            if (!assemblyFullName.Contains("Version="))
            {
                return null;
            }

            var @return = (from da in DependentAssemblies
                           select da.UpdateAssemblyName(assemblyFullName)).FirstOrDefault(x => x != null);

            return @return;
        }
    }
}

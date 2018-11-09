using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PoshKentico.AssemblyBinding
{
    public class Runtime
    {
        [XmlElement("assemblyBinding", Namespace = "urn:schemas-microsoft-com:asm.v1")]
        public AssemblyBinding AssemblyBinding { get; set; }
    }
}

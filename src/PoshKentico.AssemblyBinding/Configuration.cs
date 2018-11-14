using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PoshKentico.AssemblyBinding
{
    [XmlRoot("configuration")]
    public class Configuration
    {
        [XmlElement("runtime")]
        public Runtime Runtime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PoshKentico.AssemblyBinding
{
    public class DependentAssembly
    {
        [XmlElement("assemblyIdentity")]
        public AssemblyIdentity AssemblyIdentity { get; set; }

        [XmlElement("bindingRedirect")]
        public BindingRedirect BindingRedirect { get; set; }

        public string UpdateAssemblyName(string assemblyFullName)
        {
            if (AssemblyIdentity.Matches(assemblyFullName) && BindingRedirect.Matches(assemblyFullName))
            {
                return $"{AssemblyIdentity.Name}, Version={BindingRedirect.NewVersion}, Culture={AssemblyIdentity.Culture}, PublicKeyToken={AssemblyIdentity.PublicKeyToken}";
            }

            return null;
        }
    }
}

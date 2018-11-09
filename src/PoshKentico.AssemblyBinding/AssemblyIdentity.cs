using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PoshKentico.AssemblyBinding
{
    public class AssemblyIdentity
    {
        private static readonly Regex cultureRegex = new Regex(@"(?<=Culture=)[a-z]+(?=,)", RegexOptions.Compiled);
        private static readonly Regex nameRegex = new Regex(@"^[a-zA-Z_]\w*(\.[a-zA-Z_]\w*)*(?=,)", RegexOptions.Compiled);
        private static readonly Regex publicKeyTokenRegex = new Regex(@"(?<=PublicKeyToken=)[a-f0-9]+", RegexOptions.Compiled);

        [XmlAttribute("culture")]
        public string Culture { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("publicKeyToken")]
        public string PublicKeyToken { get; set; }

        public bool Matches(string assemblyFullName)
        {
            var culture = cultureRegex.Match(assemblyFullName).Value;
            var name = nameRegex.Match(assemblyFullName).Value;
            var publicKeyToken = publicKeyTokenRegex.Match(assemblyFullName).Value;

            return this.Name == name &&
                this.Culture == culture &&
                this.PublicKeyToken == publicKeyToken;
        }
    }
}

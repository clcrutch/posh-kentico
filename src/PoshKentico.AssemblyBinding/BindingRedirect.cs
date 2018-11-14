using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PoshKentico.AssemblyBinding
{
    public class BindingRedirect
    {
        private static readonly Regex AssemblyNameVersionRegex = new Regex(@"(?<=Version=)[0-9\.]+(?=,)", RegexOptions.Compiled);

        private Version minVersion;
        private Version maxVersion;

        [XmlAttribute("newVersion")]
        public string NewVersion { get; set; }

        [XmlAttribute("oldVersion")]
        public string OldVersion { get; set; }

        [XmlIgnore]
        protected Version MinVersion
        {
            get
            {
                if (minVersion == null)
                {
                    var versionStrings = OldVersion.Split('-');
                    minVersion = Version.Parse(versionStrings[0]);
                }

                return minVersion;   
            }
        }

        [XmlIgnore]
        protected Version MaxVersion
        {
            get
            {
                if (maxVersion == null)
                {
                    var versionStrings = OldVersion.Split('-');
                    maxVersion = Version.Parse(versionStrings[1]);
                }

                return maxVersion;
            }
        }

        public bool Matches(string assemblyFullName)
        {
            var version = Version.Parse(AssemblyNameVersionRegex.Match(assemblyFullName).Value);

            return version >= MinVersion && version <= MaxVersion;
        }
    }
}

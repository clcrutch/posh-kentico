// <copyright file="BindingRedirect.cs" company="Chris Crutchfield">
// Copyright (C) 2017  Chris Crutchfield
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see &lt;http://www.gnu.org/licenses/&gt;.
// </copyright>

using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace PoshKentico.AssemblyBinding
{
    /// <summary>
    /// Definition of the BindingRedirect node.
    /// </summary>
    public class BindingRedirect
    {
        #region ReadOnly

        private static readonly Regex AssemblyNameVersionRegex = new Regex(@"(?<=Version=)[0-9\.]+(?=,)", RegexOptions.Compiled);

        #endregion

        #region Variables

        private Version minVersion;
        private Version maxVersion;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the new version attribute.
        /// </summary>
        [XmlAttribute("newVersion")]
        public string NewVersion { get; set; }

        /// <summary>
        /// Gets or sets the old version attribute.
        /// </summary>
        [XmlAttribute("oldVersion")]
        public string OldVersion { get; set; }

        /// <summary>
        /// Gets the minimum old version.
        /// </summary>
        [XmlIgnore]
        protected Version MinVersion
        {
            get
            {
                if (this.minVersion == null)
                {
                    var versionStrings = this.OldVersion.Split('-');
                    this.minVersion = Version.Parse(versionStrings[0]);
                }

                return this.minVersion;
            }
        }

        /// <summary>
        /// Gets teh maximum old version.
        /// </summary>
        [XmlIgnore]
        protected Version MaxVersion
        {
            get
            {
                if (this.maxVersion == null)
                {
                    var versionStrings = this.OldVersion.Split('-');
                    this.maxVersion = Version.Parse(versionStrings[1]);
                }

                return this.maxVersion;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks to see if the <paramref name="assemblyFullName"/> matches the current <see cref="BindingRedirect"/>.
        /// </summary>
        /// <param name="assemblyFullName">The assembly full name to test.</param>
        /// <returns>True if matches, false otherwise.</returns>
        public bool Matches(string assemblyFullName)
        {
            var version = Version.Parse(AssemblyNameVersionRegex.Match(assemblyFullName).Value);

            return version >= this.MinVersion && version <= this.MaxVersion;
        }

        #endregion

    }
}

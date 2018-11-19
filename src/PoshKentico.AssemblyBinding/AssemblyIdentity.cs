// <copyright file="AssemblyIdentity.cs" company="Chris Crutchfield">
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

using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace PoshKentico.AssemblyBinding
{
    /// <summary>
    /// Definition of the AssemblyIdentity node.
    /// </summary>
    public class AssemblyIdentity
    {
        #region Readonly

        private static readonly Regex CultureRegex = new Regex(@"(?<=Culture=)[a-z]+(?=,)", RegexOptions.Compiled);
        private static readonly Regex NameRegex = new Regex(@"^[a-zA-Z_]\w*(\.[a-zA-Z_]\w*)*(?=,)", RegexOptions.Compiled);
        private static readonly Regex PublicKeyTokenRegex = new Regex(@"(?<=PublicKeyToken=)[a-f0-9]+", RegexOptions.Compiled);

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the culture attribute.
        /// </summary>
        [XmlAttribute("culture")]
        public string Culture { get; set; }

        /// <summary>
        /// Gets or sets the name attribute.
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the publickeytoken attribute.
        /// </summary>
        [XmlAttribute("publicKeyToken")]
        public string PublicKeyToken { get; set; }

        #endregion

        /// <summary>
        /// Checks to see if the <paramref name="assemblyFullName"/> matches the current <see cref="AssemblyIdentity"/>.
        /// </summary>
        /// <param name="assemblyFullName">The assembly full name to test.</param>
        /// <returns>True if matches, false otherwise.</returns>
        public bool Matches(string assemblyFullName)
        {
            var culture = CultureRegex.Match(assemblyFullName).Value;
            var name = NameRegex.Match(assemblyFullName).Value;
            var publicKeyToken = PublicKeyTokenRegex.Match(assemblyFullName).Value;

            return this.Name == name &&
                this.Culture == culture &&
                this.PublicKeyToken == publicKeyToken;
        }
    }
}

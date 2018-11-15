// <copyright file="AssemblyBinding.cs" company="Chris Crutchfield">
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

using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace PoshKentico.AssemblyBinding
{
    /// <summary>
    /// Definition of an AssemblyBinding node.
    /// </summary>
    public class AssemblyBinding
    {
        #region Properties

        /// <summary>
        /// Gets or sets a list of dependent assembly nodes.
        /// </summary>
        [XmlElement("dependentAssembly")]
        public List<DependentAssembly> DependentAssemblies { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Converts the <paramref name="assemblyFullName"/> to one based off of the dependent assemblies.  Returns null if not found.
        /// </summary>
        /// <param name="assemblyFullName">The assembly name get a redirect for.</param>
        /// <returns>The redirected assembly name.  Null if not defined.</returns>
        public string UpdateAssemblyName(string assemblyFullName)
        {
            if (this.DependentAssemblies == null)
            {
                return null;
            }

            if (!assemblyFullName.Contains("Version="))
            {
                return null;
            }

            var @return = (from da in this.DependentAssemblies
                           select da.UpdateAssemblyName(assemblyFullName)).FirstOrDefault(x => x != null);

            return @return;
        }

        #endregion

    }
}

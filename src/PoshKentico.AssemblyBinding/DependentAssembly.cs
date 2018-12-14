// <copyright file="DependentAssembly.cs" company="Chris Crutchfield">
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

using System.Xml.Serialization;

namespace PoshKentico.AssemblyBinding
{
    /// <summary>
    /// Definition of the dependent assembly node.
    /// </summary>
    public class DependentAssembly
    {
        #region Properties

        /// <summary>
        /// Gets or sets the assembly identity node.
        /// </summary>
        [XmlElement("assemblyIdentity")]
        public AssemblyIdentity AssemblyIdentity { get; set; }

        /// <summary>
        /// Gets or sets the binding redirect node.
        /// </summary>
        [XmlElement("bindingRedirect")]
        public BindingRedirect BindingRedirect { get; set; }

        #endregion

        /// <summary>
        /// Updates the assembly name using the binding redirect.
        /// </summary>
        /// <param name="assemblyFullName">The assembly name to update.</param>
        /// <returns>The redirect assembly name if exists, else null.</returns>
        public string UpdateAssemblyName(string assemblyFullName)
        {
            if (this.AssemblyIdentity.Matches(assemblyFullName) && this.BindingRedirect.Matches(assemblyFullName))
            {
                return $"{this.AssemblyIdentity.Name}, Version={this.BindingRedirect.NewVersion}, Culture={this.AssemblyIdentity.Culture}, PublicKeyToken={this.AssemblyIdentity.PublicKeyToken}";
            }

            return null;
        }
    }
}

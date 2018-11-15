// <copyright file="AssemblyResolver.cs" company="Chris Crutchfield">
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
using Mono.Cecil;

namespace PoshKentico.CodeWeaving
{
    /// <summary>
    /// Assembly resolver which allows the user to dispose of the cached references.
    /// </summary>
    internal class AssemblyResolver : BaseAssemblyResolver
    {
        #region Variables

        private readonly IDictionary<string, AssemblyDefinition> cache;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyResolver"/> class.
        /// </summary>
        public AssemblyResolver()
        {
            this.cache = new Dictionary<string, AssemblyDefinition>();
        }

        #endregion

        #region Methods

        /// <inheritdoc/>
        public override AssemblyDefinition Resolve(AssemblyNameReference name)
        {
            var fullName = name.FullName;

            if (this.cache.TryGetValue(fullName, out AssemblyDefinition @return))
            {
                return @return;
            }

            @return = base.Resolve(name);

            this.cache.Add(fullName, @return);

            return @return;
        }

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            foreach (var item in this.cache.Values)
            {
                item.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion

    }
}

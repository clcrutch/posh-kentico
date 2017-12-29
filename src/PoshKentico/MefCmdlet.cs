// <copyright file="MefCmdlet.cs" company="Chris Crutchfield">
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

using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Management.Automation;

namespace PoshKentico
{
    /// <summary>
    /// Base class for MEF cmdlets which automatically fullfill their own dependencies.
    /// </summary>
    public abstract class MefCmdlet : PSCmdlet
    {
        #region Properties

        /// <summary>
        /// The MEF container used for DI.
        /// </summary>
        internal static CompositionContainer Container { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            this.Initialize();
        }

        private void Initialize()
        {
            if (Container == null)
            {
                var catalog = new AssemblyCatalog(typeof(MefCmdlet).Assembly);

                Container = new CompositionContainer(catalog);
            }

            Container.ComposeParts(this);
        }

        #endregion

    }
}

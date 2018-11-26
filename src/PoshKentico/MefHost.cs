// <copyright file="MefHost.cs" company="Chris Crutchfield">
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

using System.ComponentModel.Composition.Hosting;
using PoshKentico.Business;
using PoshKentico.Core.Services.General;

namespace PoshKentico
{
    /// <summary>
    /// Hosts a shared MEF container which can be used by cmdlets and the navigation provider.
    /// </summary>
    public class MefHost
    {
        #region Properties

        /// <summary>
        /// The MEF container used for DI.
        /// </summary>
        internal static CompositionContainer Container { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initialize the container.
        /// </summary>
        internal static void Initialize()
        {
            if (Container == null)
            {
                var catalog = new AggregateCatalog(
                    new AssemblyCatalog(typeof(MefHost).Assembly),
                    new AssemblyCatalog(typeof(ICmsApplicationService).Assembly),
                    new AssemblyCatalog(typeof(CmdletBusinessBase).Assembly));

                Container = new CompositionContainer(catalog);
            }
        }

        #endregion
    }
}

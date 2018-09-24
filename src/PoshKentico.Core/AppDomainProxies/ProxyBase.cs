// <copyright file="ProxyBase.cs" company="Chris Crutchfield">
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
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Core.AppDomainProxies
{
    /// <summary>
    /// The base class for all app domain proxies.
    /// </summary>
    internal abstract class ProxyBase : MarshalByRefObject
    {
        #region Constructors

        static ProxyBase()
        {
            Container = new CompositionContainer(new AssemblyCatalog(typeof(ProxyBase).Assembly));
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="ICmsApplicationService"/>.
        /// </summary>
        [Import]
        public ICmsApplicationService CmsApplicationService { get; set; }

        /// <summary>
        /// Gets or sets the MEF container used for DI.
        /// </summary>
        internal static CompositionContainer Container { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Initailizes the MEF container, the object, and the CMSApplicationService.
        /// </summary>
        public void Initialize()
        {
            Container.ComposeParts(this);

            this.CmsApplicationService.Initialize(true);
        }

        #endregion

    }
}
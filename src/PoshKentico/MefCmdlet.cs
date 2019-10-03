﻿// <copyright file="MefCmdlet.cs" company="Chris Crutchfield">
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
using System.Linq;
using System.Management.Automation;
using System.Reflection;
using PoshKentico.Business;
using PoshKentico.Extensions;

namespace PoshKentico
{
    /// <summary>
    /// Base class for MEF cmdlets which automatically fullfill their own dependencies.
    /// </summary>
    /// <typeparam name="TBusinessLayer">The type for the business layer.</typeparam>
    public abstract class MefCmdlet<TBusinessLayer> : PSCmdlet, ICmdlet
    {
        #region Properties

        /// <summary>
        /// Gets or sets the Business layer for this cmdlet. Populated by MEF.
        /// </summary>
        [Import]
        public TBusinessLayer BusinessLayer { get; set; }

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
            Bootstrapper.Instance.Initialize(this);
        }

        #endregion

    }
}

// <copyright file="KenticoCmdlet.cs" company="Chris Crutchfield">
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
using PoshKentico.Services;

namespace PoshKentico
{
    /// <summary>
    /// Base class for all Kentico cmdlets which must initialize the connection to Kentico before running.
    /// </summary>
    public abstract class KenticoCmdlet : MefCmdlet
    {
        #region Properties

        /// <summary>
        /// CMS Application service used for interacting with a CMS Application.
        /// </summary>
        [Import]
        public ICmsApplicationService CmsApplicationService { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            this.CmsApplicationService.Initialize(this.WriteDebug, this.WriteVerbose);
        }

        #endregion

    }
}

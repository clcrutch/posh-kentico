// <copyright file="InitializeCMSDatabaseCmdlet.cs" company="Chris Crutchfield">
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
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using PoshKentico.Business.General;

namespace PoshKentico.Cmdlets.General
{
    /// <summary>
    /// <para type="synopsis">Installs or updates the CMS database for the initialized CMS application.</para>
    /// <para type="description">Installs or updates the CMS databse for the initalized CMS application.</para>
    /// <para type="description">This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <example>
    ///     <para>Install the Kentico database.</para>
    ///     <code>Initialize-CMSDatabase</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsData.Initialize, "CMSDatabase")]
    [Alias("initdb")]
    public class InitializeCMSDatabaseCmdlet : MefCmdlet<InitializeCMSDatabaseBusiness>
    {
        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            this.BusinessLayer.InstallSqlDatabase();
        }

        #endregion

    }
}

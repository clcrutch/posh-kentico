﻿// <copyright file="InitializeCMSDatabaseBusiness.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.General;

namespace PoshKentico.Business.General
{
    /// <summary>
    /// Business layer for the Initialize-CMSDatabase cmdlet.
    /// </summary>
    [Export(typeof(InitializeCMSDatabaseBusiness))]
    public class InitializeCMSDatabaseBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="ICmsDatabaseService"/>.
        /// </summary>
        [Import]
        public ICmsDatabaseService CmsDatabaseService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the database exists and is configured.  Installs it if it does not.
        /// </summary>
        public void InstallSqlDatabase()
        {
            if (!this.CmsDatabaseService.Exists)
            {
                throw new Exception("The specified database does not exist.  Please check the connection string and try again.");
            }

            if (!this.CmsDatabaseService.IsDatabaseInstalled())
            {
                this.CmsDatabaseService.InstallSqlDatabase();
            }
        }

        #endregion

    }
}

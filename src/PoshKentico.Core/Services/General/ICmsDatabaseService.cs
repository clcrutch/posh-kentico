// <copyright file="ICmsDatabaseService.cs" company="Chris Crutchfield">
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
using CMS.DataEngine;

namespace PoshKentico.Core.Services.General
{
    /// <summary>
    /// Represents the connection to the CMS database.
    /// </summary>
    public interface ICmsDatabaseService
    {
        #region Properties

        /// <summary>
        /// Gets the version of the CMS database.
        /// </summary>
        Version Version { get; }

        /// <summary>
        /// Gets or sets the connection string for the CMS database.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Gets a value indicating whether the database exists.
        /// </summary>
        bool Exists { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Executes a query against the CMS database.
        /// </summary>
        /// <param name="queryText">The text of the query to execute.</param>
        /// <param name="parameters">The parameters to use when executing the query.</param>
        void ExecuteQuery(string queryText, QueryDataParameters parameters);

        /// <summary>
        /// Installs the CMS SQL database.
        /// </summary>
        void InstallSqlDatabase();

        /// <summary>
        /// Gets a value indicating if the database is installed and up to date.
        /// </summary>
        /// <returns>A value indicating if the database is installed and up to date.</returns>
        bool IsDatabaseInstalled();

        #endregion

    }
}

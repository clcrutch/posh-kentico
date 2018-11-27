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
    /// Service for providing database to a CMS application.
    /// </summary>
    public interface ICmsDatabaseService
    {
        /// <summary>
        /// Gets the version of the database that the application uses.
        /// </summary>
        Version Version { get; }

        /// <summary>
        /// Gets or sets the connection string for connecting to a database.
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// Gets a value indicating whether checks if the database exists, returns true if database exists.
        /// </summary>
        bool Exists { get; }

        /// <summary>
        /// Executes the query provided in the params.
        /// </summary>
        /// <param name="queryText">The query text to execute.</param>
        /// <param name="parameters">The parameters for the query.</param>
        void ExecuteQuery(string queryText, QueryDataParameters parameters);

        /// <summary>
        /// Installs sql database.
        /// </summary>
        void InstallSqlDatabase();

        /// <summary>
        /// Checks if the database is installed.
        /// </summary>
        /// <returns>True if the database is installed, otherwise false.</returns>
        bool IsDatabaseInstalled();
    }
}

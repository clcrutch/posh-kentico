// <copyright file="KenticoCmsDatabaseService.cs" company="Chris Crutchfield">
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
using System.Management.Automation;
using CMS.DataEngine;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Core.Providers.General
{
    /// <summary>
    /// Implementation of <see cref="ICmsDatabaseService"/>.
    /// </summary>
    [Export(typeof(ICmsDatabaseService))]
    public class KenticoCmsDatabaseService : ICmsDatabaseService
    {
        #region Constants

        private const string ACTIVITY = "Creating Database";
        private const int ACTIVITYCOUNT = 533;

        #endregion

        #region Variables

        private int currentActivityCount = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="ICmsApplicationService"/>. Populated by MEF.
        /// </summary>
        [Import]
        public ICmsApplicationService CmsApplicationService { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IOutputService"/>. Populated by MEF.
        /// </summary>
        [Import]
        public IOutputService OutputService { get; set; }

        /// <summary>
        /// Gets the version from the database.
        /// </summary>
        public Version Version
        {
            get
            {
                var versionString = DatabaseHelper.DatabaseVersion;

                if (Version.TryParse(versionString, out Version result))
                {
                    return result;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets or sets the connection string for the Kentico database.
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return ConnectionHelper.ConnectionString;
            }

            set
            {
                ConnectionHelper.ConnectionString = value;
                DataConnectionFactory.ConnectionString = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the database exists.
        /// </summary>
        public bool Exists => DatabaseHelper.DatabaseExists(this.ConnectionString);

        #endregion

        #region Methods

        /// <summary>
        /// Executes a query against the Kentico database.
        /// </summary>
        /// <param name="queryText">The text of the query to execute.</param>
        /// <param name="parameters">The parameters to use when executing the query.</param>
        public void ExecuteQuery(string queryText, QueryDataParameters parameters) =>
            ConnectionHelper.ExecuteNonQuery(queryText, parameters, QueryTypeEnum.SQLQuery);

        /// <summary>
        /// Installs the Kentico SQL database.
        /// </summary>
        public void InstallSqlDatabase()
        {
            this.currentActivityCount = 0;

            var progressRecord = new ProgressRecord(Math.Abs(ACTIVITY.GetHashCode()), ACTIVITY, "Starting")
            {
                PercentComplete = 0,
                RecordType = ProgressRecordType.Processing,
            };
            this.OutputService.WriteProgress(progressRecord);

            var settings = new DatabaseInstallationSettings()
            {
                ConnectionString = this.ConnectionString,
                ScriptsFolder = SqlInstallationHelper.GetSQLInstallPath(),
                DatabaseObjectInstallationErrorMessage = "An error occurred inserting the objects into the database.",
                DataInstallationErrorMessage = "An error occurred.",
                Logger = this.Log,
            };

            bool success = SqlInstallationHelper.InstallDatabase(settings);

            progressRecord = new ProgressRecord(Math.Abs(ACTIVITY.GetHashCode()), ACTIVITY, "Finished")
            {
                PercentComplete = 100,
                RecordType = ProgressRecordType.Processing,
            };
            this.OutputService.WriteProgress(progressRecord);
        }

        /// <summary>
        /// Gets a value indicating if the database is installed.
        /// </summary>
        /// <returns>A value indicating if the database is installed.</returns>
        public bool IsDatabaseInstalled() =>
            this.Version != null;

        private void Log(string message, MessageTypeEnum type)
        {
            switch (type)
            {
                case MessageTypeEnum.Info:
                    this.OutputService.WriteVerbose(message);

                    this.currentActivityCount++;

                    var progressRecord = new ProgressRecord(Math.Abs(ACTIVITY.GetHashCode()), ACTIVITY, message)
                    {
                        PercentComplete = (int)Math.Round(((float)this.currentActivityCount / (float)ACTIVITYCOUNT) * 100),
                        RecordType = ProgressRecordType.Processing,
                    };
                    this.OutputService.WriteProgress(progressRecord);
                    break;
                case MessageTypeEnum.Warning:
                    this.OutputService.WriteWarning(message);
                    break;
                case MessageTypeEnum.Error:
                    throw new Exception(message);
            }
        }

        #endregion

    }
}

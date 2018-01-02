// <copyright file="ICmsApplicationService.cs" company="Chris Crutchfield">
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
using System.IO;

namespace PoshKentico.Core.Services.General
{
    /// <summary>
    /// Represents the initialization state of a CMS application.
    /// </summary>
    public enum InitializationState
    {
        /// <summary>
        /// Initialization has not yet occurred.
        /// </summary>
        Uninitialized,

        /// <summary>
        /// An error occurred during initialization.
        /// </summary>
        Error,

        /// <summary>
        /// Initialization was successful.
        /// </summary>
        Initialized,
    }

    /// <summary>
    /// Service for providing access to a CMS application.
    /// </summary>
    public interface ICmsApplicationService
    {
        #region Properties

        /// <summary>
        /// Gets the initialization state of the CMS Application.
        /// </summary>
        InitializationState InitializationState { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Locates the CMS Site.
        /// </summary>
        /// <param name="writeDebug">A delegate for writing to the debug stream.</param>
        /// <param name="writeVerbose">A delegate for writing to the verbose stream.</param>
        /// <returns>The directory and the connection string for the CMS site.</returns>
        (DirectoryInfo siteLocation, string connectionString) FindSite(Action<string> writeDebug = null, Action<string> writeVerbose = null);

        /// <summary>
        /// Locates and performs the necessary initialization for the CMS site.
        /// </summary>
        /// <param name="writeDebug">A delegate for writing to the debug stream.</param>
        /// <param name="writeVerbose">A delegate for writing to the verbose stream.</param>
        void Initialize(Action<string> writeDebug = null, Action<string> writeVerbose = null);

        /// <summary>
        /// Performs the necessary initialization for the provided CMS site.
        /// </summary>
        /// <param name="connectionString">The connection string to use for initializing the CMS Application.</param>
        /// <param name="directoryInfo">The directory where the CMS site resides.</param>
        /// <param name="writeDebug">A delegate for writing to the debug stream.</param>
        /// <param name="writeVerbose">A delegate for writing to the verbose stream.</param>
        void Initialize(string connectionString, DirectoryInfo directoryInfo, Action<string> writeDebug = null, Action<string> writeVerbose = null);

        #endregion

    }
}

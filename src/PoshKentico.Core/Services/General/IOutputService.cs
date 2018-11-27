// <copyright file="IOutputService.cs" company="Chris Crutchfield">
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

using System.Management.Automation;

namespace PoshKentico.Core.Services.General
{
    /// <summary>
    /// Service for providing output message to a CMS application.
    /// </summary>
    public interface IOutputService
    {
        /// <summary>
        /// Checks if the cmdlet should continue processing.
        /// </summary>
        /// <param name="target">The target cmllet.</param>
        /// <param name="action">The action.</param>
        /// <returns>If the check returns true, then continue to process the cmdlet.</returns>
        bool ShouldProcess(string target, string action);

        /// <summary>
        /// Writes the error message.
        /// </summary>
        /// <param name="errorRecord">The error record.</param>
        void WriteError(ErrorRecord errorRecord);

        /// <summary>
        /// Writes the debug message.
        /// </summary>
        /// <param name="message">The debug message.</param>
        void WriteDebug(string message);

        /// <summary>
        /// Writes the progress message.
        /// </summary>
        /// <param name="progressRecord">The progress record.</param>
        void WriteProgress(ProgressRecord progressRecord);

        /// <summary>
        /// Writes the verbose message.
        /// </summary>
        /// <param name="message">The verbose message.</param>
        void WriteVerbose(string message);

        /// <summary>
        /// Writes the warning message.
        /// </summary>
        /// <param name="message">The warning message.</param>
        void WriteWarning(string message);
    }
}

// <copyright file="ICmdlet.cs" company="Chris Crutchfield">
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

namespace PoshKentico
{
    /// <summary>
    /// Represents a commandlet.
    /// </summary>
    public interface ICmdlet
    {
        /// <summary>
        /// Confirms the operation with the user, sending the name of the resource to be changed and the action to be performed to the user for confirmation before the operation is performed.
        /// </summary>
        /// <param name="target">Name of the target resource being acted upon. This will potentially be displayed to the user.</param>
        /// <param name="action">Name of the action which is being performed. This will potentially be displayed to the user. (default is Cmdlet name).</param>
        /// <returns>If ShouldProcess returns true, the operation should be performed. If ShouldProcess returns false, the operation should not be performed, and the Cmdlet should move on to the next target resource.</returns>
        bool ShouldProcess(string target, string action);

        /// <summary>
        /// Writes a debug log entry.
        /// </summary>
        /// <param name="text">The entry to log.</param>
        void WriteDebug(string text);

        /// <summary>
        /// Writes a debug error entry.
        /// </summary>
        /// <param name="errorRecord">The error to log.</param>
        void WriteError(ErrorRecord errorRecord);

        /// <summary>
        /// Writes a progress log entry.
        /// </summary>
        /// <param name="progressRecord">The progress to log.</param>
        void WriteProgress(ProgressRecord progressRecord);

        /// <summary>
        /// Writes a verbose log entry.
        /// </summary>
        /// <param name="text">The entry to log.</param>
        void WriteVerbose(string text);

        /// <summary>
        /// Writes a warning log entry.
        /// </summary>
        /// <param name="text">The warning to log.</param>
        void WriteWarning(string text);
    }
}

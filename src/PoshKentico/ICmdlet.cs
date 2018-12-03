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

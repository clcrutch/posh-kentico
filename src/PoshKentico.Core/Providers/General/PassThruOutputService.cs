// <copyright file="PassThruOutputService.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.General;

namespace PoshKentico.Core.Providers.General
{
    /// <summary>
    /// Implementation of <see cref="IOutputService"/>.
    /// </summary>
    [Export(typeof(IOutputService))]
    public class PassThruOutputService : IOutputService
    {
        /// <summary>
        /// Sets a delegate for checking if the cmdlet should continue processing.
        /// </summary>
        public static Func<string, string, bool> ShouldProcessFunction { private get; set; }

        /// <summary>
        ///  Sets a delegate for writing to the error stream.
        /// </summary>
        public static Action<ErrorRecord> WriteErrorAction { private get; set; }

        /// <summary>
        ///  Sets a delegate for writing to the debug stream.
        /// </summary>
        public static Action<string> WriteDebugAction { private get; set; }

        /// <summary>
        ///  Sets a delegate for writing to the progress stream.
        /// </summary>
        public static Action<ProgressRecord> WriteProgressAction { private get; set; }

        /// <summary>
        ///  Sets a delegate for writing to the verbose stream.
        /// </summary>
        public static Action<string> WriteVerboseAction { private get; set; }

        /// <summary>
        ///  Sets a delegate for writing to the warning stream.
        /// </summary>
        public static Action<string> WriteWarningAction { private get; set; }

        /// <inheritdoc/>
        public bool ShouldProcess(string target, string action) =>
            ShouldProcessFunction?.Invoke(target, action) ?? false;

        /// <inheritdoc/>
        public void WriteDebug(string message) =>
            WriteDebugAction?.Invoke(message);

        /// <inheritdoc/>
        public void WriteError(ErrorRecord errorRecord) =>
            WriteErrorAction?.Invoke(errorRecord);

        /// <inheritdoc/>
        public void WriteProgress(ProgressRecord progressRecord) =>
            WriteProgressAction?.Invoke(progressRecord);

        /// <inheritdoc/>
        public void WriteVerbose(string message) =>
            WriteVerboseAction?.Invoke(message);

        /// <inheritdoc/>
        public void WriteWarning(string message) =>
            WriteWarningAction?.Invoke(message);
    }
}

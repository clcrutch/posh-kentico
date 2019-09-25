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
using System.Runtime.CompilerServices;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Core.Providers.General
{
    /// <summary>
    /// Implementation of <see cref="IOutputService"/> using delegates.
    /// </summary>
    [Export(typeof(IOutputService))]
    public class PassThruOutputService : IOutputService
    {
        #region Properties

        /// <summary>
        /// Sets a reference to the ShouldProcess function.
        /// </summary>
        public static Func<string, string, bool> ShouldProcessFunction { private get; set; }

        /// <summary>
        /// Sets a reference to the WriteError action.
        /// </summary>
        public static Action<ErrorRecord> WriteErrorAction { private get; set; }

        /// <summary>
        /// Sets a reference to the WriteDebug action.
        /// </summary>
        public static Action<string> WriteDebugAction { private get; set; }

        /// <summary>
        /// Sets a reference to the WriteProgress action.
        /// </summary>
        public static Action<ProgressRecord> WriteProgressAction { private get; set; }

        /// <summary>
        /// Sets a reference to the WriteVerbose action.
        /// </summary>
        public static Action<string> WriteVerboseAction { private get; set; }

        /// <summary>
        /// Sets a reference to the WriteWarning action.
        /// </summary>
        public static Action<string> WriteWarningAction { private get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Confirms the operation with the user, sending the name of the resource to be changed and the action to be performed to the user for confirmation before the operation is performed.
        /// </summary>
        /// <param name="target">Name of the target resource being acted upon. This will potentially be displayed to the user.</param>
        /// <param name="action">Name of the action which is being performed. This will potentially be displayed to the user. (default is Cmdlet name).</param>
        /// <returns>If ShouldProcess returns true, the operation should be performed. If ShouldProcess returns false, the operation should not be performed, and the Cmdlet should move on to the next target resource.</returns>
        public bool ShouldProcess(string target, string action) =>
            ShouldProcessFunction?.Invoke(target, action) ?? false;

        /// <summary>
        /// Display debug information.
        /// </summary>
        /// <param name="text">The entry to log.</param>
        /// <param name="memberName">The member name calling this method.</param>
        /// <param name="sourceFilePath">The source file calling this method.</param>
        /// <param name="sourceLineNumber">The line number calling this method.</param>
        public void WriteDebug(string text, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) =>
        WriteDebugAction?.Invoke(this.FormatOutput(text, memberName, sourceFilePath, sourceLineNumber));

        /// <summary>
        /// Writes the specified error to the error pipe.
        /// </summary>
        /// <param name="errorRecord">The error to write to the pipeline.</param>
        public void WriteError(ErrorRecord errorRecord) =>
            WriteErrorAction?.Invoke(errorRecord);

        /// <summary>
        /// Display progress information.
        /// </summary>
        /// <param name="progressRecord">progress information.</param>
        public void WriteProgress(ProgressRecord progressRecord) =>
            WriteProgressAction?.Invoke(progressRecord);

        /// <summary>
        /// Writes a verbose log entry.
        /// </summary>
        /// <param name="text">The entry to log.</param>
        /// <param name="memberName">The member name calling this method.</param>
        /// <param name="sourceFilePath">The source file calling this method.</param>
        /// <param name="sourceLineNumber">The line number calling this method.</param>
        public void WriteVerbose(string text, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) =>
            WriteVerboseAction?.Invoke(this.FormatOutput(text, memberName, sourceFilePath, sourceLineNumber));

        /// <summary>
        /// Display warning information.
        /// </summary>
        /// <param name="text">warning output.</param>
        /// <param name="memberName">The member name calling this method.</param>
        /// <param name="sourceFilePath">The source file calling this method.</param>
        /// <param name="sourceLineNumber">The line number calling this method.</param>
        public void WriteWarning(string text, [CallerMemberName] string memberName = "", [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0) =>
            WriteWarningAction?.Invoke(this.FormatOutput(text, memberName, sourceFilePath, sourceLineNumber));

        private string FormatOutput(string text, string memberName, string sourceFilePath, int sourceLineNumber) =>
            $"\"{sourceFilePath}\":{sourceLineNumber}:{memberName}() [MESSAGE:] {text}";

        #endregion

    }
}

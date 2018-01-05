// <copyright file="CmdletBusinessBase.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Business
{
    /// <summary>
    /// Base class for all Cmdlet Business objects.
    /// </summary>
    public abstract class CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a delegate for writing to the debug stream.
        /// </summary>
        public Action<string> WriteDebug { get; set; }

        /// <summary>
        /// Gets or sets a delegate for writing to the verbose stream.
        /// </summary>
        public Action<string> WriteVerbose { get; set; }

        /// <summary>
        /// Gets or sets a delegate for checking if the cmdlet should continue processing.
        /// </summary>
        public Func<string, string, bool> ShouldProcess { get; set; }

        #endregion

    }
}

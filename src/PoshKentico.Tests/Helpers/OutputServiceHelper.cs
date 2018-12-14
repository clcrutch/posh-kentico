// <copyright file="OutputServiceHelper.cs" company="Chris Crutchfield">
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

using NUnit.Framework;
using PoshKentico.Core.Providers.General;
using PoshKentico.Core.Services.General;

namespace PoshKentico.Tests.Helpers
{
    /// <summary>
    /// Helper to setup methods for unit testing.
    /// </summary>
    internal static class OutputServiceHelper
    {
        /// <summary>
        /// Sets up the <see cref="IOutputService"/> for unit testing.
        /// </summary>
        /// <returns>An instance of the <see cref="PassThruOutputService"/> setup.</returns>
        public static PassThruOutputService GetPassThruOutputService()
        {
            PassThruOutputService.WriteDebugAction = Assert.NotNull;
            PassThruOutputService.WriteVerboseAction = Assert.NotNull;

            return new PassThruOutputService();
        }
    }
}

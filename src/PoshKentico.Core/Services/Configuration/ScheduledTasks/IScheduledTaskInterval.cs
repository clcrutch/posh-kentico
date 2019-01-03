// <copyright file="IScheduledTaskInterval.cs" company="Chris Crutchfield">
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
using System.Collections.Generic;

namespace PoshKentico.Core.Services.Configuration.ScheduledTasks
{
    /// <summary>
    /// Represents a scheduled task interval.
    /// </summary>
    public interface IScheduledTaskInterval
    {
        #region Properties

        /// <summary>
        /// Gets an indication of how often the task is set to occur.
        /// </summary>
        int Every { get; }

        /// <summary>
        /// Gets which days the task should execute on.
        /// </summary>
        IEnumerable<DayOfWeek> Days { get; }

        /// <summary>
        /// Gets the period which the task should be executed on.
        /// </summary>
        string Period { get; }

        /// <summary>
        /// Gets the start time for the interval.
        /// </summary>
        DateTime StartTime { get; }

        #endregion

    }
}

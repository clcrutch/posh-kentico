// <copyright file="IScheduledTask.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Core.Services.Configuration.ScheduledTasks
{
    /// <summary>
    /// Represents a scheduled task.
    /// </summary>
    public interface IScheduledTask
    {
        #region Properties

        /// <summary>
        /// Gets the assembly name for the task.
        /// </summary>
        string TaskAssemblyName { get; }

        /// <summary>
        /// Gets the class for the task.
        /// </summary>
        string TaskClass { get; }

        /// <summary>
        /// Gets the data for the task.
        /// </summary>
        string TaskData { get; }

        /// <summary>
        /// Gets the display name for the task.
        /// </summary>
        string TaskDisplayName { get; }

        /// <summary>
        /// Gets a value indicating whether the task is enabled.
        /// </summary>
        bool TaskEnabled { get; }

        /// <summary>
        /// Gets the interval for the task.
        /// </summary>
        string TaskInterval { get; }

        /// <summary>
        /// Gets the ID for the task.
        /// </summary>
        int TaskID { get; }

        /// <summary>
        /// Gets the name for the task.
        /// </summary>
        string TaskName { get; }

        /// <summary>
        /// Gets the next run time for the task.
        /// </summary>
        DateTime TaskNextRunTime { get; }

        /// <summary>
        /// Gets the site for the task.
        /// </summary>
        int TaskSiteID { get; }

        #endregion

    }
}

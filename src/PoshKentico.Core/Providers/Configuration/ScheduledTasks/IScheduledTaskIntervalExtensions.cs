// <copyright file="IScheduledTaskIntervalExtensions.cs" company="Chris Crutchfield">
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
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CMS.Scheduler;
using PoshKentico.Core.Services.Configuration.ScheduledTasks;

namespace PoshKentico.Core.Providers.Configuration.ScheduledTasks
{
    /// <summary>
    /// Extensions for scheduled task interval.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class IScheduledTaskIntervalExtensions
    {
        #region Methods

        /// <summary>
        /// Encodes the scheduled task interval into a string.
        /// </summary>
        /// <param name="interval">The interval to encode into a string.</param>
        /// <returns>A <see cref="string"/> representation of the interval.</returns>
        public static string Encode(this IScheduledTaskInterval interval) =>
            SchedulingHelper.EncodeInterval(interval.ToTaskInterval());

        /// <summary>
        /// Gets the first run for the interval.
        /// </summary>
        /// <param name="interval">The interval to get the first run time for.</param>
        /// <returns>The <see cref="DateTime"/> for the first run of the interval.</returns>
        public static DateTime GetFirstRun(this IScheduledTaskInterval interval) =>
            SchedulingHelper.GetFirstRunTime(interval.ToTaskInterval());

        /// <summary>
        /// Converts the <see cref="IScheduledTask"/> to a <see cref="TaskInterval"/>.
        /// </summary>
        /// <param name="interval">The interval to convert to <see cref="TaskInterval"/>.</param>
        /// <returns><see cref="TaskInterval"/> representation of <see cref="IScheduledTaskInterval"/>.</returns>
        public static TaskInterval ToTaskInterval(this IScheduledTaskInterval interval)
        {
            if (interval == null)
            {
                return null;
            }

            return new TaskInterval
            {
                Days = interval.Days.ToList(),
                Every = interval.Every,
                Period = interval.Period,
                StartTime = interval.StartTime,
            };
        }

        #endregion

    }
}

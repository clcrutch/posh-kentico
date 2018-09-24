// <copyright file="GetCmsScheduledTaskBusiness.cs" company="Chris Crutchfield">
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

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text.RegularExpressions;
using PoshKentico.Core.Services.Configuration.ScheduledTasks;

namespace PoshKentico.Business.Configuration.ScheduledTasks
{
    [Export(typeof(GetCmsScheduledTaskBusiness))]
    public class GetCmsScheduledTaskBusiness : CmdletBusinessBase
    {
        #region Properties

        [Import]
        public IScheduledTaskService ScheduledTaskService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a list of <see cref="IScheduledTask"/> returned by Kentico.
        /// </summary>
        /// <returns>A list of <see cref="IScheduledTask"/>.</returns>
        public IEnumerable<IScheduledTask> GetScheduledTasks() =>
            this.ScheduledTaskService.ScheduledTasks;

        /// <summary>
        /// Gets a list of <see cref="IScheduledTask"/> returned by Kentico whose assembly name matches the match string.
        /// </summary>
        /// <param name="matchString"></param>
        /// <param name="isRegex"></param>
        /// <returns></returns>
        public IEnumerable<IScheduledTask> GetScheduledTasksByAssemblyName(string matchString, bool isRegex)
        {
            var regex = this.GenerateRegex(matchString, isRegex);

            var matched = from t in this.ScheduledTaskService.ScheduledTasks
                          where regex.IsMatch(t.TaskAssemblyName)
                          select t;

            return matched.ToArray();
        }

        public IEnumerable<IScheduledTask> GetScheduledTasksByNameOrDisplayName(string matchString, bool isRegex)
        {
            var regex = this.GenerateRegex(matchString, isRegex);

            var matched = from t in this.ScheduledTaskService.ScheduledTasks
                          where regex.IsMatch(t.TaskDisplayName) ||
                            regex.IsMatch(t.TaskName)
                          select t;

            return matched.ToArray();
        }

        private Regex GenerateRegex(string matchString, bool isRegex)
        {
            if (isRegex)
            {
                return new Regex(matchString, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            else
            {
                return new Regex($"^{matchString.Replace("*", ".*")}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
        }

        #endregion

    }
}

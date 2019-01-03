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
using PoshKentico.Core.Services.Configuration.Sites;

namespace PoshKentico.Business.Configuration.ScheduledTasks
{
    /// <summary>
    /// Business layer of the Get-CMSScheduledTask cmdlet.
    /// </summary>
    [Export(typeof(GetCmsScheduledTaskBusiness))]
    public class GetCmsScheduledTaskBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets the scheduled task service.  Populated by MEF.
        /// </summary>
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
        /// Gets a list of <see cref="IScheduledTask"/> for a particular <see cref="ISite"/> return by Kentico.
        /// </summary>
        /// <param name="site">The <see cref="ISite"/> to get the list of <see cref="IScheduledTask"/> for.</param>
        /// <returns>A list of the <see cref="IScheduledTask"/> which are associated with a <see cref="ISite"/>.</returns>
        public IEnumerable<IScheduledTask> GetScheduledTasks(ISite site) =>
            (from s in this.ScheduledTaskService.ScheduledTasks
             where s.TaskSiteID == site.SiteID
             select s).ToArray();

        /// <summary>
        /// Gets a list of <see cref="IScheduledTask"/> returned by Kentico whose assembly name matches the match string.
        /// </summary>
        /// <param name="matchString">The string which to match the <see cref="IScheduledTask"/> to.</param>
        /// <param name="isRegex">Indicates whether <paramref name="matchString"/> is a regular expression.</param>
        /// <returns>A list of <see cref="IScheduledTask"/> matching the <paramref name="matchString"/>.</returns>
        public IEnumerable<IScheduledTask> GetScheduledTasksByAssemblyName(string matchString, bool isRegex) =>
            this.GetScheduledTasksByAssemblyName(matchString, isRegex, this.ScheduledTaskService.ScheduledTasks);

        /// <summary>
        /// Gets a list of <see cref="IScheduledTask"/> for a particular <see cref="ISite"/> returned by Kentico whose assembly matches the match string.
        /// </summary>
        /// <param name="matchString">The string which to match the <see cref="IScheduledTask"/> to.</param>
        /// <param name="isRegex">Indicates whether <paramref name="matchString"/> is a regular expression.</param>
        /// <param name="site">The <see cref="ISite"/> to get the list of <see cref="IScheduledTask"/> for.</param>
        /// <returns>A list of <see cref="IScheduledTask"/> matching the <paramref name="matchString"/> and the <paramref name="site"/>.</returns>
        public IEnumerable<IScheduledTask> GetScheduledTasksByAssemblyName(string matchString, bool isRegex, ISite site) =>
            this.GetScheduledTasksByAssemblyName(matchString, isRegex, this.GetScheduledTasks(site));

        /// <summary>
        /// Gets a list of <see cref="IScheduledTask"/> returned by Kentico whose name or display name matches the match string.
        /// </summary>
        /// <param name="matchString">The string which to match the scheduled task to.</param>
        /// <param name="isRegex">Indicates whether <paramref name="matchString"/> is a regular expression.</param>
        /// <returns>A list of <see cref="IScheduledTask"/> matching the <paramref name="matchString"/>.</returns>
        public IEnumerable<IScheduledTask> GetScheduledTasksByNameOrDisplayName(string matchString, bool isRegex) =>
            this.GetScheduledTasksByNameOrDisplayName(matchString, isRegex, this.ScheduledTaskService.ScheduledTasks);

        /// <summary>
        /// Gets a list of <see cref="IScheduledTask"/> for a particular <see cref="ISite"/> returned by Kentico whose name or display name matches the match string.
        /// </summary>
        /// <param name="matchString">The string which to match the <see cref="IScheduledTask"/> to.</param>
        /// <param name="isRegex">Indicates whether <paramref name="matchString"/> is a regular expression.</param>
        /// <param name="site">The <see cref="ISite"/> to get the list of <see cref="IScheduledTask"/> for.</param>
        /// <returns>A list of <see cref="IScheduledTask"/> matching the <paramref name="matchString"/> and the <paramref name="site"/>.</returns>
        public IEnumerable<IScheduledTask> GetScheduledTasksByNameOrDisplayName(string matchString, bool isRegex, ISite site) =>
            this.GetScheduledTasksByNameOrDisplayName(matchString, isRegex, this.GetScheduledTasks(site));

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

        private IEnumerable<IScheduledTask> GetScheduledTasksByAssemblyName(string matchString, bool isRegex, IEnumerable<IScheduledTask> scheduledTasks)
        {
            var regex = this.GenerateRegex(matchString, isRegex);

            var matched = from t in scheduledTasks
                          where regex.IsMatch(t.TaskAssemblyName)
                          select t;

            return matched.ToArray();
        }

        private IEnumerable<IScheduledTask> GetScheduledTasksByNameOrDisplayName(string matchString, bool isRegex, IEnumerable<IScheduledTask> scheduledTasks)
        {
            var regex = this.GenerateRegex(matchString, isRegex);

            var matched = from t in scheduledTasks
                          where regex.IsMatch(t.TaskDisplayName) ||
                            regex.IsMatch(t.TaskName)
                          select t;

            return matched.ToArray();
        }

        #endregion

    }
}

// <copyright file="GetCMSControlCategoryBusiness.cs" company="Chris Crutchfield">
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
using System.Linq;
using System.Text.RegularExpressions;
using PoshKentico.Core.Services.Development;

namespace PoshKentico.Business.Development
{
    /// <summary>
    /// Base Business layer for the Get-CMSControlCategory cmdlet.
    /// </summary>
    public abstract class GetCMSControlCategoryBusiness<TControlService, TControl, TControlCategory> : ControlBusinessBase<TControlService, TControl, TControlCategory>
        where TControlService : IControlService<TControl, TControlCategory>
    {
        #region Methods

        /// <summary>
        /// Gets a list of all of the <see cref="IControlCategory{T}"/>.
        /// </summary>
        /// <returns>A list of all of the <see cref="IControlCategory{T}"/>.</returns>
        public IEnumerable<IControlCategory<TControlCategory>> GetControlCategories() => this.ControlService.Categories;

        /// <summary>
        /// Gets a list of all of the <see cref="IControlCategory{T}"/> which match the specified criteria.
        /// </summary>
        /// <param name="matchString">The string which to match the control categories to.</param>
        /// <param name="isRegex">Indicates whether <paramref name="matchString"/> is a regular expression.</param>
        /// <param name="recurse">Indicates whether control categories should be returned recursively.</param>
        /// <returns>A list of all of the <see cref="IControlCategory{T}"/> which match the specified criteria.</returns>
        public virtual IEnumerable<IControlCategory<TControlCategory>> GetControlCategories(string matchString, bool isRegex, bool recurse)
        {
            Regex regex = null;

            if (isRegex)
            {
                regex = new Regex(matchString, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            else
            {
                regex = new Regex($"^{matchString.Replace("*", ".*")}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }

            var matched = from c in this.ControlService.Categories
                          where regex.IsMatch(c.Name) ||
                              regex.IsMatch(c.DisplayName)
                          select c;

            if (recurse)
            {
                return this.GetRecurseControlCategories(matched);
            }
            else
            {
                return matched.ToArray();
            }
        }

        /// <summary>
        /// Gets a list of the <see cref="IControlCategory{T}"/> which match the supplied IDs.
        /// </summary>
        /// <param name="ids">The IDs of the <see cref="IControlCategory{T}"/> to return.</param>
        /// <param name="recurse">Indicates whether control categories should be returned recursively.</param>
        /// <returns>A list of the <see cref="IControlCategory{T}"/> which match the supplied IDs.</returns>
        public IEnumerable<IControlCategory<TControlCategory>> GetControlCategories(int[] ids, bool recurse)
        {
            var controlCategories = from id in ids
                                    select this.ControlService.GetControlCategory(id);

            var nonNullCategories = from wpc in controlCategories
                                    where wpc != null
                                    select wpc;

            if (recurse)
            {
                return this.GetRecurseControlCategories(nonNullCategories);
            }
            else
            {
                return nonNullCategories.ToArray();
            }
        }

        /// <summary>
        /// Gets a list of the <see cref="IControlCategory{T}"/> which are children of the <paramref name="parentControlCategory"/>.
        /// </summary>
        /// <param name="parentControlCategory">The <see cref="IControlCategory{T}"/> which is parent to the categories to find.</param>
        /// <param name="recurse">Indicates whether control categories should be returned recursively.</param>
        /// <returns>A list of the <see cref="IControlCategory{T}"/> which are children to the supplied <paramref name="parentControlCategory"/>.</returns>
        public IEnumerable<IControlCategory<TControlCategory>> GetControlCategories(IControlCategory<TControlCategory> parentControlCategory, bool recurse)
        {
            var categories = this.ControlService.GetControlCategories(parentControlCategory);

            if (recurse)
            {
                return this.GetRecurseControlCategories(categories);
            }
            else
            {
                return categories;
            }
        }

        /// <summary>
        /// Gets a list of control categories by path.
        /// </summary>
        /// <param name="path">The path to get the list of control categories.</param>
        /// <param name="recurse">Indicates if the control category children should be returned as well.</param>
        /// <returns>A list of all of the control categories found at the specified path.</returns>
        public IEnumerable<IControlCategory<TControlCategory>> GetControlCategories(string path, bool recurse)
        {
            var categories = from c in this.ControlService.Categories
                             where c.Path.Equals(path, StringComparison.InvariantCultureIgnoreCase)
                             select c;

            if (recurse)
            {
                return this.GetRecurseControlCategories(categories);
            }
            else
            {
                return categories.ToArray();
            }
        }

        /// <summary>
        /// Gets the control category for the current control.
        /// </summary>
        /// <param name="control">The control to get the category for.</param>
        /// <returns>The control category.</returns>
        public IControlCategory<TControlCategory> GetControlCategory(IControl<TControl> control) =>
            (from c in this.ControlService.Categories
             where c.ID == control.CategoryID
             select c).SingleOrDefault();

        private IEnumerable<IControlCategory<TControlCategory>> GetRecurseControlCategories(IEnumerable<IControlCategory<TControlCategory>> controlCategories) =>
            controlCategories
                .Select(wp => this.GetControlCategories(wp, true))
                .SelectMany(c => c)
                .Concat(controlCategories)
                .ToArray();

        #endregion

    }
}

// <copyright file="GetCMSWebPartFieldBusiness.cs" company="Chris Crutchfield">
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
using System.Linq;
using System.Text.RegularExpressions;
using PoshKentico.Core.Services.Development;

namespace PoshKentico.Business.Development
{
    public abstract class GetCMSControlFieldBusiness<TControlService, TControl, TControlCategory> : ControlBusinessBase<TControlService, TControl, TControlCategory>
        where TControlService : IControlService<TControl, TControlCategory>
    {
        #region Methods

        /// <summary>
        /// Gets a list of the <see cref="IControlField{T}"/> associated with a <see cref="IControl{T}"/>.
        /// </summary>
        /// <param name="control">The <see cref="IControl{T}"/> to get the list <see cref="IControlField{T}"/> for.</param>
        /// <returns>A list of <see cref="IControlField{T}"/> associated with the <see cref="IControl{T}"/>.</returns>
        public IEnumerable<IControlField<TControl>> GetControlFields(IControl<TControl> control) =>
            this.ControlService.GetControlFields(control);

        /// <summary>
        /// Gets a list of the <see cref="IControlField{T}"/> associated with a <see cref="IControl{T}"/> which match the supplied criteria.
        /// </summary>
        /// <param name="matchString">The string which to match the control fields to.</param>
        /// <param name="isRegex">Indicates whether <paramref name="matchString"/> is a regular expression.</param>
        /// <param name="control">The <see cref="IControl{T}"/> to get the list <see cref="IControlField{T}"/> for.</param>
        /// <returns>A list of <see cref="IControlField{T}"/> associated with the <see cref="IControl{T}"/> which match the supplied criteria.</returns>
        public IEnumerable<IControlField<TControl>> GetControlFields(string matchString, bool isRegex, IControl<TControl> control)
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

            var matched = from f in this.GetControlFields(control)
                          where regex.IsMatch(f.Name) ||
                            regex.IsMatch(f.Caption ?? string.Empty)
                          select f;

            return matched.ToArray();
        }

        #endregion

    }
}

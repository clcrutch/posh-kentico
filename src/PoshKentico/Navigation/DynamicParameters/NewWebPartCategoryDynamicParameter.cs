// <copyright file="NewWebPartCategoryDynamicParameter.cs" company="Chris Crutchfield">
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

using System.Management.Automation;

namespace PoshKentico.Navigation.DynamicParameters
{
    /// <summary>
    /// Dynamic parameter used for New-Item -ItemType WebPartCategory
    /// </summary>
    public class NewWebPartCategoryDynamicParameter
    {
        #region Properties

        /// <summary>
        /// <para type="description">The display name for the webpart category.</para>
        /// </summary>
        [Parameter]
        public string DisplayName { get; set; }

        /// <summary>
        /// <para type="description">The image to display for the webpart category.</para>
        /// </summary>
        [Parameter]
        public string ImagePath { get; set; }

        #endregion

    }
}

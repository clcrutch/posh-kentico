// <copyright file="GetCMSWebPartCategory.cs" company="Chris Crutchfield">
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
using System.ComponentModel.Composition;
using System.Linq;
using System.Management.Automation;
using CMS.PortalEngine;
using PoshKentico.Services;

namespace PoshKentico.Development
{
    /// <summary>
    /// <para type="synopsis">Gets the web part categories selected by the provided input.</para>
    /// <para type="description">Gets the web part categories selected by the provided input.  This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">Without parameters, this command returns all webpart categories.</para>
    /// <para type="description">With parameters, this command returns the webpart categories that match the criteria.</para>
    /// <example>
    ///     <para>Get all the webpart categories.</para>
    ///     <code>Get-CMSWebPartCategory</code>
    /// </example>
    /// <example>
    ///     <para>Get all webparts with a category name "*bas*", display name "*bas*", or a path "bas*".</para>
    ///     <code>Get-CMSWebPartCategory bas</code>
    /// </example>
    /// <example>
    ///     <para>Get all webparts with a category name "basic", display name "basic", or path "basic"</para>
    ///     <code>Get-CMSWebPartCategory basic -Exact</code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "CMSWebPartCategory", DefaultParameterSetName = NONE)]
    public class GetCMSWebPartCategory : MefCmdlet
    {
        #region Constants

        private const string NONE = "None";
        private const string CATEGORYNAME = "Category Name";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The category name, display name, or path of the webpart category.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = CATEGORYNAME)]
        [Alias("DisplayName", "Name", "Path")]
        public string CategoryName { get; set; }

        /// <summary>
        /// <para type="description">If set, the match is exact,
        /// else the match performs a contains for display name and category name and starts with for path.</para>
        /// </summary>
        [Parameter(ParameterSetName = CATEGORYNAME)]
        public SwitchParameter Exact { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<WebPartCategoryInfo> categories = null;

            switch (this.ParameterSetName)
            {
                case CATEGORYNAME:
                    var lowerCategoryName = this.CategoryName.ToLowerInvariant();

                    if (this.Exact.ToBool())
                    {
                        categories = from c in WebPartCategoryInfoProvider.GetCategories()
                                     where c.CategoryName.ToLowerInvariant().Equals(this.CategoryName, StringComparison.InvariantCultureIgnoreCase) ||
                                        c.CategoryDisplayName.ToLowerInvariant().Equals(this.CategoryName, StringComparison.InvariantCultureIgnoreCase) ||
                                        c.CategoryPath.ToLowerInvariant().Equals(this.CategoryName, StringComparison.InvariantCultureIgnoreCase)
                                     select c;
                    }
                    else
                    {
                        categories = from c in WebPartCategoryInfoProvider.GetCategories()
                                     where c.CategoryName.ToLowerInvariant().Contains(lowerCategoryName) ||
                                        c.CategoryDisplayName.ToLowerInvariant().Contains(lowerCategoryName) ||
                                        c.CategoryPath.ToLowerInvariant().StartsWith(lowerCategoryName)
                                     select c;
                    }

                    break;
                case NONE:
                    categories = WebPartCategoryInfoProvider.GetCategories();
                    break;
            }

            foreach (var category in categories)
            {
                this.WriteObject(category);
            }
        }

        #endregion

    }
}

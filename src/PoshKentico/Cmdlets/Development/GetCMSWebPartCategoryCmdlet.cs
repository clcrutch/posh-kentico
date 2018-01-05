// <copyright file="GetCMSWebPartCategoryCmdlet.cs" company="Chris Crutchfield">
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
using System.Management.Automation;
using ImpromptuInterface;
using PoshKentico.Business.Development;
using PoshKentico.Core.Services.Development;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development
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
    /// <example>
    ///     <para>Get all the webparts with the specified IDs.</para>
    ///     <code>Get-CMSWebPartCategory -ID 5,304,5</code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "CMSWebPartCategory", DefaultParameterSetName = NONE)]
    public class GetCMSWebPartCategoryCmdlet : MefCmdlet
    {
        #region Constants

        private const string NONE = "None";
        private const string CATEGORYNAME = "Category Name";
        private const string IDSETNAME = "ID";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The category name, display name, or path of the webpart category.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = CATEGORYNAME)]
        [Alias("DisplayName", "Name", "Path")]
        public string CategoryName { get; set; }

        /// <summary>
        /// <para type="description">If set, the match is exact,</para>
        /// <para type="description">else the match performs a contains for display name and category name and starts with for path.</para>
        /// </summary>
        [Parameter(ParameterSetName = CATEGORYNAME)]
        public SwitchParameter Exact { get; set; }

        /// <summary>
        /// <para type="description">The IDs of the web part category to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = IDSETNAME)]
        public int[] ID { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this web part. Populated by MEF.
        /// </summary>
        [Import]
        public GetCMSWebPartCategoryBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<IWebPartCategory> categories = null;

            switch (this.ParameterSetName)
            {
                case CATEGORYNAME:
                    categories = this.BusinessLayer.GetWebPartCategories(this.CategoryName, this.Exact.ToBool());
                    break;
                case IDSETNAME:
                    categories = this.BusinessLayer.GetWebPartCategories(this.ID);
                    break;
                case NONE:
                    categories = this.BusinessLayer.GetWebPartCategories();
                    break;
            }

            foreach (var category in categories)
            {
                this.WriteObject(category.UndoActLike());
            }
        }

        #endregion

    }
}

// <copyright file="RemoveCMSWebPartCategoryCmdlet.cs" company="Chris Crutchfield">
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

using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using CMS.PortalEngine;
using ImpromptuInterface;
using PoshKentico.Business.Development;
using PoshKentico.Core.Services.Development;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.WebPartCategory
{
    /// <summary>
    /// <para type="synopsis">Deletes the web part categories selected by the provided input.</para>
    /// <para type="description">Deletes the web part categories selected by the provided input.  This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">This command deletes the webpart categories that match the criteria.</para>
    /// <example>
    ///     <para>Delete all the webpart categories.</para>
    ///     <code>Get-CMSWebPartCategory | Remove-CMSWebPartCategory</code>
    /// </example>
    /// <example>
    ///     <para>Delete all webparts with a category name "*bas*", display name "*bas*", or a path "bas*".</para>
    ///     <code>Remove-CMSWebPartCategory bas</code>
    /// </example>
    /// <example>
    ///     <para>Delete all webparts with a category name "basic", display name "basic", or path "basic"</para>
    ///     <code>Remove-CMSWebPartCategory basic -Exact</code>
    /// </example>
    /// <example>
    ///     <para>Delete all the webparts with the specified IDs.</para>
    ///     <code>Remove-CMSWebPartCategory -ID 5,304,5</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSWebPartCategory", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [Alias("rwpc")]
    public class RemoveCMSWebPartCategoryCmdlet : MefCmdlet
    {
        #region Constants

        private const string CATEGORYNAME = "Category Name";
        private const string IDSETNAME = "ID";
        private const string WEBPARTCATEGORY = "Web Part Category";

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
        /// <para type="description">The IDs of the web part category to delete.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = IDSETNAME)]
        public int[] ID { get; set; }

        /// <summary>
        /// <para type="description">A reference to the WebPart category to delete.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = WEBPARTCATEGORY)]
        [Alias("Category")]
        public WebPartCategoryInfo WebPartCategory { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this web part.  Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCMSWebPartCategoryBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case CATEGORYNAME:
                    this.BusinessLayer.RemoveWebPartCategories(this.CategoryName, this.Exact.ToBool());
                    break;
                case IDSETNAME:
                    this.BusinessLayer.RemoveWebPartCategories(this.ID);
                    break;
                case WEBPARTCATEGORY:
                    this.BusinessLayer.RemoveWebPartCategory(this.WebPartCategory.ActLike<IWebPartCategory>());
                    break;
            }
        }

        #endregion

    }
}

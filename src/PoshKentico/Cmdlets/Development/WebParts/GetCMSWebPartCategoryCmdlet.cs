﻿// <copyright file="GetCMSWebPartCategoryCmdlet.cs" company="Chris Crutchfield">
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
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using CMS.PortalEngine;
using ImpromptuInterface;
using PoshKentico.Core.Providers.Development.WebParts;
using PoshKentico.Core.Services.Development;
using PoshKentico.Core.Services.Development.WebParts;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.WebParts
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
    ///     <para>Get all web part categories with a category name "*bas*", display name "*bas*".</para>
    ///     <code>Get-CMSWebPartCategory *bas*</code>
    /// </example>
    /// <example>
    ///     <para>Get all web part categories with a category name "basic", display name "basic"</para>
    ///     <code>Get-CMSWebPartCategory basic</code>
    /// </example>
    /// <example>
    ///     <para>Get all the web part categories with the specified IDs.</para>
    ///     <code>Get-CMSWebPartCategory -ID 5,304,5</code>
    /// </example>
    /// <example>
    ///     <para>Get all the web part categories under the basic category.</para>
    ///     <code>Get-CMSWebPartCategory basic -Recurse</code>
    /// </example>
    /// <example>
    ///     <para>Get the web part category associated with the web part.</para>
    ///     <code>$webPart | Get-WebPartCategory</code>
    /// </example>
    /// <example>
    ///     <para>Get the web part categories under a parent category.</para>
    ///     <code>$webPartCategory | Get-WebPartCategory</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Get, "CMSWebPartCategory", DefaultParameterSetName = NONE)]
    [OutputType(typeof(WebPartCategoryInfo[]))]
    [Alias("gwpc")]
    public class GetCMSWebPartCategoryCmdlet : GetCMSWebPartCategoryCmdletBase
    {
        #region Constants

        private const string PARENTCATEGORY = "Parent Category";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The webpart category that contains the webpart categories.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = PARENTCATEGORY)]
        [Alias("Parent", "ParentCategory")]
        public WebPartCategoryInfo ParentWebPartCategory { get; set; }

        /// <summary>
        /// <para type="description">Indiciates if the cmdlet should look recursively for web part categories.</para>
        /// </summary>
        [Parameter(ParameterSetName = PARENTCATEGORY)]
        public override SwitchParameter Recurse
        {
            get => base.Recurse;
            set => base.Recurse = value;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            if (this.ParameterSetName == PARENTCATEGORY)
            {
                IEnumerable<IControlCategory<WebPartCategoryInfo>> categories = this.BusinessLayer.GetWebPartCategories(new WebPartCategory(this.ParentWebPartCategory), this.Recurse.ToBool());

                foreach (var category in categories)
                {
                    this.ActOnObject(category);
                }
            }
            else
            {
                base.ProcessRecord();
            }
        }

        #endregion

    }
}

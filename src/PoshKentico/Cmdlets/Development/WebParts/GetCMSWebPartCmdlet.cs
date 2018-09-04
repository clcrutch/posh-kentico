// <copyright file="GetCMSWebPartCmdlet.cs" company="Chris Crutchfield">
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
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using CMS.FormEngine;
using CMS.PortalEngine;
using ImpromptuInterface;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.WebParts
{
    /// <summary>
    /// <para type="synopsis">Gets the web parts selected by the provided input.</para>
    /// <para type="description">Gets the web parts selected by the provided input.</para>
    /// <para type="description">This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">Without parameters, this command returns all webparts.</para>
    /// <para type="description">With parameters, this command returns the webparts that match the criteria.</para>
    /// <example>
    ///     <para>Get all the webparts.</para>
    ///     <code>Get-CMSWebPart</code>
    /// </example>
    /// <example>
    ///     <para>Get web parts by category.</para>
    ///     <code>Get-CMSWebPartCategory | Get-CMSWebPart</code>
    /// </example>
    /// <example>
    ///     <para>Get web parts by category name.</para>
    ///     <code>Get-CMSWebPart -Category *test*</code>
    /// </example>
    /// <example>
    ///     <para>Get web parts by name.</para>
    ///     <code>Get-CMSWebPart -WebPartName *webpartname*</code>
    /// </example>
    /// <example>
    ///     <para>Get web parts by path</para>
    ///     <code>Get-CMSWebPart -Path /path/to/webpart</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Get, "CMSWebPart", DefaultParameterSetName = NONE)]
    [OutputType(typeof(WebPartInfo[]))]
    [Alias("gwp")]
    public class GetCMSWebPartCmdlet : MefCmdlet
    {
        #region Constants

        /// <summary>
        /// Represents no parameters.
        /// </summary>
        protected const string NONE = "None";

        private const string CATEGORY = "Category";
        private const string CATEGORYNAME = "Category Name";
        private const string FIELD = "Field";
        private const string NAME = "Name";
        private const string PATH = "Path";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The category name or display name of the webpart category that contains the webparts.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = CATEGORYNAME)]
        [Alias("Category")]
        public string CategoryName { get; set; }

        /// <summary>
        /// <para type="description">The field to get the associated web part for.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = FIELD, ValueFromPipeline = true)]
        [Alias("Property")]
        public FormFieldInfo Field { get; set; }

        /// <summary>
        /// <para type="description">Indicates if the CategoryName or Name supplied is a regular expression.</para>
        /// </summary>
        [Parameter(ParameterSetName = CATEGORYNAME)]
        [Parameter(ParameterSetName = NAME)]
        [Alias("Regex")]
        public SwitchParameter RegularExpression { get; set; }

        /// <summary>
        /// <para type="description">The name or display name of the webpart.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = NAME)]
        [Alias("Name")]
        public string WebPartName { get; set; }

        /// <summary>
        /// <para type="description">The path to the webpart.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = PATH)]
        [Alias("Path")]
        public string WebPartPath { get; set; }

        /// <summary>
        /// <para type="description">An object that represents the webpart category that contains the webparts.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = CATEGORY)]
        public WebPartCategoryInfo WebPartCategory { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this web part. Populated by MEF.
        /// </summary>
        [Import]
        public GetCMSWebPartBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<IWebPart> webparts = null;

            switch (this.ParameterSetName)
            {
                case CATEGORY:
                    webparts = this.BusinessLayer.GetWebPartsByCategory(this.WebPartCategory.ActLike<IWebPartCategory>());
                    break;
                case CATEGORYNAME:
                    webparts = this.BusinessLayer.GetWebPartsByCategory(this.CategoryName, this.RegularExpression.ToBool());
                    break;
                case FIELD:
                    webparts = new IWebPart[]
                    {
                        this.BusinessLayer.GetWebPart(this.Field.ActLike<IWebPartField>()),
                    };
                    break;
                case NAME:
                    webparts = this.BusinessLayer.GetWebParts(this.WebPartName, this.RegularExpression.ToBool());
                    break;
                case PATH:
                    webparts = new IWebPart[]
                    {
                        this.BusinessLayer.GetWebPart(this.WebPartPath),
                    };
                    break;

                case NONE:
                    webparts = this.BusinessLayer.GetWebParts();
                    break;
            }

            foreach (var webpart in webparts)
            {
                this.ActOnObject(webpart);
            }
        }

        /// <summary>
        /// When overridden in a child class, operates on the specified web part.
        /// </summary>
        /// <param name="webPart">The web part to operate on.</param>
        protected virtual void ActOnObject(IWebPart webPart)
        {
            this.WriteObject(webPart?.UndoActLike());
        }

        #endregion

    }
}

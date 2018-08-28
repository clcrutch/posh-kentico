// <copyright file="NewCMSWebPartCmdlet.cs" company="Chris Crutchfield">
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
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.WebParts
{
    /// <summary>
    /// <para type="synopsis">Creates a new web part.</para>
    /// <para type="description">Creates a new web part category based off of the provided input.</para>
    /// <para type="description">This cmdlet returns the newly created web part when the -PassThru switch is used.</para>
    /// <example>
    ///     <para>Create a new web part implying the display name from the path.</para>
    ///     <code>New-CMSWebPart -Path /TestCategory/TestWebPart -FileName Test.ascx</code>
    /// </example>
    /// <example>
    ///     <para>Create a new web part using the path.</para>
    ///     <code>New-CMSWebPart -Path /TestCategory/TestWebPart -FileName Test.ascx -DisplayName TestDisplayName</code>
    /// </example>
    /// <example>
    ///     <para>Create a new web part implying the display name from the name.</para>
    ///     <code>$category | New-CMSWebPart -Name TestWebPart -FileName Test.ascx</code>
    /// </example>
    /// <example>
    ///     <para>Create a new web part using the category and name.</para>
    ///     <code>$category | New-CMSWebPart -Name TestWebPart -FileName Test.ascx -DisplayName TestDisplayName</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.New, "CMSWebPart", DefaultParameterSetName = PATH)]
    [OutputType(typeof(WebPartInfo[]))]
    [Alias("nwp")]
    public class NewCMSWebPartCmdlet : MefCmdlet
    {
        #region Constants
        private const string PATH = "Path";
        private const string CATEGORY = "Category";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The display name for the newly created webpart.</para>
        /// </summary>
        [Parameter(Position = 2)]
        public string DisplayName { get; set; }

        /// <summary>
        /// <para type="description">The file name for the webpart code behind.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1)]
        [Alias("File")]
        public string FileName { get; set; }

        /// <summary>
        /// <para type="description">The Code Name for the webpart.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = CATEGORY)]
        [Alias("CodeName")]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">The path to the webpart.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PATH)]
        public string Path { get; set; }

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the newly created web part.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        /// <para type="description">The webpart category to add the webpart under.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = CATEGORY)]
        [Alias("Category", "Parent")]
        public WebPartCategoryInfo WebPartCategory { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this web part. Populated by MEF.
        /// </summary>
        [Import]
        public NewCMSWebPartBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IWebPart webPart = null;
            switch (this.ParameterSetName)
            {
                case PATH:
                    webPart = this.BusinessLayer.CreateWebPart(this.Path, this.FileName, this.DisplayName);
                    break;
                case CATEGORY:
                    webPart = this.BusinessLayer.CreateWebPart(this.Name, this.FileName, this.DisplayName, this.WebPartCategory.ActLike<IWebPartCategory>());
                    break;
            }

            if (this.PassThru.ToBool())
            {
                this.WriteObject(webPart.UndoActLike());
            }
        }

        #endregion
    }
}

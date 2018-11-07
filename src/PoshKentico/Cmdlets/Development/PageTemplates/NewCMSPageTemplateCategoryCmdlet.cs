﻿// <copyright file="NewCMSPageTemplateCategoryCmdlet.cs" company="Chris Crutchfield">
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
using PoshKentico.Business.Development.PageTemplates;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.PageTemplate
{
    /// <summary>
    /// <para type="synopsis">Creates a new page template category.</para>
    /// <para type="description">Creates a new page template category based off of the provided input.</para>
    /// <para type="description">This cmdlet returns the newly created page template category when the -PassThru switch is used.</para>
    /// <para type="description">This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <example>
    ///     <para>Create a new page template category implying the display name.</para>
    ///     <code>New-CMSPageTemplateCategory -Path /Test/Test1</code>
    /// </example>
    /// <example>
    ///     <para>Create a new page template category specifying the display name.</para>
    ///     <code>New-CMSPageTemplateCategory -Path /Test/Test1 -DisplayName "My Test Category" -CodeName "MyCodeName"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.New, "CMSPageTemplateCategory")]
    [OutputType(typeof(PageTemplateCategoryInfo[]), ParameterSetName = new string[] { PASSTHRU })]
    [Alias("nwpc")]
    public class NewCMSPageTemplateCategoryCmdlet : MefCmdlet
    {
        #region Constants

        private const string PASSTHRU = "PassThru";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The code name for the newly created page template category.</para>
        /// <para type="description">If null, then the name portion of the path is used for the code name.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 2)]
        public string CodeName { get; set; }

        /// <summary>
        /// <para type="description">The display name for the newly created page template category.</para>
        /// <para type="description">If null, then the name portion of the path is used for the display name.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 1)]
        public string DisplayName { get; set; }

        /// <summary>
        /// <para type="description">The path to create the new page template category at.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string Path { get; set; }

        /// <summary>
        /// <para type="description">The path for the icon for the newly created page template category.</para>
        /// </summary>
        [Parameter]
        [Alias("IconPath")]
        public string ImagePath { get; set; }

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the newly created page template category.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = PASSTHRU)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this page template.  Populated by MEF.
        /// </summary>
        [Import]
        public NewCMSPageTemplateCategoryBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            var newCategory = this.BusinessLayer.CreatePageTemplateCategory(this.Path, this.DisplayName, this.ImagePath);

            if (this.PassThru.ToBool())
            {
                this.WriteObject(newCategory.UndoActLike());
            }
        }

        #endregion

    }
}
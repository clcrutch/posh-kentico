// <copyright file="SetCMSPageTemplateCategoryCmdlet.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Development.PageTemplates;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.PageTemplates
{
    /// <summary>
    /// <para type="synopsis">Sets a page template category.</para>
    /// <para type="description">Sets a page template category.</para>
    /// <para type="description">This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <example>
    ///     <para>Sets a page template category.</para>
    ///     <code>$pageTemplateCategory | Set-CMSPageTemplateCategory</code>
    /// </example>
    /// <example>
    ///     <para>Sets a page template category and returns the result.</para>
    ///     <code>$pageTemplateCategory | Set-CMSPageTemplateCategory -PassThru</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Set, "CMSPageTemplateCategory")]
    [OutputType(typeof(PageTemplateCategoryInfo[]), ParameterSetName = new string[] { PASSTHRU })]
    [Alias("sptc")]
    public class SetCMSPageTemplateCategoryCmdlet : MefCmdlet
    {
        #region Constants

        private const string PASSTHRU = "PassThru";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the page template category.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = PASSTHRU)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        /// <para type="description">A reference to the PageTemplate category to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        [Alias("Category")]
        public PageTemplateCategoryInfo PageTemplateCategory { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this page template.  Populated by MEF.
        /// </summary>
        [Import]
        public SetCMSPageTemplateCategoryBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            this.BusinessLayer.Set(this.PageTemplateCategory.ActLike<IPageTemplateCategory>());

            if (this.PassThru.ToBool())
            {
                this.WriteObject(this.PageTemplateCategory);
            }
        }

        #endregion

    }
}

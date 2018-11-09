// <copyright file="RemoveCMSPageTemplateCmdlet.cs" company="Chris Crutchfield">
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
    /// <para type="synopsis">Removes the page templates selected by the provided input.</para>
    /// <para type="description">Removes the page templates selected by the provided input.</para>
    /// <para type="description">This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">Without parameters, this command removes all pagetemplates.</para>
    /// <para type="description">With parameters, this command removes the pagetemplates that match the criteria.</para>
    /// <example>
    ///     <para>Remove all the pagetemplates.</para>
    ///     <code>Remove-CMSPageTemplate</code>
    /// </example>
    /// <example>
    ///     <para>Remove page templates by category.</para>
    ///     <code>Get-CMSPageTemplateCategory | Remove-CMSPageTemplate</code>
    /// </example>
    /// <example>
    ///     <para>Remove page templates by category name.</para>
    ///     <code>Remove-CMSPageTemplate -Category *test*</code>
    /// </example>
    /// <example>
    ///     <para>Remove page templates by name.</para>
    ///     <code>Remove-CMSPageTemplate -PageTemplateName *pagetemplatename*</code>
    /// </example>
    /// <example>
    ///     <para>Remove page templates by path</para>
    ///     <code>Remove-CMSPageTemplate -Path /path/to/pagetemplate</code>
    /// </example>
    /// <example>
    ///     <code>Remove page templates through the pipeline.</code>
    ///     <code>$pageTemplate | Remove-CMSPageTemplate</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSPageTemplate", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = NONE)]
    [Alias("rpt")]
    public class RemoveCMSPageTemplateCmdlet : GetCMSPageTemplateCmdlet
    {
        #region Constants

        private const string WEBPART = "PageTemplate";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The page template to remove from the system.</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, Mandatory = true, ParameterSetName = WEBPART)]
        public PageTemplateInfo PageTemplate { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this page template. Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCMSPageTemplateBusiness RemoveBusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            if (this.ParameterSetName == WEBPART)
            {
                this.ActOnObject(this.PageTemplate.ActLike<IPageTemplate>());
            }
            else
            {
                base.ProcessRecord();
            }
        }

        /// <inheritdoc />
        protected override void ActOnObject(IPageTemplate pageTemplate)
        {
            if (pageTemplate == null)
            {
                return;
            }

            this.RemoveBusinessLayer.RemovePageTemplate(pageTemplate);
        }

        #endregion

    }
}

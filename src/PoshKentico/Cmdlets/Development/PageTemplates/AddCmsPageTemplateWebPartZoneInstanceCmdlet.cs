﻿// <copyright file="AddCmsPageTemplateWebPartZoneInstance.cs" company="Chris Crutchfield">
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
using CMS.FormEngine;
using CMS.PortalEngine;
using ImpromptuInterface;
using PoshKentico.Business.Development.PageTemplates;
using PoshKentico.Core.Services.Development.PageTemplates;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.PageTemplates
{
    /// <summary>
    /// <para type="synopsis">Adds a field to a web part.</para>
    /// <para type="description">Adds a field to the web part and then immediately saves the additional field in Kentico.</para>
    /// <para type="description">This cmdlet returns the newly created web part field when the -PassThru switch is used.</para>
    /// <para type="description">This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <example>
    ///     <para>Add field to web part.</para>
    ///     <code>$webPart | Add-CMSPageTemplateWebPartInstanced -DataType Text -Name TestProp -required -size 150 -defaultvalue TestValue</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Add, "CmsPageTemplateWebPartZone")]
    [OutputType(typeof(FormInfo))]
    [Alias("aptwpzi")]
    public class AddCmsPageTemplateWebPartZoneInstance : MefCmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">The caption for the new field.</para>
        /// </summary>
        [Parameter(Position = 2)]
        public string webPartZone { get; set; }

        /// <summary>
        /// <para type="description">The default value for the new field.</para>
        /// </summary>
        [Parameter(Position = 1)]
        public object DefaultValue { get; set; }

        /// <summary>
        /// <para type="description">The default value for the new field.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the newly created web part field.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        /// <para type="description">Indicates if a value is required for the field.</para>
        /// </summary>
        [Parameter]
        public SwitchParameter Required { get; set; }

        /// <summary>
        /// <para type="description">The size to make the new field.</para>
        /// </summary>
        [Parameter]
        public int Size { get; set; }

        /// <summary>
        /// <para type="description">The web part to add the zone to.</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, Mandatory = true)]
        public PageTemplateInfo PageTemplate { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this web part. Populated by MEF.
        /// </summary>
        [Import]
        public AddCMSPageTemplateWebPartInstanceBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            //wpi.IsWidget = false


            //var addFieldParameter = default(AddCMSPageTemplateWebPartInstanceBusiness.AddFieldParameter);
            //addFieldParameter.Caption = this.Caption;
            //addFieldParameter.ColumnType = this.DataType;
            //addFieldParameter.DefaultValue = this.DefaultValue;
            //addFieldParameter.Name = this.Name;
            //addFieldParameter.Required = this.Required.ToBool();
            //addFieldParameter.Size = this.Size;

            //var field = this.BusinessLayer.AddField(addFieldParameter, this.PageTemplate.ActLike<IPageTemplate>());

            //if (this.PassThru.ToBool())
            //{
            //    this.WriteObject(field.UndoActLike());
            //}
        }

        #endregion

    }
}

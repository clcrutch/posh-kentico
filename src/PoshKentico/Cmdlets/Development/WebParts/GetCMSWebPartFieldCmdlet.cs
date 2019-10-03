// <copyright file="GetCMSWebPartFieldCmdlet.cs" company="Chris Crutchfield">
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
    /// <para type="synopsis">Gets the web part fields selected by the provided input.</para>
    /// <para type="description">Gets the web part fields selected by the provided input.</para>
    /// <para type="description">This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <example>
    ///     <para>Gets all the web part fields associated with a web part.</para>
    ///     <code>$webPart | Get-CMSWebPartField</code>
    /// </example>
    /// <example>
    ///     <para>Gets the web part fields associated with a web part that match the specified name.</para>
    ///     <code>$webPart | Get-CMSWebPartField -Name Test*</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Get, "CMSWebPartField", DefaultParameterSetName = NONAME)]
    [OutputType(typeof(FormFieldInfo[]))]
    [Alias("gwp")]
    public class GetCMSWebPartFieldCmdlet : MefCmdlet<GetCMSWebPartFieldBusiness>
    {
        #region Constants

        private const string NAME = "Name";
        private const string NONAME = "No Name";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The name for the field to search for.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = NAME)]
        [Alias("Caption")]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Indicates if the CategoryName supplied is a regular expression.</para>
        /// </summary>
        [Parameter(ParameterSetName = NAME)]
        [Alias("Regex")]
        public SwitchParameter RegularExpression { get; set; }

        /// <summary>
        /// <para type="description">The web part to get the fields for.</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, Mandatory = true, ParameterSetName = NONAME)]
        [Parameter(ValueFromPipeline = true, Mandatory = true, ParameterSetName = NAME)]
        public WebPartInfo WebPart { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<IWebPartField> fields = null;

            switch (this.ParameterSetName)
            {
                case NAME:
                    fields = this.BusinessLayer.GetWebPartFields(this.Name, this.RegularExpression.ToBool(), this.WebPart.ActLike<IWebPart>());
                    break;
                case NONAME:
                    fields = this.BusinessLayer.GetWebPartFields(this.WebPart.ActLike<IWebPart>());
                    break;
            }

            foreach (var field in fields)
            {
                this.ActOnObject(field);
            }
        }

        /// <summary>
        /// When overridden in a child class, operates on the specified field.
        /// </summary>
        /// <param name="field">The field to operate on.</param>
        protected virtual void ActOnObject(IWebPartField field)
        {
            this.WriteObject(field?.UndoActLike());
        }

        #endregion

    }
}

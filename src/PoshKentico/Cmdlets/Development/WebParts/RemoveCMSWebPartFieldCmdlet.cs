// <copyright file="RemoveCMSWebPartFieldCmdlet.cs" company="Chris Crutchfield">
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
using ImpromptuInterface;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.WebParts
{
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSWebPartField", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    [Alias("rmwpf")]
    public class RemoveCMSWebPartFieldCmdlet : GetCMSWebPartFieldCmdlet
    {
        #region Constants

        private const string FIELD = "Field";

        #endregion

        #region Properties

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = FIELD)]
        [Alias("Property")]
        public FormFieldInfo Field { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this web part. Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCMSWebPartFieldBusiness RemoveBusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            if (this.ParameterSetName == FIELD)
            {
                this.ActOnObject(this.Field.ActLike<IWebPartField>());
            }
            else
            {
                base.ProcessRecord();
            }
        }

        /// <inheritdoc/>
        protected override void ActOnObject(IWebPartField field)
        {
            if (this.WebPart != null)
            {
                field.WebPart = this.WebPart.ActLike<IWebPart>();
            }

            this.RemoveBusinessLayer.RemoveField(field);
        }

        #endregion

    }
}

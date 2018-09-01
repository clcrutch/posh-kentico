// <copyright file="SetCMSWebPartFieldCmdlet.cs" company="Chris Crutchfield">
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
    [Cmdlet(VerbsCommon.Set, "CMSWebPartField")]
    [Alias("swpf")]
    public class SetCMSWebPartFieldCmdlet : MefCmdlet
    {
        #region Constants

        private const string PASSTHRU = "PassThru";

        #endregion

        /// <summary>
        /// <para type="description">The field to set in Kentico.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [Alias("Property")]
        public FormFieldInfo Field { get; set; }

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the web part.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = PASSTHRU)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this web part. Populated by MEF.
        /// </summary>
        [Import]
        public SetCMSWebPartFieldBusiness BusinessLayer { get; set; }

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            this.BusinessLayer.Set(this.Field.ActLike<IWebPartField>());

            if (this.PassThru.ToBool())
            {
                this.WriteObject(this.Field);
            }
        }
    }
}

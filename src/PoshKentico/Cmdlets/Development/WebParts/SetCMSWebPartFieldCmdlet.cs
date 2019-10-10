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

using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using CMS.FormEngine;
using ImpromptuInterface;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.WebParts
{
    /// <summary>
    /// <para type="synopsis">Sets a web part field.</para>
    /// <para type="description">Sets a web part field.</para>
    /// <para type="description">This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <example>
    ///     <para>Sets a web part field.</para>
    ///     <code>$webPartField | Set-CMSWebPartField</code>
    /// </example>
    /// <example>
    ///     <para>Sets a web part field and returns the result.</para>
    ///     <code>$webPartField | Set-CMSWebPartField -PassThru</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Set, "CMSWebPartField")]
    [OutputType(typeof(FormFieldInfo), ParameterSetName = new string[] { PASSTHRU })]
    [Alias("swpf")]
    public class SetCMSWebPartFieldCmdlet : MefCmdlet<SetCMSWebPartFieldBusiness>
    {
        #region Constants

        private const string PASSTHRU = "PassThru";

        #endregion

        #region Properties

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

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            this.BusinessLayer.Set(this.Field.ActLike<IWebPartField>());

            if (this.PassThru.ToBool())
            {
                this.WriteObject(this.Field);
            }
        }

        #endregion

    }
}

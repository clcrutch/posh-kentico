// <copyright file="GetCMSControlFieldCmdlet.cs" company="Chris Crutchfield">
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

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using ImpromptuInterface;
using PoshKentico.Business.Development;
using PoshKentico.Core.Services.Development;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development
{
    /// <summary>
    /// Base class for all Get-CMSControlField cmdlets.
    /// </summary>
    /// <typeparam name="TBusinessLayer">The business layer type used by the cmdlet.</typeparam>
    /// <typeparam name="TControlHolder">The type of control holder used to fields.</typeparam>
    /// <typeparam name="TControlService">The type of the control service used to get the controls and control categories.</typeparam>
    /// <typeparam name="TControl">The type of control return by the control service.</typeparam>
    /// <typeparam name="TControlCategory">The type of control category returned by the control service.</typeparam>
    [ExcludeFromCodeCoverage]
    public class GetCMSControlFieldCmdlet<TBusinessLayer, TControlHolder, TControlService, TControl, TControlCategory> : MefCmdlet<TBusinessLayer>
        where TBusinessLayer : GetCMSControlFieldBusiness<TControlService, TControl, TControlCategory>
        where TControlHolder : IControl<TControl>
        where TControlService : IControlService<TControl, TControlCategory>
    {
        #region Constants

        /// <summary>
        /// Represents parameter sets that include the name parameter.
        /// </summary>
        protected const string NAME = "Name";

        /// <summary>
        /// Represents parameter sets that do not include the name parameter.
        /// </summary>
        protected const string NONAME = "No Name";

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
        [Alias("WebPart", "Widget")]
        public TControl Control { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<IControlField<TControl>> fields = null;

            switch (this.ParameterSetName)
            {
                case NAME:
                    fields = this.BusinessLayer.GetControlFields(this.Name, this.RegularExpression.ToBool(), (TControlHolder)Activator.CreateInstance(typeof(TControlHolder), this.Control));
                    break;
                case NONAME:
                    fields = this.BusinessLayer.GetControlFields((TControlHolder)Activator.CreateInstance(typeof(TControlHolder), this.Control));
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
        protected virtual void ActOnObject(IControlField<TControl> field)
        {
            this.WriteObject(field?.UndoActLike());
        }

        #endregion

    }
}

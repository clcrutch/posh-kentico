// <copyright file="AddCmsUiElementToRoleCmdlet.cs" company="Chris Crutchfield">
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
using CMS.Membership;
using ImpromptuInterface;
using PoshKentico.Business.Configuration.Roles;
using PoshKentico.Core.Services.Configuration.Roles;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.Roles
{
    /// <summary>
    /// <para type="synopsis">Adds  Ui Element to specified roles.</para>
    /// <para type="description">Adds  Ui Element to specified roles based off of the provided input.</para>
    /// <example>
    ///     <para>Add Ui Element to all roles with role name "role".</para>
    ///     <code>Add-CmsUiElementToRole -ResourceName "CMS.Design" -ElementName "Design" -RoleName "Rolename"</code>
    /// </example>
    /// <example>
    ///     <para>Add Ui Element to all roles with role name "*role*".</para>
    ///     <code>Add-CmsUiElementToRole -ResourceName "CMS.Design" -ElementName "Design" -RoleName "role" -RegularExpression</code>
    /// </example>
    /// <example>
    ///     <para>Add Ui Element to all roles with role name "role", site is 2.</para>
    ///     <code>Add-CmsUiElementToRole -ResourceName "CMS.Design" -ElementName "Design" -RoleName "role" -SiteID 2</code>
    /// </example>
    ///  <example>
    ///     <para>Add Ui Element to all roles with role name "*role*", site is 2.</para>
    ///     <code>Add-CmsUiElementToRole -ResourceName "CMS.Design" -ElementName "Design" -RoleName "role" -SiteID 2 -RegularExpression</code>
    /// </example>
    /// <example>
    ///     <para>Add Ui Element with the specified role IDs.</para>
    ///     <code>Add-CmsUiElementToRole -ResourceName "CMS.Design" -ElementName "Design" -RoleIds 1,2,3</code>
    /// </example>
    /// <example>
    ///     <para>Add Ui Element with the specified role IDs.</para>
    ///     <code>Add-CmsUiElementToRole -ResourceName "CMS.Design" -ElementName "Design" -UserName "Username"</code>
    /// </example>
    ///  <example>
    ///     <para>Add Ui Element to all roles with a role name "role".</para>
    ///     <code>$role | Add-CmsUiElementToRole -ResourceName "CMS.Design" -ElementName "Design"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Add, "CmsUiElementToRole")]
    [Alias("aetorole")]
    public class AddCmsUIElementToRoleCmdlet : GetCmsRoleCmdlet
    {
        #region Constants

        private const string ROLEOBJECT = "ROLE";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The element name of the Ui element to add to the specified role.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = NONE)]
        [Parameter(Mandatory = true, ParameterSetName = ROLENAME)]
        [Parameter(Mandatory = true, ParameterSetName = IDSETNAME)]
        [Parameter(Mandatory = true, ParameterSetName = USERNAME)]
        [Parameter(Mandatory = true, ParameterSetName = USEROBJECT)]
        [Parameter(Mandatory = true, ParameterSetName = ROLEOBJECT)]
        public string ElementName { get; set; }

        /// <summary>
        /// <para type="description">The resource name of the Ui element to add to the specified role.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = NONE)]
        [Parameter(Mandatory = true, ParameterSetName = ROLENAME)]
        [Parameter(Mandatory = true, ParameterSetName = IDSETNAME)]
        [Parameter(Mandatory = true, ParameterSetName = USERNAME)]
        [Parameter(Mandatory = true, ParameterSetName = USEROBJECT)]
        [Parameter(Mandatory = true, ParameterSetName = ROLEOBJECT)]
        public string ResourceName { get; set; }

        /// <summary>
        /// <para type="description">The role to add a Ui element to.</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 1, ParameterSetName = ROLEOBJECT)]
        public RoleInfo Role { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this role. Populated by MEF.
        /// </summary>
        [Import]
        public AddCmsUIElementToRoleBusiness AddBusinessLayer { get; set; }
        #endregion

        #region Methods

        /// <inheritdoc/>
        protected override void ProcessRecord()
        {
            if (this.ParameterSetName == ROLEOBJECT)
            {
                this.ActOnObject(this.Role.ActLike<IRole>());
            }
            else
            {
                base.ProcessRecord();
            }
        }

        /// <inheritdoc />
        protected override void ActOnObject(IRole role)
        {
            if (role == null)
            {
                return;
            }

            this.AddBusinessLayer.AddUiElementToRole(role, this.ResourceName, this.ElementName);
        }
        #endregion
    }
}

// <copyright file="AddCmsPermissionToRoleCmdlet.cs" company="Chris Crutchfield">
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
    /// <para type="synopsis">Adds  module permissions to specified roles.</para>
    /// <para type="description">Adds  module permissions to specified roles based off of the provided input.</para>
    /// <example>
    ///     <para>Add module permissions to all roles with role name "role".</para>
    ///     <code>Add-CmsPermissionToRole -PermissionNames Read,Write -ResourceName "CMS.Content" -RoleName "Rolename"</code>
    /// </example>
    /// <example>
    ///     <para>Add module permissions to all roles with role name "*role*".</para>
    ///     <code>Add-CmsPermissionToRole -PermissionNames Read,Write -ResourceName "CMS.Content" -RoleName "role" -RegularExpression</code>
    /// </example>
    /// <example>
    ///     <para>Add module permissions to all roles with role name "role", site is 2.</para>
    ///     <code>Add-CmsPermissionToRole -PermissionNames Read,Write -ResourceName "CMS.Content" -RoleName "role" -SiteID 2</code>
    /// </example>
    ///  <example>
    ///     <para>Add module permissions to all roles with role name "*role*", site is 2.</para>
    ///     <code>Add-CmsPermissionToRole -PermissionNames Read,Write -ResourceName "CMS.Content" -RoleName "role" -SiteID 2 -RegularExpression</code>
    /// </example>
    /// <example>
    ///     <para>Add module permissions with the specified role IDs.</para>
    ///     <code>Add-CmsPermissionToRole -PermissionNames Read,Write -ResourceName "CMS.Content" -RoleIds 1,2,3</code>
    /// </example>
    /// <example>
    ///     <para>Add module permissions with the specified role IDs.</para>
    ///     <code>Add-CmsPermissionToRole -PermissionNames Read,Write -ResourceName "CMS.Content" -UserName "Username"</code>
    /// </example>
    ///  <example>
    ///     <para>Add module permissions to all roles with a role name "role".</para>
    ///     <code>$role | Add-CmsPermissionToRole -PermissionNames Read,Write -ResourceName "CMS.Content"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Add, "CmsPermissionToRole")]
    [Alias("aptorole")]
    public class AddCmsPermissionToRoleCmdlet : GetCmsRoleCmdlet
    {
        #region Constants

        private const string ROLEOBJECT = "ROLE";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The permission names of the module to add to the specified role.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = NONE)]
        [Parameter(Mandatory = true, ParameterSetName = ROLENAME)]
        [Parameter(Mandatory = true, ParameterSetName = IDSETNAME)]
        [Parameter(Mandatory = true, ParameterSetName = USERNAME)]
        [Parameter(Mandatory = true, ParameterSetName = USEROBJECT)]
        [Parameter(Mandatory = true, ParameterSetName = ROLEOBJECT)]
        public string[] PermissionNames { get; set; }

        /// <summary>
        /// <para type="description">The resource name of the module to add to the specified role.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = NONE)]
        [Parameter(Mandatory = true, ParameterSetName = ROLENAME)]
        [Parameter(Mandatory = true, ParameterSetName = IDSETNAME)]
        [Parameter(Mandatory = true, ParameterSetName = USERNAME)]
        [Parameter(Mandatory = true, ParameterSetName = USEROBJECT)]
        [Parameter(Mandatory = true, ParameterSetName = ROLEOBJECT)]
        public string ResourceName { get; set; }

        /// <summary>
        /// <para type="description">The class name of the module to add to the specified role.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = NONE)]
        [Parameter(Mandatory = false, ParameterSetName = ROLENAME)]
        [Parameter(Mandatory = false, ParameterSetName = IDSETNAME)]
        [Parameter(Mandatory = false, ParameterSetName = USERNAME)]
        [Parameter(Mandatory = false, ParameterSetName = USEROBJECT)]
        [Parameter(Mandatory = false, ParameterSetName = ROLEOBJECT)]
        public string ClassName { get; set; }

        /// <summary>
        /// <para type="description">The role to add the permissions to.</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 1, ParameterSetName = ROLEOBJECT)]
        public RoleInfo Role { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this role. Populated by MEF.
        /// </summary>
        [Import]
        public AddCmsPermissionToRoleBusiness AddBusinessLayer { get; set; }
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

            this.AddBusinessLayer.AddPermissionToRole(role, this.PermissionNames, this.ResourceName, this.ClassName);
        }
        #endregion
    }
}

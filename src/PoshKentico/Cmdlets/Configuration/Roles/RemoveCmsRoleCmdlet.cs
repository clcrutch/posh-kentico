// <copyright file="RemoveCmsRoleCmdlet.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Cmdlets.Configuration.Roles
{
    /// <summary>
    /// <para type="synopsis">Removes the roles selected by the provided input.</para>
    /// <para type="description">Removes the roles selected by the provided input.  This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">Without parameters, this command returns all roles.</para>
    /// <para type="description">With parameters, this command returns the roles that match the criteria.</para>
    /// <example>
    ///     <para>Remove all the roles.</para>
    ///     <code>Remove-CMSRole</code>
    /// </example>
    /// <example>
    ///     <para>Remove all roles with a role name "*role*".</para>
    ///     <code>Remove-CMSRole -RoleName "role" </code>
    /// </example>
    /// <example>
    ///     <para>Remove all roles with  role name "NewRole".</para>
    ///     <code>Remove-CMSRole -RoleName "NewRole" -Exact</code>
    /// </example>
    /// <example>
    ///     <para>Remove all roles with  role name "NewRole", site is 2.</para>
    ///     <code>Remove-CMSRole -RoleName "NewRole" -SiteID 2</code>
    /// </example>
    /// <example>
    ///     <para>Remove all roles with  role name "NewRole", site is 2.</para>
    ///     <code>Remove-CMSRole -RoleName "NewRole" -SiteID 2 -Exact</code>
    /// </example>
    /// <example>
    ///     <para>Remove all roles with the specified IDs.</para>
    ///     <code>Remove-CMSRole -ID 1,3</code>
    /// </example>
    /// <example>
    ///     <para>Remove the specified role.</para>
    ///     <code>Remove-CMSRole -Role $role</code>
    /// </example>
    /// <example>
    ///     <para>Remove the specified role.</para>
    ///     <code>$role | Remove-CMSRole</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSRole", DefaultParameterSetName = NONE, SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public class RemoveCmsRoleCmdlet : GetCmsRoleCmdlet
    {
        #region Constants

        private const string ROLEOBJECT = "Role";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The display name of the role to retrive.</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, Mandatory = true, ParameterSetName = ROLEOBJECT)]
        public RoleInfo Role { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this role. Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCmsRoleBusiness RemoveBusinessLayer { get; set; }
        #endregion

        #region Methods

        /// <inheritdoc />
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

            this.RemoveBusinessLayer.RemoveRoles(role);
        }

        #endregion
    }
}

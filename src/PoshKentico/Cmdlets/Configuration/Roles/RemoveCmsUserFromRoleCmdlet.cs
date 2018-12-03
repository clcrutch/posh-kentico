// <copyright file="RemoveCmsUserFromRoleCmdlet.cs" company="Chris Crutchfield">
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
    /// <para type="synopsis">Removes a user to a specified role.</para>
    /// <para type="description">Removes a user to a specified role based off of the provided input.</para>
    /// <para type="description">This cmdlet returns the newly modified role when the -PassThru switch is used.</para>
    /// <example>
    ///     <para>Remove a user with username "Username" to all roles with a role name "role".</para>
    ///     <code>Remove-CMSUserFromRole -UserNameToRemove "Username" -RoleName "Rolename"</code>
    /// </example>
    /// <example>
    ///     <para>Remove a user with username "Username" to all roles with  role name "*role*".</para>
    ///     <code>Remove-CMSUserFromRole -UserNameToRemove "Username" -RoleName "role" -RegularExpression</code>
    /// </example>
    /// <example>
    ///     <para>Remove a user with username "Username" to all roles with  role name "role", site is 2.</para>
    ///     <code>Remove-CMSUserFromRole -UserNameToRemove "Username" -RoleName "role" -SiteID 2</code>
    /// </example>
    ///  <example>
    ///     <para>Remove a user with username "Username" to all roles with  role name "*role*", site is 2.</para>
    ///     <code>Remove-CMSUserFromRole -UserNameToRemove "Username" -RoleName "role" -SiteID 2 -RegularExpression</code>
    /// </example>
    /// <example>
    ///     <para>Remove a user with username "Username" with the specified role IDs.</para>
    ///     <code>Remove-CMSUserFromRole -UserNameToRemove "Username" -RoleIds 1,2,3</code>
    /// </example>
    /// <example>
    ///     <para>Remove a user with username "Username" with the specified role IDs.</para>
    ///     <code>Remove-CMSUserFromRole -UserNameToRemove "Username" -UserName "Username"</code>
    /// </example>
    ///  <example>
    ///     <para>Remove a user with username "Username" to all roles with a role name "role".</para>
    ///     <code>$role | Remove-CMSUserFromRole -UserNameToRemove "Username"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CmsUserToRole")]
    [Alias("rufrole")]
    public class RemoveCmsUserFromRoleCmdlet : GetCmsRoleCmdlet
    {
        #region Constants

        private const string ROLEOBJECT = "ROLE";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The user name of the user to add to the specified role.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = NONE)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ROLENAME)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = IDSETNAME)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = USERNAME)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = USEROBJECT)]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ROLEOBJECT)]
        public string UserNameToRemove { get; set; }

        /// <summary>
        /// <para type="description">The role to add a user to.</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, Mandatory = true, Position = 1, ParameterSetName = ROLEOBJECT)]
        public RoleInfo Role { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this role. Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCmsUserFromRoleBusiness RemoveBusinessLayer { get; set; }
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

            this.RemoveBusinessLayer.RemoveUserFromRole(this.UserNameToRemove, role);
        }
        #endregion

    }
}

// <copyright file="GetCmsRoleCmdlet.cs" company="Chris Crutchfield">
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
using CMS.Membership;
using ImpromptuInterface;
using PoshKentico.Business.Configuration.Roles;
using PoshKentico.Core.Services.Configuration.Roles;
using PoshKentico.Core.Services.Configuration.Users;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.Roles
{
    /// <summary>
    /// <para type="synopsis">Gets the roles selected by the provided input.</para>
    /// <para type="description"></para>
    /// <para type="description">Without parameters, this command returns all roles.</para>
    /// <para type="description">With parameters, this command returns the roles that match the criteria.</para>
    /// <example>
    ///     <para>Get all the roles.</para>
    ///     <code>Get-CMSRole</code>
    /// </example>
    /// <example>
    ///     <para>Get all roles with a role name "role".</para>
    ///     <code>Get-CMSRole -RoleName "role" </code>
    /// </example>
    /// <example>
    ///     <para>Get all roles with  role name "*role*".</para>
    ///     <code>Get-CMSRole -RoleName "role" -RegularExpression</code>
    /// </example>
    /// <example>
    ///     <para>Get all roles with  role name "role", site is 2.</para>
    ///     <code>Get-CMSRole -RoleName "role" -SiteID 2</code>
    /// </example>
    /// <example>
    ///     <para>Get all roles with  role name "role", site is 2.</para>
    ///     <code>Get-CMSRole -RoleName "role" -SiteID 2 -RegularExpression</code>
    /// </example>
    /// <example>
    ///     <para>Get all roles with the specified IDs.</para>
    ///     <code>Get-CMSRole -RoleIds 1,3</code>
    /// </example>
    /// <example>
    ///     <para>Get all roles with the specified user name.</para>
    ///     <code>Get-CMSRole -UserName "Username"</code>
    /// </example>
    /// <example>
    ///     <para>Get all roles with the specified user.</para>
    ///     <code>$user | Get-CMSRole</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Get, "CMSRole", DefaultParameterSetName = NONE)]
    [OutputType(typeof(RoleInfo[]))]
    public class GetCmsRoleCmdlet : MefCmdlet<GetCmsRoleBusiness>
    {
        #region Constants

        /// <summary>
        /// Represents no parameters.
        /// </summary>
        protected const string NONE = "None";

        /// <summary>
        /// Represents role name is used in parameter.
        /// </summary>
        protected const string ROLENAME = "Role Name";

        /// <summary>
        /// Represents role id is used in parameter.
        /// </summary>
        protected const string IDSETNAME = "ID";

        /// <summary>
        /// Represents user name is used in parameter.
        /// </summary>
        protected const string USERNAME = "User Name";

        /// <summary>
        /// Represents user object is used in parameter.
        /// </summary>
        protected const string USEROBJECT = "User";
        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The role name of the role to retrive.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ROLENAME)]
        public string RoleName { get; set; }

        /// <summary>
        /// <para type="description">The site name of the role to retrive.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = ROLENAME)]
        public string SiteName { get; set; }

        /// <summary>
        /// <para type="description">The IDs of the role to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = IDSETNAME)]
        public int[] RoleIds { get; set; }

        /// <summary>
        /// <para type="description">The user name of the user to retrive roles from.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = USERNAME)]
        public string UserName { get; set; }

        /// <summary>
        /// <para type="description">The user to retrive roles from.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline =true, ParameterSetName = USEROBJECT)]
        public UserInfo User { get; set; }

        /// <summary>
        /// <para type="description">If set, do a regex match, else the exact match.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [Alias("Regex")]
        public SwitchParameter RegularExpression { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<IRole> roles = null;

            switch (this.ParameterSetName)
            {
                case ROLENAME:
                    roles = this.BusinessLayer.GetRoles(this.RoleName, this.SiteName, this.RegularExpression.ToBool());
                    break;
                case IDSETNAME:
                    roles = this.BusinessLayer.GetRoles(this.RoleIds);
                    break;
                case USERNAME:
                    roles = this.BusinessLayer.GetRolesFromUser(this.UserName);
                    break;
                case USEROBJECT:
                    roles = this.BusinessLayer.GetRolesFromUser(this.User.ActLike<IUser>());
                    break;
                case NONE:
                    roles = this.BusinessLayer.GetRoles();
                    break;
            }

            foreach (var role in roles)
            {
                this.ActOnObject(role);
            }
        }

        /// <summary>
        /// When overridden in a child class, operates on the specified action.
        /// </summary>
        /// <param name="role">The role to operate on.</param>
        protected virtual void ActOnObject(IRole role)
        {
            this.WriteObject(role.UndoActLike());
        }

        #endregion
    }
}

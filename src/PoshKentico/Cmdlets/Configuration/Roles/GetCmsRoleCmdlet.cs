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

namespace PoshKentico.Cmdlets.Configuration.Roles
{
    /// <summary>
    /// <para type="synopsis">Gets the roles selected by the provided input.</para>
    /// <para type="description">Gets the roles selected by the provided input.  This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">Without parameters, this command returns all roles.</para>
    /// <para type="description">With parameters, this command returns the roles that match the criteria.</para>
    /// <example>
    ///     <para>Get all the roles.</para>
    ///     <code>Get-CMSRole</code>
    /// </example>
    /// <example>
    ///     <para>Get all roles with a role name "*role*".</para>
    ///     <code>Get-CMSRole -RoleName "role" </code>
    /// </example>
    /// <example>
    ///     <para>Get all roles with  role name "NewRole".</para>
    ///     <code>Get-CMSRole -RoleName "NewRole" -Exact</code>
    /// </example>
    /// <example>
    ///     <para>Get all roles with  role name "NewRole", site is 2.</para>
    ///     <code>Get-CMSRole -RoleName "NewRole" -SiteID 2</code>
    /// </example>
    /// <example>
    ///     <para>Get all roles with  role name "NewRole", site is 2.</para>
    ///     <code>Get-CMSRole -RoleName "NewRole" -SiteID 2 -Exact</code>
    /// </example>
    /// <example>
    ///     <para>Get all roles with the specified IDs.</para>
    ///     <code>Get-CMSRole -ID 1,3</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Get, "CMSRole", DefaultParameterSetName = NONE)]
    [OutputType(typeof(RoleInfo[]))]
    public class GetCmsRoleCmdlet : MefCmdlet
    {
        #region Constants

        /// <summary>
        /// Represents no parameters.
        /// </summary>
        protected const string NONE = "None";
        private const string ROLENAME = "Role Name";
        private const string IDSETNAME = "ID";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The display name of the role to retrive.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = ROLENAME)]
        public string RoleName { get; set; }

        /// <summary>
        /// <para type="description">The display name of the role to retrive.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = ROLENAME)]
        public int? SiteID { get; set; }

        /// <summary>
        /// <para type="description">The IDs of the role to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = IDSETNAME)]
        public int[] ID { get; set; }

        /// <summary>
        /// <para type="description">If set, the match is exact, else the match performs a contains for display name and category name and starts with for path.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter Exact { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this role. Populated by MEF.
        /// </summary>
        [Import]
        public GetCmsRoleBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<IRole> roles = null;

            switch (this.ParameterSetName)
            {
                case ROLENAME:
                    this.SiteID = this.SiteID == null ? -1 : this.SiteID;
                    roles = this.BusinessLayer.GetRoles(this.RoleName, (int)this.SiteID, this.Exact.ToBool());
                    break;
                case IDSETNAME:
                    roles = this.BusinessLayer.GetRoles(this.ID);
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

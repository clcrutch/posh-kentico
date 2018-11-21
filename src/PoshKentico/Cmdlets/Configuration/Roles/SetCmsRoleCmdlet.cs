// <copyright file="SetCmsRoleCmdlet.cs" company="Chris Crutchfield">
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
    /// <para type="synopsis">Sets a role.</para>
    /// <para type="description">Sets a role based off of the provided input.</para>
    /// <para type="description">This cmdlet returns the updated role when the -PassThru switch is used.</para>
    /// <example>
    ///     <para>Set a role specifying the role.</para>
    ///     <code>Set-CMSRole -Role $role</code>
    /// </example>
    /// <example>
    ///     <para>Set a role specifying the role name.</para>
    ///     <code>$role | Set-CMSRole</code>
    /// </example>
    /// <example>
    ///     <para>Find a role by the role name and site id, and set its display name.</para>
    ///     <code>Set-CMSRole -RoleName "NewRole" -SiteID 2 -DisplayName "New Role"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Set, "CMSRole")]
    [OutputType(typeof(RoleInfo))]
    public class SetCmsRoleCmdlet : MefCmdlet
    {
        #region Constants

        private const string OBJECTSET = "Object";
        private const string PROPERTYSET = "Property";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">A reference to the role to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = OBJECTSET)]
        public RoleInfo Role { get; set; }

        /// <summary>
        /// <para type="description">The role name for the role.</para>
        /// <para type="description">Role name cannot be blank.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PROPERTYSET)]
        public string RoleName { get; set; }

        /// <summary>
        /// <para type="description">The Site ID for the role.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = PROPERTYSET)]
        public int SiteID { get; set; }

        /// <summary>
        /// <para type="description">The display name for the role.</para>
        /// <para type="description">Role display name cannot be blank.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 2, ParameterSetName = PROPERTYSET)]
        public string DisplayName { get; set; }

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the role.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this role.  Populated by MEF.
        /// </summary>
        [Import]
        public SetCmsRoleBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IRole updatedRole = null;
            switch (this.ParameterSetName)
            {
                case OBJECTSET:
                    updatedRole = this.BusinessLayer.Set(this.Role.ActLike<IRole>());
                    break;
                case PROPERTYSET:
                    updatedRole = this.BusinessLayer.Set(this.DisplayName, this.RoleName, this.SiteID);
                    break;
            }

            if (this.PassThru.ToBool())
            {
                this.WriteObject(updatedRole.UndoActLike());
            }
        }

        #endregion
    }
}

﻿// <copyright file="SetCmsRoleNoLogCmdlet.cs" company="Chris Crutchfield">
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
using PoshKentico.Business.Configuration.Staging;
using PoshKentico.Core.Services.Configuration.Roles;

namespace PoshKentico.Cmdlets.Configuration.Staging
{
    /// <summary>
    /// <para type="synopsis">Sets a new role without logging any staging tasks.</para>
    /// <example>
    ///     <para>Sets a new role without logging any staging tasks.</para>
    ///     <code>Set-CMSRoleNoLog -Role $role</code>
    /// </example>
    /// <example>
    ///     <para>Set a new role without logging any staging tasks.</para>
    ///     <code>$role | Set-CMSRoleNoLog</code>
    /// </example>
    /// <example>
    ///     <para>Set a new role without logging any staging tasks..</para>
    ///     <code>Set-CMSRoleNoLog -RoleDisplayName "Role Display Name" -RoleName "Role Name" -SiteID "Site Id"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet("Set", "CMSRoleNoLog")]
    [OutputType(typeof(RoleInfo), ParameterSetName = new string[] { PASSTHRU })]
    public class SetCmsRoleNoLogCmdlet : MefCmdlet
    {
        #region Constants

        private const string PASSTHRU = "PassThru";
        private const string OBJECTSET = "Object";
        private const string PROPERTYSET = "Property";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">A reference to the role to set.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = OBJECTSET)]
        public RoleInfo RoleToSet { get; set; }

        /// <summary>
        /// <para type="description">The role name for the role to set.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PROPERTYSET)]
        public string RoleName { get; set; }

        /// <summary>
        /// <para type="description">The role site id for the role to set.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = PROPERTYSET)]
        public int SiteID { get; set; }

        /// <summary>
        /// <para type="description">The display name for the role to set.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = PROPERTYSET)]
        public string DisplayName { get; set; }

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the role to set.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = PASSTHRU)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this role.  Populated by MEF.
        /// </summary>
        [Import]
        public SetCmsRoleNoLogBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case OBJECTSET:
                    this.BusinessLayer.SetNoLogRole(this.RoleToSet.ActLike<IRole>());
                    break;
                case PROPERTYSET:
                    this.BusinessLayer.SetNoLogRole(this.DisplayName, this.RoleName, this.SiteID);
                    break;
            }

            if (this.PassThru.ToBool())
            {
                this.WriteObject(this.RoleToSet);
            }
        }

        #endregion
    }
}
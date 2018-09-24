// <copyright file="NewCmsRoleCmdlet.cs" company="Chris Crutchfield">
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
using CMS.Base;
using CMS.Membership;
using ImpromptuInterface;
using PoshKentico.Business.Configuration.Roles;

namespace PoshKentico.Cmdlets.Configuration.Roles
{
    /// <summary>
    /// <para type="synopsis">Creates a new role.</para>
    /// <para type="description">Creates a new role based off of the provided input.</para>
    /// <para type="description">This cmdlet returns the newly created role when the -PassThru switch is used.</para>
    /// <example>
    ///     <para>Create a new role specifying the role display name.</para>
    ///     <code>New-CMSRole -DisplayName "NewRole"</code>
    /// </example>
    /// <example>
    ///     <para>Create a new role specifying the role name, full name, email, culture code and privilege level.</para>
    ///     <code>New-CMSRole -DisplayName "New Role" -RoleName "NewRole" -SiteID 2</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.New, "CMSRole")]
    [OutputType(typeof(RoleInfo))]
    public class NewCmsRoleCmdlet : MefCmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">The display name for the newly created role.</para>
        /// <para type="description">Role display name cannot be blank.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string DisplayName { get; set; }

        /// <summary>
        /// <para type="description">The Role name for the newly created role.</para>
        /// <para type="description">Role name cannot be blank.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 1)]
        public string RoleName { get; set; }

        /// <summary>
        /// <para type="description">The email for the newly created role.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 2)]
        public int SiteID { get; set; }

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the newly created role.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this role.  Populated by MEF.
        /// </summary>
        [Import]
        public NewCmsRoleBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            var newRole = this.BusinessLayer.CreateRole(this.DisplayName, this.RoleName, this.SiteID);

            if (this.PassThru.ToBool())
            {
                this.WriteObject(newRole.UndoActLike());
            }
        }

        #endregion
    }
}

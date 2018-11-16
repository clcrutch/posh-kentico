// <copyright file="RemoveCmsUserCmdlet.cs" company="Chris Crutchfield">
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
using PoshKentico.Business.Configuration.Users;
using PoshKentico.Core.Services.Configuration.Users;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.Users
{
    /// <summary>
    /// <para type="synopsis">Removes the users selected by the provided input.</para>
    /// <para type="description">Removes the users selected by the provided input.  This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">Without parameters, this command removes all users.</para>
    /// <para type="description">With parameters, this command removes the users that match the criteria.</para>
    /// <example>
    ///     <para>Remove all the users.</para>
    ///     <code>Remove-CMSUser</code>
    /// </example>
    /// <example>
    ///     <para>Remove all users with a user name "*user*".</para>
    ///     <code>Remove-CMSUser user</code>
    /// </example>
    /// <example>
    ///     <para>Remove all users with  user name "NewUser".</para>
    ///     <code>Remove-CMSUser -UserName "NewUser" -Exact</code>
    /// </example>
    /// <example>
    ///     <para>Remove all users with the specified IDs.</para>
    ///     <code>Remove-CMSUser -ID 1,3</code>
    /// </example>
    /// <example>
    ///     <para>Remove the specified user.</para>
    ///     <code>Remove-CMSUser -User $user</code>
    /// </example>
    /// <example>
    ///     <para>Remove the specified user.</para>
    ///     <code>$user | Remove-CMSUser</code>
    /// </example>
    /// <example>
    ///     <para>Remove the specified users from a role.</para>
    ///     <code>Remove-CMSUser -RoleName "roleName" -SiteID 4</code>
    /// </example>
    /// <example>
    ///     <para>Remove the specified users from a role.</para>
    ///     <code>$role | Remove-CMSUser</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSUser", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = NONE)]
    [Alias("ruser")]
    public class RemoveCmsUserCmdlet : GetCmsUserCmdlet
    {
        #region Constants

        private const string USEROBJECT = "User";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The display name of the user to retrive.</para>
        /// </summary>
        [Parameter(ValueFromPipeline = true, Mandatory = true, ParameterSetName = USEROBJECT)]
        public UserInfo User { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this user. Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCmsUserBusiness RemoveBusinessLayer { get; set; }
        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            if (this.ParameterSetName == USEROBJECT)
            {
                this.ActOnObject(this.User.ActLike<IUser>());
            }
            else
            {
                base.ProcessRecord();
            }
        }

        /// <inheritdoc />
        protected override void ActOnObject(IUser user)
        {
            if (user == null)
            {
                return;
            }

            this.RemoveBusinessLayer.RemoveUsers(user);
        }

        #endregion
    }
}

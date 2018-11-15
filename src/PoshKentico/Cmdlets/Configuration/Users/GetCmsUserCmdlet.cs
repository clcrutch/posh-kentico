// <copyright file="GetCmsUserCmdlet.cs" company="Chris Crutchfield">
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
using PoshKentico.Business.Configuration.Users;
using PoshKentico.Core.Services.Configuration.Users;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Configuration.Users
{
    /// <summary>
    /// <para type="synopsis">Gets the users selected by the provided input.</para>
    /// <para type="description">Gets the users selected by the provided input.  This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">Without parameters, this command returns all users.</para>
    /// <para type="description">With parameters, this command returns the users that match the criteria.</para>
    /// <example>
    ///     <para>Get all the users.</para>
    ///     <code>Get-CMSUser</code>
    /// </example>
    /// <example>
    ///     <para>Get all users with a user name "*user*".</para>
    ///     <code>Get-CMSUser user</code>
    /// </example>
    /// <example>
    ///     <para>Get all users with  user name "NewUser".</para>
    ///     <code>Get-CMSUser -UserName "NewUser" -Exact</code>
    /// </example>
    /// <example>
    ///     <para>Get all users with the specified IDs.</para>
    ///     <code>Get-CMSUser -ID 1,3</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Get, "CMSUser", DefaultParameterSetName = NONE)]
    [OutputType(typeof(UserInfo[]))]
    [Alias("guser")]
    public class GetCmsUserCmdlet : MefCmdlet
    {
        #region Constants

        /// <summary>
        /// Represents no parameters.
        /// </summary>
        protected const string NONE = "None";
        private const string USERNAME = "User Name";
        private const string IDSETNAME = "ID";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The display name of the user to retrive.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = USERNAME)]
        public string UserName { get; set; }

        /// <summary>
        /// <para type="description">The IDs of the user to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = IDSETNAME)]
        public int[] ID { get; set; }

        /// <summary>
        /// <para type="description">If set, the match is exact, else the match performs a contains for display name and category name and starts with for path.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        [Alias("Regex")]
        public SwitchParameter RegularExpression { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this user. Populated by MEF.
        /// </summary>
        [Import]
        public GetCmsUserBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<IUser> users = null;

            switch (this.ParameterSetName)
            {
                case USERNAME:
                    users = this.BusinessLayer.GetUsers(this.UserName, this.RegularExpression.ToBool());
                    break;
                case IDSETNAME:
                    users = this.BusinessLayer.GetUsers(this.ID);
                    break;
                case NONE:
                    users = this.BusinessLayer.GetUsers();
                    break;
            }

            foreach (var user in users)
            {
                this.ActOnObject(user);
            }
        }

        /// <summary>
        /// When overridden in a child class, operates on the specified action.
        /// </summary>
        /// <param name="user">The user to operate on.</param>
        protected virtual void ActOnObject(IUser user)
        {
            this.WriteObject(user.UndoActLike());
        }

        #endregion
    }
}

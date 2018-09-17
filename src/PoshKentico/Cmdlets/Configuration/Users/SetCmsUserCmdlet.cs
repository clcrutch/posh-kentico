// <copyright file="SetCmsUserCmdlet.cs" company="Chris Crutchfield">
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
using PoshKentico.Business.Configuration.Users;
using PoshKentico.Core.Services.Configuration.Users;

namespace PoshKentico.Cmdlets.Configuration.Users
{
    /// <summary>
    /// <para type="synopsis">Sets a user.</para>
    /// <para type="description">Sets a user based off of the provided input.</para>
    /// <para type="description">This cmdlet returns the updated user when the -PassThru switch is used.</para>
    /// <example>
    ///     <para>Set a user specifying the user name.</para>
    ///     <code>Set-CMSUser -User $user</code>
    /// </example>
    /// <example>
    ///     <para>Set a user specifying the user name.</para>
    ///     <code>$user | Set-CMSUser</code>
    /// </example>
    /// <example>
    ///     <para>Set a user specifying the user name, full name, email, culture code and privilege level.</para>
    ///     <code>Set-CMSUser -UserName "NewUser" -FullName "New user" -Email "new.user@domain.com" -PreferredCultureCode "en-us" -SiteIndependentPrivilegeLevel UserPrivilegeLevelEnum.Editor</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Set, "CMSUser")]
    [OutputType(typeof(UserInfo))]
    public class SetCmsUserCmdlet : MefCmdlet
    {
        #region Constants

        private const string OBJECTSET = "Object";
        private const string PROPERTYSET = "Property";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">A reference to the user to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = OBJECTSET)]
        public UserInfo User { get; set; }

        /// <summary>
        /// <para type="description">The User name for the newly created user.</para>
        /// <para type="description">User name cannot be blank.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PROPERTYSET)]
        public string UserName { get; set; }

        /// <summary>
        /// <para type="description">The full name for the newly created user.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = PROPERTYSET)]
        public string FullName { get; set; }

        /// <summary>
        /// <para type="description">The email for the newly created user.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 2, ParameterSetName = PROPERTYSET)]
        public string Email { get; set; }

        /// <summary>
        /// <para type="description">The preferred culture code for the newly created user.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 3, ParameterSetName = PROPERTYSET)]
        public string PreferredCultureCode { get; set; }

        /// <summary>
        /// <para type="description">The preferred culture code for the newly created user.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 4, ParameterSetName = PROPERTYSET)]
        public UserPrivilegeLevelEnum SiteIndependentPrivilegeLevel { get; set; }

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the user to update.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this user.  Populated by MEF.
        /// </summary>
        [Import]
        public SetCmsUserBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IUser updatedUser = null;
            switch (this.ParameterSetName)
            {
                case OBJECTSET:
                    updatedUser = this.BusinessLayer.Set(this.User.ActLike<IUser>());
                    break;
                case PROPERTYSET:
                    updatedUser = this.BusinessLayer.Set(this.UserName, this.FullName, this.Email, this.PreferredCultureCode, this.SiteIndependentPrivilegeLevel);
                    break;
            }

            if (this.PassThru.ToBool())
            {
                this.WriteObject(updatedUser.UndoActLike());
            }
        }

        #endregion
    }
}

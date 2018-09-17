// <copyright file="NewCmsUserCmdlet.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Cmdlets.Configuration.Users
{
    /// <summary>
    /// <para type="synopsis">Creates a new user.</para>
    /// <para type="description">Creates a new user based off of the provided input.</para>
    /// <para type="description">This cmdlet returns the newly created user when the -PassThru switch is used.</para>
    /// <example>
    ///     <para>Create a new user specifying the user name.</para>
    ///     <code>New-CMSUser -UserName "NewUser"</code>
    /// </example>
    /// <example>
    ///     <para>Create a new user specifying the user name, full name, email, culture code and privilege level.</para>
    ///     <code>New-CMSUser -UserName "NewUser" -FullName "New user" -Email "new.user@domain.com" -PreferredCultureCode "en-us" -SiteIndependentPrivilegeLevel UserPrivilegeLevelEnum.Editor</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.New, "CMSUser")]
    [OutputType(typeof(UserInfo))]
    public class NewCmsUserCmdlet : MefCmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">The User name for the newly created user.</para>
        /// <para type="description">User name cannot be blank.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string UserName { get; set; }

        /// <summary>
        /// <para type="description">The full name for the newly created user.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 1)]
        public string FullName { get; set; }

        /// <summary>
        /// <para type="description">The email for the newly created user.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 2)]
        public string Email { get; set; }

        /// <summary>
        /// <para type="description">The preferred culture code for the newly created user.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 3)]
        public string PreferredCultureCode { get; set; }

        /// <summary>
        /// <para type="description">The preferred culture code for the newly created user.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 4)]
        public UserPrivilegeLevelEnum SiteIndependentPrivilegeLevel { get; set; }

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the newly created user.</para>
        /// </summary>
        [Parameter(Mandatory = false)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this user.  Populated by MEF.
        /// </summary>
        [Import]
        public NewCmsUserBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            var newUser = this.BusinessLayer.CreateUser(this.UserName, this.FullName, this.Email, this.PreferredCultureCode, this.SiteIndependentPrivilegeLevel);

            if (this.PassThru.ToBool())
            {
                this.WriteObject(newUser.UndoActLike());
            }
        }

        #endregion
    }
}

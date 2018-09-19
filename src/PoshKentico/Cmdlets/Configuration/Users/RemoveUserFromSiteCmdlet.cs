// <copyright file="RemoveUserFromSiteCmdlet.cs" company="Chris Crutchfield">
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
using CMS.SiteProvider;
using ImpromptuInterface;
using PoshKentico.Business.Configuration.Users;
using PoshKentico.Core.Services.Configuration.Users;

namespace PoshKentico.Cmdlets.Configuration.Users
{
    /// <summary>
    /// <para type="synopsis">Removes the users from the site selected by the provided input.</para>
    /// <para type="description">Removes the users from the site selected by the provided input.  This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <example>
    ///     <para>Remove the specified user from the site.</para>
    ///     <code>$user | Remove-CMSUserToSite -Site $site</code>
    /// </example>
    /// <example>
    ///     <para>Remove the specified user from the site.</para>
    ///     <code>$user | Remove-CMSUserToSite -SiteName "MySite"</code>
    /// </example>
    /// <example>
    ///     <para>Remove the specified user from the site.</para>
    ///     <code>Remove-CMSUserToSite -User $user -Site $site</code>
    /// </example>
    /// <example>
    ///     <para>Remove the specified user from the site.</para>
    ///     <code>Remove-CMSUserToSite -User $user -SiteName "MySite"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSUserFromSite")]
    public class RemoveUserFromSiteCmdlet : MefCmdlet
    {
        #region Constants

        /// <summary>
        /// Represents no parameters.
        /// </summary>
        protected const string NONE = "None";
        private const string SITENAME = "Site Name";
        private const string SITEOBJECT = "Site";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The display name of the user to retrive.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public UserInfo User { get; set; }

        /// <summary>
        /// <para type="description">The display name of the user to retrive.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = SITENAME)]
        public string SiteName { get; set; }

        /// <summary>
        /// <para type="description">The IDs of the user to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName =SITEOBJECT)]
        public SiteInfo Site { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this user. Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCmsUserFromSiteBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case SITEOBJECT:
                    this.BusinessLayer.RemoveUserFromSite(this.User.ActLike<IUser>(), this.Site.SiteName);
                    break;
                case SITENAME:
                    this.BusinessLayer.RemoveUserFromSite(this.User.ActLike<IUser>(), this.SiteName);
                    break;
            }
        }

        #endregion
    }
}
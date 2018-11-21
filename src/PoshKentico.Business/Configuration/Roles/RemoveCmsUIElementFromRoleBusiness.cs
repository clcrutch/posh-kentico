// <copyright file="RemoveCmsUIElementFromRoleBusiness.cs" company="Chris Crutchfield">
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
using CMS.Modules;
using ImpromptuInterface;
using PoshKentico.Core.Services.Configuration.Roles;
using PoshKentico.Core.Services.Development.Modules;

namespace PoshKentico.Business.Configuration.Roles
{
    /// <summary>
    /// Business Layer of Remove-CmsUIElementFromRole cmdlet.
    /// </summary>
    [Export(typeof(RemoveCmsUIElementFromRoleBusiness))]
    public class RemoveCmsUIElementFromRoleBusiness : CmdletBusinessBase
    {
        #region Properties

        /// <summary>
        /// Gets or sets a reference to the Role Service.  Populated by MEF.
        /// </summary>
        [Import]
        public IRoleService RoleService { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Remove an UI element from a role.
        /// </summary>
        /// <param name="role">The role to remove an UI element from.</param>
        /// <param name="resourceName">The resource name related to the UI element <see cref="IUIElement"/>.</param>
        /// <param name="elementName">The element name of the ui element <see cref="IUIElement"/>.</param>
        public void RemoveUiElementFromRole(IRole role, string resourceName, string elementName)
        {
            UIElementInfo elementInfo = UIElementInfoProvider.GetUIElementInfo(resourceName, elementName);
            if (elementInfo != null)
            {
                var element = new
                {
                    ElementName = elementName,
                    elementInfo.ElementResourceID,
                };
                this.RoleService.RemoveUiElementFromRole(element.ActLike<IUIElement>(), role);
            }
        }
        #endregion

    }
}

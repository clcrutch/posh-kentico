// <copyright file="RemoveCMSWebPartFieldBusiness.cs" company="Chris Crutchfield">
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
using CMS.PortalEngine;
using PoshKentico.Core.Services.Development;
using PoshKentico.Core.Services.Development.WebParts;

namespace PoshKentico.Business.Development.WebParts
{
    /// <summary>
    /// Business layer for the Remove-CMSWebPartField cmdlet.
    /// </summary>
    [Export(typeof(RemoveCMSWebPartFieldBusiness))]
    public class RemoveCMSWebPartFieldBusiness : ControlBusinessBase<IWebPartService, WebPartInfo, WebPartCategoryInfo>
    {
        #region Methods

        /// <summary>
        /// Removes an <see cref="IControlField{T}"/> from Kentico.
        /// </summary>
        /// <param name="field">The <see cref="IControlField{T}"/> to remove.</param>
        public void RemoveField(IControlField<WebPartInfo> field)
        {
            if (this.OutputService.ShouldProcess(field.Name, $"Remove the field from web part named '{field.Control.Name}'."))
            {
                this.ControlService.RemoveField(field);
            }
        }

        #endregion

    }
}

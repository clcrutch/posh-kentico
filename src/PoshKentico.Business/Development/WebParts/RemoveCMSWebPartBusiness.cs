// <copyright file="RemoveCMSWebPartBusiness.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Development.WebParts;

namespace PoshKentico.Business.Development.WebParts
{
    /// <summary>
    /// Business layer fo the Remove-CMSWebPart cmdlet.
    /// </summary>
    [Export(typeof(RemoveCMSWebPartBusiness))]
    public class RemoveCMSWebPartBusiness : WebPartBusinessBase
    {
        #region Methods

        /// <summary>
        /// Removes the supplied <see cref="IWebPart"/> from the system.
        /// </summary>
        /// <param name="webPart">The <see cref="IWebPart"/> to remove.</param>
        public void RemoveWebPart(IWebPart webPart)
        {
            if (this.ShouldProcess(webPart.WebPartName, "Remove the web part from Kentico."))
            {
                this.WebPartService.Delete(webPart);
            }
        }

        #endregion

    }
}

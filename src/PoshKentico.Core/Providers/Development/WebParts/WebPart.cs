// <copyright file="WebPart.cs" company="Chris Crutchfield">
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

using CMS.PortalEngine;
using PoshKentico.Core.Services.Development.WebParts;

namespace PoshKentico.Core.Providers.Development.WebParts
{
    /// <summary>
    /// Wrapper around Kentico's <see cref="WebPart"/>.
    /// </summary>
    public class WebPart : Control<WebPartInfo>, IWebPart
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebPart"/> class.
        /// </summary>
        public WebPart()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebPart"/> class.
        /// </summary>
        /// <param name="backingControl">The underlying <see cref="WebPartInfo"/> that will be wrapped.</param>
        public WebPart(WebPartInfo backingControl)
            : base(backingControl)
        {
        }

        /// <inheritdoc />
        public override int CategoryID { get => this.BackingControl.WebPartCategoryID; set => this.BackingControl.WebPartCategoryID = value; }

        /// <inheritdoc />
        public override string DisplayName { get => this.BackingControl.WebPartDisplayName; set => this.BackingControl.WebPartDisplayName = value; }

        /// <inheritdoc />
        public override int ID { get => this.BackingControl.WebPartID; set => this.BackingControl.WebPartID = value; }

        /// <inheritdoc />
        public override string Name { get => this.BackingControl.WebPartName; set => this.BackingControl.WebPartName = value; }

        /// <summary>
        /// Gets or sets the file name of the underlying web part.
        /// </summary>
        public string FileName { get => this.BackingControl.WebPartFileName; set => this.BackingControl.WebPartFileName = value; }

        /// <inheritdoc />
        public override string Properties { get => this.BackingControl.WebPartProperties; set => this.BackingControl.WebPartProperties = value; }
    }
}

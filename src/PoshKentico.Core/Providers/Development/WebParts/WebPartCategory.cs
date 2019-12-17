// <copyright file="WebPartCategory.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Core.Providers.Development.WebParts
{
    /// <summary>
    /// Wrapper around Kentico's <see cref="WebPartCategory"/>.
    /// </summary>
    public class WebPartCategory : ControlCategory<WebPartCategoryInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebPartCategory"/> class.
        /// </summary>
        public WebPartCategory()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebPartCategory"/> class.
        /// </summary>
        /// <param name="backingControlCategory">The underlying <see cref="WebPartCategoryInfo"/> that will be wrapped.</param>
        public WebPartCategory(WebPartCategoryInfo backingControlCategory)
            : base(backingControlCategory)
        {
        }

        /// <inheritdoc />
        public override string DisplayName { get => this.BackingControlCategory.CategoryDisplayName; set => this.BackingControlCategory.CategoryDisplayName = value; }

        /// <inheritdoc />
        public override int ID { get => this.BackingControlCategory.CategoryID; set => this.BackingControlCategory.CategoryID = value; }

        /// <inheritdoc />
        public override string ImagePath { get => this.BackingControlCategory.CategoryImagePath; set => this.BackingControlCategory.CategoryImagePath = value; }

        /// <inheritdoc />
        public override string Name { get => this.BackingControlCategory.CategoryName; set => this.BackingControlCategory.CategoryName = value; }

        /// <inheritdoc />
        public override int ParentID { get => this.BackingControlCategory.CategoryParentID; set => this.BackingControlCategory.CategoryParentID = value; }

        /// <inheritdoc />
        public override string Path { get => this.BackingControlCategory.CategoryPath; set => this.BackingControlCategory.CategoryPath = value; }
    }
}

// <copyright file="WidgetCategory.cs" company="Chris Crutchfield">
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

namespace PoshKentico.Core.Providers.Development.Widgets
{
    /// <summary>
    /// Wrapper around Kentico's <see cref="WidgetCategoryInfo"/>.
    /// </summary>
    public class WidgetCategory : ControlCategory<WidgetCategoryInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetCategory"/> class.
        /// </summary>
        public WidgetCategory()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetCategory"/> class.
        /// </summary>
        /// <param name="backingControlCategory">The underlying <see cref="WidgetCategoryInfo"/> that will be wrapped.</param>
        public WidgetCategory(WidgetCategoryInfo backingControlCategory)
            : base(backingControlCategory)
        {
        }

        /// <inheritdoc />
        public override string DisplayName { get => this.BackingControlCategory.WidgetCategoryDisplayName; set => this.BackingControlCategory.WidgetCategoryDisplayName = value; }

        /// <inheritdoc />
        public override int ID { get => this.BackingControlCategory.WidgetCategoryID; set => this.BackingControlCategory.WidgetCategoryID = value; }

        /// <inheritdoc />
        public override string ImagePath { get => this.BackingControlCategory.WidgetCategoryImagePath; set => this.BackingControlCategory.WidgetCategoryImagePath = value; }

        /// <inheritdoc />
        public override string Name { get => this.BackingControlCategory.WidgetCategoryName; set => this.BackingControlCategory.WidgetCategoryName = value; }

        /// <inheritdoc />
        public override int ParentID { get => this.BackingControlCategory.WidgetCategoryParentID; set => this.BackingControlCategory.WidgetCategoryParentID = value; }

        /// <inheritdoc />
        public override string Path { get => this.BackingControlCategory.WidgetCategoryPath; set => this.BackingControlCategory.WidgetCategoryPath = value; }
    }
}

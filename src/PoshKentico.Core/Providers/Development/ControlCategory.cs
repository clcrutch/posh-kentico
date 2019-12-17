// <copyright file="ControlCategory.cs" company="Chris Crutchfield">
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

using System;
using PoshKentico.Core.Services.Development;

namespace PoshKentico.Core.Providers.Development
{
    /// <summary>
    /// Wraps a control category.
    /// </summary>
    /// <typeparam name="T">The control category type to wrap.</typeparam>
    public abstract class ControlCategory<T> : IControlCategory<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ControlCategory{T}"/> class.
        /// </summary>
        public ControlCategory()
        {
            this.BackingControlCategory = Activator.CreateInstance<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlCategory{T}"/> class.
        /// </summary>
        /// <param name="backingControlCategory">The underlying control category to wrap.</param>
        public ControlCategory(T backingControlCategory)
        {
            this.BackingControlCategory = backingControlCategory;
        }

        /// <summary>
        /// Gets the category of the control.
        /// </summary>
        public T BackingControlCategory { get; private set; }

        /// <summary>
        /// Gets or sets the display name of the control.
        /// </summary>
        public abstract string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the control.
        /// </summary>
        public abstract int ID { get; set; }

        /// <summary>
        /// Gets or sets the image path of the control.
        /// </summary>
        public abstract string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the name of the control.
        /// </summary>
        public abstract string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent ID of the control.
        /// </summary>
        public abstract int ParentID { get; set; }

        /// <summary>
        /// Gets or sets the path of the control.
        /// </summary>
        public abstract string Path { get; set; }
    }
}

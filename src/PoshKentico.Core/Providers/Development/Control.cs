// <copyright file="Control.cs" company="Chris Crutchfield">
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
    /// Wraps a control.
    /// </summary>
    /// <typeparam name="T">The control type to wrap.</typeparam>
    public abstract class Control<T> : IControl<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Control{T}"/> class.
        /// </summary>
        public Control()
        {
            this.BackingControl = Activator.CreateInstance<T>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Control{T}"/> class.
        /// </summary>
        /// <param name="backingControl">The underlying control to wrap.</param>
        public Control(T backingControl)
        {
            this.BackingControl = backingControl;
        }

        /// <summary>
        /// Gets the underlying control.
        /// </summary>
        public T BackingControl { get; private set; }

        /// <summary>
        /// Gets or sets the category ID of the underlying control.
        /// </summary>
        public abstract int CategoryID { get; set; }

        /// <summary>
        /// Gets or sets the display name of the underlying control.
        /// </summary>
        public abstract string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the ID of the underlying control.
        /// </summary>
        public abstract int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the underlying control.
        /// </summary>
        public abstract string Name { get; set; }

        /// <summary>
        /// Gets or sets the properties of the underlying control.
        /// </summary>
        public abstract string Properties { get; set; }
    }
}

﻿// <copyright file="IPageTemplateHolder.cs" company="Chris Crutchfield">
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

using PoshKentico.Core.Services.Development.PageTemplates;

namespace PoshKentico.Core.Providers.Development.PageTemplates
{
    /// <summary>
    /// Used by Dynamic Proxy to add a place to set a <see cref="IPageTemplate"/>.
    /// </summary>
    internal interface IPageTemplateHolder
    {
        /// <summary>
        /// Gets or sets a <see cref="IPageTemplate"/> associated with the current object.
        /// </summary>
        IPageTemplate PageTemplate { get; set; }
    }
}

// <copyright file="IPageTemplate.cs" company="Chris Crutchfield">
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.Development.PageTemplates
{
    /// <summary>
    /// Represents a Page Template.
    /// </summary>
    public interface IPageTemplate
    {
        /// <summary>
        /// Gets the ID for the page template category this paeg template belongs to.
        /// </summary>
        int CategoryID { get; }

        /// <summary>
        /// Gets the page template site ID.
        /// </summary>
        int PageTemplateSiteID { get; }

        /// <summary>
        /// Gets the ID for the current page template.
        /// </summary>
        int PageTemplateID { get; }

        /// <summary>
        /// Gets the display name for the current page template.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Gets the file name for the current page template.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Gets the page template description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the page template PageTemplates.
        /// </summary>
        string PageTemplates { get; }

        /// <summary>
        /// Gets the code name for the current page template.
        /// </summary>
        string CodeName { get; }

        /// <summary>
        /// Gets or sets the properties for the current page template.
        /// </summary>
        string PageTemplateProperties { get; set; }

        /// <summary>
        /// Gets page template layout.
        /// </summary>
        string PageTemplateLayout { get; }

        /// <summary>
        /// Gets page template icon class defining the page template thumbnail.
        /// </summary>
        string PageTemplateIconClass { get; }

        /// <summary>
        /// Gets page template CSS.
        /// </summary>
        string PageTemplateCSS { get; }

        /// <summary>
        /// Gets a value indicating whether the page template is reusable.
        /// </summary>
        bool IsReusable { get; }
    }
}

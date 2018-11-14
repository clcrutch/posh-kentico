// <copyright file="AddCMSPageTemplateWebPartZoneInstanceBusiness.cs" company="Chris Crutchfield">
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
using System.ComponentModel.Composition;
using System.Linq;
using PoshKentico.Core.Services.Development.PageTemplates;

namespace PoshKentico.Business.Development.PageTemplates
{
    /// <summary>
    /// Business layer fo the Add-CMSPageTemplateWebPartZoneInstance cmdlet.
    /// </summary>
    [Export(typeof(AddCMSPageTemplateWebPartZoneInstanceBusiness))]
    public class AddCMSPageTemplateWebPartZoneInstanceBusiness : PageTemplateBusinessBase
    {
        #region Methods

        /// <summary>
        /// Adds a field to the specified <see cref="IPageTemplate"/>.
        /// </summary>
        /// <param name="addFieldParameter">Holds the parameters used to create the field.</param>
        /// <param name="pageTemplate">The <see cref="IPageTemplate"/> to add the field to.</param>
        /// <returns>The <see cref="IPageTemplateWebPartInstance"/> that was added to the <see cref="IPageTemplate"/>.</returns>
        public IPageTemplateWebPartInstance AddWebPartInstance(AddWebPartInstanceParamter addFieldParameter, IPageTemplate pageTemplate)
        {
            //var dataType = typeof(FieldDataType).GetMember(addFieldParameter.ColumnType.ToString())
            //                .Single()
            //                .GetCustomAttributes(typeof(ValueAttribute), false)
            //                .Select(x => x as ValueAttribute)
            //                .Single()
            //                .Value;

            //var field = new Field
            //{
            //    AllowEmpty = !addFieldParameter.Required,
            //    Caption = addFieldParameter.Caption,
            //    DataType = dataType,
            //    DefaultValue = addFieldParameter.DefaultValue?.ToString(),
            //    Name = addFieldParameter.Name,
            //    Size = addFieldParameter.Size,
            //};

            //return this.PageTemplateService.AddField(field, pageTemplate);

            return default(IPageTemplateWebPartInstance);
        }

        #endregion

        #region Classes

        /// <summary>
        /// Represents the input from the cmdlet.
        /// </summary>
        public struct AddWebPartInstanceParamter
        {
            /// <summary>
            /// Gets or sets the caption for the field.
            /// </summary>
            public string Caption { get; set; }

            /// <summary>
            /// Gets or sets the default value for the field.
            /// </summary>
            public object DefaultValue { get; set; }

            /// <summary>
            /// Gets or sets the name for the field.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether the field is required.
            /// </summary>
            public bool Required { get; set; }

            /// <summary>
            /// Gets or sets the size of the field.
            /// </summary>
            public int Size { get; set; }
        }

        private class Field : IPageTemplateWebPartInstance
        {
            public bool AllowEmpty { get; set; }

            public string Caption { get; set; }

            public string DataType { get; set; }

            public string DefaultValue { get; set; }

            public string Name { get; set; }

            public int Size { get; set; }

            public IPageTemplate PageTemplate { get; set; }
        }

        #endregion

    }
}

// <copyright file="AddCMSWebPartFieldBusiness.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.Development.WebParts;

namespace PoshKentico.Business.Development.WebParts
{
    /// <summary>
    /// Business layer of the Add-CMSWebPartField cmdlet.
    /// </summary>
    [Export(typeof(AddCMSWebPartFieldBusiness))]
    public class AddCMSWebPartFieldBusiness : WebPartBusinessBase
    {
        #region Methods

        /// <summary>
        /// Adds a field to the specified <see cref="IWebPart"/>.
        /// </summary>
        /// <param name="addFieldParameter">Holds the parameters used to create the field.</param>
        /// <param name="webPart">The <see cref="IWebPart"/> to add the field to.</param>
        /// <returns>The <see cref="IWebPartField"/> that was added to the <see cref="IWebPart"/>.</returns>
        public IWebPartField AddField(AddFieldParameter addFieldParameter, IWebPart webPart)
        {
            var dataType = typeof(FieldDataType).GetMember(addFieldParameter.ColumnType.ToString())
                            .Single()
                            .GetCustomAttributes(typeof(ValueAttribute), false)
                            .Select(x => x as ValueAttribute)
                            .Single()
                            .Value;

            var field = new Field
            {
                AllowEmpty = !addFieldParameter.Required,
                Caption = addFieldParameter.Caption,
                DataType = dataType,
                DefaultValue = addFieldParameter.DefaultValue?.ToString(),
                Name = addFieldParameter.Name,
                Size = addFieldParameter.Size,
            };

            return this.WebPartService.AddField(field, webPart);
        }

        #endregion

        #region Classes

        /// <summary>
        /// Represents the input from the cmdlet.
        /// </summary>
        public struct AddFieldParameter
        {
            /// <summary>
            /// Gets or sets the caption for the field.
            /// </summary>
            public string Caption { get; set; }

            /// <summary>
            /// Gets or sets the column type for the field.
            /// </summary>
            public FieldDataType ColumnType { get; set; }

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

        private class Field : IWebPartField
        {
            public bool AllowEmpty { get; set; }

            public string Caption { get; set; }

            public string DataType { get; set; }

            public string DefaultValue { get; set; }

            public string Name { get; set; }

            public int Size { get; set; }

            public IWebPart WebPart { get; set; }
        }

        #endregion

    }
}

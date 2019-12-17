// <copyright file="Widget.cs" company="Chris Crutchfield">
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

using System.Linq;
using System.Xml.Linq;
using CMS.PortalEngine;

namespace PoshKentico.Core.Providers.Development.Widgets
{
    /// <summary>
    /// Wrapper around Kentico's <see cref="WidgetInfo"/>.
    /// </summary>
    public class Widget : Control<WidgetInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Widget"/> class.
        /// </summary>
        /// <param name="backingControl">The underlying <see cref="WidgetInfo"/> that will be wrapped.</param>
        public Widget(WidgetInfo backingControl)
            : base(backingControl)
        {
        }

        /// <inheritdoc />
        public override int CategoryID { get => this.BackingControl.WidgetCategoryID; set => this.BackingControl.WidgetCategoryID = value; }

        /// <inheritdoc />
        public override string DisplayName { get => this.BackingControl.WidgetDisplayName; set => this.BackingControl.WidgetDisplayName = value; }

        /// <inheritdoc />
        public override int ID { get => this.BackingControl.WidgetID; set => this.BackingControl.WidgetID = value; }

        /// <inheritdoc />
        public override string Name { get => this.BackingControl.WidgetName; set => this.BackingControl.WidgetName = value; }

        /// <inheritdoc />
        public override string Properties { get => this.MergeProperites(); set => this.BackingControl.WidgetProperties = value; }

        private string CoalesceEmptyString(string value, string @default) =>
            string.IsNullOrWhiteSpace(value) ? @default : value;

        private string MergeProperites()
        {
            var webPart = WebPartInfoProvider.GetWebPartInfo(this.BackingControl.WidgetWebPartID);

            var widgetDocument = XDocument.Parse(this.CoalesceEmptyString(this.BackingControl.WidgetProperties, "<form />"));
            var webPartDocument = XDocument.Parse(this.CoalesceEmptyString(webPart.WebPartProperties, "<form />"));

            this.MergeForms(webPartDocument, widgetDocument);

            var finalForm = XDocument.Parse("<form version=\"2\" />");

            var orderedElements = from e in webPartDocument.Element("form").Elements()
                                  orderby int.Parse(e.Attribute("order")?.Value ?? "0")
                                  select e;

            foreach (var element in orderedElements)
            {
                finalForm.Root.Add(element);
            }

            return finalForm.ToString();
        }

        private void MergeForms(XDocument form1, XDocument form2)
        {
            this.MergeFields(form1, form2);
            this.MergeCategories(form1, form2);
        }

        private void MergeFields(XDocument form1, XDocument form2)
        {
            foreach (var field in form1.Descendants("field"))
            {
                var matchedField = (from f in form2.Descendants("field")
                                    where f.Attribute("column").Value == field.Attribute("column").Value
                                    select f).SingleOrDefault();

                if (matchedField != null)
                {
                    this.MergeElement(field, matchedField);
                }
            }

            foreach (var field in form2.Descendants("field"))
            {
                form1.Root.Add(field);
            }
        }

        private void MergeCategories(XDocument form1, XDocument form2)
        {
            foreach (var category in form1.Descendants("category"))
            {
                var matchedCategory = (from f in form2.Descendants("category")
                                       where f.Attribute("name").Value == category.Attribute("name").Value
                                       select f).SingleOrDefault();

                if (matchedCategory != null)
                {
                    this.MergeElement(category, matchedCategory);
                }
            }

            foreach (var category in form2.Descendants("category"))
            {
                form1.Root.Add(category);
            }
        }

        private void MergeElement(XElement element1, XElement element2)
        {
            foreach (var attribute in element2.Attributes())
            {
                if (element1.Attributes(attribute.Name).Any())
                {
                    element1.Attribute(attribute.Name).Value = attribute.Value;
                }
                else
                {
                    element1.SetAttributeValue(attribute.Name, attribute.Value);
                }
            }

            this.AddOrMergeSubElement(element1, element1.Element("properties"), element2.Element("properties"));
            this.AddOrMergeSubElement(element1, element1.Element("settings"), element2.Element("settings"));

            element2.Remove();
        }

        private void AddOrMergeSubElement(XElement element1Parent, XElement element1, XElement element2, bool merge = true)
        {
            if (element2 != null)
            {
                if (element1 != null)
                {
                    if (merge)
                    {
                        this.MergeSubElement(element1, element2);
                    }
                }
                else
                {
                    element1Parent.Add(element2);
                }
            }
        }

        private void MergeSubElement(XElement element1, XElement element2)
        {
            if (element1 == null || element2 == null)
            {
                return;
            }

            foreach (var element in element2.Elements())
            {
                if (element1.Elements(element.Name).Any())
                {
                    element1.Element(element.Name).ReplaceWith(element);

                    continue;
                }

                element1.Add(element);
            }
        }
    }
}

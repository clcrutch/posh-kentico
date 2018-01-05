// <copyright file="RemoveCMSWebPartCategoryCmdlet.cs" company="Chris Crutchfield">
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

using System.ComponentModel.Composition;
using System.Management.Automation;
using CMS.PortalEngine;
using ImpromptuInterface;
using PoshKentico.Business.Development;
using PoshKentico.Core.Services.Development;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development
{
    [Cmdlet(VerbsCommon.Remove, "CMSWebPartCategory", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public class RemoveCMSWebPartCategoryCmdlet : MefCmdlet
    {
        #region Constants

        private const string CATEGORYNAME = "Category Name";
        private const string IDSETNAME = "ID";
        private const string WEBPARTCATEGORY = "Web Part Category";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The category name, display name, or path of the webpart category.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = CATEGORYNAME)]
        [Alias("DisplayName", "Name", "Path")]
        public string CategoryName { get; set; }

        /// <summary>
        /// <para type="description">If set, the match is exact,</para>
        /// <para type="description">else the match performs a contains for display name and category name and starts with for path.</para>
        /// </summary>
        [Parameter(ParameterSetName = CATEGORYNAME)]
        public SwitchParameter Exact { get; set; }

        /// <summary>
        /// <para type="description">The IDs of the web part category to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = IDSETNAME)]
        public int[] ID { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = WEBPARTCATEGORY)]
        public WebPartCategoryInfo WebPartCategory { get; set; }

        [Import]
        public RemoveCMSWebPartCategoryBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case CATEGORYNAME:
                    this.BusinessLayer.DeleteWebPartCategory(this.CategoryName, this.Exact.ToBool());
                    break;
                case IDSETNAME:
                    this.BusinessLayer.DeleteWebPartCategory(this.ID);
                    break;
                case WEBPARTCATEGORY:
                    this.BusinessLayer.DeleteWebPartCategory(this.WebPartCategory.ActLike<IWebPartCategory>());
                    break;
            }
        }

        #endregion

    }
}

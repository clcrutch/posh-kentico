// <copyright file="NewCMSWebPartCategoryCmdlet.cs" company="Chris Crutchfield">
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
using ImpromptuInterface;
using PoshKentico.Business.Development;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development
{
    /// <summary>
    /// <para type="synopsis">Creates a new web part category.</para>
    /// <para type="description">Creates a new web part category based off of the provided input.</para>
    /// <para type="description">This cmdlet returns the newly created web part category.</para>
    /// <example>
    ///     <para>Create a new web part category implying the display name.</para>
    ///     <code>New-CMSWebPartCategory -Path /Test/Test1</code>
    /// </example>
    /// <example>
    ///     <para>Create a new web part category specifying the display name.</para>
    ///     <code>New-CMSWebPartCategory -Path /Test/Test1 -DisplayName "My Test Category"</code>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.New, "CMSWebPartCategory")]
    [Alias("nwpc")]
    public class NewCMSWebPartCategoryCmdlet : MefCmdlet
    {
        #region Properties

        /// <summary>
        /// <para type="description">The display name for the newly created web part category.</para>
        /// <para type="description">If null, then the name portion of the path is used for the display name.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 1)]
        public string DisplayName { get; set; }

        /// <summary>
        /// <para type="description">The path to create the new web part category at.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0)]
        public string Path { get; set; }

        /// <summary>
        /// <para type="description">The path for the icon for the newly created web part category.</para>
        /// </summary>
        [Parameter]
        [Alias("IconPath")]
        public string ImagePath { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this web part.  Populated by MEF.
        /// </summary>
        [Import]
        public NewCMSWebPartCategoryBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            this.WriteObject(this.BusinessLayer.CreateWebPart(this.Path, this.DisplayName, this.ImagePath).UndoActLike());
        }

        #endregion

    }
}

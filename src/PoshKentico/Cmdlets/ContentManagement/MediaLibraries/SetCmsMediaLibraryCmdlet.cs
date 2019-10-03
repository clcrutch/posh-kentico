// <copyright file="SetCmsMediaLibraryCmdlet.cs" company="Chris Crutchfield">
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
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using CMS.MediaLibrary;
using ImpromptuInterface;
using PoshKentico.Business.ContentManagement.MediaLibraries;
using PoshKentico.Core.Services.ContentManagement.MediaLibraries;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.ContentManagement.MediaLibraries
{
    /// <summary>
    /// <para type="synopsis">Sets a library.</para>
    /// <para type="description">Sets a library based off of the provided input.</para>
    /// <para type="description">This cmdlet returns the updated library when the -PassThru switch is used.</para>
    /// <example>
    ///     <para>Set library specifying an existing library.</para>
    ///     <code>Set-CMSMediaLibrary -MediaLibrary $library</code>
    /// </example>
    /// <example>
    ///     <para>Set library specifying an existing library.</para>
    ///     <code>$library | Set-CMSMediaLibrary</code>
    /// </example>
    /// <example>
    ///     <para>Get library specifying the SiteID and DisplayName, set its LibraryName, Description and Folder.</para>
    ///     <code>Set-CMSMediaLibrary -SiteID 1 -LibraryName "Name" -DisplayName "My Test Name" -Description "Library description" -Folder "Images"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Set, "CMSMediaLibrary")]
    [OutputType(typeof(MediaLibraryInfo))]
    [Alias("smlib")]
    public class SetCmsMediaLibraryCmdlet : MefCmdlet<SetCmsMediaLibraryBusiness>
    {
        #region Constants

        private const string OBJECTSET = "Object";
        private const string PROPERTYSET = "Property";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">A reference to the updated library.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = OBJECTSET)]
        public MediaLibraryInfo LibraryToSet { get; set; }

        /// <summary>
        /// <para type="description">The site id for the updated library.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PROPERTYSET)]
        public int SiteID { get; set; }

        /// <summary>
        /// <para type="description">The library name for the updated library.</para>
        /// <para type="description">The library name cannot be blank.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = PROPERTYSET)]
        public string LibraryName { get; set; }

        /// <summary>
        /// <para type="description">The display name for the updated library.</para>
        /// <para type="description">The Media Library display name cannot be blank.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 2, ParameterSetName = PROPERTYSET)]
        public string DisplayName { get; set; }

        /// <summary>
        /// <para type="description">The library description for the updated library.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 3, ParameterSetName = PROPERTYSET)]
        public string Description { get; set; }

        /// <summary>
        /// <para type="description">The library folder for the updated library.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 4, ParameterSetName = PROPERTYSET)]
        public string Folder { get; set; }

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the updated library.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = OBJECTSET)]
        [Parameter(Mandatory = false, ParameterSetName = PROPERTYSET)]
        public SwitchParameter PassThru { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IMediaLibrary updatedLibrary = null;
            switch (this.ParameterSetName)
            {
                case OBJECTSET:
                    updatedLibrary = this.BusinessLayer.Set(this.LibraryToSet.ActLike<IMediaLibrary>());
                    break;
                case PROPERTYSET:
                    updatedLibrary = this.BusinessLayer.Set(this.SiteID, this.LibraryName, this.DisplayName, this.Description, this.Folder);
                    break;
            }

            if (this.PassThru.ToBool())
            {
                this.WriteObject(updatedLibrary.UndoActLike());
            }
        }

        #endregion
    }
}

// <copyright file="NewCmsMediaLibraryCmdlet.cs" company="Chris Crutchfield">
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
using CMS.SiteProvider;
using ImpromptuInterface;
using PoshKentico.Business.ContentManagement.MediaLibraries;

namespace PoshKentico.Cmdlets.ContentManagement.MediaLibraries
{
    /// <summary>
    /// <para type="synopsis">Creates a new library.</para>
    /// <para type="description">Creates a new library based off of the provided input.</para>
    /// <para type="description">This cmdlet returns the newly created library when the -PassThru switch is used.</para>
    /// <example>
    ///     <para>Create a new library specifying the associated site id, display name, library name, description, and folder name.</para>
    ///     <code>New-CMSMediaLibrary -SiteID 1 -DisplayName "My Test Name" -LibraryName "Name" -Description "Library description" -Folder "Images"</code>
    /// </example>
    /// <example>
    ///     <para>Create a new library specifying the associated site $site, display name, library name, description, and folder name.</para>
    ///     <code>$site | New-CMSMediaLibrary -DisplayName "My Test Name" -LibraryName "Name" -Description "Library description" -Folder "Images"</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.New, "CMSMediaLibrary")]
    [OutputType(typeof(MediaLibraryInfo))]
    public class NewCmsMediaLibraryCmdlet : MefCmdlet
    {
        #region Constants

        private const string OBJECTSET = "Object";
        private const string PROPERTYSET = "Property";

        #endregion

        #region Properties

        /// <summary>
        /// <para type="description">The associalted site for the newly created library.</para>
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, ParameterSetName = OBJECTSET)]
        public SiteInfo Site { get; set; }

        /// <summary>
        /// <para type="description">The library site id for the library to update.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = PROPERTYSET)]
        public int SiteID { get; set; }

        /// <summary>
        /// <para type="description">The display name for the newly created library.</para>
        /// <para type="description">The Media Library display name cannot be blank.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = PROPERTYSET)]
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = OBJECTSET)]
        public string DisplayName { get; set; }

        /// <summary>
        /// <para type="description">The library name for the newly created library.</para>
        /// <para type="description">The library name cannot be blank.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = PROPERTYSET)]
        [Parameter(Mandatory = true, Position = 2, ParameterSetName = OBJECTSET)]
        public string LibraryName { get; set; }

        /// <summary>
        /// <para type="description">The library description for the newly created library.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 3, ParameterSetName = PROPERTYSET)]
        [Parameter(Mandatory = false, Position = 3, ParameterSetName = OBJECTSET)]
        public string Description { get; set; }

        /// <summary>
        /// <para type="description">The library folder for the newly created library.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 4, ParameterSetName = PROPERTYSET)]
        [Parameter(Mandatory = true, Position = 4, ParameterSetName = OBJECTSET)]
        public string Folder { get; set; }

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the newly created library.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = OBJECTSET)]
        [Parameter(Mandatory = false, ParameterSetName = PROPERTYSET)]
        public SwitchParameter PassThru { get; set; }

        /// <summary>
        ///  Gets or sets the Business Layer for this library.  Populated by MEF.
        /// </summary>
        [Import]
        public NewCmsMediaLibraryBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            int siteID = this.ParameterSetName == PROPERTYSET ? this.SiteID : this.Site.SiteID;

            var newMediaLibrary = this.BusinessLayer.CreateMediaLibrary(
                                        this.DisplayName,
                                        this.LibraryName,
                                        this.Description,
                                        this.Folder,
                                        siteID);

            if (this.PassThru.ToBool())
            {
                this.WriteObject(newMediaLibrary.UndoActLike());
            }
        }

        #endregion
    }
}

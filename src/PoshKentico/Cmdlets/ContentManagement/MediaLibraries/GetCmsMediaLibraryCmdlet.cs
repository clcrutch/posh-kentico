// <copyright file="GetCmsMediaLibraryCmdlet.cs" company="Chris Crutchfield">
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

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using CMS.MediaLibrary;
using CMS.SiteProvider;
using ImpromptuInterface;
using PoshKentico.Business.ContentManagement.MediaLibraries;
using PoshKentico.Core.Services.ContentManagement.MediaLibraries;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.ContentManagement.MediaLibraries
{
    /// <summary>
    /// <para type="synopsis">Gets the media libraries by the provided input.</para>
    /// <para type="description">Gets the media libraries by the provided input. </para>
    /// <para type="description">Without parameters, this command returns all libraries.</para>
    /// <para type="description">With parameters, this command returns the libraries that match the criteria.</para>
    /// <example>
    ///     <para>Get all the libraries.</para>
    ///     <code>Get-CMSMediaLibrary</code>
    /// </example>
    /// <example>
    ///     <para>Get all libraries with a site ID 1 and library name "*tes*".</para>
    ///     <code>Get-CMSMediaLibrary -SiteID 1 -LibraryName bas</code>
    /// </example>
    /// <example>
    ///     <para>Get all libraries with a site ID 1 and library name "test"</para>
    ///     <code>Get-CMSMediaLibrary -SiteID 1 -LibraryName "test" -Exact</code>
    /// </example>
    /// <example>
    ///     <para>Get all libraries with a site $site and library name "*tes*".</para>
    ///     <code>$site | Get-CMSMediaLibrary -LibraryName bas</code>
    /// </example>
    /// <example>
    ///     <para>Get all libraries with a site $site and library name "test"</para>
    ///     <code>$site | Get-CMSMediaLibrary -LibraryName "test" -Exact</code>
    /// </example>
    /// <example>
    ///     <para>Get all the libraries with the specified IDs.</para>
    ///     <code>Get-CMSMediaLibrary -ID 1,3</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Get, "CMSMediaLibrary", DefaultParameterSetName = NONE)]
    [OutputType(typeof(MediaLibraryInfo[]))]
    [Alias("gmlib")]
    public class GetCmsMediaLibraryCmdlet : MefCmdlet
    {
        #region Constants

        private const string NONE = "None";
        private const string OBJECTSET = "Object";
        private const string LIBRARYNAME = "Library Name";
        private const string IDSETNAME = "ID";

        #endregion
        #region Properties

        /// <summary>
        /// <para type="description">The associalted site for the server to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, ParameterSetName = OBJECTSET)]
        public SiteInfo Site { get; set; }

        /// <summary>
        /// <para type="description">The site name of the library to retrive.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = LIBRARYNAME)]
        public int SiteID { get; set; }

        /// <summary>
        /// <para type="description">The library name of the library to retrive.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = LIBRARYNAME)]
        public string LibraryName { get; set; }

        /// <summary>
        /// <para type="description">If set, the match is exact,else the match performs a contains for library name and category name and starts with for path.</para>
        /// </summary>
        [Parameter(ParameterSetName = LIBRARYNAME)]
        public SwitchParameter Exact { get; set; }

        /// <summary>
        /// <para type="description">The IDs of the library to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = IDSETNAME)]
        public int[] ID { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this media library service. Populated by MEF.
        /// </summary>
        [Import]
        public GetCmsMediaLibraryBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            IEnumerable<IMediaLibrary> libraries = null;

            switch (this.ParameterSetName)
            {
                case OBJECTSET:
                    libraries = this.BusinessLayer.GetMediaLibraries(this.Site.SiteID, this.LibraryName, this.Exact.ToBool());
                    break;
                case LIBRARYNAME:
                    libraries = this.BusinessLayer.GetMediaLibraries(this.SiteID, this.LibraryName, this.Exact.ToBool());
                    break;
                case IDSETNAME:
                    libraries = this.BusinessLayer.GetMediaLibraries(this.ID);
                    break;
                case NONE:
                    libraries = this.BusinessLayer.GetMediaLibraries();
                    break;
            }

            foreach (var library in libraries)
            {
                this.ActOnObject(library);
            }
        }

        /// <summary>
        /// When overridden in a child class, operates on the specified action.
        /// </summary>
        /// <param name="library">The media library to operate on.</param>
        protected virtual void ActOnObject(IMediaLibrary library)
        {
            this.WriteObject(library.UndoActLike());
        }
        #endregion
    }
}

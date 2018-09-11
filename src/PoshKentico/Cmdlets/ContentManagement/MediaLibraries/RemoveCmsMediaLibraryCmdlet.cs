// <copyright file="RemoveCmsMediaLibraryCmdlet.cs" company="Chris Crutchfield">
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
using PoshKentico.Core.Services.ContentManagement.MediaLibraries;

namespace PoshKentico.Cmdlets.ContentManagement.MediaLibraries
{
    /// <summary>
    /// <para type="synopsis">Deletes a library.</para>
    /// <para type="description">Deletes a library.</para>
    /// <example>
    ///     <para>Deletes all libraries with a site ID 1 and display name "*tes*".</para>
    ///     <code>Remove-CMSMediaLibrary -SiteID 1 -DisplayName tes</code>
    /// </example>
    /// <example>
    ///     <para>Deletes all libraries with a site ID 1 and display name "test"</para>
    ///     <code>Remove-CMSMediaLibrary -SiteID 1 -DisplayName test -Exact</code>
    /// </example>
    /// <example>
    ///     <para>Deletes a library.</para>
    ///     <code>$library | Remove-CMSMediaLibrary</code>
    /// </example>
    /// <example>
    ///     <para>Delete all the libraries with the specified IDs.</para>
    ///     <code>Remove-CMSMediaLibrary -ID 1,2,3</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSMediaLibrary", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public class RemoveCmsMediaLibraryCmdlet : MefCmdlet
    {
        #region Constants

        private const string OBJECTSET = "ObjectSet";
        private const string DISPLAYNAME = "Dislpay Name";
        private const string IDSETNAME = "ID";

        #endregion
        #region Properties

        /// <summary>
        /// <para type="description">The associalted site for the library to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = false, ValueFromPipeline = true, Position = 0, ParameterSetName = OBJECTSET)]
        public MediaLibraryInfo Library { get; set; }

        /// <summary>
        /// <para type="description">The site name of the library to retrive.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ValueFromPipeline = true, ParameterSetName = DISPLAYNAME)]
        public int SiteID { get; set; }

        /// <summary>
        /// <para type="description">The display name of the library to retrive.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 1, ParameterSetName = DISPLAYNAME)]
        public string DisplayName { get; set; }

        /// <summary>
        /// <para type="description">If set, the match is exact,else the match performs a contains for display name and category name and starts with for path.</para>
        /// </summary>
        [Parameter(ParameterSetName = DISPLAYNAME)]
        public SwitchParameter Exact { get; set; }

        /// <summary>
        /// <para type="description">The IDs of the library to retrieve.</para>
        /// </summary>
        [Parameter(Mandatory = false, Position = 0, ParameterSetName = IDSETNAME)]
        public int[] ID { get; set; }

        /// <summary>
        /// Gets or sets the Business layer for this media library service. Populated by MEF.
        /// </summary>
        [Import]
        public RemoveCmsMediaLibraryBusiness BusinessLayer { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case DISPLAYNAME:
                    this.BusinessLayer.Remove(this.SiteID, this.DisplayName, this.Exact.ToBool());
                    break;
                case OBJECTSET:
                    this.BusinessLayer.Remove(this.Library.ActLike<IMediaLibrary>());
                    break;
                case IDSETNAME:
                    this.BusinessLayer.Remove(this.ID);
                    break;
            }

            #endregion
        }
    }
}

// <copyright file="SetCmsMediaLibraryFileCmdlet.cs" company="Chris Crutchfield">
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

using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;
using CMS.MediaLibrary;

namespace PoshKentico.Cmdlets.ContentManagement.MediaLibraries
{
    /// <summary>
    /// <para type="synopsis">Sets a media file.</para>
    /// <para type="description">Sets a media file based off of the provided input.</para>
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
    [OutputType(typeof(MediaFileInfo))]
    public class SetCmsMediaLibraryFileCmdlet : MefCmdlet
    {
    }
}

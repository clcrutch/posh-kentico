﻿// <copyright file="GetCMSWebPartCmdlet.cs" company="Chris Crutchfield">
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
using CMS.PortalEngine;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Providers.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.WebParts
{
    /// <summary>
    /// <para type="synopsis">Gets the web parts selected by the provided input.</para>
    /// <para type="description">Gets the web parts selected by the provided input.</para>
    /// <para type="description">This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">Without parameters, this command returns all webparts.</para>
    /// <para type="description">With parameters, this command returns the webparts that match the criteria.</para>
    /// <example>
    ///     <para>Get all the webparts.</para>
    ///     <code>Get-CMSWebPart</code>
    /// </example>
    /// <example>
    ///     <para>Get web parts by category.</para>
    ///     <code>Get-CMSWebPartCategory | Get-CMSWebPart</code>
    /// </example>
    /// <example>
    ///     <para>Get web parts by category name.</para>
    ///     <code>Get-CMSWebPart -Category *test*</code>
    /// </example>
    /// <example>
    ///     <para>Get web parts by name.</para>
    ///     <code>Get-CMSWebPart -WebPartName *webpartname*</code>
    /// </example>
    /// <example>
    ///     <para>Get web parts by path</para>
    ///     <code>Get-CMSWebPart -Path /path/to/webpart</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Get, "CMSWebPart", DefaultParameterSetName = NONE)]
    [OutputType(typeof(WebPartInfo[]))]
    [Alias("gwp")]
    public class GetCMSWebPartCmdlet : GetCMSControlCmdlet<GetCMSWebPartBusiness, GetCMSWebPartCategoryBusiness, IWebPartService, WebPartInfo, WebPartCategoryInfo, WebPartCategory>
    {
    }
}

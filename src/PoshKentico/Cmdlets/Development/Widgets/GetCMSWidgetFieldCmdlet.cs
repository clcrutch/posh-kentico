// <copyright file="GetCMSWebPartFieldCmdlet.cs" company="Chris Crutchfield">
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
using CMS.FormEngine;
using CMS.PortalEngine;
using PoshKentico.Business.Development.Widgets;
using PoshKentico.Core.Providers.Development.Widgets;
using PoshKentico.Core.Services.Development.Widgets;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.Widgets
{
    /// <summary>
    /// <para type="synopsis">Gets the web part fields selected by the provided input.</para>
    /// <para type="description">Gets the web part fields selected by the provided input.</para>
    /// <para type="description">This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <example>
    ///     <para>Gets all the web part fields associated with a web part.</para>
    ///     <code>$webPart | Get-CMSWebPartField</code>
    /// </example>
    /// <example>
    ///     <para>Gets the web part fields associated with a web part that match the specified name.</para>
    ///     <code>$webPart | Get-CMSWebPartField -Name Test*</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Get, "CMSWidgetField", DefaultParameterSetName = NONAME)]
    [OutputType(typeof(FormFieldInfo[]))]
    [Alias("gwf")]
    public class GetCMSWidgetFieldCmdlet : GetCMSControlFieldCmdlet<GetCMSWidgetFieldBusiness, Widget, IWidgetService, WidgetInfo, WidgetCategoryInfo>
    {
    }
}

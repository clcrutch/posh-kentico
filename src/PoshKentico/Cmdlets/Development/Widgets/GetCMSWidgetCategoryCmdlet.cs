// <copyright file="GetCMSWebPartCategoryCmdlet.cs" company="Chris Crutchfield">
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
using PoshKentico.Business.Development.Widgets;
using PoshKentico.Core.Providers.Development.Widgets;
using PoshKentico.Core.Services.Development.Widgets;
using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.Widgets
{
    /// <summary>
    /// <para type="synopsis">Gets the web part categories selected by the provided input.</para>
    /// <para type="description">Gets the web part categories selected by the provided input.  This command automatically initializes the connection to Kentico if not already initialized.</para>
    /// <para type="description"></para>
    /// <para type="description">Without parameters, this command returns all webpart categories.</para>
    /// <para type="description">With parameters, this command returns the webpart categories that match the criteria.</para>
    /// <example>
    ///     <para>Get all the widget categories.</para>
    ///     <code>Get-CMSWebPartCategory</code>
    /// </example>
    /// <example>
    ///     <para>Get all web part categories with a category name "*bas*", display name "*bas*".</para>
    ///     <code>Get-CMSWebPartCategory *bas*</code>
    /// </example>
    /// <example>
    ///     <para>Get all web part categories with a category name "basic", display name "basic"</para>
    ///     <code>Get-CMSWebPartCategory basic</code>
    /// </example>
    /// <example>
    ///     <para>Get all the web part categories with the specified IDs.</para>
    ///     <code>Get-CMSWebPartCategory -ID 5,304,5</code>
    /// </example>
    /// <example>
    ///     <para>Get all the web part categories under the basic category.</para>
    ///     <code>Get-CMSWebPartCategory basic -Recurse</code>
    /// </example>
    /// <example>
    ///     <para>Get the web part category associated with the web part.</para>
    ///     <code>$webPart | Get-WebPartCategory</code>
    /// </example>
    /// <example>
    ///     <para>Get the web part categories under a parent category.</para>
    ///     <code>$widgetCategory | Get-WidgetCategory</code>
    /// </example>
    /// </summary>
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Get, "CMSWidgetCategory", DefaultParameterSetName = NONE)]
    [OutputType(typeof(WidgetCategoryInfo[]))]
    [Alias("gwc")]
    public class GetCMSWidgetCategoryCmdlet : GetCMSControlCategoryCmdlet<GetCMSWidgetCategoryBusiness, IWidgetService, WidgetInfo, WidgetCategoryInfo, Widget, WidgetCategory>
    {
    }
}

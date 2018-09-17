// <copyright file="ResourceProvider.cs" company="Chris Crutchfield">
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

using System.Management.Automation;
using System.Management.Automation.Provider;
using CMS.Base;
using PoshKentico.Business.Resource;
using PoshKentico.Core.Services.Resource;

namespace PoshKentico.CmdletProviders.Resource
{
    [OutputType(typeof(ResourceItem), ProviderCmdlet = ProviderCmdlet.GetItem)]
    [CmdletProvider("KenticoResourceProvider", ProviderCapabilities.ExpandWildcards)]
    public class ResourceProvider : MefCmdletProvider<KenticoFileSystemBusines>
    {
        protected sealed override string ProviderName { get => "KenticoResourceProvider"; }
        protected sealed override string DriveName { get => "Kenti"; }
        protected sealed override string DriveRootPath { get => SystemContext.WebApplicationPhysicalPath; }
        protected sealed override string DriveDescription { get => "Kentico resource provider."; }
    }
}

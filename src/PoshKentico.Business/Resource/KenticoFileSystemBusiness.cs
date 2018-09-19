// <copyright file="KenticoFileSystemBusines.cs" company="Chris Crutchfield">
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PoshKentico.Core.Services.Resource;

namespace PoshKentico.Business.Resource
{
    /// <summary>
    /// Business layer for NavigationCmdletProvider
    /// </summary>
    [Export(typeof(KenticoFileSystemBusiness))]
    public class KenticoFileSystemBusiness : CmdletProviderBusinessBase
    {
        /// <inheritdoc />
        [Import(typeof(IFileSystemResourceService))]
        public override IResourceService ResourceService { get; set; }

        /// <inheritdoc />
        [Import(typeof(IFileSystemReaderWriterService))]
        public override IResourceReaderWriterService ReaderWriterService { get; set; }
    }
}

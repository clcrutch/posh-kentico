// <copyright file="NewCMSWebPartCmdlet.cs" company="Chris Crutchfield">
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

using CMS.PortalEngine;
using ImpromptuInterface;
using PoshKentico.Business.Development.WebParts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.WebParts
{
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.New, "CMSWebPart")]
    [OutputType(typeof(WebPartInfo[]))]
    [Alias("nwp")]
    public class NewCMSWebPartCmdlet : MefCmdlet
    {
        [Parameter(Position = 2)]
        public string DisplayName { get; set; }

        [Parameter(Mandatory = true, Position = 1)]
        public string FileName { get; set; }

        [Parameter(Mandatory = true, Position = 0)]
        public string Path { get; set; }

        [Parameter]
        public SwitchParameter PassThru { get; set; }

        [Import]
        public NewCMSWebPartBusiness BusinessLayer { get; set; }

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            var webPart = this.BusinessLayer.CreateWebPart(this.Path, this.FileName, this.DisplayName);

            if (this.PassThru.ToBool())
            {
                this.WriteObject(webPart.UndoActLike());
            }
        }
    }
}

// <copyright file="NewCMSWebPartCategoryCmdlet.cs" company="Chris Crutchfield">
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
using System.Management.Automation;
using ImpromptuInterface;
using PoshKentico.Business.Development;
using PoshKentico.Core.Services.Development;

namespace PoshKentico.Development
{
    [Cmdlet(VerbsCommon.New, "CMSWebPartCategory")]
    public class NewCMSWebPartCategoryCmdlet : MefCmdlet
    {
        [Parameter(Mandatory = false, Position = 1)]
        public string DisplayName { get; set; }

        [Parameter(Mandatory = true, Position = 0)]
        public string Path { get; set; }

        [Parameter]
        public string ImagePath { get; set; }

        [Import]
        public NewCMSWebPartCategoryBusiness BusinessLayer { get; set; }

        protected override void ProcessRecord()
        {
            this.WriteObject(this.BusinessLayer.CreateWebPart(this.Path, this.DisplayName, this.ImagePath).UndoActLike());
        }
    }
}

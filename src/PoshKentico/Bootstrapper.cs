// <copyright file="Bootstrapper.cs" company="Chris Crutchfield">
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
using System.ComponentModel.Composition;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PoshKentico.Business;
using PoshKentico.Core.Providers.General;
using PoshKentico.Core.Services.General;
using PoshKentico.Extensions;

namespace PoshKentico
{
    /// <summary>
    /// A singleton used for initialization.
    /// </summary>
    public sealed class Bootstrapper
    {
#pragma warning disable SA1311 // Static readonly fields should begin with upper-case letter
        /// <summary>
        /// The instance.
        /// </summary>
        private static readonly Bootstrapper instance = new Bootstrapper();
#pragma warning restore SA1311 // Static readonly fields should begin with upper-case letter

        private bool bootstrapperInitialized = false;

        private Bootstrapper()
        {
        }

        /// <summary>
        /// The instance.
        /// </summary>
        public static Bootstrapper Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>
        /// Gets or sets the output service.
        /// </summary>
        [Import]
        public IOutputService OutputService { get; set; }

        /// <summary>
        /// Cmdlet initialization logic.
        /// </summary>
        /// <param name="cmdlet">The. </param>
        public void Initialize(ICmdlet cmdlet)
        {
            MefHost.Initialize();

            if (this.bootstrapperInitialized)
            {
                MefHost.Container.ComposeParts(this);

                this.bootstrapperInitialized = true;
            }

            MefHost.Container.ComposeParts(cmdlet);

            var businessLayerProps = (from p in cmdlet.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                      where p.PropertyType.InheritsFrom(typeof(CmdletBusinessBase))
                                      select p).ToArray();

            PassThruOutputService.ShouldProcessFunction = cmdlet.ShouldProcess;

            PassThruOutputService.WriteDebugAction = cmdlet.WriteDebug;
            PassThruOutputService.WriteErrorAction = cmdlet.WriteError;
            PassThruOutputService.WriteProgressAction = cmdlet.WriteProgress;
            PassThruOutputService.WriteVerboseAction = cmdlet.WriteVerbose;
            PassThruOutputService.WriteWarningAction = cmdlet.WriteWarning;

            foreach (var prop in businessLayerProps)
            {
                var instance = (CmdletBusinessBase)prop.GetValue(cmdlet);

                instance.Initialize();
            }
        }
    }
}

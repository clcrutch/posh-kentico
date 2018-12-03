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

using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using PoshKentico.Business;
using PoshKentico.Core.Providers.General;
using PoshKentico.Extensions;

namespace PoshKentico
{
    /// <summary>
    /// A singleton used for initialization.
    /// </summary>
    public sealed class Bootstrapper
    {
        #region Variables

#pragma warning disable SA1311 // Static readonly fields should begin with upper-case letter
        /// <summary>
        /// The instance.
        /// </summary>
        private static readonly Bootstrapper instance = new Bootstrapper();
#pragma warning restore SA1311 // Static readonly fields should begin with upper-case letter

        #endregion

        #region Constructors

        private Bootstrapper()
        {
        }

        #endregion

        #region Properties

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

        #endregion

        #region Methods

        /// <summary>
        /// Cmdlet initialization logic.
        /// </summary>
        /// <param name="cmdlet">The. </param>
        public void Initialize(ICmdlet cmdlet)
        {
            MefHost.Initialize();

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

        #endregion

    }
}

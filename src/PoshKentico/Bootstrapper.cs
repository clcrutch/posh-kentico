using PoshKentico.Business;
using PoshKentico.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Provider;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico
{
    public sealed class Bootstrapper
    {
        private static readonly Bootstrapper instance = new Bootstrapper();

        static Bootstrapper() { }

        private Bootstrapper() { }

        public static Bootstrapper Instance
        {
            get
            {
                return instance;
            }
        }

        public void Initialize(ICmdlet cmdlet)
        {
            MefHost.Initialize();

            MefHost.Container.ComposeParts(cmdlet);

            var businessLayerProps = (from p in cmdlet.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                      where p.PropertyType.InheritsFrom(typeof(CmdletBusinessBase))
                                      select p).ToArray();

            foreach (var prop in businessLayerProps)
            {
                var instance = (CmdletBusinessBase)prop.GetValue(cmdlet);
                instance.WriteDebug = cmdlet.WriteDebug;
                instance.WriteVerbose = cmdlet.WriteVerbose;
                instance.ShouldProcess = cmdlet.ShouldProcess;

                instance.Initialize();
            }
        }
    }
}

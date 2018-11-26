using PoshKentico.Core.Services.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Providers.General
{
    [Export(typeof(IOutputService))]
    public class PassThruOutputService : IOutputService
    {
        public static Func<string, string, bool> ShouldProcessFunction { private get; set; }

        public static Action<string> WriteDebugAction { private get; set; }

        public static Action<string> WriteVerboseAction { private get; set; }

        public bool ShouldProcess(string target, string action) =>
            ShouldProcessFunction?.Invoke(target, action) ?? false;

        public void WriteDebug(string message) =>
            WriteDebugAction?.Invoke(message);

        public void WriteVerbose(string message) =>
            WriteVerboseAction?.Invoke(message);
    }
}

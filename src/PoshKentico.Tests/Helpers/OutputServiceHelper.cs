using NUnit.Framework;
using PoshKentico.Core.Providers.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Tests.Helpers
{
    internal static class OutputServiceHelper
    {
        public static PassThruOutputService GetPassThruOutputService()
        {
            PassThruOutputService.WriteDebugAction = Assert.NotNull;
            PassThruOutputService.WriteVerboseAction = Assert.NotNull;

            return new PassThruOutputService();
        }
    }
}

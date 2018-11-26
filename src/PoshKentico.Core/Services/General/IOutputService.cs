using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.General
{
    public interface IOutputService
    {
        bool ShouldProcess(string target, string action);

        void WriteDebug(string message);

        void WriteVerbose(string message);
    }
}

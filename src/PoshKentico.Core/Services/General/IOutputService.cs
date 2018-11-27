using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.General
{
    public interface IOutputService
    {
        bool ShouldProcess(string target, string action);

        void WriteError(ErrorRecord errorRecord);

        void WriteDebug(string message);

        void WriteProgress(ProgressRecord progressRecord);

        void WriteVerbose(string message);

        void WriteWarning(string message);
    }
}

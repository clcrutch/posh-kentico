using PoshKentico.Core.Services.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico
{
    public interface ICmdlet
    {
        void WriteDebug(string text);
        void WriteVerbose(string text);
        bool ShouldProcess(string target, string action);
    }
}

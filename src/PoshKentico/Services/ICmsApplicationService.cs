using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Services
{
    public interface ICmsApplicationService
    {
        (DirectoryInfo siteLocation, string connectionString) FindSite(Action<string> writeDebug = null, Action<string> writeVerbose = null);

        void Initialize(Action<string> writeDebug = null, Action<string> writeVerbose = null);

        void Initialize(string connectionString, DirectoryInfo directoryInfo, Action<string> writeDebug = null, Action<string> writeVerbose = null);
    }
}

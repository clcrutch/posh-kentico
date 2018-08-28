using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.Development.WebParts
{
    public interface IField
    {
        bool AllowEmpty { get; }

        string DataType { get; }

        string DefaultValue { get; }

        string Name { get; }

        int Size { get; }
    }
}

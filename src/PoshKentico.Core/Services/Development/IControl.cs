using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.Development
{
    public interface IControl<T>
    {
        T BackingControl { get; }

        int CategoryID { get; }

        string DisplayName { get; }

        int ID { get; }

        string Name { get; }
    }
}

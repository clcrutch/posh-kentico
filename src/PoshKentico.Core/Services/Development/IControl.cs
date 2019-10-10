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

        int CategoryID { get; set; }

        string DisplayName { get; set; }

        int ID { get; set; }

        string Name { get; set; }
    }
}

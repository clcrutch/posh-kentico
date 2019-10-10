using PoshKentico.Core.Services.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Providers.Development
{
    public abstract class Control<T> : IControl<T>
    {
        public Control(T backingControl)
        {
            this.BackingControl = backingControl;
        }

        public T BackingControl { get; private set; }

        public abstract int CategoryID { get; }

        public abstract string DisplayName { get; }

        public abstract int ID { get; }

        public abstract string Name { get; }
    }
}

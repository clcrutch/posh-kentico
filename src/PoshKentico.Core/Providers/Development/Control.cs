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
        public Control()
        {
            this.BackingControl = Activator.CreateInstance<T>();
        }

        public Control(T backingControl)
        {
            this.BackingControl = backingControl;
        }

        public T BackingControl { get; private set; }

        public abstract int CategoryID { get; set; }

        public abstract string DisplayName { get; set; }

        public abstract int ID { get; set; }

        public abstract string Name { get; set; }

        public abstract string Properties { get; set; }
    }
}

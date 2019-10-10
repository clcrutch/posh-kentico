using PoshKentico.Core.Services.Development;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Providers.Development
{
    public abstract class ControlCategory<T> : IControlCategory<T>
    {
        public ControlCategory(T backingControlCategory)
        {
            this.BackingControlCategory = backingControlCategory;
        }

        public T BackingControlCategory { get; private set; }

        public abstract string DisplayName { get; }

        public abstract int ID { get; }

        public abstract string ImagePath { get; }

        public abstract string Name { get; }

        public abstract int ParentID { get; }

        public abstract string Path { get; }
    }
}

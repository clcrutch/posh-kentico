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
        public ControlCategory()
        {
            this.BackingControlCategory = Activator.CreateInstance<T>();
        }

        public ControlCategory(T backingControlCategory)
        {
            this.BackingControlCategory = backingControlCategory;
        }

        public T BackingControlCategory { get; private set; }

        public abstract string DisplayName { get; set; }

        public abstract int ID { get; set; }

        public abstract string ImagePath { get; set; }

        public abstract string Name { get; set; }

        public abstract int ParentID { get; set; }

        public abstract string Path { get; set; }
    }
}

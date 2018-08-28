using System;

namespace PoshKentico.Business.Development.WebParts
{
    internal class ValueAttribute : Attribute
    {
        public string Value { get; }

        public ValueAttribute(string value)
        {
            this.Value = value;
        }
    }
}

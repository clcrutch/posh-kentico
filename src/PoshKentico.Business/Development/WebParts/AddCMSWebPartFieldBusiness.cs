using System.ComponentModel.Composition;
using System.Linq;
using PoshKentico.Core.Services.Development.WebParts;

namespace PoshKentico.Business.Development.WebParts
{
    [Export(typeof(AddCMSWebPartFieldBusiness))]
    public class AddCMSWebPartFieldBusiness : WebPartBusinessBase
    {
        public IField AddField(AddFieldParameter addFieldParameter, IWebPart webPart)
        {
            var dataType = typeof(FieldDataType).GetMember(addFieldParameter.ColumnType.ToString())
                            .Single()
                            .GetCustomAttributes(typeof(ValueAttribute), false)
                            .Select(x => x as ValueAttribute)
                            .Single()
                            .Value;

            var field = new Field
            {
                AllowEmpty = !addFieldParameter.Required,
                DataType = dataType,
                DefaultValue = addFieldParameter.DefaultValue.ToString(),
                Name = addFieldParameter.Name,
                Size = addFieldParameter.Size,
            };

            return this.WebPartService.AddField(field, webPart);
        }

        public struct AddFieldParameter
        {
            public FieldDataType ColumnType { get; set; }

            public object DefaultValue { get; set; }

            public string Name { get; set; }

            public bool Required { get; set; }

            public int Size { get; set; }
        }

        private class Field : IField
        {
            public bool AllowEmpty { get; set; }

            public string DataType { get; set; }

            public string DefaultValue { get; set; }

            public string Name { get; set; }

            public int Size { get; set; }
        }
    }
}

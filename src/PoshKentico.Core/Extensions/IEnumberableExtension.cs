using PoshKentico.Core.Services.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Extensions
{
    public static class ResourceExtensions
    {
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> items, Func<T, IEnumerable<T>> flattenItems)
        {
            return items.SelectMany(c => flattenItems(c).Flatten(flattenItems)).Concat(items);
        }
    }
}

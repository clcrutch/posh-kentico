using CMS.PortalEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.Development
{
    public interface IWebPartService
    {
        IEnumerable<WebPartCategoryInfo> GetWebPartCategories();
    }
}

using CMS.PortalEngine;
using PoshKentico.Core.Services.Development;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Providers.Development
{
    [Export(typeof(IWebPartService))]
    public class KenticoWebPartService : IWebPartService
    {
        public IEnumerable<WebPartCategoryInfo> GetWebPartCategories()
        {
            return WebPartCategoryInfoProvider.GetCategories();
        }
    }
}

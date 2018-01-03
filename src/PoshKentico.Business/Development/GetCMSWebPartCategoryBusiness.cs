using CMS.PortalEngine;
using PoshKentico.Core.Services.Development;
using PoshKentico.Core.Services.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Business.Development
{
    [Export(typeof(GetCMSWebPartCategoryBusiness))]
    public class GetCMSWebPartCategoryBusiness : CmdletBusinessBase
    {
        [Import]
        public ICmsApplicationService CmsApplicationService { get; set; }

        [Import]
        public IWebPartService WebPartService { get; set; }

        public IEnumerable<WebPartCategoryInfo> GetWebPartCategories()
        {
            this.CmsApplicationService.Initialize(true, this.WriteVerbose, this.WriteDebug);

            return this.WebPartService.GetWebPartCategories();
        }

        public IEnumerable<WebPartCategoryInfo> GetWebPartCategories(string matchString, bool exact)
        {
            this.CmsApplicationService.Initialize(true, this.WriteVerbose, this.WriteDebug);

            if (exact)
            {
                return (from c in this.WebPartService.GetWebPartCategories()
                        where c.CategoryName.ToLowerInvariant().Equals(matchString, StringComparison.InvariantCultureIgnoreCase) ||
                            c.CategoryDisplayName.ToLowerInvariant().Equals(matchString, StringComparison.InvariantCultureIgnoreCase) ||
                            c.CategoryPath.ToLowerInvariant().Equals(matchString, StringComparison.InvariantCultureIgnoreCase)
                        select c).ToArray();
            }
            else
            {
                var lowerMatchString = matchString.ToLowerInvariant();

                return (from c in this.WebPartService.GetWebPartCategories()
                        where c.CategoryName.ToLowerInvariant().Contains(lowerMatchString) ||
                           c.CategoryDisplayName.ToLowerInvariant().Contains(lowerMatchString) ||
                           c.CategoryPath.ToLowerInvariant().StartsWith(lowerMatchString)
                        select c).ToArray();
            }
        }
    }
}

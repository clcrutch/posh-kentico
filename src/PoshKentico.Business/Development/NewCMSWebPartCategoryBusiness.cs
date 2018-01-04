using ImpromptuInterface;
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
    [Export(typeof(NewCMSWebPartCategoryBusiness))]
    public class NewCMSWebPartCategoryBusiness : CmdletBusinessBase
    {
        [Import]
        public ICmsApplicationService CmsApplicationService;

        [Import]
        public IWebPartService WebPartService { get; set; }

        public IWebPartCategory CreateWebPart(string path, string displayName, string imagePath)
        {
            this.CmsApplicationService.Initialize(true, this.WriteDebug, this.WriteVerbose);

            var name = path.Substring(path.LastIndexOf('/') + 1);
            var basePath = path.Substring(0, path.LastIndexOf('/'));

            if (string.IsNullOrWhiteSpace(basePath))
            {
                basePath = "/";
            }

            var parent = (from c in this.WebPartService.WebPartCategories
                          where c.CategoryPath.Equals(basePath, StringComparison.InvariantCultureIgnoreCase)
                          select c).SingleOrDefault();

            if (parent == null)
            {
                throw new Exception("Cannot find parent WebPartCategory.");
            }

            var data = new
            {
                CategoryName = name,
                CategoryPath = path,
                CategoryDisplayName = displayName ?? name,
                CategoryImagePath = imagePath,
                CategoryParentID = parent.CategoryID,

                CategoryID = -1,
            };

            return this.WebPartService.Save(data.ActLike<IWebPartCategory>());
        }
    }
}

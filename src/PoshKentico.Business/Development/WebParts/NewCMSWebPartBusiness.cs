using ImpromptuInterface;
using PoshKentico.Core.Services.Development.WebParts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Business.Development.WebParts
{
    [Export(typeof(NewCMSWebPartBusiness))]
    public class NewCMSWebPartBusiness : WebPartBusinessBase
    {
        public IWebPart CreateWebPart(string path, string fileName, string displayName)
        {
            var name = path.Substring(path.LastIndexOf('/') + 1);
            var basePath = path.Substring(0, path.LastIndexOf('/'));

            if (string.IsNullOrWhiteSpace(basePath))
            {
                basePath = "/";
            }

            var parent = this.GetCategoryFromPath(basePath);

            if (string.IsNullOrEmpty(displayName))
            {
                displayName = name;
            }

            var data = new
            {
                WebPartDisplayName = displayName,
                WebPartFileName = fileName,
                WebPartName = name,

                WebPartCategoryID = -1,
            };

            return this.WebPartService.Create(data.ActLike<IWebPart>());
        }
    }
}

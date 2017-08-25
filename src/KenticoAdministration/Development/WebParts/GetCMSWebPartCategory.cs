using CMS.PortalEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace KenticoAdministration.Development.WebParts
{
    [Cmdlet(VerbsCommon.Get, "CmsWebPartCategory", DefaultParameterSetName = NONE)]
    public class GetCmsWebPartCategory : PSCmdlet
    {
        private const string NONE = "None";
        private const string BY_CODE_NAME = "ByCodeName";
        private const string CHILDREN = "Children";

        [Parameter(Mandatory = true, ParameterSetName = BY_CODE_NAME)]
        public string CodeName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = CHILDREN, ValueFromPipeline = true)]
        public WebPartCategoryInfo Parent { get; set; }

        protected override void ProcessRecord()
        {
            if (ParameterSetName == NONE)
                WriteObject(WebPartCategoryInfoProvider.GetCategories(), true);
            else if (ParameterSetName == BY_CODE_NAME)
                WriteObject(WebPartCategoryInfoProvider.GetWebPartCategoryInfoByCodeName(CodeName));
            else if (ParameterSetName == CHILDREN)
                WriteObject(from c in WebPartCategoryInfoProvider.GetCategories()
                            where c.CategoryParentID == Parent.CategoryID
                            select c, true);
        }
    }
}

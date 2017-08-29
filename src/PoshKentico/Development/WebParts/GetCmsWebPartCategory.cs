using CMS.PortalEngine;
using System.Linq;
using System.Management.Automation;

namespace PoshKentico.Development.WebParts
{
    [Cmdlet(VerbsCommon.Get, "CMSWebPartCategory", DefaultParameterSetName = NONE)]
    public class GetCmsWebPartCategory : PSCmdlet
    {

        #region Constants

        private const string NONE = "None";
        private const string BY_CODE_NAME = "ByCodeName";
        private const string CHILDREN = "Children";

        #endregion


        #region Properties

        [Parameter(Mandatory = true, ParameterSetName = BY_CODE_NAME)]
        public string CodeName { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = CHILDREN, ValueFromPipeline = true)]
        public WebPartCategoryInfo Parent { get; set; }

        #endregion


        #region Methods

        protected override void ProcessRecord()
        {
            switch (ParameterSetName)
            {
                case (NONE):
                    WriteObject(WebPartCategoryInfoProvider.GetCategories(), true);
                    break;
                case (BY_CODE_NAME):
                    WriteObject(WebPartCategoryInfoProvider.GetWebPartCategoryInfoByCodeName(CodeName));
                    break;
                case (CHILDREN):
                    WriteObject(from c in WebPartCategoryInfoProvider.GetCategories()
                                where c.CategoryParentID == Parent.CategoryID
                                select c, true);

                    break;
            }
        }

        #endregion

    }
}

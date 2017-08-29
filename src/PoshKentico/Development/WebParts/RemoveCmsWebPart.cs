using CMS.PortalEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Development.WebParts
{
    [Cmdlet(VerbsCommon.Remove, "CmsWebPart", DefaultParameterSetName = NONE)]
    public class RemoveCmsWebPart : PSCmdlet
    {

        #region Constants

        private const string NONE = "None";
        private const string NAME = "Name";

        #endregion


        #region Properties

        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = NONE)]
        [Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ParameterSetName = NAME)]
        public WebPartCategoryInfo Category { get; set; }

        [Parameter(Mandatory = true, Position = 1, ParameterSetName = NAME)]
        public string Name { get; set; }

        #endregion

        #region Methods

        protected override void ProcessRecord()
        {
            IEnumerable<WebPartInfo> webPartsToDelete = null;

            switch (ParameterSetName)
            {
                case (NONE):
                    webPartsToDelete = WebPartInfoProvider.GetAllWebParts(Category.CategoryID);
                    break;
            }

            if (webPartsToDelete != null)
            {
                foreach (var webPartToDelete in webPartsToDelete)
                {
                    if (!webPartToDelete.Delete())
                        throw new Exception($"Failed to delete \"{webPartToDelete.WebPartName}\".");
                }
            }
        }

        #endregion

    }
}

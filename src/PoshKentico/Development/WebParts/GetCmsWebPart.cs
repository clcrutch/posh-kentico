using CMS.PortalEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Development.WebParts
{
    [Cmdlet(VerbsCommon.Get, "CMSWebPart", DefaultParameterSetName = NONE)]
    public class GetCmsWebPart : PSCmdlet
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
            switch (ParameterSetName)
            {
                case (NONE):
                    WriteObject(WebPartInfoProvider.GetAllWebParts(Category.CategoryID), true);
                    break;
                case (NAME):
                    WriteObject(from w in WebPartInfoProvider.GetAllWebParts(Category.CategoryID)
                                where w.WebPartName.Equals(Name, StringComparison.InvariantCultureIgnoreCase)
                                select w, true);
                    break;
            }
        }

        #endregion

    }
}

using CMS.PortalEngine;
using ImpromptuInterface;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.WebParts
{
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSWebPart", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = NONE)]
    [Alias("rmwp")]
    public class RemoveCMSWebPartCmdlet : GetCMSWebPartCmdlet
    {
        private const string WEBPART = "WebPart";

        [Parameter(ValueFromPipeline = true, Mandatory = true, ParameterSetName = WEBPART)]
        public WebPartInfo WebPart { get; set; }

        [Import]
        public RemoveCMSWebPartBusiness RemoveBusinessLayer { get; set; }

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            if (this.ParameterSetName == WEBPART)
            {
                this.ActOnObject(this.WebPart.ActLike<IWebPart>());
            }
            else
            {
                base.ProcessRecord();
            }
        }

        protected override void ActOnObject(IWebPart webPart)
        {
            if (webPart == null)
            {
                return;
            }

            this.RemoveBusinessLayer.RemoveWebPart(webPart);
        }
    }
}

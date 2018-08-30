using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using ImpromptuInterface;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;

namespace PoshKentico.Cmdlets.Development.WebParts
{
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSWebPartField", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public class RemoveCMSWebPartFieldCmdlet : GetCMSWebPartFieldCmdlet
    {
        [Import]
        public RemoveCMSWebPartFieldBusiness RemoveBusinessLayer { get; set; }

        /// <inheritdoc/>
        protected override void ActOnObject(IWebPartField field)
        {
            this.RemoveBusinessLayer.RemoveField(field, this.WebPart.ActLike<IWebPart>());
        }
    }
}

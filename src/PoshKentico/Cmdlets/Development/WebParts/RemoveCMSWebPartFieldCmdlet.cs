using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using CMS.FormEngine;
using ImpromptuInterface;
using PoshKentico.Business.Development.WebParts;
using PoshKentico.Core.Services.Development.WebParts;

using AliasAttribute = System.Management.Automation.AliasAttribute;

namespace PoshKentico.Cmdlets.Development.WebParts
{
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Remove, "CMSWebPartField", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public class RemoveCMSWebPartFieldCmdlet : GetCMSWebPartFieldCmdlet
    {
        private const string FIELD = "Field";

        [Parameter(Mandatory = true, ValueFromPipeline = true, ParameterSetName = FIELD)]
        [Alias("Property")]
        public FormFieldInfo Field { get; set; }

        [Import]
        public RemoveCMSWebPartFieldBusiness RemoveBusinessLayer { get; set; }

        protected override void ProcessRecord()
        {
            if (this.ParameterSetName == FIELD)
            {
                this.ActOnObject(this.Field.ActLike<IWebPartField>());
            }
            else
            {
                base.ProcessRecord();
            }
        }

        /// <inheritdoc/>
        protected override void ActOnObject(IWebPartField field)
        {
            this.RemoveBusinessLayer.RemoveField(field, this.WebPart?.ActLike<IWebPart>() ?? field.WebPart);
        }
    }
}

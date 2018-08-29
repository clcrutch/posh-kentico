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

namespace PoshKentico.Cmdlets.Development.WebParts
{
    [ExcludeFromCodeCoverage]
    [Cmdlet(VerbsCommon.Set, "CMSWebPart")]
    public class SetCMSWebPartCmdlet : MefCmdlet
    {
        #region Constants

        private const string PASSTHRU = "PassThru";

        #endregion

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the web part.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = PASSTHRU)]
        public SwitchParameter PassThru { get; set; }

        [Parameter(Mandatory = true)]
        public WebPartInfo WebPart { get; set; }

        [Import]
        public SetCMSWebPartBusiness BusinessLayer { get; set; }

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            this.BusinessLayer.Set(this.WebPart.ActLike<IWebPart>());

            if (this.PassThru.ToBool())
            {
                this.WriteObject(this.WebPart);
            }
        }
    }
}

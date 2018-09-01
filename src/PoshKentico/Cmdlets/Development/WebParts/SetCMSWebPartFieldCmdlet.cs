using CMS.FormEngine;
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
    [Cmdlet(VerbsCommon.Set, "CMSWebPartField")]
    [Alias("swpf")]
    public class SetCMSWebPartFieldCmdlet : MefCmdlet
    {
        #region Constants

        private const string PASSTHRU = "PassThru";

        #endregion

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        [Alias("Property")]
        public FormFieldInfo Field { get; set; }

        /// <summary>
        /// <para type="description">Tell the cmdlet to return the web part.</para>
        /// </summary>
        [Parameter(Mandatory = false, ParameterSetName = PASSTHRU)]
        public SwitchParameter PassThru { get; set; }

        [Import]
        public SetCMSWebPartFieldBusiness BusinessLayer { get; set; }

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            this.BusinessLayer.Set(this.Field.ActLike<IWebPartField>());

            if (this.PassThru.ToBool())
            {
                this.WriteObject(this.Field);
            }
        }
    }
}

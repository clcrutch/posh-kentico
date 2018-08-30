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
    [Cmdlet(VerbsCommon.Get, "CMSWebPartField", DefaultParameterSetName = NONAME)]
    public class GetCMSWebPartFieldCmdlet : MefCmdlet
    {
        private const string NAME = "Name";
        private const string NONAME = "No Name";

        [Parameter(Mandatory = true, Position = 0, ParameterSetName = NAME)]
        [Alias("Caption")]
        public string Name { get; set; }

        /// <summary>
        /// <para type="description">Indicates if the CategoryName supplied is a regular expression.</para>
        /// </summary>
        [Parameter(ParameterSetName = NAME)]
        [Alias("Regex")]
        public SwitchParameter RegularExpression { get; set; }

        [Parameter(ValueFromPipeline = true, Mandatory = true, ParameterSetName = NONAME)]
        [Parameter(ValueFromPipeline = true, Mandatory = true, ParameterSetName = NAME)]
        public WebPartInfo WebPart { get; set; }

        [Import]
        public GetCMSWebPartFieldBusiness BusinessLayer { get; set; }

        protected override void ProcessRecord()
        {
            IEnumerable<IWebPartField> fields = null;

            switch (this.ParameterSetName)
            {
                case NAME:
                    fields = this.BusinessLayer.GetWebPartFields(this.Name, this.RegularExpression.ToBool(), this.WebPart.ActLike<IWebPart>());
                    break;
                case NONAME:
                    fields = this.BusinessLayer.GetWebPartFields(this.WebPart.ActLike<IWebPart>());
                    break;
            }

            foreach (var field in fields)
            {
                this.ActOnObject(field);
            }
        }

        protected virtual void ActOnObject(IWebPartField field)
        {
            this.WriteObject(field?.UndoActLike());
        }
    }
}

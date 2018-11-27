using PoshKentico.Business.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Cmdlets.General
{
    [Cmdlet(VerbsData.Initialize, "CMSDatabase")]
    public class InitializeCMSDatabaseCmdlet : MefCmdlet
    {
        [Import]
        public InitializeCMSDatabaseBusiness BusinessLayer { get; set; }

        protected override void ProcessRecord()
        {
            this.BusinessLayer.InstallSqlDatabase();
        }
    }
}

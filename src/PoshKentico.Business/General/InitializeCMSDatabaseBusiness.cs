using PoshKentico.Core.Services.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Business.General
{
    [Export(typeof(InitializeCMSDatabaseBusiness))]
    public class InitializeCMSDatabaseBusiness : CmdletBusinessBase
    {
        [Import]
        public ICmsDatabaseService CmsDatabaseService { get; set; }

        public void InstallSqlDatabase()
        {
            if (!this.CmsDatabaseService.Exists)
            {
                throw new Exception("The specified database does not exist.  Please check the connection string and try again.");
            }

            if (!this.CmsDatabaseService.IsDatabaseInstalled())
            {
                this.CmsDatabaseService.InstallSqlDatabase();
            }
        }
    }
}

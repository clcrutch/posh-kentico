using CMS.DataEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Core.Services.General
{
    public interface ICmsDatabaseService
    {
        Version Version { get; }

        string ConnectionString { get; set; }

        bool Exists { get; }

        void ExecuteQuery(string queryText, QueryDataParameters parameters);

        void InstallSqlDatabase();

        bool IsDatabaseInstalled();
    }
}

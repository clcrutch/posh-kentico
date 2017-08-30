using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace PoshKentico.Navigation.DynamicParameters
{
    public class NewWebPartCategoryDynamicParameter
    {
        [Parameter]
        public string DisplayName { get; set; }
        [Parameter]
        public string ImagePath { get; set; }
    }
}

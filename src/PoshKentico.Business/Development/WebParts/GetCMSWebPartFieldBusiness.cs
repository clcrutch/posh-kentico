using PoshKentico.Core.Services.Development.WebParts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PoshKentico.Business.Development.WebParts
{
    [Export(typeof(GetCMSWebPartFieldBusiness))]
    public class GetCMSWebPartFieldBusiness : WebPartBusinessBase
    {
        public IEnumerable<IWebPartField> GetWebPartFields(IWebPart webPart) =>
            this.WebPartService.GetWebPartFields(webPart);

        public IEnumerable<IWebPartField> GetWebPartFields(string matchString, bool isRegex, IWebPart webPart)
        {
            Regex regex = null;

            if (isRegex)
            {
                regex = new Regex(matchString, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }
            else
            {
                regex = new Regex($"^{matchString.Replace("*", ".")}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            }

            var matched = from f in this.GetWebPartFields(webPart)
                          where regex.IsMatch(f.Name) ||
                            regex.IsMatch(f.Caption)
                          select f;

            return matched.ToArray();
        }
    }
}

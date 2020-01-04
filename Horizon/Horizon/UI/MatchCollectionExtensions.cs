using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Horizon.UI
{
    public static class MatchCollectionExtensions
    {
        public static List<Match> ToList(this MatchCollection matchCollection)
        {
            List<Match> returnList = new List<Match>();
            foreach (Match m in matchCollection)
            {
                returnList.Add(m);
            }
            return returnList;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Horizon.Utilities
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> DistinctWhere<T, TKey>(this IEnumerable<T> collection, Func<T, TKey> keySelector)
        {
            return collection
              .GroupBy(keySelector)
              .Select(g => g.First());
        }
    }
}
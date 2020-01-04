using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Core
{
    public static class StringHelper
    {
        /// <summary>
        /// Gets whether the string is null, empty, or comprised entirely of whitespace.
        /// </summary>
        /// <param name="s">
        /// </param>
        /// <returns>
        /// True if the string is null, empty, or comprised entirely of whitespace, otherwise false.
        /// </returns>
        public static bool IsNullOrWhitespace(this string s) => string.IsNullOrWhiteSpace(s);
    }
}
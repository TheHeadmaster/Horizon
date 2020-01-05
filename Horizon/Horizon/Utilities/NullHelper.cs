using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Utilities
{
    public static class NullHelper
    {
        /// <summary>
        /// Gets whether this object is not null.
        /// </summary>
        /// <param name="obj">
        /// The object.
        /// </param>
        /// <returns>
        /// True if the object is not null, otherwise false.
        /// </returns>
        public static bool IsNotNull(this object obj) => !(obj is null);
    }
}
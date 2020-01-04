using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.UI
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    internal class DisplayableAttribute : Attribute
    {
        public string Category { get; set; }

        public string Description { get; set; }

        public string DisplayName { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsExpandable { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Json
{
    public class ReplaceOperation : PatchOperation
    {
        public string Value { get; set; }
    }
}
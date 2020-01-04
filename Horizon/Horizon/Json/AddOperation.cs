using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Core.Data.Json
{
    public class AddOperation : PatchOperation
    {
        public string Value { get; set; }
    }
}
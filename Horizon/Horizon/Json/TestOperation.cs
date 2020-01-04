using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Core.Data.Json
{
    public class TestOperation : PatchOperation
    {
        public List<PatchOperation> Children { get; set; }

        public TestValue TestValue { get; set; }
    }
}
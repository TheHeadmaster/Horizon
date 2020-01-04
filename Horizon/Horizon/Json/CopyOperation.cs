using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Core.Data.Json
{
    public class CopyOperation : PatchOperation
    {
        public string CopyPath { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MethodBoundaryAspect.Fody.Attributes;

namespace Horizon.Diagnostics
{
    public class LogAttribute : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
        }

        public override void OnException(MethodExecutionArgs args)
        {
        }

        public override void OnExit(MethodExecutionArgs args)
        {
        }
    }
}
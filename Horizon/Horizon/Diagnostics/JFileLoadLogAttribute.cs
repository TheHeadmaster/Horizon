using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horizon.Json;
using MethodBoundaryAspect.Fody.Attributes;

namespace Horizon.Diagnostics
{
    public class JFileLoadLogAttribute : LogAttribute
    {
        public JFileLoadLogAttribute()
        {
            this.ExceptionLevel = LogType.Error;
            this.ExceptionMessage = "Failed to load file. See exception for details.";
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            string path = Path.Combine(args.Arguments[0].ToString(), args.Arguments[1].ToString());
            DiagManager.LogInfo($"Attempting to load file {path}...");
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            JFile returnValue = args.ReturnValue as JFile;
            DiagManager.LogInfo($"Loaded File {returnValue.FileName}.");
        }
    }
}
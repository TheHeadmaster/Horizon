using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MethodBoundaryAspect.Fody.Attributes;

namespace Horizon.Diagnostics
{
    /// <summary>
    /// Handles general logging for methods.
    /// </summary>
    public class LogAttribute : OnMethodBoundaryAspect
    {
        public string EntryMessage { get; set; }

        public LogType ExceptionLevel { get; set; }

        public string ExceptionMessage { get; set; }

        public string ExitMessage { get; set; }

        public LogAttribute()
        {
            this.EntryMessage = "";
            this.ExceptionMessage = "";
            this.ExitMessage = "";
            this.ExceptionLevel = LogType.Error;
        }

        public LogAttribute(string entryMessage = "", string exitMessage = "", string exceptionMessage = "", LogType exceptionLevel = LogType.Error)
        {
            this.EntryMessage = entryMessage;
            this.ExitMessage = exitMessage;
            this.ExceptionLevel = exceptionLevel;
            this.ExceptionMessage = exceptionMessage;
        }

        public override void OnEntry(MethodExecutionArgs args) => DiagManager.LogInfo(this.EntryMessage);

        public override void OnException(MethodExecutionArgs args)
        {
            DiagManager.Log(this.ExceptionMessage, this.ExceptionLevel);
            DiagManager.Log($"Exception: {args.Exception.Message}", this.ExceptionLevel);
            DiagManager.Log($"Source: {args.Exception.Source}", this.ExceptionLevel);
            DiagManager.Log($"Inner Exception: {args.Exception.InnerException?.Message ?? "null"}", this.ExceptionLevel);
            DiagManager.Log($"Method Name: {args.Method.Name}", this.ExceptionLevel);
            DiagManager.Log($"Stack Trace: {args.Exception.StackTrace}", this.ExceptionLevel);
        }

        public override void OnExit(MethodExecutionArgs args) => DiagManager.LogInfo(this.ExitMessage);
    }
}
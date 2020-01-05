using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MethodBoundaryAspect.Fody.Attributes;

namespace Horizon.Diagnostics
{
    /// <summary>
    /// Handles the logging for the application entry point.
    /// </summary>
    public class WelcomeLogAttribute : LogAttribute
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            DiagManager.LogInfo($"Welcome to Horizon v.{App.CurrentVersion.ToString()}.");
            DiagManager.LogInfo("Opening splash and program selection window...");
        }
    }
}
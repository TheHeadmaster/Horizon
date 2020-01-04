using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Diagnostics
{
    /// <summary>
    /// Manages diagnostics and logging.
    /// </summary>
    public static class DiagManager
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(
            MethodBase.GetCurrentMethod().DeclaringType);

        public static void Log(string message, LogType logType)
        {
            switch (logType)
            {
                case LogType.Info:
                    {
                        log.Info(message);
                        break;
                    }
                case LogType.Warning:
                    {
                        log.Warn(message);
                        break;
                    }
                case LogType.Error:
                    {
                        log.Error(message);
                        break;
                    }
                case LogType.Fatal:
                    {
                        log.Fatal(message);
                        break;
                    }
            }
        }

        public static void LogError(string message) => Log(message, LogType.Error);

        public static void LogFatal(string message) => Log(message, LogType.Fatal);

        public static void LogInfo(string message) => Log(message, LogType.Info);

        public static void LogWarning(string message) => Log(message, LogType.Warning);
    }
}
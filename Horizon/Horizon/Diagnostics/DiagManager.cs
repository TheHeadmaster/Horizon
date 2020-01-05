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

        /// <summary>
        /// Logs a message to a file and the output window.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        /// <param name="logType">
        /// The level of the log.
        /// </param>
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

        /// <summary>
        /// Logs an error to a file and the output window.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        public static void LogError(string message) => Log(message, LogType.Error);

        /// <summary>
        /// Logs a fatal to a file and the output window.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        public static void LogFatal(string message) => Log(message, LogType.Fatal);

        /// <summary>
        /// Logs an info to a file and the output window.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        public static void LogInfo(string message) => Log(message, LogType.Info);

        /// <summary>
        /// Logs a warning to a file and the output window.
        /// </summary>
        /// <param name="message">
        /// The message to log.
        /// </param>
        public static void LogWarning(string message) => Log(message, LogType.Warning);
    }
}
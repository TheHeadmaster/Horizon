using Horizon.Diagnostics;
using Horizon.Json;
using Horizon.ObjectModel;
using Horizon.UI;
using Horizon.Windows;
using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace Horizon
{
    /// <summary>
    /// Houses global application data and the entry point of the application.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Gets the directory of the executing assembly.
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        /// <summary>
        /// Gets Horizon's current version.
        /// </summary>
        public static Version CurrentVersion => Assembly.GetExecutingAssembly().GetName().Version;

        /// <summary>
        /// Gets the Instance of the App.
        /// </summary>
        public static App Instance { get; private set; }

        public static LauncherMeta LauncherMeta { get; } = new LauncherMeta();

        public static UserMeta Metadata { get; private set; }

        public static StyleManager StyleManager { get; private set; } = new StyleManager();

        /// <summary>
        /// Serves as the application entry point.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="args">
        /// Contains the arguments pertaining to the application startup event.
        /// </param>
        [WelcomeLog]
        private void AppStartup(object sender, StartupEventArgs args)
        {
            Instance = this;

            Metadata = JFile.Load<UserMetaFile>(AssemblyDirectory, "metadata.json").CreateModel();

            bool? _ = new Splash().ShowDialog();
        }
    }
}
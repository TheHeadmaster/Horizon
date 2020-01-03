using Horizon.UI;
using Horizon.Windows;
using System;
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
        /// Gets the Instance of the App.
        /// </summary>
        public static App Instance { get; private set; }

        /// <summary>
        /// Gets Horizon's current version.
        /// </summary>
        public Version CurrentVersion => Assembly.GetExecutingAssembly().GetName().Version;

        /// <summary>
        /// Serves as the application entry point.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="args">
        /// Contains the arguments pertaining to the application startup event.
        /// </param>
        private void AppStartup(object sender, StartupEventArgs args)
        {
            Instance = this;
            LauncherMeta.Initialize();

            bool? _ = new Splash().ShowDialog();
        }
    }
}
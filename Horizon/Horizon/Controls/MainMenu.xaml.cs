using Horizon.ViewModels;
using Horizon.Windows;
using Octokit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Horizon.Controls
{
    /// <summary>
    /// Toolbar used to access various commands and functions.
    /// </summary>
    public partial class MainMenu : UserControl
    {
        public static MainMenu Instance { get; private set; }

        public MainMenuViewModel ViewModel { get; set; }

        public MainMenu()
        {
            this.InitializeComponent();
            Instance = this;

            this.ViewModel = new MainMenuViewModel();
            this.DataContext = this.ViewModel;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs args) => Process.Start(@"https://github.com/TheHeadmaster/Horizon/wiki");

        private void MenuItem_Click_1(object sender, RoutedEventArgs args) => Process.Start(@"https://github.com/TheHeadmaster/Horizon/issues/new?assignees=&labels=bug&template=bug_report.md&title=");

        private void MenuItem_Click_2(object sender, RoutedEventArgs args) => Process.Start(@"https://github.com/TheHeadmaster/Horizon/issues/new?assignees=&labels=enhancement&template=feature_request.md&title=");

        private void MenuItem_Click_3(object sender, RoutedEventArgs args) => new AboutWindow().ShowDialog();

        private void MenuItem_Click_4(object sender, RoutedEventArgs args)
        {
            GitHubClient client = new GitHubClient(new ProductHeaderValue("Horizon"));
            IReadOnlyList<Release> releases = client.Repository.Release.GetAll("TheHeadmaster", "Horizon").Result;
            Release latest = releases[0];
            string[] version = latest.TagName.Split(new char[] { '.', '-' });
            bool needsUpdate = false;
            if (App.CurrentVersion.Major < Convert.ToInt32(version[0]))
            {
                needsUpdate = true;
            }
            else if (App.CurrentVersion.Major == Convert.ToInt32(version[0]) && App.CurrentVersion.Minor < Convert.ToInt32(version[1]))
            {
                needsUpdate = true;
            }
            else if (App.CurrentVersion.Major == Convert.ToInt32(version[0]) &&
                App.CurrentVersion.Minor == Convert.ToInt32(version[1]) &&
                App.CurrentVersion.Build < Convert.ToInt32(version[2]))
            {
                needsUpdate = true;
            }

            if (needsUpdate)
            {
                MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show($"A new version of Horizon ({latest.TagName}) is available. Do you wish to install it?", "Horizon Update", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    result = Xceed.Wpf.Toolkit.MessageBox.Show($"Horizon will need to close during the updating procedure. Press \"Yes\" to save your work and continue, or \"No\" to cancel.", "Horizon Update", MessageBoxButton.YesNo);
                }
                if (result == MessageBoxResult.Yes)
                {
                    string url = $"https://github.com/TheHeadmaster/Horizon/releases/download/{latest.TagName}/Horizon.Installer.msi";
                    string path = System.IO.Path.GetTempPath();
                    string fileName = "Horizon.Installer.msi";
                    string tmpFile = System.IO.Path.Combine(path, fileName);

                    using (WebClient c = new WebClient())
                    {
                        c.DownloadFile(url, tmpFile);
                    }

                    Process installerProcess = new Process();
                    ProcessStartInfo processInfo = new ProcessStartInfo();
                    processInfo.Arguments = $"/i {tmpFile}";
                    processInfo.FileName = "msiexec";
                    installerProcess.StartInfo = processInfo;
                    installerProcess.Start();

                    App.Current.Shutdown();
                }
                else
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show($"Horizon is of the latest version.", "Horizon Up to Date", MessageBoxButton.OK);
                }
            }
        }
    }
}
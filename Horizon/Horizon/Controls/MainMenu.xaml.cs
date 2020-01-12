using Horizon.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    }
}
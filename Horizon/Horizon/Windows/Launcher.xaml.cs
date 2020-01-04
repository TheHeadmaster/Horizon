using Horizon.UI;
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
using System.IO;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Horizon.Windows
{
    /// <summary>
    /// Allows the user to enable and disable mods, and launch the game.
    /// </summary>
    public partial class Launcher : Window
    {
        public static Launcher Instance { get; private set; }

        public LauncherViewModel ViewModel { get; set; }

        public Launcher()
        {
            this.InitializeComponent();
            Instance = this;
            this.ViewModel = new LauncherViewModel();
            this.DataContext = this.ViewModel;

            this.ModsListBox.ItemsSource = this.ViewModel.Mods;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = Path.Combine(App.LauncherMeta.LauncherPath, "win64", "starbound.exe");
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.Verb = "runas";
            proc.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) => Environment.Exit(0);

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dlg = new CommonOpenFileDialog();
            dlg.Title = "Choose the starbound root folder...";
            dlg.IsFolderPicker = true;
            dlg.InitialDirectory = @"C:\Starbound";

            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.DefaultDirectory = @"C:\Starbound";
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;

            if (dlg.ShowDialog(this) == CommonFileDialogResult.Ok)
            {
                App.LauncherMeta.LauncherPath = dlg.FileName;
            }
        }
    }
}
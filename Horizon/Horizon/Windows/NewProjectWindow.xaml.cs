using Horizon.Controls;
using Horizon.ObjectModel;
using Horizon.UI;
using Horizon.ViewModels;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Horizon.Windows
{
    /// <summary>
    /// Interaction logic for NewProjectWindow.xaml
    /// </summary>
    public partial class NewProjectWindow : BorderlessWindow
    {
        public static NewProjectWindow Instance { get; private set; }

        public NewProjectViewModel ViewModel { get; set; }

        public NewProjectWindow()
        {
            this.InitializeComponent();
            Instance = this;
            this.ViewModel = new NewProjectViewModel()
            {
                ProjectName = "My Project",
                ProjectPath = @"C:\Starbound"
            };
            this.DataContext = this.ViewModel;
            this.projectNameTextBox.Watermark = "My Project";
            this.projectPathTextBox.Watermark = @"C:\Starbound";
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs args)
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
                this.ViewModel.ProjectPath = dlg.FileName;
            }
        }

        private bool CheckForFiles(string path)
        {
            if (!File.Exists(Path.Combine(path, "win32", "starbound.exe")))
            {
                Xceed.Wpf.Toolkit.MessageBox.Show($"{path} is not a valid folder. No starbound.exe exists inside {Path.Combine(path, "win32", "starbound.exe")}. Please choose a valid Starbound folder.", "Invalid Starbound Folder", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs args)
        {
            if (!this.CheckForFiles(this.ViewModel.ProjectPath)) { return; }
            Project project = new Project { Name = this.ViewModel.ProjectName };
            App.Metadata.RecentlyOpenedProjects.Add(new RecentItem { Name = this.ViewModel.ProjectName, Path = this.ViewModel.ProjectPath });
            project.FilePath = this.ViewModel.ProjectPath;
            project.FileName = "project.json";
            project.Save();
            project.LoadMods();
            IDEWindow.Instance.ViewModel.CurrentProject = project;
            App.Metadata.Save();
            this.Close();
        }
    }
}
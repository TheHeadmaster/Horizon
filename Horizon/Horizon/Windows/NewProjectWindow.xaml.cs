using Horizon.ObjectModel;
using Horizon.UI;
using Horizon.ViewModels;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Horizon.Windows
{
    /// <summary>
    /// Interaction logic for NewProjectWindow.xaml
    /// </summary>
    public partial class NewProjectWindow : Window
    {
        public NewProjectViewModel ViewModel { get; set; }

        public NewProjectWindow()
        {
            this.InitializeComponent();
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

        private void SaveButton_Click(object sender, RoutedEventArgs args)
        {
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
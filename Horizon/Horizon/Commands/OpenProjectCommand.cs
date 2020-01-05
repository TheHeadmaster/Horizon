using Horizon.Core.Data.Json;
using Horizon.Diagnostics;
using Horizon.Json;
using Horizon.ObjectModel;
using Horizon.UI;
using Horizon.Windows;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Horizon.Commands
{
    /// <summary>
    /// Opens a project.
    /// </summary>
    public class OpenProjectCommand : RelayCommand
    {
        public static ICommand Instance { get; } = new OpenProjectCommand();

        private string CommonFileDialog()
        {
            CommonOpenFileDialog dlg = new CommonOpenFileDialog();
            dlg.Title = "Select a project file to open...";
            dlg.IsFolderPicker = false;
            dlg.InitialDirectory = @"C:\Starbound";

            dlg.Filters.Add(new CommonFileDialogFilter("JSON Project Files", "*.json"));
            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.DefaultDirectory = @"C:\Starbound";
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return dlg.FileName;
            }
            else
            {
                return null;
            }
        }

        private void OpenFromDialog()
        {
            if (IDEWindow.Instance.ViewModel.CurrentProject is null)
            {
                string path = this.CommonFileDialog();
                if (path is null) { return; }
                ProjectFile file = JFile.Load<ProjectFile>(Path.GetDirectoryName(path), "project.json");
                IDEWindow.Instance.ViewModel.CurrentProject = file.CreateModel();
                App.Metadata.RecentlyOpenedProjects.Add(new RecentItem { Name = IDEWindow.Instance.ViewModel.CurrentProject.Name, Path = IDEWindow.Instance.ViewModel.CurrentProject.FilePath });
                IDEWindow.Instance.ViewModel.CurrentProject.LoadMods();
                App.Metadata.Save();
            }
            else
            {
                MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show($"Changes have been made to {IDEWindow.Instance.ViewModel.CurrentProject.Name}. Would you like to save these changes before closing the project?", "Save Changes?", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    string path = this.CommonFileDialog();
                    if (path is null) { return; }

                    IDEWindow.Instance.ViewModel.CurrentProject.Close(true);
                    ProjectFile file = JFile.Load<ProjectFile>(Path.GetDirectoryName(path), "project.json");
                    IDEWindow.Instance.ViewModel.CurrentProject = file.CreateModel();
                    App.Metadata.RecentlyOpenedProjects.Add(new RecentItem { Name = IDEWindow.Instance.ViewModel.CurrentProject.Name, Path = IDEWindow.Instance.ViewModel.CurrentProject.FilePath });
                    IDEWindow.Instance.ViewModel.CurrentProject.LoadMods();
                    App.Metadata.Save();
                }
                else if (result == MessageBoxResult.No)
                {
                    string path = this.CommonFileDialog();
                    if (path is null) { return; }

                    IDEWindow.Instance.ViewModel.CurrentProject.Close(false);
                    ProjectFile file = JFile.Load<ProjectFile>(Path.GetDirectoryName(path), "project.json");
                    IDEWindow.Instance.ViewModel.CurrentProject = file.CreateModel();
                    App.Metadata.RecentlyOpenedProjects.Add(new RecentItem { Name = IDEWindow.Instance.ViewModel.CurrentProject.Name, Path = IDEWindow.Instance.ViewModel.CurrentProject.FilePath });
                    IDEWindow.Instance.ViewModel.CurrentProject.LoadMods();
                    App.Metadata.Save();
                }
                else
                {
                    return;
                }
            }
        }

        private void OpenFromPath(string path)
        {
            if (IDEWindow.Instance.ViewModel.CurrentProject is null)
            {
                ProjectFile file = JFile.Load<ProjectFile>(path, "project.json");
                IDEWindow.Instance.ViewModel.CurrentProject = file.CreateModel();
                App.Metadata.RecentlyOpenedProjects.Add(new RecentItem { Name = IDEWindow.Instance.ViewModel.CurrentProject.Name, Path = IDEWindow.Instance.ViewModel.CurrentProject.FilePath });
                IDEWindow.Instance.ViewModel.CurrentProject.LoadMods();
                App.Metadata.Save();
            }
            else
            {
                MessageBoxResult result = Xceed.Wpf.Toolkit.MessageBox.Show($"Changes have been made to {IDEWindow.Instance.ViewModel.CurrentProject.Name}. Would you like to save these changes before closing the project?", "Save Changes?", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    IDEWindow.Instance.ViewModel.CurrentProject.Close(true);
                    ProjectFile file = JFile.Load<ProjectFile>(path, "project.json");
                    IDEWindow.Instance.ViewModel.CurrentProject = file.CreateModel();
                    App.Metadata.RecentlyOpenedProjects.Add(new RecentItem { Name = IDEWindow.Instance.ViewModel.CurrentProject.Name, Path = IDEWindow.Instance.ViewModel.CurrentProject.FilePath });
                    IDEWindow.Instance.ViewModel.CurrentProject.LoadMods();
                    App.Metadata.Save();
                }
                else if (result == MessageBoxResult.No)
                {
                    IDEWindow.Instance.ViewModel.CurrentProject.Close(false);
                    ProjectFile file = JFile.Load<ProjectFile>(path, "project.json");
                    IDEWindow.Instance.ViewModel.CurrentProject = file.CreateModel();
                    App.Metadata.RecentlyOpenedProjects.Add(new RecentItem { Name = IDEWindow.Instance.ViewModel.CurrentProject.Name, Path = IDEWindow.Instance.ViewModel.CurrentProject.FilePath });
                    IDEWindow.Instance.ViewModel.CurrentProject.LoadMods();
                    App.Metadata.Save();
                }
                else
                {
                    return;
                }
            }
        }

        public override bool CanExecute(object parameter) => true;

        [Log("Opening a project...", ExitMessage = "Project opened.")]
        public override void Execute(object parameter)
        {
            if (parameter is string path)
            {
                this.OpenFromPath(path);
            }
            else if (parameter is null)
            {
                this.OpenFromDialog();
            }
        }
    }
}
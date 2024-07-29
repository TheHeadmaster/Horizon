using Microsoft.WindowsAPICodePack.Dialogs;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.IO;
using System.Reactive.Disposables;
using System.Windows;

namespace Horizon.View.Windows;

/// <summary>
/// Interaction logic for NewProjectWindow.xaml
/// </summary>
public partial class NewProjectWindow
{
    public NewProjectWindow()
    {
        this.InitializeComponent();

        this.ViewModel = new()
        {
            AvailableTemplates = App.AvailableTemplates
        };

        this.WhenActivated(dispose =>
        {
            this.Bind(this.ViewModel,
                vm => vm.Project.Name,
                view => view.ProjectName.Text)
            .DisposeWith(dispose);

            this.Bind(this.ViewModel,
                vm => vm.Project.FilePath,
                view => view.FilePath.Text)
            .DisposeWith(dispose);

            this.OneWayBind(this.ViewModel,
                vm => vm.NameError,
                view => view.ProjectNameError.Text)
            .DisposeWith(dispose);

            this.OneWayBind(this.ViewModel,
                vm => vm.PathError,
                view => view.FilePathError.Text)
            .DisposeWith(dispose);

            this.OneWayBind(this.ViewModel,
                vm => vm.IsValid,
                view => view.CreateProjectButton.IsEnabled)
            .DisposeWith(dispose);

            this.OneWayBind(this.ViewModel,
                vm => vm.AvailableTemplates,
                view => view.ProjectTemplatesList.ItemsSource)
            .DisposeWith(dispose);

            this.CreateProjectButton.Events()
                .Click
                .Subscribe(x =>
                {
                    this.DialogResult = true;
                    this.Close();
                })
            .DisposeWith(dispose);

            this.CloseButton.Events()
                .Click
                .Subscribe(x =>
                {
                    this.DialogResult = false;
                    this.Close();
                })
            .DisposeWith(dispose);

            this.FilePathBrowseButton.Events()
                .Click
                .Subscribe(x =>
                {
                    CommonOpenFileDialog dialog = new()
                    {
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                        Multiselect = false,
                        Title = "Select a save folder...",
                        IsFolderPicker = true
                    };

                    if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        this.ViewModel.Project.FilePath = Path.Combine(dialog.FileName, "Project.horizon");
                    }

                    this.Activate();
                })
            .DisposeWith(dispose);

            this.ProjectTypeNextButton
                .Events()
                .Click
                .Subscribe(x =>
                {
                    this.ProjectTypePage.Visibility = Visibility.Collapsed;
                    this.ProjectMetadataPage.Visibility = Visibility.Visible;
                })
            .DisposeWith(dispose);

            this.ProjectMetadataPreviousButton
                .Events()
                .Click
                .Subscribe(x =>
                {
                    this.ProjectTypePage.Visibility = Visibility.Visible;
                    this.ProjectMetadataPage.Visibility = Visibility.Collapsed;
                })
            .DisposeWith(dispose);
        });
    }
}
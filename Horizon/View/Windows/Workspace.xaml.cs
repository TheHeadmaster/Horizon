using ReactiveUI;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Horizon.View.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class Workspace
{
    /// <inheritdoc />
    public Workspace()
    {
        this.InitializeComponent();

        this.ViewModel = new();

        this.WhenActivated(dispose =>
        {
            this.Interactions(dispose);
        });
    }

    /// <summary>
    /// Registers interaction handlers.
    /// </summary>
    /// <param name="dispose">The disposable.</param>
    private void Interactions(CompositeDisposable dispose)
    {
        App.CommandsViewModel
            .NewProjectDialogInteraction
            .RegisterHandler(interaction =>
            {
                NewProjectWindow newProjectDialog = new();

                return Observable.Start(() =>
                {
                    if (newProjectDialog.ShowDialog() ?? false)
                    {
                        interaction.SetOutput(newProjectDialog.ViewModel!.Project);
                    }
                    else
                    {
                        interaction.SetOutput(null);
                    }
                }, RxApp.MainThreadScheduler);
            })
            .DisposeWith(dispose);
    }
}
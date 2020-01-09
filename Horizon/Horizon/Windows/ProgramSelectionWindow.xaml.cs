using Horizon.Diagnostics;
using Horizon.ViewModels;
using ReactiveUI;
using Splat;
using System.Reactive;
using System.Reactive.Disposables;
using System.Windows;

namespace Horizon.Windows
{
    /// <summary>
    /// Displays a window that allows the user to choose between the launcher and the IDE.
    /// </summary>
    public partial class ProgramSelectionWindow : ReactiveWindow<ProgramSelectionViewModel>
    {
        public ProgramSelectionWindow()
        {
            this.InitializeComponent();

            this.ViewModel = new ProgramSelectionViewModel(this);

            this.WhenActivated(dispose =>
            {
                this.BindCommand(this.ViewModel,
                viewModel => viewModel.SwitchToIDECommand,
                view => view.IDEButton)
                .DisposeWith(dispose);

                this.BindCommand(this.ViewModel,
                viewModel => viewModel.SwitchToLauncherCommand,
                view => view.LauncherButton)
                .DisposeWith(dispose);
            });
        }
    }
}
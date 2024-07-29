using Horizon.ViewModel;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;

namespace Horizon.View.Controls;

/// <summary>
/// Interaction logic for MainMenu.xaml
/// </summary>
public partial class MainMenu
{
    public MainMenu()
    {
        this.InitializeComponent();

        this.ViewModel = new MainMenuViewModel();

        this.WhenActivated(dispose =>
        {
            this.NewProjectMenuItem.Events()
                .Click
                .Select(x => new Unit())
                .InvokeCommand(App.ViewModel, x => x.NewProjectDialog)
                .DisposeWith(dispose);
        });
    }
}
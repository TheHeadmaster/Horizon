using AvalonDock;
using Horizon.Converters;
using ReactiveMarbles.ObservableEvents;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EventExtensions = System.Windows.Controls.EventExtensions;

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
            this.PropertyBindings(dispose);
            this.EventBindings(dispose);
            this.KeyBindings(dispose);
            this.Interactions(dispose);
        });
    }

    /// <summary>
    /// Binds view model properties to the view.
    /// </summary>
    /// <param name="dispose">The disposable.</param>
    private void PropertyBindings(CompositeDisposable dispose)
    {
        this.OneWayBind(this.ViewModel, vm => vm.Title, view => view.Title)
            .DisposeWith(dispose);

        this.OneWayBind(this.ViewModel, vm => vm.Tools, view => view.Dock.AnchorablesSource)
            .DisposeWith(dispose);

        this.OneWayBind(this.ViewModel, vm => vm.Pages, view => view.Dock.DocumentsSource)
            .DisposeWith(dispose);

        this.Bind(this.ViewModel, vm => vm.ActiveDocument, view => view.Dock.ActiveContent,
            vmToViewConverterOverride: new ActiveDocumentConverter(),
            viewToVMConverterOverride: new ActiveDocumentConverter())
        .DisposeWith(dispose);
    }

    /// <summary>
    /// Binds view model commands to view events.
    /// </summary>
    /// <param name="dispose">The disposable.</param>
    private void EventBindings(CompositeDisposable dispose)
    {
        this.Dock.Events().DocumentClosing
            .Select(x => x)
            .InvokeCommand(this, x => x.ViewModel!.DocumentClosing)
        .DisposeWith(dispose);

        this.Events().Closing
            .Select(x => this.Dock)
            .InvokeCommand(this, x => x.ViewModel!.SaveLayout)
        .DisposeWith(dispose);

        EventExtensions.Events(this.Dock).Loaded
            .Select(x => x.Source as DockingManager)
            .InvokeCommand(this, x => x.ViewModel!.LoadLayout)
        .DisposeWith(dispose);

        EventExtensions.Events(this.Dock).Unloaded
            .Select(x => x.Source as DockingManager)
            .InvokeCommand(this, x => x.ViewModel!.SaveLayout)
        .DisposeWith(dispose);
    }

    /// <summary>
    /// Binds view model commands to key bindings.
    /// </summary>
    /// <param name="dispose">The disposable.</param>
    private void KeyBindings(CompositeDisposable dispose)
    {
        this.Events().KeyUp
            .Where(x => x.Key == Key.N && Keyboard.Modifiers.HasFlag(ModifierKeys.Control | ModifierKeys.Shift))
            .Select(x => new Unit())
            .InvokeCommand(App.CommandsViewModel, x => x.NewProjectDialog)
        .DisposeWith(dispose);

        this.Events().KeyUp
            .Where(x => x.Key == Key.F4 && Keyboard.Modifiers.HasFlag(ModifierKeys.Alt))
            .Subscribe(_ => this.Close())
        .DisposeWith(dispose);
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
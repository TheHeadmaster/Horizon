using ReactiveUI;
using System.Reactive.Disposables;

namespace Horizon.View;

/// <summary>
/// Interaction logic for LoadingSplash.xaml
/// </summary>
public partial class LoadingSplash
{
    public LoadingSplash()
    {
        this.InitializeComponent();

        this.ViewModel = new();

        this.WhenActivated(dispose =>
        {
            // Compensate for alpha section of splash
            this.Top -= 100;

            this.OneWayBind(this.ViewModel,
                vm => vm.Status,
                view => view.Status.Text)
            .DisposeWith(dispose);

            this.OneWayBind(this.ViewModel,
                vm => vm.ProgressPercentage,
                view => view.Progress.Value)
            .DisposeWith(dispose);
        });
    }
}